using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands
{
    [UsedImplicitly]
    public class UserSessionTerminateCommandHandler : IRequestHandler<UserSessionTerminate.Command, UserSessionTerminate.Result>
    {
        private readonly IRepository<SessionAccessToken> _sessionAccessTokenRepository;
        private readonly UserSessionSettings _userSessionSettings;
        private readonly ICurrentUserProvider _currentUserProvider;

        public UserSessionTerminateCommandHandler(
            IRepository<SessionAccessToken> sessionAccessTokenRepository,
            UserSessionSettings userSessionSettings,
            ICurrentUserProvider currentUserProvider)
        {
            _sessionAccessTokenRepository = sessionAccessTokenRepository;
            _userSessionSettings = userSessionSettings;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<UserSessionTerminate.Result> Handle(UserSessionTerminate.Command request, CancellationToken cancellationToken)
        {
            var sessionAccessTokens = await _sessionAccessTokenRepository.QueryAll()
                .Include(x => x.UserSession)
                .ThenInclude(x => x.CreatedBy)
                .QueryByValue(request.AccessToken)
                .QueryByOrigin(_userSessionSettings.SessionOrigin)
                .ToListAsync(cancellationToken);


            var userEmail = sessionAccessTokens.Any() ? sessionAccessTokens.FirstOrDefault().UserSession.CreatedBy.Email : _currentUserProvider.Email;

            sessionAccessTokens.ForEach(x => x?.UserSession.Terminate());

            return UserSessionTerminate.Result.Create(userEmail);
        }
    }
}
