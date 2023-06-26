using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Utils;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security
{
    public static class BackOfficePoliciesToPermissionsMap
    {
        public static List<PolicyPermissions> Map() => new List<PolicyPermissions>
        {
            new PolicyPermissions(PolicyNames.BackOffice.MapRead, new[] {
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.MapContentWrite,
                PermissionId.ReportReadWrite,
                PermissionId.Management,
                PermissionId.ReadOnly
            }),
            new PolicyPermissions(PolicyNames.BackOffice.MapContentRead, new[] {
                PermissionId.MapContentRead,
                PermissionId.MapContentWrite,
                PermissionId.Management,
                PermissionId.ReadOnly
            }),
            new PolicyPermissions(PolicyNames.BackOffice.MapContentWrite, new[] {
                PermissionId.MapContentWrite,
                PermissionId.Management
            }),
            new PolicyPermissions(PolicyNames.BackOffice.TimeRegistrationPersonalReadWrite, new[] {
                PermissionId.TimeRegistrationPersonalReadWrite,
                PermissionId.Management
            }),
            new PolicyPermissions(PolicyNames.BackOffice.TimeRegistrationManagementReadWrite, new[] {
                PermissionId.TimeRegistrationManagementReadWrite,
                PermissionId.Management
            }),
            // TODO: Should we have user read/user write policies separated?
            new PolicyPermissions(PolicyNames.BackOffice.UserRead, new[] {
                PermissionId.UserReadWrite,
                PermissionId.Management,
                PermissionId.AssignMaintainerRole
            }),
            new PolicyPermissions(PolicyNames.BackOffice.UserWrite, new[] {
                PermissionId.UserReadWrite,
                PermissionId.Management,
                PermissionId.AssignMaintainerRole
            }),
            new PolicyPermissions(PolicyNames.BackOffice.RoleRead, new[] {
                PermissionId.UserReadWrite,
                PermissionId.Management,
                PermissionId.AssignMaintainerRole
            }),
            new PolicyPermissions(PolicyNames.BackOffice.RoleWrite, new[] {
                PermissionId.Management
            }),
            new PolicyPermissions(PolicyNames.BackOffice.ReportReadWrite, new[] {
                PermissionId.ReportReadWrite,
                PermissionId.Management,
                PermissionId.ReadOnly
            }),
            new PolicyPermissions(PolicyNames.BackOffice.OrganizationRead, new[] {
                PermissionId.Management,
                PermissionId.UserReadWrite,
                PermissionId.ReportReadWrite,
                PermissionId.AssignMaintainerRole,
                PermissionId.ReadOnly
            }),
            new PolicyPermissions(PolicyNames.BackOffice.Management, new[] {
                PermissionId.Management
            }),
            new PolicyPermissions(PolicyNames.BackOffice.AnyPermission, Enum<PermissionId>.GetAll().ToArray())
        };

        public static string[] GetPolicies(PermissionId[] permissions) =>
            Map().Where(x => x.Permissions.Intersect(permissions).Any()).Select(x => x.PolicyName).ToArray();
    }
}
