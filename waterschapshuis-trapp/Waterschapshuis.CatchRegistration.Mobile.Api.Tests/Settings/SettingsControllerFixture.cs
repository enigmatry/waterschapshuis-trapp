using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Settings;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Settings
{

    [Category("integration")]
    public class SettingsControllerFixture : MobileApiIntegrationFixtureBase
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GivenUser_GetGeoServerSettings_ReturnsPopulatedSettings()
        {
            var settings = await Client.GetAsync<GetGeoServerSettings.Response>("settings/geoserver");

            settings.Url.Should().Be("https://someGeoserverDomain/geoserver");
            settings.AccessKey.Should().Be("geoserver_access_key");
            settings.BackOfficeUser.Should().Be("geoserver_back_office_user");
            settings.MobileUser.Should().Be("geoserver_mobile_user");
        }
    }
}
