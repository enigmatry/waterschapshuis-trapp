using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles
{
    public class Permission : Entity<PermissionId>
    {
        private readonly List<RolePermission> _rolePermissions = new List<RolePermission>();

        public string Name { get; private set; } = String.Empty;

        public short Order { get; private set; }

        public static Permission Create(PermissionId id)
        {
            return new Permission { Id = id, Name = id.GetDescription(), Order = (short)id};
        }

        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        public static PermissionId[] GetMapReadPermissionIds()
        {
            return new[] {PermissionId.MapContentRead, PermissionId.MapContentWrite, PermissionId.MapRead};
        }
    }
}
