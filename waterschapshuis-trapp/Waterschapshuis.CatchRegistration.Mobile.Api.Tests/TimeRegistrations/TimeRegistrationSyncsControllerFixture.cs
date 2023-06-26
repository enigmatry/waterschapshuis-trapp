using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.TimeRegistrations
{
    [Category("integration")]
    public class TimeRegistrationSyncsControllerFixture : MobileApiIntegrationFixtureBase
    {
        private const int NumDecimalPlacesToRound = 2;
        private const double LongitudeOne = 4.899432;
        private const double LatitudeOne = 52.379190;
        private const double LongitudeTwo = 5.964231;
        private const double LatitudeTwo = 62.026651;

        private SubAreaHourSquare _subAreaHourSquareOne = null!;
        private SubAreaHourSquare _subAreaHourSquareTwo = null!;

        private ICurrentUserProvider _currentUserProvider = null!;
        private IRepository<Tracking> _trackingRepository = null!;
        private ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService = null!;

        [SetUp]
        public void SetUp()
        {
            _currentUserProvider = Resolve<ICurrentUserProvider>();
            _trackingRepository = Resolve<IRepository<Tracking>>();
            _currentVersionRegionalLayoutService = Resolve<ICurrentVersionRegionalLayoutService>();

            _subAreaHourSquareOne = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(LongitudeOne, LatitudeOne);
            _subAreaHourSquareTwo = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(LongitudeTwo, LatitudeTwo);

            AddAndSaveChanges(_subAreaHourSquareOne, _subAreaHourSquareTwo);
        }

        [Test]
        public async Task TestSingleSessionSingleTypeSingleSubAreaSingleDay()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeId = TrappingType.MuskusratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionId = Guid.NewGuid();

            var endDateTime = await SyncTrackingsForMinuteIntervals(sessionId, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime, 10, 4, true, false);

            var trackings = _trackingRepository
                .QueryAll()
                .QueryUserTrackingsForTheDay(currentUserId, endDateTime)
                .ToList();

            trackings.Should().NotBeEmpty();
            trackings.Count.Should().Be(4);

            var timeRegistration = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeId, endDateTime);

            timeRegistration.Should().NotBeNull();
            timeRegistration?.Hours.Should().Be(0.5);
            timeRegistration?.IsCreatedFromTrackings.Should().BeTrue();
        }

        [Test]
        public async Task TestMultipleSessionsSimpleSingleTypeSingleSubAreaSingleDay()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeId = TrappingType.MuskusratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionIdOne = Guid.NewGuid();
            var sessionIdTwo = Guid.NewGuid();

            var endDateTimeOne = await SyncTrackingsForMinuteIntervals(sessionIdOne, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime, 10, 4, true, false);
            _ = await SyncTrackingsForMinuteIntervals(sessionIdTwo, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime.AddHours(2), 10, 7, true, false);

            var timeRegistration = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeId, endDateTimeOne);

            timeRegistration.Should().NotBeNull();
            timeRegistration?.Hours.Should().Be(1.5);
            timeRegistration?.IsCreatedFromTrackings.Should().BeTrue();
        }

        [Test]
        public async Task TestMultipleSessionsComplexSingleTypeSingleSubAreaSingleDay()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeId = TrappingType.MuskusratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionIdOne = Guid.NewGuid();
            var sessionIdTwo = Guid.NewGuid();
            var sessionIdThree = Guid.NewGuid();
            var sessionIdFour = Guid.NewGuid();

            _ = await SyncTrackingsForMinuteIntervals(sessionIdOne, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime, 10, 4, true, false);
            _ = await SyncTrackingsForMinuteIntervals(sessionIdTwo, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime.AddHours(2), 10, 7, true, false);
            _ = await SyncTrackingsForMinuteIntervals(sessionIdThree, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime.AddHours(4), 10, 4, true, false);
            var endDateTime = await SyncTrackingsForMinuteIntervals(sessionIdFour, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime.AddHours(5), 10, 7, true, false);

            var timeRegistration = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeId, endDateTime);

            timeRegistration.Should().NotBeNull();
            if (timeRegistration != null)
            {
                Math.Round(timeRegistration.Hours, NumDecimalPlacesToRound).Should().Be(3.0);
                timeRegistration.Date.Should().Be(endDateTime.Date);
                timeRegistration.IsCreatedFromTrackings.Should().BeTrue();
            }
        }

        [Test]
        public async Task TestMultipleSessionsMultipleTypeSimpleSingleSubAreaSingleDay()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeIdOne = TrappingType.MuskusratId;
            var trappingTypeIdTwo = TrappingType.BeverratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionIdOne = Guid.NewGuid();
            var sessionIdTwo = Guid.NewGuid();

            var endDateTimeOne = await SyncTrackingsForMinuteIntervals(sessionIdOne, trappingTypeIdOne, LongitudeOne, LatitudeOne, startingDateTime, 10, 4, true, false);
            var endDateTimeTwo = await SyncTrackingsForMinuteIntervals(sessionIdTwo, trappingTypeIdTwo, LongitudeOne, LatitudeOne, startingDateTime.AddHours(2), 10, 7, true, false);

            var timeRegistrationOne = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeIdOne, endDateTimeOne);

            var timeRegistrationTwo = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeIdTwo, endDateTimeTwo);

            timeRegistrationOne.Should().NotBeNull();
            if (timeRegistrationOne != null)
            {
                Math.Round(timeRegistrationOne.Hours, NumDecimalPlacesToRound).Should().Be(0.5);
                timeRegistrationOne.Date.Should().Be(endDateTimeOne.Date);
                timeRegistrationOne.IsCreatedFromTrackings.Should().BeTrue();
            }

            timeRegistrationTwo.Should().NotBeNull();
            if (timeRegistrationTwo != null)
            {
                Math.Round(timeRegistrationTwo.Hours, NumDecimalPlacesToRound).Should().Be(1.0);
                timeRegistrationTwo.Date.Should().Be(endDateTimeTwo.Date);
                timeRegistrationTwo.IsCreatedFromTrackings.Should().BeTrue();
            }
        }

        [Test]
        public async Task TestMultipleSessionsMultipleTypeComplexSingleSubAreaSingleDay()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeIdOne = TrappingType.MuskusratId;
            var trappingTypeIdTwo = TrappingType.BeverratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionIdOne = Guid.NewGuid();
            var sessionIdTwo = Guid.NewGuid();
            var sessionIdThree = Guid.NewGuid();
            var sessionIdFour = Guid.NewGuid();

            _ = await SyncTrackingsForMinuteIntervals(sessionIdOne, trappingTypeIdOne, LongitudeOne, LatitudeOne, startingDateTime, 10, 4, true, false);
            var endDateTimeOne = await SyncTrackingsForMinuteIntervals(sessionIdTwo, trappingTypeIdOne, LongitudeOne, LatitudeOne, startingDateTime.AddMinutes(40), 10, 4, true, false); // THIS!!!!
            _ = await SyncTrackingsForMinuteIntervals(sessionIdThree, trappingTypeIdTwo, LongitudeOne, LatitudeOne, startingDateTime.AddHours(2), 10, 7, true, false);
            var endDateTimeTwo = await SyncTrackingsForMinuteIntervals(sessionIdFour, trappingTypeIdTwo, LongitudeOne, LatitudeOne, startingDateTime.AddHours(4), 10, 7, true, false);

            var timeRegistrationOne = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeIdOne, endDateTimeOne);

            var timeRegistrationTwo = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeIdTwo, endDateTimeTwo);

            timeRegistrationOne.Should().NotBeNull();
            if (timeRegistrationOne != null)
            {
                Math.Round(timeRegistrationOne.Hours, NumDecimalPlacesToRound).Should().Be(1.0);
                timeRegistrationOne.Date.Should().Be(endDateTimeOne.Date);
                timeRegistrationOne.IsCreatedFromTrackings.Should().BeTrue();
            }

            timeRegistrationTwo.Should().NotBeNull();
            if (timeRegistrationTwo != null)
            {
                Math.Round(timeRegistrationTwo.Hours, NumDecimalPlacesToRound).Should().Be(2.0);
                timeRegistrationTwo.Date.Should().Be(endDateTimeTwo.Date);
                timeRegistrationTwo.IsCreatedFromTrackings.Should().BeTrue();
            }
        }

        [Test]
        public async Task TestSingleSessionMultipleTypeSingleSubAreaSingleDay()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeIdOne = TrappingType.MuskusratId;
            var trappingTypeIdTwo = TrappingType.BeverratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionId = Guid.NewGuid();

            var endDateTimeOne = await SyncTrackingsForMinuteIntervals(sessionId, trappingTypeIdOne, LongitudeOne,
                LatitudeOne, startingDateTime, 10, 4, true, false);
            var endDateTimeTwo = await SyncTrackingsForMinuteIntervals(sessionId, trappingTypeIdTwo, LongitudeOne,
                LatitudeOne, startingDateTime.AddHours(2), 10, 7, true, false);

            var timeRegistrationOne = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeIdOne, endDateTimeOne);

            var timeRegistrationTwo = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeIdTwo, endDateTimeTwo);

            timeRegistrationOne.Should().NotBeNull();
            if (timeRegistrationOne != null)
            {
                Math.Round(timeRegistrationOne.Hours, NumDecimalPlacesToRound).Should().Be(0.5);
                timeRegistrationOne.Date.Should().Be(endDateTimeOne.Date);
                timeRegistrationOne.IsCreatedFromTrackings.Should().BeTrue();
            }

            timeRegistrationTwo.Should().NotBeNull();
            if (timeRegistrationTwo != null)
            {
                Math.Round(timeRegistrationTwo.Hours, NumDecimalPlacesToRound).Should().Be(1.0);
                timeRegistrationTwo.Date.Should().Be(endDateTimeTwo.Date);
                timeRegistrationTwo.IsCreatedFromTrackings.Should().BeTrue();
            }
        }

        [Test]
        public async Task TestSingleSessionSingleTypeMultipleSubAreaSingleDay()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeId = TrappingType.MuskusratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionId = Guid.NewGuid();

            var endDateTimeOne = await SyncTrackingsForMinuteIntervals(sessionId, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime, 10, 4, true, false);
            var endDateTimeTwo = await SyncTrackingsForMinuteIntervals(sessionId, trappingTypeId, LongitudeTwo, LatitudeTwo, startingDateTime.AddHours(2), 10, 7, true, false);

            var timeRegistrationOne = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeId, endDateTimeOne);

            var timeRegistrationTwo = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareTwo.Id, trappingTypeId, endDateTimeTwo);

            timeRegistrationOne.Should().NotBeNull();
            if (timeRegistrationOne != null)
            {
                Math.Round(timeRegistrationOne.Hours, NumDecimalPlacesToRound).Should().Be(0.5);
                timeRegistrationOne.Date.Should().Be(endDateTimeOne.Date);
                timeRegistrationOne.IsCreatedFromTrackings.Should().BeTrue();
            }

            timeRegistrationTwo.Should().NotBeNull();
            if (timeRegistrationTwo != null)
            {
                Math.Round(timeRegistrationTwo.Hours, NumDecimalPlacesToRound).Should().Be(1.0);
                timeRegistrationTwo.Date.Should().Be(endDateTimeTwo.Date);
                timeRegistrationTwo.IsCreatedFromTrackings.Should().BeTrue();
            }
        }

        [Test]
        public async Task TestSingleSessionSingleTypeSingleSubAreaMultipleDays()
        {
            var startingDateTime = GetTimeOfTheDay(-1, 8, 30);
            var trappingTypeId = TrappingType.MuskusratId;
            var currentUserId = _currentUserProvider.UserId.GetValueOrDefault();
            var sessionId = Guid.NewGuid();

            var endDateTimeOne = await SyncTrackingsForMinuteIntervals(sessionId, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime.AddDays(-1), 10, 4, true, false);
            var endDateTimeTwo = await SyncTrackingsForMinuteIntervals(sessionId, trappingTypeId, LongitudeOne, LatitudeOne, startingDateTime, 10, 7, true, false);

            var timeRegistrationOne = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeId, endDateTimeOne);

            var timeRegistrationTwo = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .ExistingTimeRegistrationEntryEntity(currentUserId, _subAreaHourSquareOne.Id, trappingTypeId, endDateTimeTwo);

            timeRegistrationOne.Should().NotBeNull();
            if (timeRegistrationOne != null)
            {
                Math.Round(timeRegistrationOne.Hours, NumDecimalPlacesToRound).Should().Be(0.5);
                timeRegistrationOne.Date.Should().Be(endDateTimeOne.Date);
                timeRegistrationOne.IsCreatedFromTrackings.Should().BeTrue();
            }

            timeRegistrationTwo.Should().NotBeNull();
            if (timeRegistrationTwo != null)
            {
                Math.Round(timeRegistrationTwo.Hours, NumDecimalPlacesToRound).Should().Be(1.0);
                timeRegistrationTwo.Date.Should().Be(endDateTimeTwo.Date);
                timeRegistrationTwo.IsCreatedFromTrackings.Should().BeTrue();
            }
        }

        private async Task<DateTimeOffset> SyncTrackingsForMinuteIntervals(Guid sessionId, Guid trappingTypeId, double longitude, double latitude, DateTimeOffset startTime, int minuteInterval, int count, bool isTimewriting, bool isTrackingMap)
        {
            var trackingItems = new List<TrackingSync.Command.TrackingItem>();
            DateTimeOffset endTime = startTime;

            for (int i = 0; i < count; i++)
            {
                endTime = startTime.AddMinutes(i * minuteInterval);
                trackingItems.Add(
                    new TrackingSync.Command.TrackingItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        Longitude = longitude,
                        Latitude = latitude,
                        RecordedOn = endTime,
                        TrappingTypeId = trappingTypeId,
                        SessionId = sessionId,
                        IsTimewriting = isTimewriting,
                        IsTrackingMap = isTrackingMap
                    });
            }

            var command = new TrackingSync.Command
            {
                TrackingLocations = trackingItems
            };

            _ = await Client.PostAsync<TrackingSync.Command, TrackingSync.Result>("syncs/tracking", command);

            return endTime;
        }

        private static DateTimeOffset GetTimeOfTheDay(int daysToAdd, int hours, int minutes)
        {
            var yesterday = DateTime.Now.AddDays(daysToAdd);
            return
                new DateTimeOffset(yesterday.Year, yesterday.Month, yesterday.Day, hours, minutes, 0, TimeSpan.Zero);
        }
    }
}
