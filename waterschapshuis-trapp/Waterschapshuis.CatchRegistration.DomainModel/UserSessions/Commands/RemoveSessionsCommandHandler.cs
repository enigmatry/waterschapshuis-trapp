using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands
{
    [UsedImplicitly]
    public class RemoveSessionsCommandHandler : IRequestHandler<RemoveSessions.Command, RemoveSessions.Result>
    {
        private readonly IRepository<UserSession> _userSessionRepository;
        private readonly ILogger<RemoveSessionsCommandHandler> _logger;

        public RemoveSessionsCommandHandler(
            IRepository<UserSession> userSessionRepository,
            ILogger<RemoveSessionsCommandHandler> logger)
        {
            _userSessionRepository = userSessionRepository;
            _logger = logger;
        }

        public async Task<RemoveSessions.Result> Handle(RemoveSessions.Command request,
            CancellationToken cancellationToken)
        {
            List<UserSession> sessionsToDelete = await _userSessionRepository
                .QueryAll()
                .Include(u => u.AccessTokens)
                .QueryByReadyForRemoval(request.CreatedBeforeDate)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Found {Count} expired session(s). Ids: {Ids}", sessionsToDelete.Count,
                String.Join(",", sessionsToDelete.Select(s => s.Id)));

            _userSessionRepository.DeleteRange(sessionsToDelete);

            return new RemoveSessions.Result
            {
                UserSessionsDeleted = sessionsToDelete.Count
            };
        }
    }
}
