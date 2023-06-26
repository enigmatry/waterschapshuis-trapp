using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Observations;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests.Observations
{
    [Category("integration")]
    public class ObservationODataControllerFixture : ExternalApiIntegrationFixtureBase
    {
        private Observation _observation = null!;
        private double _latitude;
        private double _longitude;

        [SetUp]
        public void SetUp()
        {
            _latitude = 4.899431;
            _longitude = 52.379189;

            _observation = new ObservationBuilder()
                .WithRemarks("test remarks")
                .WithLocation(_longitude, _latitude)
                .WithRecordedOn(DateTimeOffset.Now)
                .WithHasPhoto(true)
                .WithId(Guid.NewGuid());

            AddAndSaveChanges(_observation);
        }

        [Test]
        public async Task TestGetObservations()
        {
            var response = await Client.GetAsync<JObject>("odata/observations");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"].Count().Should().Be(1);
        }

        [Test]
        public async Task GivenLimitedAccessUser_ObservationsController_ShouldReturnAnonymizedData()
        {
            ChangeCurrentUserPermissions(new[] { PermissionId.ApiPublic });

            var response = await Client.GetAsync<JObject>("odata/observations");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"]?[0]?["CreatedBy"]?.ToString().Should().Be("Geanonimiseerd");
        }

        [Test]
        public async Task GivenUnlimitedAccessUser_ObservationsController_ShouldNotReturnAnonymizedData()
        {
            ChangeCurrentUserPermissions(new[] { PermissionId.ApiPrivate });

            var response = await Client.GetAsync<JObject>("odata/observations");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"]?[0]?["CreatedBy"]?.ToString().Should().Be(TestPrincipal.TestUserName);
        }

        [Test]
        public async Task TestGetObservationById()
        {
            var response = await Client.GetAsync<JObject>($"odata/observations({_observation.Id})");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["Id"].Should().NotBeNull();
            response["Id"]!.ToString().Should().Be(_observation.Id.ToString());
        }
    }
}
