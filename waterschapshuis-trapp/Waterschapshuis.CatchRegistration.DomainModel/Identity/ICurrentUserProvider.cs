using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity
{
    public interface ICurrentUserProvider
    {
        Guid? UserId { get; }
        bool IsAuthenticated { get; }
        UserAuthorizationStatus UserAuthorizationStatus { get; }
        string Email { get; }
        string Name { get; }
        PermissionId[] PermissionIds { get; }
        Guid[] RoleIds { get; }
        Organization? Organization { get; }

        bool UserHasAnyPermission(IEnumerable<PermissionId> permissionIds);
        void TryReloadUser();

        Guid GetUserId();
    }
}
