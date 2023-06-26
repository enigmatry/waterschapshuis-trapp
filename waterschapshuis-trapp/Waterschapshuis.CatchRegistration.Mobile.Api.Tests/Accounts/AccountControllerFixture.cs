using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands;
using Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Users;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Accounts
{
    [Category("integration")]
    public class AccountControllerFixture : MobileApiIntegrationFixtureBase
    {
        private readonly AccessToken _otherAccessTokenValue = AccessToken.Create("OTHER_ACCESS_TOKEN");

        [Test]
        public async Task GetProfile()
        {
            var user = await Client.GetAsync<GetCurrentUserProfile.Response>($"account/profile");

            user.Should().NotBeNull();

            user.Name.Should().Be("INTEGRATION_TEST");
            user.Email.Should().Be("INTEGRATION_TEST@CatchReg.com");
            user.Authorized.Should().BeTrue();
        }

        [Test]
        public async Task LogOut()
        {
            var secondValidSession = UserSession.Create(
                _otherAccessTokenValue,
                DateTimeOffset.Now.AddDays(1),
                UserSessionOrigin.MobileApi,
                TestPrincipal.TestUserId);
            AddAndSaveChanges(secondValidSession);

            LoadCurrentUser().UserSessions
                .Count(x => x.IsValid())
                .Should().Be(4, "Test user has 3 valid sessions per origin + 1 new mobile session we just created");

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
                .Be(TestPrincipal.MobileApiAccessToken.GetValueWithAuthorizationHeaderPrefix(), "Session containing token in Client http request header should be terminated");
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
