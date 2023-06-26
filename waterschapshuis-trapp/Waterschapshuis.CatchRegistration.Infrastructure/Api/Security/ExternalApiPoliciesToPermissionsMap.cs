using System;
using System.Collections.Generic;
using System.Text;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security
{
    public static class ExternalApiPoliciesToPermissionsMap
    {
        public static List<PolicyPermissions> Map() => new List<PolicyPermissions>
        {
            new PolicyPermissions(PolicyNames.ExternalApi.LimitedAccess, new[] {
                PermissionId.ApiPublic,
                PermissionId.ApiPrivate
            }),
            new PolicyPermissions(PolicyNames.ExternalApi.UnlimitedAccess, new[] {
                PermissionId.ApiPrivate
            })
        };
    }
}
