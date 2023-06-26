using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security
{
    public readonly struct PolicyPermissions
    {
        public string PolicyName { get; }
        public PermissionId[] Permissions { get; }

        public PolicyPermissions(string policyName, PermissionId[] permissions)
        {
            PolicyName = policyName;
            Permissions = permissions;
        }
    }
}
