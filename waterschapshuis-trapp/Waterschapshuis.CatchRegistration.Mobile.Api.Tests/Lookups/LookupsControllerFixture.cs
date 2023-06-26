using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.Lookups;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Lookups
{
    [Category("integration")]
    public class LookupsControllerFixture : MobileApiIntegrationFixtureBase
    {
        private TrapType _trapType = null!;
        private CatchType _catchType = null!;

        [SetUp]
        public void SetUp()
        {
            _trapType = new TrapTypeBuilder()
                .WithName("Trap type from test")
                .WithTrappingTypeId(TrappingType.MuskusratId)
                .WithActive(true)
                .WithOrder(30);

            AddAndSaveChanges(_trapType);

            _catchType = new CatchTypeBuilder()
                .WithName("Catch type from test")
                .WithIsByCatch(true);

            AddAndSaveChanges(_catchType);
        }

        [Test]
        public async Task TestGetTrapTypes()
        {
            var response = (await Client.GetAsync<ListResponse<GetTrapTypes.ResponseItem>>("trap-types")).Items.ToList();

            //Assert
            response.Count.Should().Be(18, "we have 1 trap type created in test and 17 active seeded.");

            AssertTrapTypeExists(response, "Trap type from test");
            AssertTrapTypeExists(response, "Conibear");
        }

        [Test]
        public async Task TestGetCatchTypes()
        {
            var response = (await Client.GetAsync<ListResponse<GetCatchTypes.ResponseItem>>("catch-types")).Items.ToList();

            //Assert
            response.Count.Should().Be(123, "we have 1 catch type created in test and 124 seeded but excluded two historical catch types");

            AssertCatchTypeExists(response, "Catch type from test", true);
            AssertCatchTypeExists(response, "brandgans", true);
        }


        [Test]
        public async Task TestGetTrappingTypes()
        {
            var response = (await Client.GetAsync<ListResponse<GetTrappingTypes.ResponseItem>>("trapping-types")).Items.ToList();

            //Assert
            response.Count.Should().Be(2, "we have 2 trapping types seeded");
        }

        private static void AssertTrapTypeExists(IEnumerable<NamedEntity.Item> response, string name)
        {
            var count = response.Count(u => u.Name == name);
            count.Should().BeGreaterThan(0);
        }

        private static void AssertCatchTypeExists(IEnumerable<GetCatchTypes.ResponseItem> response, string name, bool isByCatch)
        {
            var item = response.Single(u => u.Name == name);
            item.Name.Should().Be(name);
            item.IsByCatch.Should().Be(isByCatch);
        }
    }
}
