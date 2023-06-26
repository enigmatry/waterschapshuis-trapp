using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.CatchAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Areas;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Areas
{
    [Category("integration")]
    public class AreasControllerGetLocationDataFixture : MobileApiIntegrationFixtureBase
    {
        private const int Longitude = 50;
        private const int Latitude = 50;

        private Guid _catchAreaId;
        private Guid _subAreaId;

        // we need to show the current week, instead of previous week WVR-2048
        private readonly DateTimeOffset _currentWeek = DateTimeOffset.Now.MondayDateInWeekOfDate().AddDays(1);
        private readonly DateTimeOffset _notCurrentWeek = DateTimeOffset.Now.MondayDateInWeekOfDate().AddDays(-8);

        [SetUp]
        public void SetUp()
        {
            CatchArea catchArea = new CatchAreaBuilder()
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(Longitude, Latitude));

            SubArea subArea1 = new SubAreaBuilder()
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(Longitude, Latitude));
            SubArea subArea2 = new SubAreaBuilder()
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(Longitude, Latitude));

            HourSquare hourSquare1 = new HourSquareBuilder()
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(Longitude, Latitude));
            HourSquare hourSquare2 = new HourSquareBuilder()
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(Longitude, Latitude));

            SubAreaHourSquare subAreaHourSquare1 = new SubAreaHourSquareBuilder()
                .WithHourSquare(hourSquare1)
                .WithSubArea(subArea1);
            SubAreaHourSquare subAreaHourSquare2 = new SubAreaHourSquareBuilder()
                .WithHourSquare(hourSquare2)
                .WithSubArea(subArea1);

            TimeRegistration timeRegistration1 = new TimeRegistrationBuilder()
                .WithHours(6.5)
                .WithDate(_currentWeek)
                .WithTrappingTypeId(TrappingType.BeverratId)
                .WithUserId(User.SystemUserId)
                .WithSubAreaHourSquareId(subAreaHourSquare1.Id);
            TimeRegistration timeRegistration2 = new TimeRegistrationBuilder()
                .WithHours(6.75)
                .WithDate(_currentWeek)
                .WithTrappingTypeId(TrappingType.BeverratId)
                .WithUserId(User.SystemUserId)
                .WithSubAreaHourSquareId(subAreaHourSquare2.Id);
            TimeRegistration timeRegistration3 = new TimeRegistrationBuilder()
                .WithHours(6.5)
                .WithDate(_notCurrentWeek)
                .WithTrappingTypeId(TrappingType.BeverratId)
                .WithUserId(User.SystemUserId)
                .WithSubAreaHourSquareId(subAreaHourSquare1.Id);
            TimeRegistration timeRegistration4 = new TimeRegistrationBuilder()
                .WithHours(6.75)
                .WithDate(_notCurrentWeek)
                .WithTrappingTypeId(TrappingType.BeverratId)
                .WithUserId(User.SystemUserId)
                .WithSubAreaHourSquareId(subAreaHourSquare2.Id);

            Trap trap1 = new TrapBuilder()
                .WithCoordinates(Longitude, Latitude)
                .WithStatus(TrapStatus.Catching)
                .WithNumberOfTraps(2)
                .WithTrapTypeId(TrapType.ConibearBeverratId);
            Trap trap2 = new TrapBuilder()
                .WithCoordinates(Longitude, Latitude)
                .WithStatus(TrapStatus.Catching)
                .WithNumberOfTraps(2)
                .WithTrapTypeId(TrapType.GrondklemBeverratId);
            Trap trap3 = new TrapBuilder()
                .WithCoordinates(Longitude, Latitude)
                .WithStatus(TrapStatus.Removed)
                .WithNumberOfTraps(2)
                .WithTrapTypeId(TrapType.ConibearBeverratId);
            Trap trap4 = new TrapBuilder()
                .WithCoordinates(Longitude, Latitude)
                .WithStatus(TrapStatus.NotCatching)
                .WithNumberOfTraps(2)
                .WithTrapTypeId(TrapType.GrondklemBeverratId);

            CatchType catchType1 = new CatchTypeBuilder()
                .WithAnimalType(AnimalType.Mammal)
                .WithIsByCatch(false)
                .WithName(nameof(catchType1));
            CatchType catchType2 = new CatchTypeBuilder()
                .WithAnimalType(AnimalType.Mammal)
                .WithIsByCatch(false)
                .WithName(nameof(catchType2));
            CatchType byCatchType1 = new CatchTypeBuilder()
                .WithAnimalType(AnimalType.Fish)
                .WithIsByCatch(true)
                .WithName(nameof(byCatchType1));
            CatchType byCatchType2 = new CatchTypeBuilder()
                .WithAnimalType(AnimalType.Fish)
                .WithIsByCatch(true)
                .WithName(nameof(byCatchType2));

            Catch catch1 = new CatchBuilder()
                .WithRecordedOn(_currentWeek)
                .WithCatchType(catchType1)
                .WithNumberOfCatches(2)
                .WithTrap(trap1);
            Catch catch2 = new CatchBuilder()
                .WithRecordedOn(_notCurrentWeek)
                .WithCatchType(catchType1)
                .WithNumberOfCatches(2)
                .WithTrap(trap1);
            Catch catch3 = new CatchBuilder()
                .WithRecordedOn(_currentWeek)
                .WithCatchType(byCatchType1)
                .WithNumberOfCatches(3)
                .WithTrap(trap1);
            Catch catch4 = new CatchBuilder()
                .WithRecordedOn(_notCurrentWeek)
                .WithCatchType(byCatchType1)
                .WithNumberOfCatches(3)
                .WithTrap(trap1);
            Catch catch5 = new CatchBuilder()
                .WithRecordedOn(_currentWeek)
                .WithCatchType(catchType2)
                .WithNumberOfCatches(2)
                .WithTrap(trap2);
            Catch catch6 = new CatchBuilder()
                .WithRecordedOn(_notCurrentWeek)
                .WithCatchType(catchType2)
                .WithNumberOfCatches(2)
                .WithTrap(trap2);
            Catch catch7 = new CatchBuilder()
                .WithRecordedOn(_currentWeek)
                .WithCatchType(byCatchType2)
                .WithNumberOfCatches(3)
                .WithTrap(trap2);
            Catch catch8 = new CatchBuilder()
                .WithRecordedOn(_notCurrentWeek)
                .WithCatchType(byCatchType2)
                .WithNumberOfCatches(3)
                .WithTrap(trap2);

            catchArea.AddSubArea(subArea1);
            catchArea.AddSubArea(subArea2);

            subArea1.AddSubAreaHourSquare(subAreaHourSquare1);
            subArea2.AddSubAreaHourSquare(subAreaHourSquare2);

            subAreaHourSquare1.AddTrap(trap1);
            subAreaHourSquare1.AddTrap(trap3);
            subAreaHourSquare2.AddTrap(trap2);
            subAreaHourSquare2.AddTrap(trap4);

            _catchAreaId = catchArea.Id;
            _subAreaId = subArea1.Id;

            AddAndSaveChanges(
                catchArea,
                catchType1,
                catchType2,
                byCatchType1,
                byCatchType2,
                timeRegistration1,
                timeRegistration2,
                timeRegistration3,
                timeRegistration4,
                catch1,
                catch2,
                catch3,
                catch4,
                catch5,
                catch6,
                catch7,
                catch8);
        }

        [Test]
        public async Task TestGetLocationData()
        {
            var response = await Client.GetAsync<GetLocationAreaData.Response>(
                    $"areas/location-data?catchAreaId={_catchAreaId}&subAreaId={_subAreaId}");

            AssertCatchArea(response);
            AssertSubArea(response);
        }

        private static void AssertSubArea(GetLocationAreaData.Response response)
        {
            response.SubArea.CatchingTrapsTotal.Should().Be(2);
            response.SubArea.CatchingTraps.Should().HaveCount(1);

            response.SubArea.LastWeekCatchesTotal.Should().Be(2);
            response.SubArea.LastWeekCatches.Should().HaveCount(1);

            response.SubArea.LastWeekByCatchesTotal.Should().Be(3);
            response.SubArea.LastWeekByCatches.Should().HaveCount(1);

            response.SubArea.LastWeekTimeTotal.Hours.Should().Be(6);
            response.SubArea.LastWeekTimeTotal.Minutes.Should().Be(30);
        }

        private static void AssertCatchArea(GetLocationAreaData.Response response)
        {
            response.CatchArea.CatchingTrapsTotal.Should().Be(4);
            response.CatchArea.CatchingTraps.Should().HaveCount(2);

            response.CatchArea.LastWeekCatchesTotal.Should().Be(4);
            response.CatchArea.LastWeekCatches.Should().HaveCount(2);

            response.CatchArea.LastWeekByCatchesTotal.Should().Be(6);
            response.CatchArea.LastWeekByCatches.Should().HaveCount(2);

            response.CatchArea.LastWeekTimeTotal.Hours.Should().Be(13);
            response.CatchArea.LastWeekTimeTotal.Minutes.Should().Be(15);
        }
    }
}
