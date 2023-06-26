using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles
{
    public class RolePermission
    {
        public Guid RoleId { get; private set; }
        public PermissionId PermissionId { get; private set; }

        public Role Role { get; private set; } = null!;
        public Permission Permission { get; private set; } = null!;

        public static RolePermission Create(Guid roleId, PermissionId permissionId) =>
            new RolePermission
            {
                PermissionId = permissionId,
                RoleId = roleId
            };
    }
}
