using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity
{
    [Category("unit")]
    public class UserFixture
    {
        private User _user = null!;

        [TestCaseSource(typeof(RoleTransitionTestCases), nameof(RoleTransitionTestCases.RoleTransitions))]
        public void TestWithRoles(RoleTransitionTestCase testCase)
        {
            _user = new UserBuilder().WithRoles(testCase.FromRoles);

            //act
            _user.AssignRoles(testCase.ToRoles, testCase.Permissions);

            //assert
            var result = _user.UserRoles.Select(ur => ur.RoleId);
            result.Should().BeEquivalentTo(testCase.ResultRoles, testCase.Because);
        }

        private static class RoleTransitionTestCases
        {
            private static readonly Guid Role1 = Guid.NewGuid();
            private static readonly Guid Role2 = Guid.NewGuid();
            private static readonly Guid MaintainerRole = Role.MaintainerRoleId;

            public static readonly RoleTransitionTestCase[] RoleTransitions =
            {
                new RoleTransitionTestCase(new[] {Role1, Role2, MaintainerRole},
                    new[] {Role2},
                    new[] {Role1, Role2, MaintainerRole},
                    new PermissionId[0],
                    "user without any permission should not be allowed to update roles"),

                new RoleTransitionTestCase(new[] {Role1, Role2, MaintainerRole},
                    new[] {Role1, MaintainerRole},
                    new[] {Role1, MaintainerRole},
                    new[] {PermissionId.Management},
                    "user with Management permission should be able to update non maintainer roles"),

                new RoleTransitionTestCase(new[] {Role1, Role2, MaintainerRole},
                    new[] {Role1, MaintainerRole},
                    new[] {Role1, MaintainerRole},
                    new[] {PermissionId.AssignMaintainerRole},
                    "user with EditMaintainerRole permission should be able to update non maintainer roles"),

                new RoleTransitionTestCase(new[] {Role1, Role2, MaintainerRole},
                    new[] {Role1},
                    new[] {Role1, Role2, MaintainerRole},
                    new[] {PermissionId.ApiPrivate},
                    "user without Management permission should not be able to update roles"),

                new RoleTransitionTestCase(new[] {Role1, Role2, MaintainerRole},
                    new[] {Role1},
                    new[] {Role1, MaintainerRole},
                    new[] {PermissionId.Management},
                    "user without EditMaintainerRole permission should not be able to update maintainer role"),

                new RoleTransitionTestCase(new[] {Role1, Role2, MaintainerRole},
                    new Guid[] { },
                    new Guid[] { },
                    new[] {PermissionId.AssignMaintainerRole},
                    "user with EditMaintainerRole be able to update all roles"),
            };
        }

        public class RoleTransitionTestCase
        {
            public RoleTransitionTestCase(
                Guid[] fromRoles,
                Guid[] toRoles,
                Guid[] resultRoles,
                PermissionId[] permissions,
                string because)
            {
                FromRoles = fromRoles;
                ToRoles = toRoles;
                ResultRoles = resultRoles;
                Permissions = permissions;
                Because = because;
            }

            public Guid[] FromRoles { get; }
            public Guid[] ToRoles { get; }
            public Guid[] ResultRoles { get; }
            public PermissionId[] Permissions { get; }
            public string Because { get; }
        }
    }
}
