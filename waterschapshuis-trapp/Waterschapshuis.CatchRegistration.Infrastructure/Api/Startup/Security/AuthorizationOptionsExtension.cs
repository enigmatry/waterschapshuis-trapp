using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security
{
    public static class AuthorizationOptionsExtension
    {
        public static void AppAddUserHasPermissionPolicies(this AuthorizationOptions options, List<PolicyPermissions> map) =>
            map.ForEach(policyPermissions =>
                    options.AddPolicy(
                        policyPermissions.PolicyName,
                        policy => policy.AddRequirements(new UserHasPermission.Requirement(policyPermissions.Permissions)))
                );
    }
}
