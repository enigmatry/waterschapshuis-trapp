using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests.Traps
{
    [Category("integration")]
    public class TrapsODataControllerFixture : ExternalApiIntegrationFixtureBase
    {
        private Trap _trap = null!;
        private Trap _emptyTrap = null!;
        private CatchType _catchType = null!;
        private CatchType _byCatchCatchType = null!;

        [SetUp]
        public void SetUp()
        {
            _catchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.BeverratMoerOudId);
            _byCatchCatchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.ByCatchVlaamseGaaiId);

            _emptyTrap = new TrapBuilder()
                .WithId(Guid.NewGuid());

            Catch c1 = new CatchBuilder()
                .WithCatchType(_byCatchCatchType)
                .WithNumberOfCatches(1);

            Catch c2 = new CatchBuilder()
                .WithCatchType(_catchType)
                .WithNumberOfCatches(2);

            _trap = new TrapBuilder()
                .WithCoordinates(1, 1)
                .WithNumberOfTraps(2)
                .WithRemarks("Test remarks")
                .WithStatus(TrapStatus.Catching)
                .WithId(Guid.NewGuid())
                .WithCatches(c1, c2);

            AddAndSaveChanges(_trap, _emptyTrap);
        }

        [Test]
        public async Task TestGetTraps()
        {
            var response = await Client.GetAsync<JObject>("odata/traps");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"].Count().Should().Be(2);
        }

        [Test]
        public async Task GivenLimitedAccessUser_TrapsController_ShouldReturnAnonymizedData()
        {
            ChangeCurrentUserPermissions(new[] { PermissionId.ApiPublic });

            var response = await Client.GetAsync<JObject>("odata/traps");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"]?[0]?["CreatedBy"]?.ToString().Should().Be("Geanonimiseerd");
        }

        [Test]
        public async Task GivenUnlimitedAccessUser_TrapsController_ShouldNotReturnAnonymizedData()
        {
            ChangeCurrentUserPermissions(new[] { PermissionId.ApiPrivate });

            var response = await Client.GetAsync<JObject>("odata/traps");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();
            
            response["value"].Should().NotBeEmpty();
            response["value"]?[0]?["CreatedBy"]?.ToString().Should().Be(TestPrincipal.TestUserName);
        }

        [Test]
        public async Task TestGetTrapById()
        {
            var response = await Client.GetAsync<JObject>($"odata/traps({_trap.Id})");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["Id"].Should().NotBeNull();
            response["Id"]!.ToString().Should().Be(_trap.Id.ToString());
        }
    }
}
