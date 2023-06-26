using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Utils;
using Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Users;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Roles
{
    [Category("integration")]
    public class RolesControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private static readonly List<Role> AllRoles = new RolesAndPermissionsSeeding().GetRoles();
        private static readonly List<PermissionId> AllPermissions = Enum<PermissionId>.GetAll().ToList();
        private static readonly List<PermissionId> MaintainerRolePermissions = new List<PermissionId>
        {
            PermissionId.MapRead,
            PermissionId.MapContentRead,
            PermissionId.MapContentWrite,
            PermissionId.ReportReadWrite,
            PermissionId.TimeRegistrationPersonalReadWrite,
            PermissionId.UserReadWrite,
            PermissionId.Management,
            PermissionId.AssignMaintainerRole,
            PermissionId.Mobile
        };

        [Test]
        public async Task TestGetRoles()
        {
            var roles = (await Client.GetAsync<GetRoles.Response>("roles")).Items.ToList();

            roles.Count.Should().Be(8);
            GetRoles.Response.Item maintainerRole = roles.SingleOrDefault(u => u.Name == "Beheerder");
            maintainerRole.Should().NotBeNull();
            maintainerRole.Id.Should().Be(Role.MaintainerRoleId);

            maintainerRole.Permissions
                .Select(x => x.Id).Intersect(AllPermissions).Count().Should().Be(AllPermissions.Count);
            maintainerRole.Permissions
                .Where(x => MaintainerRolePermissions.Contains(x.Id))
                .All(x => x.AssignedToRole).Should().BeTrue();
            maintainerRole.Permissions
                .Where(x => !MaintainerRolePermissions.Contains(x.Id))
                .All(x => x.AssignedToRole).Should().BeFalse();
            maintainerRole.Permissions
                .Single(x => x.Id == PermissionId.Management).CanBeChanged
                .Should().BeFalse("Maintainer must not lose Management permission");

            foreach (GetRoles.Response.Item role in roles)
            {
                role.CanCurrentUserChangeToThisRole.Should().BeTrue("test user has all permissions");
            }
        }

        [Test]
        public async Task TestUpdateRolesPermissions()
        {
            var roles = (await Client.GetAsync<GetRoles.Response>("roles")).Items.ToList();

            AssertRoles(roles, MaintainerRolePermissions);

            var command = UpdateRolesPermissions.Command.Create(AllRoles);
            var commandMaintainerRole = command.Roles.Single(x => x.Id == Role.MaintainerRoleId);
            commandMaintainerRole.PermissionIds = AllPermissions.ToArray();

            roles = (await Client.PutAsync<UpdateRolesPermissions.Command, GetRoles.Response>("roles", command)).Items.ToList();

            AssertRoles(roles, AllPermissions);

            roles.Where(x => x.Id != Role.MaintainerRoleId)
                .SelectMany(x => x.Permissions)
                .All(x => x.AssignedToRole).Should().BeFalse();
        }

        private void AssertRoles(List<GetRoles.Response.Item> result, List<PermissionId> resultMaintainerPermissions)
        {
            result.Select(x => x.Id)
                .Intersect(AllRoles.Select(x => x.Id)).Count()
                .Should().Be(AllRoles.Count);
            var maintainerRole = result.Single(x => x.Id == Role.MaintainerRoleId);
            maintainerRole.Permissions.Select(x => x.Id)
                .Intersect(resultMaintainerPermissions).Count()
                .Should().Be(resultMaintainerPermissions.Count());
        }
    }
}
