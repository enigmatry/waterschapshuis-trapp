using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests
{
    [Category("integration")]
    public class SecurityCheckControllerFixture : MobileApiIntegrationFixtureBase
    {
        [Test]
        public async Task GivenUserHasNoPermissions_LookupsController_ShouldReturnUnauthorized()
        {
            ClearCurrentUserPermissions();
            HttpResponseMessage response = await Client.GetAsync("catch-types");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GivenUserHasNoPermissions_LookupsController_ShouldReturnOK()
        {
            HttpResponseMessage response = await Client.GetAsync("catch-types");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
