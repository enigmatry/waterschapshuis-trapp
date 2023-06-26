using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests
{
    [Category("integration")]
    public class SecurityCheckControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        [Test]
        public async Task GivenUserHasNoPermissions_OrganizationsController_ShouldReturnUnauthorized()
        {
            ClearCurrentUserPermissions();
            HttpResponseMessage response = await Client.GetAsync("organizations");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GivenUserHasPermissions_OrganizationsController_ShouldReturnOk()
        {
            HttpResponseMessage response = await Client.GetAsync("organizations");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
