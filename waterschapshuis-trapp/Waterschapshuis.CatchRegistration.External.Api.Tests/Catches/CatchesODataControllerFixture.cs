using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests.Catches
{
    [Category("integration")]
    public class CatchesODataControllerFixture : ExternalApiIntegrationFixtureBase
    {
        private double _longitude = 4.899431;
        private double _latitude = 52.379189;
        private Catch _catch = null!;
        private CatchType _catchType = null!;

        [SetUp]
        public void SetUp()
        {
            _catchType = new CatchTypeBuilder()
                .WithName("Catch type from test");

            AddAndSaveChanges(_catchType);

            _catch = new CatchBuilder()
                .WithCatchType(_catchType)
                .WithNumberOfCatches(5)
                .WithTrap(new TrapBuilder()
                    .WithStatus(TrapStatus.Catching)
                    .WithNumberOfTraps(2)
                    .WithTrapTypeId(new Guid("A0A0503E-0CD7-0642-73AB-464E7CA0A28E"))
                    .WithRemarks("Test Trap")
                    .WithCoordinates(_longitude, _latitude)
                )
                .WithId(Guid.NewGuid());

            AddAndSaveChanges(_catch);
        }

        [Test]
        public async Task TestGetCatches()
        {
            var response = await Client.GetAsync<JObject>("odata/catches");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"].Count().Should().Be(1);
        }

        [Test]
        public async Task GivenUserLimitedAccessUser_CatchesController_ShouldReturnAnonymizedData()
        {
            ChangeCurrentUserPermissions(new[] { PermissionId.ApiPublic });

            var response = await Client.GetAsync<JObject>("odata/catches");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();
            
            response["value"].Should().NotBeEmpty();
            response["value"]?[0]?["CreatedBy"]?.ToString().Should().Be("Geanonimiseerd");
        }

        [Test]
        public async Task GivenUnlimitedAccessUser_CatchesController_ShouldNotReturnAnonymizedData()
        {
            ChangeCurrentUserPermissions(new[] { PermissionId.ApiPrivate });

            var response = await Client.GetAsync<JObject>("odata/catches");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"]?[0]?["CreatedBy"]?.ToString().Should().Be(TestPrincipal.TestUserName);
        }

        [Test]
        public async Task TestGetCatchById()
        {
            var response = await Client.GetAsync<JObject>($"odata/catches({_catch.Id})");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["Id"].Should().NotBeNull();
            response["Id"]!.ToString().Should().Be(_catch.Id.ToString());
        }
    }
}
