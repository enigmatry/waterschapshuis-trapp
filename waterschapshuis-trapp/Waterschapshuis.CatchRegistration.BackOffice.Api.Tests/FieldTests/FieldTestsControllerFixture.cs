using System;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.FieldTests;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest.Commands;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.FieldTests
{
    [Category("integration")]
    public class FieldTestsControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private FieldTest _fieldTest1 = null!;
        private FieldTest _fieldTest2 = null!;
        private HourSquare _hourSquare1 = null!;
        private HourSquare _hourSquare2 = null!;
        private HourSquare _hourSquare3 = null!;

        [SetUp]
        public void SetUp()
        {
            _fieldTest1 = FieldTest.Create("Test 1", "2020-12", "2020-15");
            _fieldTest2 = FieldTest.Create("Test 2", "2020-09", "2020-10");
            _hourSquare1 = HourSquare.Create("hsq-1", new Point(1,1));
            _hourSquare2 = HourSquare.Create("hsq-2", new Point(2, 1));
            _hourSquare3 = HourSquare.Create("hsq-3", new Point(3, 1));

            AddAndSaveChanges(_fieldTest1, _fieldTest2, _hourSquare1, _hourSquare2, _hourSquare3);
        }


        [Test]
        public async Task TestGetCatchTypesDetails()
        {
            var response = await Client.GetAsync<PagedResponse<GetFieldTest.Response>>("fieldTests?pageSize=100");

            var fieldTest = response.Items.First();

            //Assert
            fieldTest.Name.Should().Be("Test 1");
            fieldTest.StartPeriod.Should().Be("2020-12");
        }

        [Test]
        public async Task GivenValidFieldTestId_GetById_ReturnsFieldTest()
        {
            var fieldTest = await Client.GetAsync<GetFieldTest.Response>($"fieldTests/{_fieldTest1.Id}");

            fieldTest.Should().NotBeNull();
            fieldTest.Name.Should().Be("Test 1");
            fieldTest.StartPeriod.Should().Be("2020-12");
        }

        [Test]
        public async Task TestCreate()
        {
            var command = new FieldTestCreateOrUpdate.Command
            {
                Name = "test field test",
                StartPeriod = "2020-03",
                EndPeriod = "2020-04",
                HourSquareIds = new[] { _hourSquare1.Id, _hourSquare2.Id }
            };
            GetFieldTest.Response fieldTest =
                await Client.PostAsync<FieldTestCreateOrUpdate.Command, GetFieldTest.Response>("fieldTests", command);

            fieldTest.Should().NotBeNull();
            fieldTest.Name.Should().Be("test field test");
            fieldTest.HourSquares.Count().Should().Be(2);
        }

        [Test]
        public async Task TestUpdate()
        {
            var command = new FieldTestCreateOrUpdate.Command
            {
                Id = _fieldTest1.Id,
                Name = "new name",
                StartPeriod = "2020-11",
                EndPeriod = "2020-14",
                HourSquareIds = new[]{_hourSquare1.Id, _hourSquare2.Id, _hourSquare3.Id}
            };
            GetFieldTest.Response fieldTest =
                await Client.PostAsync<FieldTestCreateOrUpdate.Command, GetFieldTest.Response>("fieldTests", command);

            fieldTest.Should().NotBeNull();
            fieldTest.Name.Should().Be("new name");
            fieldTest.HourSquares.Count().Should().Be(3);

        }

    }
}
