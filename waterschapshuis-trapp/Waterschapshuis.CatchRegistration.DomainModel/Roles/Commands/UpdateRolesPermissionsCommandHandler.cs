using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands
{
    [UsedImplicitly]
    public class UpdateRolesPermissionsCommandHandler : IRequestHandler<UpdateRolesPermissions.Command, UpdateRolesPermissions.Result>
    {
        private readonly IRepository<Role> _roleRepository;

        public UpdateRolesPermissionsCommandHandler(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<UpdateRolesPermissions.Result> Handle(UpdateRolesPermissions.Command request, CancellationToken cancellationToken)
        {
            var roles = _roleRepository
                .QueryAll()
                .Include(x => x.RolePermissions)
                .Where(x => request.GetRoleIds().Contains(x.Id))
                .ToList();

            roles.ForEach(context => 
                context.Update(request.Roles.Single(item => item.Id == context.Id).PermissionIds));

            return Task.FromResult(new UpdateRolesPermissions.Result());
        }
    }
}
