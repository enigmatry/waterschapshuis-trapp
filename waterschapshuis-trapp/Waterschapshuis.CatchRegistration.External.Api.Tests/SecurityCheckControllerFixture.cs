using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests
{
    [Category("integration")]
    public class SecurityCheckControllerFixture : ExternalApiIntegrationFixtureBase
    {
        [SetUp]
        public void SetUp()
        {
            ClearCurrentUserPermissions();
        }

        [Test]
        public async Task GivenUserHasNoPermissions_LookupsController_ShouldReturnUnauthorized()
        {
            HttpResponseMessage response = await Client.GetAsync("odata/catches");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
