using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Roles.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles
{
    public class Role : Entity<Guid>
    {
        public static readonly Guid MaintainerRoleId = new Guid("da236317-eff5-44c4-b668-006ee2914309");
        public static readonly Guid SeniorUserId = new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba");
        public static readonly Guid TrapperRoleId = new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4");
        public static readonly Guid NationalReporterId = new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965");
        public static readonly Guid RegionsApplicationManagerId = new Guid("fcf68091-04f0-4714-b889-b9230435feff");
        public static readonly Guid NationalApplicationManagerId = new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e");
        public static readonly Guid ExternalPublicInterfaceId = new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04");
        public static readonly Guid ExternalPrivateInterfaceId = new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1");

        public static IEnumerable<PermissionId> PermissionsThatCanChangeRole =>
            new[] { PermissionId.Management, PermissionId.UserReadWrite, PermissionId.AssignMaintainerRole };

        public string Name { get; private set; } = String.Empty;
        public string Description { get; private set; } = String.Empty;
        public int DisplayOrderIndex { get; private set; } = 0;

        private readonly List<UserRole> _userRoles = new List<UserRole>();
        private readonly List<RolePermission> _rolePermissions = new List<RolePermission>();

        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();
        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        public IEnumerable<Permission> GetPermissions() => _rolePermissions.Select(x => x.Permission);

        public static Role Create(string name, string description, int displayOrderIndex) =>
            new Role { Name = name, Description = description, DisplayOrderIndex = displayOrderIndex };

        public Role Update(params PermissionId[] permissions)
        {
            WithPermissions(permissions);
            AddDomainEvent(new RoleUpdatedDomainEvent(Id, Name, permissions));
            return this;
        }

        public Role WithPermissions(params PermissionId[] permissions)
        {
            _rolePermissions.Clear();
            _rolePermissions.AddRange(permissions.Select(permissionId => RolePermission.Create(Id, permissionId)));
            return this;
        }

        public bool HasAnyOfPermissions(PermissionId[] permissions)
        {
            return RolePermissions.Any(up => permissions.Any(p => up.PermissionId == p));
        }

        public bool CanUserChangeToThisRole(PermissionId[] permissionIds)
        {
            if (!permissionIds.Any(p => PermissionsThatCanChangeRole.Any(c => c == p)))
            {
                // only theses roles can alter roles
                return false;
            }

            if (Id == MaintainerRoleId)
            {
                return permissionIds.Any(p => p == PermissionId.AssignMaintainerRole);
            }

            return true;
        }
    }
}
