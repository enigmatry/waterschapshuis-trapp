using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Permissions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Accounts
{
    [Category("integration")]
    public class AccountControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private readonly AccessToken _otherAccessTokenValue = AccessToken.Create("OTHER_ACCESS_TOKEN");

        [SetUp]
        public void SetUp()
        {
            Role role1 = new RoleBuilder()
                .WithName("role_1")
                .WithPermissions(PermissionId.ApiPrivate, PermissionId.TimeRegistrationPersonalReadWrite);
            Role role2 = new RoleBuilder()
                .WithName("role_2")
                .WithPermissions(PermissionId.ApiPrivate, PermissionId.ReportReadWrite);

            AddAndSaveChanges(role1, role2);

            QueryDb<User>()
                .Include(u => u.UserRoles)
                .Include(u => u.UserSessions)
                .ThenInclude(us => us.AccessTokens)
                .QueryByEmail(TestPrincipal.TestUserEmail)
                .Single()
                .AssignRoles(new[] {
                    role1.Id,
                    role2.Id
                }, Resolve<ICurrentUserProvider>().PermissionIds);

            SaveChanges();
        }

        [Test]
        public async Task GetProfile()
        {
            var user = await Client.GetAsync<GetCurrentUserProfile.Response>($"account/profile");

            user.Should().NotBeNull();
            user.Name.Should().Be(TestPrincipal.TestUserName);
            user.Email.Should().Be(TestPrincipal.TestUserEmail);
            user.Authorized.Should().BeTrue();

            AssertUserPolicies(user.Policies);
        }

        [Test]
        public async Task LogOut()
        {
            var secondValidSession = UserSession.Create(
                _otherAccessTokenValue,
                DateTimeOffset.Now.AddDays(1),
                UserSessionOrigin.BackOfficeApi,
                TestPrincipal.TestUserId);
            AddAndSaveChanges(secondValidSession);

            LoadCurrentUser().UserSessions
                .Count(x => x.IsValid())
                .Should().Be(4, "Test user has 3 valid sessions per origin + 1 new back-office session we just created");

            var result = await Client.PostAsync<UserSessionTerminate.Result>($"account/log-out");

            result.Should().NotBeNull();
            result.UserEmail.Should().Be(TestPrincipal.TestUserEmail);

            var userSessions = LoadCurrentUser().UserSessions.ToList();
            userSessions.Should().NotBeNull();
            userSessions.Count(x => x.IsValid())
                .Should().Be(3, "One session is terminated");
            userSessions.Single(x => !x.IsValid())
                .AccessTokens.Single().Value
                .Should()
                .Be(TestPrincipal.BackOfficeApiAccessToken.GetValueWithAuthorizationHeaderPrefix(), "Session containing token in Client http request header should be terminated");
        }

        private static void AssertUserPolicies(string[] policies)
        {
            policies.Length
                .Should()
                .Be(5, "we have 2 roles, each 2 permissions, but only 3 distinct, resulting in 5 policies (1 policy for any permission)");

            policies.FirstOrDefault(x => x == PolicyNames.BackOffice.TimeRegistrationPersonalReadWrite).Should().NotBeNull();
            policies.FirstOrDefault(x => x == PolicyNames.BackOffice.ReportReadWrite).Should().NotBeNull();
            policies.FirstOrDefault(x => x == PolicyNames.BackOffice.MapRead).Should().NotBeNull();
            policies.FirstOrDefault(x => x == PolicyNames.BackOffice.OrganizationRead).Should().NotBeNull();
            policies.FirstOrDefault(x => x == PolicyNames.BackOffice.AnyPermission).Should().NotBeNull();
        }

        private User LoadCurrentUser() =>
            QueryDbSkipCache<User>()
                .Include(u => u.UserRoles)
                .Include(u => u.UserSessions)
                .ThenInclude(us => us.AccessTokens)
                .QueryByEmail(TestPrincipal.TestUserEmail)
                .Single();
    }
}
