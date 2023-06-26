using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.External.Api.Features.Catches;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests.Catches
{
    [Category("integration")]
    public class CatchesControllerFixture : ExternalApiIntegrationFixtureBase
    {
        private DateTimeOffset _createdOn;
        private double _longitude = 4.899431;
        private double _latitude = 52.379189;
        private Catch _catch = null!;
        private CatchType _catchType = null!;
        private CatchType _otherCatchType = null!;

        [SetUp]
        public void SetUp()
        {
            _createdOn = Resolve<ITimeProvider>().Now;

            _catchType = new CatchTypeBuilder()
                .WithName("Catch type from test");

            _otherCatchType = new CatchTypeBuilder()
                .WithName("Other catch type")
                .WithIsByCatch(true)
                .WithAnimalType(AnimalType.Fish);

            AddAndSaveChanges(_catchType, _otherCatchType);

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
            var response = await Client.GetAsync<PagedResponse<GetCatch.CatchItem>>("catches");

            response.Should().NotBeNull();
            response.Items.Should().NotBeEmpty();
            response.Items.Count().Should().Be(1);
        }
    }
}
