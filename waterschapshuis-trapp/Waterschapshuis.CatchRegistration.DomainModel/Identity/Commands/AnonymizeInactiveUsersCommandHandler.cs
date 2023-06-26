using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    [UsedImplicitly]
    public class
        AnonymizeInactiveUsersCommandHandler : IRequestHandler<AnonymizeInactiveUsers.Command,
            AnonymizeInactiveUsers.Result>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<AnonymizeInactiveUsersCommandHandler> _logger;

        public AnonymizeInactiveUsersCommandHandler(
            IRepository<User> userRepository,
            ILogger<AnonymizeInactiveUsersCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<AnonymizeInactiveUsers.Result> Handle(AnonymizeInactiveUsers.Command request,
            CancellationToken cancellationToken)
        {
            var usersToAnonymize = await _userRepository
                .QueryAll()
                .QueryUnauthorizedOnly()
                .QueryInactiveBeforeDate(request.InactiveBefore)
                .ToListAsync(cancellationToken);

            _logger.LogInformation($"Anonymize {usersToAnonymize.Count} users.");

            foreach (var user in usersToAnonymize)
            {
                user.Anonymize();
            }

            return await Task.FromResult(AnonymizeInactiveUsers.Result.CreateResult(usersToAnonymize.Count));
        }
    }
}
