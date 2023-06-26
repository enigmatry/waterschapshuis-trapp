using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Areas;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.CatchAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreas;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Areas
{
    [Category("integration")]
    public class AreasControllerFixture : MobileApiIntegrationFixtureBase
    {
        private CatchArea _catchAreaOne = null!;
        private CatchArea _catchAreaTwo = null!;
        private SubArea _subAreaOne = null!;
        private SubArea _subAreaTwo = null!;
        private HourSquare _hourSquareOne = null!;
        private HourSquare _hourSquareTwo = null!;

        [SetUp]
        public void SetUp()
        {
            _catchAreaOne = new CatchAreaBuilder().WithName("Catch Area 1");
            _catchAreaTwo = new CatchAreaBuilder().WithName("Catch Area 2");

            _subAreaOne = new SubAreaBuilder().WithName("Sub Area 1");
            _subAreaTwo = new SubAreaBuilder().WithName("Sub Area 2");

            _catchAreaOne.AddSubArea(_subAreaOne);
            _catchAreaOne.AddSubArea(_subAreaTwo);

            _hourSquareOne = new HourSquareBuilder().WithName("HS 1");

            _hourSquareTwo = new HourSquareBuilder().WithName("HS 2");

            SubAreaHourSquare sahsOne = new SubAreaHourSquareBuilder()
                .WithSubArea(_subAreaOne)
                .WithHourSquare(_hourSquareOne);
            SubAreaHourSquare sahsTwo = new SubAreaHourSquareBuilder()
                .WithSubArea(_subAreaOne)
                .WithHourSquare(_hourSquareTwo);

            AddAndSaveChanges(_catchAreaOne, _catchAreaTwo);
            AddAndSaveChanges(sahsOne, sahsTwo);
        }

        [Test]
        public async Task GetCatchAreas_All()
        {
            var catchAreas = await Client.GetAsync<GetAreaEntities.Response>("areas/catch-areas");

            catchAreas.Should().NotBeNull();

            catchAreas.Items.Should().NotBeEmpty();
            catchAreas.Items.Count().Should().Be(1, "Only 'Catch Area 1' belongs to the current VRL");
            catchAreas.Items.First().Name.Should().Be(_catchAreaOne.Name);
        }

        [Test]
        public async Task GetCatchAreas_FilteredByName_Exist()
        {
            var url = QueryHelpers.AddQueryString("areas/catch-areas", "filter", "1");
            var catchAreas = await Client.GetAsync<GetAreaEntities.Response>(url);

            catchAreas.Should().NotBeNull();

            catchAreas.Items.Should().NotBeEmpty();
            catchAreas.Items.Count().Should().Be(1);
            catchAreas.Items.First().Name.Should().Contain("1");
        }

        [Test]
        public async Task GetCatchAreas_FilteredByName_NotExist()
        {
            var url = QueryHelpers.AddQueryString("areas/catch-areas", "filter", "3");
            var catchAreas = await Client.GetAsync<GetAreaEntities.Response>(url);

            catchAreas.Should().NotBeNull();

            catchAreas.Items.Should().BeEmpty();
        }

        [Test]
        public async Task GetSubAreas_ByCatchArea_Exist()
        {
            var url = QueryHelpers.AddQueryString("areas/sub-areas", "catchAreaId", _catchAreaOne.Id.ToString());
            var subAreas = await Client.GetAsync<GetAreaEntities.Response>(url);

            subAreas.Should().NotBeNull();

            subAreas.Items.Should().NotBeEmpty();
            subAreas.Items.Count().Should().Be(1, "Only 'Sub Area 1' belongs to the current VRL");
            subAreas.Items.First().Name.Should().Be(_subAreaOne.Name);
        }

        [Test]
        public async Task GetSubAreas_ByCatchArea_NotExist()
        {
            var url = QueryHelpers.AddQueryString("areas/sub-areas", "catchAreaId", _catchAreaTwo.Id.ToString());
            var subAreas = await Client.GetAsync<GetAreaEntities.Response>(url);

            subAreas.Should().NotBeNull();

            subAreas.Items.Should().BeEmpty();
        }

        [Test]
        public async Task GetSubAreas_FilteredByName_Exist()
        {
            var url = QueryHelpers.AddQueryString("areas/sub-areas", "filter", "1");
            var subAreas = await Client.GetAsync<GetAreaEntities.Response>(url);

            subAreas.Should().NotBeNull();

            subAreas.Items.Should().NotBeEmpty();
            subAreas.Items.Count().Should().Be(1);
            subAreas.Items.First().Name.Should().Contain("1");
        }

        [Test]
        public async Task GetSubAreas_FilteredByName_NotExist()
        {
            var url = QueryHelpers.AddQueryString("areas/sub-areas", "filter", "3");
            var subAreas = await Client.GetAsync<GetAreaEntities.Response>(url);

            subAreas.Should().NotBeNull();

            subAreas.Items.Should().BeEmpty();
        }

        [Test]
        public async Task GetSubAreas_CombinedFilter_Exist()
        {
            var url = QueryHelpers.AddQueryString(
                "areas/sub-areas", 
                new Dictionary<string, string>
                {
                    { "catchAreaId", _catchAreaOne.Id.ToString() },
                    { "filter", "SUB AREA" }
                });
            var subAreas = await Client.GetAsync<GetAreaEntities.Response>(url);

            subAreas.Should().NotBeNull();

            subAreas.Items.Should().NotBeEmpty();
            subAreas.Items.Count().Should().Be(1, "Only 'Sub Area 1' belongs to 'Catch Area 1' and the current VRL");
            subAreas.Items.First().Name.Should().Contain("1");
        }

        [Test]
        public async Task GetHourSquares_BySubArea_Exist()
        {
            var url = QueryHelpers.AddQueryString("areas/hour-squares", "subAreaId", _subAreaOne.Id.ToString());
            var hourSquares = await Client.GetAsync<GetAreaEntities.Response>(url);

            hourSquares.Should().NotBeNull();

            hourSquares.Items.Should().NotBeEmpty();
            hourSquares.Items.Count().Should().Be(2);
            hourSquares.Items.First().Name.Should().Be(_hourSquareOne.Name);
        }

        [Test]
        public async Task GetHourSquares_BySubArea_NotExist()
        {
            var url = QueryHelpers.AddQueryString("areas/hour-squares", "subAreaId", _subAreaTwo.Id.ToString());
            var hourSquares = await Client.GetAsync<GetAreaEntities.Response>(url);

            hourSquares.Should().NotBeNull();

            hourSquares.Items.Should().BeEmpty();
        }

        [Test]
        public async Task GetHourSquares_ByName_Exist()
        {
            var url = QueryHelpers.AddQueryString("areas/hour-squares", "filter", "1");
            var hourSquares = await Client.GetAsync<GetAreaEntities.Response>(url);

            hourSquares.Should().NotBeNull();

            hourSquares.Items.Should().NotBeEmpty();
            hourSquares.Items.Count().Should().Be(1);
            hourSquares.Items.First().Name.Should().Contain("1");
        }

        [Test]
        public async Task GetHourSquares_ByName_NotExist()
        {
            var url = QueryHelpers.AddQueryString("areas/hour-squares", "filter", "3");
            var hourSquares = await Client.GetAsync<GetAreaEntities.Response>(url);

            hourSquares.Should().NotBeNull();

            hourSquares.Items.Should().BeEmpty();
        }

        [Test]
        public async Task GetHourSqares_CombinedFilter_Exist()
        {
            var url = QueryHelpers.AddQueryString(
                "areas/hour-squares", 
                new Dictionary<string, string>
                {
                    { "subAreaId", _subAreaOne.Id.ToString() },
                    { "filter", "HS" }
                });
            var hourSquares = await Client.GetAsync<GetAreaEntities.Response>(url);

            hourSquares.Should().NotBeNull();

            hourSquares.Items.Should().NotBeEmpty();
            hourSquares.Items.Count().Should().Be(2);
            hourSquares.Items.First().Name.Should().Contain("1");
        }
    }
}
