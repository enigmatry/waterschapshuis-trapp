using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.UserSessions
{
    public static class ValidateAccessToken
    {
        public class Query : IRequest<Response>
        {
            public AccessToken AccessToken { get; }
            public UserSessionOrigin Origin { get; }

            private Query(AccessToken accessToken, UserSessionOrigin origin)
            {
                AccessToken = accessToken;
                Origin = origin;
            }

            public static Query Create(AccessToken accessToken, UserSessionOrigin origin)
            {
                return new Query(accessToken, origin);
            }
        }

        [PublicAPI]
        public class Response
        {
            public bool IsValid { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<SessionAccessToken> _sessionAccessTokenRepository;
            private readonly ILogger<RequestHandler> _logger;

            public RequestHandler(IRepository<SessionAccessToken> sessionAccessTokenRepository,
                ILogger<RequestHandler> logger)
            {
                _sessionAccessTokenRepository = sessionAccessTokenRepository;
                _logger = logger;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                bool isValid =
                    await ValidateAccessToken(request.AccessToken, request.Origin, cancellationToken);
                return new Response {IsValid = isValid};
            }

            private async Task<bool> ValidateAccessToken(AccessToken accessToken,
                UserSessionOrigin origin,
                CancellationToken cancellationToken)
            {
                _logger.LogDebug("Validating accessToken: {Origin}", origin);
                var isValid = await _sessionAccessTokenRepository.QueryAll()
                    .QueryValid()
                    .QueryValidUserSessions()
                    .QueryByOrigin(origin)
                    .QueryByValue(accessToken)
                    .AnyAsync(cancellationToken);

                if (isValid)
                {
                    return isValid;
                }

                _logger.LogWarning("AccessToken was not valid. Getting more information");

                // run query again to get more information why it failed
                SessionAccessToken? fromDb = await _sessionAccessTokenRepository
                    .QueryAll()
                    .Include(s => s.UserSession)
                    .QueryByValue(accessToken)
                    .FirstOrDefaultAsync(cancellationToken);

                if (fromDb == null)
                {
                    _logger.LogWarning("Access token was not found in the database");
                }
                else
                {
                    _logger.LogWarning(
                        "Access token was found: {Id}, {Expired}, {UserSessionId}, {UserSessionCreatedOn}, {UserSessionExpiresOn}",
                        fromDb.Id, fromDb.Expired, fromDb.UserSession.Id, fromDb.UserSession.CreatedOn,
                        fromDb.UserSession.ExpiresOn);
                }

                return isValid;
            }
        }
    }
}
