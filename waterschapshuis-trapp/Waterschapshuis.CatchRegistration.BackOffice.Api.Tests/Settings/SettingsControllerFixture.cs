using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Settings;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core.Utils;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Settings
{
    [Category("integration")]
    public class SettingsControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private TestCurrentUserProvider _currentUserProvider = null!;

        [SetUp]
        public void SetUp()
        {
            _currentUserProvider = Resolve<TestCurrentUserProvider>();
        }

        [Test]
        [TestCase(PermissionId.MapRead)]
        [TestCase(PermissionId.MapContentRead)]
        [TestCase(PermissionId.MapContentWrite)]
        public async Task GivenUserHasPermissionsForMaps_GetGeoServerSettings_ReturnsPopulatedSettings(
            PermissionId permission)
        {
            SetUserToHavePermissions(permission);

            var settings = await Client.GetAsync<GetGeoServerSettings.Response>("settings/geoserver");

            settings.Url.Should().Be("https://someGeoserverDomain/geoserver");
            settings.AccessKey.Should().Be("geoserver_access_key");
            settings.BackOfficeUser.Should().Be("geoserver_back_office_user");
            settings.MobileUser.Should().Be("geoserver_mobile_user");
        }

        [Test]
        public async Task GivenUserHasNoPermissionsForMaps_GetGeoServerSettings_ReturnsEmptySettings()
        {
            SetUserToHavePermissions(AllPermissionExceptMapPermissions());

            var settings = await Client.GetAsync<GetGeoServerSettings.Response>("settings/geoserver");

            settings.Url.Should().Be("https://someGeoserverDomain/geoserver");
            settings.AccessKey.Should().BeEmpty();
            settings.BackOfficeUser.Should().BeEmpty();
            settings.MobileUser.Should().BeEmpty();
        }

        private void SetUserToHavePermissions(params PermissionId[] permissions)
        {
            _currentUserProvider.PermissionIds = permissions;
        }

        private static PermissionId[] AllPermissionExceptMapPermissions() => 
            Enum<PermissionId>.GetAll().Except(Permission.GetMapReadPermissionIds()).ToArray();
    }
}
