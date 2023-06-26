using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.CatchTypes;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TrapTypes;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.CatchTypes
{
    [Category("integration")]
    public class CatchTypesControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private CatchType _catchType = null!;

        [SetUp]
        public void SetUp()
        {

            _catchType = new CatchTypeBuilder()
                .WithName("Catch type from test")
                .WithIsByCatch(true)
                .WithOrder(45)
                .WithAnimalType(AnimalType.Bird);

            AddAndSaveChanges(_catchType);
        }


        [Test]
        public async Task TestGetCatchTypesDetails()
        {
            var response = await Client.GetAsync<PagedResponse<GetCatchType.Response>>("catchTypes?pageSize=200");

            var items = response.Items.ToList();
            //Assert
            items.Count().Should().Be(125, "we have 1 catch type created in test and 124 seeded");
        }

        [Test]
        public async Task GivenValidCatchTypeId_GetById_ReturnsCatchTypeDetails()
        {
            var catchType = await Client.GetAsync<GetCatchType.Response>($"catchTypes/{_catchType.Id}");

            catchType.Should().NotBeNull();
            catchType.Name.Should().Be("Catch type from test");
            catchType.AnimalType.Should().Be(AnimalType.Bird);
            catchType.Order.Should().Be(45);
        }

        [Test]
        public async Task TestCreate()
        {
            var command = new CatchTypeCreateOrUpdate.Command
            {
                Name = "test catch type",
                IsByCatch = true,
                Order = 300,
                AnimalType = AnimalType.Bird
            };
            GetCatchType.Response catchType =
                await Client.PostAsync<CatchTypeCreateOrUpdate.Command, GetCatchType.Response>("catchTypes", command);

            catchType.Should().NotBeNull();
            catchType.Name.Should().Be("test catch type");
        }

        [Test]
        public async Task TestUpdate()
        {
            var command = new CatchTypeCreateOrUpdate.Command
            {
                Id = _catchType.Id,
                Name = "new name",
                IsByCatch = true,
                Order = 300,
                AnimalType = AnimalType.Fish
            };
            GetCatchType.Response catchType =
                await Client.PostAsync<CatchTypeCreateOrUpdate.Command, GetCatchType.Response>("catchTypes", command);

            catchType.Should().NotBeNull();
            catchType.Name.Should().Be("new name");

        }

    }
}
