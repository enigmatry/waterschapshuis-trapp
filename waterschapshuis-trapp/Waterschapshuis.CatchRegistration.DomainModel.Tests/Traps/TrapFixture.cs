using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Provinces;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps
{
    [Category("unit")]
    public class TrapFixture
    {
        private readonly DateTimeOffset _currentDateTime = DateTimeOffset.Now;
        private readonly Province _otherProvince = new ProvinceBuilder().WithName("other province");
        private readonly SubAreaHourSquare _otherSubAreaHourSquare = new SubAreaHourSquareBuilder().WithStartingCoordinate(99, 99, .5);
        private readonly List<CatchType> _catchTypes = new List<CatchType> { new CatchTypeBuilder() };

        [Test]
        [TestCase(TrapStatus.Catching, true, true, TestName = "Update all but recordedOn, if Trap is catching, created today, without catches")]
        [TestCase(TrapStatus.Catching, false, true, TestName = "Update all but trapType/recordedOn, if Trap is catching, not created today, without catches")]
        [TestCase(TrapStatus.Catching, true, true, TestName = "Update all but trapType/recordedOn, if Trap is catching, created today, with catches")]
        [TestCase(TrapStatus.NotCatching, false, true, TestName = "Update remarks/status, if Trap is not-catching")]
        [TestCase(TrapStatus.Removed, false, true, TestName = "Update remarks, if Trap is removed")]
        public void Update(TrapStatus trapStatus, bool trapCreatedToday, bool trapWithoutCatches)
        {
            Trap trapMobile = InitTrap(trapStatus, trapCreatedToday, trapWithoutCatches);
            Trap trapBackOffice = InitTrap(trapStatus, trapCreatedToday, trapWithoutCatches);
            Trap beforeUpdate = InitTrap(trapStatus, trapCreatedToday, trapWithoutCatches);
            var updateFromMobileCommand = InitUpdateFromMobileCommand(trapMobile);
            var updateFromBackOfficeCommand = InitUpdateFromBackOfficeCommand(trapBackOffice);

            trapMobile.Update(updateFromMobileCommand, _otherSubAreaHourSquare, _otherProvince, _catchTypes);
            trapBackOffice.Update(updateFromBackOfficeCommand);

            AssertMobileUpdate(beforeUpdate, trapMobile, updateFromMobileCommand);
            AssertBackOfficeUpdate(beforeUpdate, trapBackOffice, updateFromBackOfficeCommand);
        }

        [Test]
        public void IncreaseCatchingNights_IncreasesCatchingNightsOnlyForCatchingTraps()
        {
            const int numberOfTraps = 2;
            Trap catchingTrap = new TrapBuilder()
                .WithStatus(TrapStatus.Catching)
                .WithRecordedOn(DateTimeOffset.Now)
                .WithNumberOfTraps(numberOfTraps);
            Trap notCatchingTrap = new TrapBuilder()
                .WithRecordedOn(DateTimeOffset.Now)
                .WithStatus(TrapStatus.NotCatching)
                .WithNumberOfTraps(numberOfTraps);
            Trap removedTrap = new TrapBuilder()
                .WithStatus(TrapStatus.Removed)
                .WithRecordedOn(DateTimeOffset.Now)
                .WithNumberOfTraps(numberOfTraps);

            catchingTrap.IncreaseCatchingNights();
            notCatchingTrap.IncreaseCatchingNights();
            removedTrap.IncreaseCatchingNights();

            catchingTrap.CatchingNights.Should().Be(numberOfTraps);
            notCatchingTrap.CatchingNights.Should().Be(0);
            removedTrap.CatchingNights.Should().Be(0);
        }

        [Test]
        public void SetRecorderOn_OnCreate_InitCatchingNights()
        {
            Trap trap = new TrapBuilder()
                .WithStatus(TrapStatus.Catching)
                .WithNumberOfTraps(2);

            trap.SetRecorded(_currentDateTime.AddDays(-2));

            trap.CatchingNights.Should().Be(4);
        }
        
        #region Helpers
        private Trap InitTrap(TrapStatus status, bool trapCreatedToday, bool trapWithoutCatches) {
            Trap result = new TrapBuilder()
                .WithId(Guid.NewGuid())
                .WithCoordinates(1, 1)
                .WithGeoHierarchy("organization", "rayon", "catch area", "sub area", "water authority", "hour square")
                .WithNumberOfTraps(1)
                .WithStatus(status)
                .WithTrapTypeId(TrapType.ConibearBeverratId)
                .WithCreatedById(User.SystemUserId)
                .WithRecordedOn(_currentDateTime)
                .WithRemarks("trap remarks")
                .WithCatches(trapWithoutCatches
                    ? new Catch[0]
                    : new Catch[] { new CatchBuilder().WithCatchType(_catchTypes.Single()) });
            result.SetCreated(_currentDateTime.AddDays(trapCreatedToday ? 0 : 1), User.SystemUserId);
            return result;
        }
           
        private TrapCreateOrUpdate.Command InitUpdateFromMobileCommand(Trap trap) =>
            new TrapCreateOrUpdate.Command
            {
                Id = trap.Id,
                Longitude = trap.Location.X + 1,
                Latitude = trap.Location.Y + 1,
                NumberOfTraps = trap.NumberOfTraps + 1,
                Status = trap.Status == TrapStatus.Removed ? TrapStatus.Catching : (trap.Status + 1),
                TrapTypeId = TrapType.OtterId,
                Remarks = trap.Remarks + "1",
                RecordedOn = trap.RecordedOn.AddMinutes(1)
            };

        private TrapUpdate.Command InitUpdateFromBackOfficeCommand(Trap trap) =>
            new TrapUpdate.Command
            {
                Id = trap.Id,
                TrapTypeId = TrapType.OtterId,
                Remarks = trap.Remarks + "1",
                Status = trap.Status == TrapStatus.Removed ? TrapStatus.Catching : (trap.Status + 1),
                Longitude = trap.Location.X + 1,
                Latitude = trap.Location.Y + 1
            };

        private void AssertMobileUpdate(Trap original, Trap updated, TrapCreateOrUpdate.Command updateCommand)
        {
            var skipTrapTypeAndRecordedOn = !original.Catching || !original.NoCaches || !original.CreatedToday;
            updated.RecordedOn.Should()
                .Be(original.RecordedOn);
            updated.TrapTypeId.Should()
                .Be(skipTrapTypeAndRecordedOn ? original.TrapTypeId : updateCommand.TrapTypeId);

            updated.Location.X.Should()
                .Be(original.Catching ? updateCommand.Longitude : original.Location.X);
            updated.Location.Y.Should()
                .Be(original.Catching ? updateCommand.Latitude : original.Location.Y);
            updated.SubAreaHourSquare.Geometry.Should()
                .Be(original.Catching ? _otherSubAreaHourSquare.Geometry : original.SubAreaHourSquare.Geometry);
            updated.Province.Name.Should()
                .Be(original.Catching ? _otherProvince.Name : original.Province.Name);
            updated.NumberOfTraps.Should()
                .Be(original.Catching ? updateCommand.NumberOfTraps: original.NumberOfTraps);

            updated.Status.Should()
                .Be(original.NotRemoved ? updateCommand.Status : original.Status);

            updated.Remarks.Should().Be(updateCommand.Remarks);
        }

        private void AssertBackOfficeUpdate(Trap original, Trap updated, TrapUpdate.Command updateCommand)
        {
            updated.SubAreaHourSquare.Geometry.Should().Be(original.SubAreaHourSquare.Geometry);
            updated.Province.Name.Should().Be(original.Province.Name);
            updated.NumberOfTraps.Should().Be(original.NumberOfTraps);
            updated.Status.Should().Be(updateCommand.Status);
            updated.RecordedOn.Should().Be(original.RecordedOn);

            var skipTrapType = !original.Catching || !original.NoCaches || !original.CreatedToday;
            updated.TrapTypeId.Should()
                .Be(skipTrapType ? original.TrapTypeId : updateCommand.TrapTypeId);

            var skipLocationUpdate = !original.NoCaches;
            updated.Location.X.Should()
                .Be(skipLocationUpdate ? original.Location.X : updateCommand.Longitude);
            updated.Location.Y.Should()
                .Be(skipLocationUpdate ? original.Location.Y : updateCommand.Latitude);

            updated.Remarks.Should().Be(updateCommand.Remarks);
        }
        #endregion Helpers
    }
}
