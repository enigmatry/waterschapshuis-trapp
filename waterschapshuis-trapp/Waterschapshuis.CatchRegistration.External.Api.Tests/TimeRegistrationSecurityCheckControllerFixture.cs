using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests
{
    [Category("integration")]
    public class TimeRegistrationSecurityCheckControllerFixture : ExternalApiIntegrationFixtureBase
    {
        [Test]
        public async Task GivenUserHasExternalApiPrivatePermission_TimeRegistrationController_ShouldReturnOk()
        {
            ChangeCurrentUserPermissions(new[]{ PermissionId.ApiPrivate});
            HttpResponseMessage response = await Client.GetAsync("odata/timeRegistrations");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GivenUserHasExternalApiPublicPermission_TimeRegistrationController_ShouldReturnUnauthorized()
        {
            ChangeCurrentUserPermissions(new[] { PermissionId.ApiPublic });
            HttpResponseMessage response = await Client.GetAsync("odata/timeRegistrations");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
