using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Permissions
{/*[Category("unit")]
    public class RoleFixture
    {
        [TestCase("Super Admin", RoleId.SuperAdmin)]
        [TestCase("Trapper", RoleId.Trapper)]
        public void TestConvertToRoleEnum(string roleName, RoleId? expectedRoleEnum)
        {
            Role role = new RoleBuilder()
                .WithName(roleName);

            RoleId? roleEnum = role.ToRoleEnum();

            roleEnum.Should().Be(expectedRoleEnum);
        }

        [TestCaseSource(typeof(RolePermissionsTestCases), nameof(RolePermissionsTestCases.RolePermissions))]
        public void TestGetPermissions(string roleName, int expectedPermissionsCount)
        {
            Role role = new RoleBuilder()
                .WithName(roleName);

            PermissionId[] permissions = role.Permissions.Select(p => p.Id).ToArray();

            permissions.Length.Should().Be(expectedPermissionsCount);
        }

        [TestCase("SuperAdmin", new[] {PermissionId.EditUser, PermissionId.ListTrapLocations}, true, TestName =
            "Super admin has this permission.")]
        [TestCase("Trapper", new[] {PermissionId.EditUser}, false, TestName =
            " Trapper does not have this permission.")]
        [TestCase("SomeName", new[] {PermissionId.EditUser, PermissionId.ListTrapLocations}, false,
            TestName = "Unknown role does not have any permissions")]
        public void HasAnyOfPermissions(string roleName, PermissionId[] permissionsToCheck, bool expectedResult)
        {
            Role role = new RoleBuilder()
                .WithName(roleName);

            bool result = role.HasAnyOfPermissions(permissionsToCheck);

            result.Should().Be(expectedResult);
        }
    }*/
}
