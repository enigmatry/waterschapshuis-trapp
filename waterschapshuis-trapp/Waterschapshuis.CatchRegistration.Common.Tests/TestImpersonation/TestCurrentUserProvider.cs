using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.Core.Utils;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation
{
    internal class TestCurrentUserProvider : ICurrentUserProvider
    {
        public TestCurrentUserProvider(TestPrincipal principal, ITimeProvider timeProvider)
        {
            User = User
                .Create(principal.Name, principal.Email, null)
                .Update(new UserUpdate.Command { Authorized = true }, timeProvider.Now)
                .WithId(principal.UserId)
                .WithOrganization(CreateTestOrganization());
            PermissionIds = Enum<PermissionId>.GetAll().ToArray();
            RoleIds = new[] { Role.MaintainerRoleId };
            User.WithUserSessions(new List<UserSession>
            {
                UserSession.Create(
                    TestPrincipal.BackOfficeApiAccessToken,
                    DateTimeOffset.Now.AddMinutes(15),
                    UserSessionOrigin.BackOfficeApi,
                    User.Id),
                UserSession.Create(
                    TestPrincipal.MobileApiAccessToken,
                    DateTimeOffset.Now.AddMinutes(15),
                    UserSessionOrigin.MobileApi,
                    User.Id),
                UserSession.Create(
                    TestPrincipal.ExternalApiAccessToken,
                    DateTimeOffset.Now.AddMinutes(15),
                    UserSessionOrigin.ExternalApi,
                    User.Id)
            });
        }

        public bool IsAuthenticated => true;
        public Guid? UserId => User?.Id;
        public string Email => User?.Email ?? String.Empty;
        public string Name => User?.Name ?? String.Empty;
        public PermissionId[] PermissionIds { get; set; }
        public Guid[] RoleIds { get; set; }
        public Organization? Organization => User?.Organization;
        public User? User { get; }
        public UserAuthorizationStatus UserAuthorizationStatus => UserAuthorizationStatus.Success;


        public bool UserHasAnyPermission(IEnumerable<PermissionId> permissionIds)
        {
            return permissionIds.Any(p => PermissionIds.Any(pi => pi == p));
        }

        public void TryReloadUser() { }

        public void ChangePermissions(PermissionId[] permissionIds)
        {
            PermissionIds = permissionIds;
        }

        public void ChangeRoles(Guid[] roleIds)
        {
            RoleIds = roleIds;
        }

        public void ChangeOrganization(Organization? organization)
        {
            User?.WithOrganization(organization);
        }

        public void ChangeUserId(Guid userId)
        {
            User?.WithId(userId);
        }

        private Organization CreateTestOrganization()
        {
            Organization organization = new OrganizationBuilder().WithName(TestPrincipal.TestUserOrganizationName);
            return organization.WithId(TestPrincipal.TestUserOrganizationId);
        }

        public Guid GetUserId()
        {
            return UserId.GetValueOrDefault();
        }
    }
}
