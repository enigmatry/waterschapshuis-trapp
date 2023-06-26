using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands
{
    [UsedImplicitly]
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRoles.Command, UpdateUserRoles.Result>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public UpdateUserRolesCommandHandler(IRepository<User> userRepository, ICurrentUserProvider currentUserProvider)
        {
            _userRepository = userRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<UpdateUserRoles.Result> Handle(UpdateUserRoles.Command request, CancellationToken cancellationToken)
        {
            var user = _userRepository
                .QueryAll()
                .Include(u => u.UserRoles)
                .QueryById(request.Id)
                .Single();

            user.Update(request, _currentUserProvider.PermissionIds);

            return await Task.FromResult(new UpdateUserRoles.Result() { UserId = user.Id });
        }
    }
}
