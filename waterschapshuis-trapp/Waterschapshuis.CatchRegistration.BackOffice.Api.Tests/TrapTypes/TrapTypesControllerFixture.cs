using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TrapTypes;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.TrapTypes
{
    [Category("integration")]
    public class TrapTypesControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private TrapType _trapType = null!;


        [SetUp]
        public void SetUp()
        {
            _trapType = new TrapTypeBuilder()
                .WithName("Trap type from test")
                .WithTrappingTypeId(TrappingType.MuskusratId)
                .WithActive(true)
                .WithOrder(30);

            AddAndSaveChanges(_trapType);

        }

        [Test]
        public async Task TestGetTrapTypeDetails()
        {
            var response = await Client.GetAsync<PagedResponse<GetTrapType.Response>>("trapTypes?pageSize=100");

            var items = response.Items.ToList();

            //Assert
            items.Count().Should().Be(30, "we have 1 trap type created in test and 29 seeded of TrappingType Muskusrat.");
        }

        [Test]
        public async Task GivenValidTrapTypeId_GetById_ReturnsTrapTypeDetails()
        {
            var trapType = await Client.GetAsync<GetTrapType.Response>($"trapTypes/{_trapType.Id}");

            trapType.Should().NotBeNull();
            trapType.Name.Should().Be("Trap type from test");
            trapType.Active.Should().Be(true);
            trapType.Order.Should().Be(30);
        }

        [Test]
        public async Task TestCreate()
        {
            var command = new TrapTypeCreateOrUpdate.Command
            {
                Name = "test trap type",
                Active = true,
                Order = 300,
                TrappingTypeId = TrappingType.MuskusratId
            };
            GetTrapType.Response trapType=
                await Client.PostAsync<TrapTypeCreateOrUpdate.Command, GetTrapType.Response>("trapTypes", command);

            trapType.Should().NotBeNull();
            trapType.Name.Should().Be("test trap type");
        }

        [Test]
        public async Task TestUpdate()
        {
            var command = new TrapTypeCreateOrUpdate.Command
            {
                Id = _trapType.Id,
                Name = "new name",
                Active = true,
                Order = 300,
                TrappingTypeId = TrappingType.MuskusratId
            };
            GetTrapType.Response trapType =
                await Client.PostAsync<TrapTypeCreateOrUpdate.Command, GetTrapType.Response>("trapTypes", command);

            trapType.Should().NotBeNull();
            trapType.Name.Should().Be("new name");

        }


    }
}
