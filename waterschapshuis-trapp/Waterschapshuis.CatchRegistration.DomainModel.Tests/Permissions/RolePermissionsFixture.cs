using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Core.Utils;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Permissions
{
    /*[Category("unit")]
    public class RolePermissionsFixture
    {
        [Test]
        public void SuperAdmin_Has_AllPermissions()
        {
            IEnumerable<PermissionId> allPermissions = Enum<PermissionId>.GetAll();

            IEnumerable<PermissionId> superAdminsPermissions = RolePermissions.For(RoleEnum.SuperAdmin);

            superAdminsPermissions.Should().BeEquivalentTo(allPermissions);
        }

        [Test]
        public void AllRoles_Have_DistinctPermissions()
        {
            IEnumerable<RoleEnum> allRoles = Enum<RoleEnum>.GetAll();

            foreach (RoleEnum role in allRoles)
            {
                IEnumerable<PermissionId> permissions = RolePermissions.For(role).ToList();
                permissions.Distinct().Should().BeEquivalentTo(permissions, $"{role.ToString()} should have distinct permissions.");
            }
        }
    }*/
}
