using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.UserSessions
{
    public static class CreateOrUpdateUserSession
    {
        [PublicAPI]
        public class Command : IRequest<Response>
        {
            public AccessToken AccessToken { get; }
            public Guid UserId { get; }
            public UserSessionOrigin SessionOrigin { get; }

            private Command(AccessToken accessToken, Guid userId, UserSessionOrigin sessionOrigin)
            {
                AccessToken = accessToken;
                UserId = userId;
                SessionOrigin = sessionOrigin;
            }

            public static Command Create(AccessToken accessToken, Guid userId, UserSessionOrigin sessionOrigin)
            {
                return new Command(accessToken, userId, sessionOrigin);
            }
        }

        [PublicAPI]
        public class Response
        {
            public bool IsSuccess => String.IsNullOrEmpty(FailReason);
            public string FailReason { get; set; } = String.Empty;

            public static Response Success()
            {
                return new Response();
            }

            public static Response Fail(string reason)
            {
                return new Response {FailReason = reason};
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<UserSession> _userSessionRepository;
            private readonly IRepository<SessionAccessToken> _sessionAccessTokenRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserSessionSettings _userSessionSettings;
            private readonly ITimeProvider _timeProvider;
            private readonly ILogger<RequestHandler> _logger;

            public RequestHandler(IRepository<UserSession> userSessionRepository,
                IRepository<SessionAccessToken> sessionAccessTokenRepository,
                IUnitOfWork unitOfWork,
                UserSessionSettings userSessionSettings,
                ITimeProvider timeProvider,
                ILogger<RequestHandler> logger)
            {
                _userSessionRepository = userSessionRepository;
                _unitOfWork = unitOfWork;
                _timeProvider = timeProvider;
                _sessionAccessTokenRepository = sessionAccessTokenRepository;
                _logger = logger;
                _userSessionSettings = userSessionSettings;
            }

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                var (isPresent, response) =
                    await ValidateAccessTokenIfPresent(command.SessionOrigin, command.AccessToken, cancellationToken);

                if (isPresent)
                {
                    return response;
                }

                return await AddAccessTokenToNewOrExistingSession(command, cancellationToken);
            }

            private async Task<(bool isPresent, Response response)> ValidateAccessTokenIfPresent(
                UserSessionOrigin origin,
                AccessToken accessToken,
                CancellationToken cancellationToken)
            {
                //in standard situation we would expect one access token to be returned by this query, but because there might be a race condition (when saving to db) and we do not want locks, or unique constraints (due to performance), we need to recover from these type of situations, i.e. getting more than one SessionAccessToken for given accessToken. 
                List<SessionAccessToken> sessionAccessTokens =
                    await FindSessionAccessTokensByAccessToken(accessToken, cancellationToken);
                List<SessionAccessToken> sessionAccessTokensFromOtherOrigins =
                    sessionAccessTokens.Where(at => at.UserSession.Origin != origin).ToList();

                if (sessionAccessTokensFromOtherOrigins.Any())
                {
                    _logger.LogWarning(
                        "Same Access token was found in different user session from other origin. SessionAccessTokenIds: {SessionAccessTokenIds}",
                        String.Join(",", sessionAccessTokensFromOtherOrigins.Select(s => s.Id)));
                    {
                        return (true, Response.Fail("Same Access token was found in different user session origin"));
                    }
                }

                if (sessionAccessTokens.Any())
                {
                    List<SessionAccessToken> nonExpired = sessionAccessTokens.Where(at => !at.Expired).ToList();
                    if (nonExpired.Count == 0)
                    {
                        IEnumerable<SessionAccessToken>? expired = sessionAccessTokens.Except(nonExpired);
                        _logger.LogWarning(
                            "Access token was found but was expired. SessionAccessTokenIds: {SessionAccessTokenIds}",
                            String.Join(",", expired.Select(s => s.Id)));
                        return (true, Response.Fail("Access token is expired"));
                    }
                    List<SessionAccessToken> nonExpiredWithValidUserSession =
                        nonExpired.Where(at => at.UserSession.IsValid()).ToList();

                    if (nonExpiredWithValidUserSession.Count == 0)
                    {
                        IEnumerable<SessionAccessToken>? nonExpiredWithInvalidSession =
                            nonExpired.Except(nonExpiredWithValidUserSession);
                        _logger.LogWarning("Access token was found but user session is expired. SessionAccessTokenIds: {SessionAccessTokenIds}",
                            String.Join(",", nonExpiredWithInvalidSession.Select(s => s.Id)));
                        return (true, Response.Fail("User session is expired"));
                    }

                    return (true, Response.Success());
                }

                return (false, Response.Success());
            }

            private async Task<List<SessionAccessToken>> FindSessionAccessTokensByAccessToken(AccessToken accessToken,
                CancellationToken cancellationToken)
            {
                return await _sessionAccessTokenRepository.QueryAll()
                    .QueryByValue(accessToken)
                    .Include(a => a.UserSession)
                    .ToListAsync(cancellationToken);
            }

            private async Task<Response> AddAccessTokenToNewOrExistingSession(Command command,
                CancellationToken cancellationToken)
            {
                IList<UserSession> allUserSessions =
                    await FindValidSessionsOrSessionsWithValidTokens(command.UserId, command.SessionOrigin,
                        cancellationToken);
                List<UserSession> validSessions = allUserSessions.Where(us => us.IsValid()).ToList();
                IEnumerable<UserSession> validSessionWithAccessToken =
                    validSessions.Where(s => s.HasValidToken(command.AccessToken));
                if (validSessionWithAccessToken.Any())
                {
                    _logger.LogDebug("Access token exists in the valid session");

                    // access token exists in one of the session
                    return Response.Success();
                }

                UserSession target;

                // access token is not present in any of the valid sessions
                if (validSessions.Any())
                {
                    //add access token to the session that will expire last
                    target = validSessions.OrderBy(s => s.ExpiresOn).Last();
                    _logger.LogDebug(
                        "Adding access token to existing session: UserSessionId: {UserSessionId}, Origin: {Origin}, CreatedOn: {CreatedOn}, ExpiresOn:{ExpiresOn}",
                        target.Id, target.Origin, target.CreatedOn, target.ExpiresOn);
                    target.Update(command.AccessToken);
                }
                else
                {
                    DateTimeOffset expires =
                        _timeProvider.Now.Add(_userSessionSettings.SessionDurationTimespan);
                    _logger.LogInformation("Creating new user session. Origin: {Origin}. Will expire on: {Expires}",
                        command.SessionOrigin, expires);

                    // no active sessions, create new
                    target = UserSession.Create(command.AccessToken, expires,
                        command.SessionOrigin, command.UserId);

                    _userSessionRepository.Add(target);
                }

                // terminate all other sessions that were either valid or had access tokens
                IEnumerable<UserSession> sessionsToTerminate = allUserSessions.Where(s => s != target);
                foreach (UserSession userSession in sessionsToTerminate)
                {
                    _logger.LogInformation("Terminating session. UserSessionId: {UserSessionId}", userSession.Id);
                    userSession.Terminate();
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Response.Success();
            }

            private async Task<IList<UserSession>> FindValidSessionsOrSessionsWithValidTokens(Guid? userId,
                UserSessionOrigin origin,
                CancellationToken cancellationToken)
            {
                List<UserSession>? sessions = await _userSessionRepository
                    .QueryAll()
                    .QueryValidOrWithValidTokens()
                    .QueryByUserId(userId)
                    .QueryByOrigin(origin)
                    .Include(s => s.AccessTokens)
                    .ToListAsync(cancellationToken);

                return sessions;
            }
        }
    }
}
