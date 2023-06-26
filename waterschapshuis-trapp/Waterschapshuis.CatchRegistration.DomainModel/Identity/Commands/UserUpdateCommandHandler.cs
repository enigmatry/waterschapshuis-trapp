using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    [UsedImplicitly]
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdate.Command, UserUpdate.Result>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ITimeProvider _timeProvider;

        public UserUpdateCommandHandler(
            IRepository<User> userRepository,
            ITimeProvider timeProvider)
        {
            _userRepository = userRepository;
            _timeProvider = timeProvider;
        }

        public async Task<UserUpdate.Result> Handle(UserUpdate.Command request, CancellationToken cancellationToken)
        {
            User user = await _userRepository
                .QueryAll()
                .Include(u => u.UserRoles)
                .QueryById(request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            user.Update(request, _timeProvider.Now);

            var result = UserUpdate.Result.CreateResult(user.Id);
            return result;
        }
    }
}
