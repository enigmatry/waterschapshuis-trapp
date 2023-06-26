using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Syncs
{
    [Category("integration")]
    public class SyncsControllerFixture : MobileApiIntegrationFixtureBase
    {
        private IRepository<Tracking> _trackingRepository = null!;
        private IRepository<Observation> _observationRepository = null!;
        private ICurrentUserProvider _currentUserProvider = null!;
        private readonly DateTimeOffset _recordedOnFirst = DateTimeOffset.Now.AddDays(-1);
        private readonly DateTimeOffset _recordedOnSecond = DateTimeOffset.Now.AddDays(-2);

        [SetUp]
        public void SetUp()
        {
            _trackingRepository = Resolve<IRepository<Tracking>>();
            _observationRepository = Resolve<IRepository<Observation>>();
            _currentUserProvider = Resolve<ICurrentUserProvider>();

            SubAreaHourSquare subAreaHourSquare = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(4.899432, 52.379190);

            AddAndSaveChanges(subAreaHourSquare);
        }

        [Test]
        public async Task SyncTwoTrackingLocation_SyncedLocationsInsertedInDb()
        {
            var command = new TrackingSync.Command
            {
                TrackingLocations = new List<TrackingSync.Command.TrackingItem>
                {
                    new TrackingSync.Command.TrackingItem
                    {
                        Id = "7F9F4E5E-C193-4365-94E9-C48470BA83C7",
                        Latitude = 52.379190,
                        Longitude = 4.899432,
                        RecordedOn = _recordedOnFirst,
                        TrappingTypeId = new Guid("A2BA2913-77D6-47D9-B893-F9D0CC0432BB"),
                        SessionId = new Guid("811fdadb-3a2a-4ef7-8f6d-90230fe70a8a"),
                        IsTimewriting = true,
                        IsTrackingMap = true
                    },
                    new TrackingSync.Command.TrackingItem
                    {
                        Id = "29C395F0-6F90-42E9-99A6-F164122D9F7F",
                        Latitude = 52.379191,
                        Longitude = 4.899433,
                        RecordedOn = _recordedOnSecond,
                        TrappingTypeId = new Guid("A2BA2913-77D6-47D9-B893-F9D0CC0432BB"),
                        SessionId = new Guid("811fdadb-3a2a-4ef7-8f6d-90230fe70a8a"),
                        IsTimewriting = true,
                        IsTrackingMap = false
                    },
                    new TrackingSync.Command.TrackingItem
                    {
                        Id = "", // TODO API Patch: empty guid
                        Latitude = 52.379190,
                        Longitude = 4.899432,
                        RecordedOn = _recordedOnFirst,
                        TrappingTypeId = new Guid("A2BA2913-77D6-47D9-B893-F9D0CC0432BB"),
                        SessionId = new Guid("811fdadb-3a2a-4ef7-8f6d-90230fe70a8a"),
                        IsTimewriting = true,
                        IsTrackingMap = true
                    },
                    new TrackingSync.Command.TrackingItem
                    {
                        Id = "1", // TODO API Patch invalid guid
                        Latitude = 52.379190,
                        Longitude = 4.899432,
                        RecordedOn = _recordedOnFirst,
                        TrappingTypeId = new Guid("A2BA2913-77D6-47D9-B893-F9D0CC0432BB"),
                        SessionId = new Guid("811fdadb-3a2a-4ef7-8f6d-90230fe70a8a"),
                        IsTimewriting = true,
                        IsTrackingMap = true
                    }
                }
            };

            TrackingSync.Result trackingSyncResult =
                await Client.PostAsync<TrackingSync.Command, TrackingSync.Result>("syncs/tracking", command);

            trackingSyncResult.Should().NotBeNull();

            var trackings =
                _trackingRepository
                    .QueryAll()
                    .OrderByDescending(x => x.RecordedOn)
                    .AsNoTracking()
                    .QueryByCreator(_currentUserProvider.UserId.GetValueOrDefault())
                    .ToList();

            trackings.Count.Should().Be(2);
            var firstTracking = trackings.First();

            firstTracking.Location.X.Should().Be(4.899432);
            firstTracking.Location.Y.Should().Be(52.379190);
            firstTracking.RecordedOn.AssertRecordedOn(_recordedOnFirst);
            firstTracking.CreatedById.Should().Be(_currentUserProvider.UserId.GetValueOrDefault());
            firstTracking.TrappingTypeId.Should().Be("A2BA2913-77D6-47D9-B893-F9D0CC0432BB");
            firstTracking.SessionId.Should().Be("811fdadb-3a2a-4ef7-8f6d-90230fe70a8a");
        }

        [Test]
        public async Task SyncObservation_ReturnsIdsOfSyncedObservationsAndStorageAccessKey()
        {
            var command = new ObservationSync.Command
            {
                Observations = new List<ObservationSync.Command.ObservationItem>
                {
                    new ObservationSync.Command.ObservationItem
                    {
                        Id = new Guid("E3EB6067-0CFE-4028-A87B-1B4C670CBF53"),
                        Latitude = 52.379190,
                        Longitude = 4.899432,
                        RecordedOn = _recordedOnFirst,
                        Type = 1,
                        Remarks = "Test remark 1",
                        HasPhoto = true
                    },
                    new ObservationSync.Command.ObservationItem
                    {
                        Id = new Guid("9B197A6F-6593-4188-B823-B8BB221CBADF"),
                        Latitude = 52.379191,
                        Longitude = 4.899433,
                        RecordedOn = _recordedOnSecond,
                        Type = 2,
                        Remarks = "Test remark 2",
                        HasPhoto = false
                    }
                }
            };

            ObservationSync.Result observationsSyncResult =
                await Client.PostAsync<ObservationSync.Command, ObservationSync.Result>("syncs/observation", command);

            observationsSyncResult.Should().NotBeNull();
            observationsSyncResult.SavedItems.Should().NotBeNull();
            observationsSyncResult.SavedItems.Count().Should().Be(2);
            observationsSyncResult.StorageAccessKey.Should().NotBeNullOrWhiteSpace();

            observationsSyncResult.SavedItems
                .FirstOrDefault(item => item.Id == new Guid("E3EB6067-0CFE-4028-A87B-1B4C670CBF53")).Should()
                .NotBeNull();

            var observations =
                _observationRepository
                    .QueryAll()
                    .OrderByDescending(x => x.RecordedOn)
                    .AsNoTracking()
                    .QueryByCreator(_currentUserProvider.UserId.GetValueOrDefault())
                    .ToList();

            observations.Count.Should().Be(2);
            var firstObservation = observations.First();
            var secondObservation = observations.Last();

            firstObservation.Location.X.Should().Be(4.899432);
            firstObservation.Location.Y.Should().Be(52.379190);
            firstObservation.RecordedOn.AssertRecordedOn(_recordedOnFirst);
            firstObservation.CreatedById.Should().Be(_currentUserProvider.UserId.GetValueOrDefault());
            firstObservation.Type.Should().Be(1);
            firstObservation.Remarks.Should().Be("Test remark 1");
            firstObservation.PhotoUrl.Should().NotBeEmpty();

            secondObservation.PhotoUrl.Should().BeNull();
        }
    }
}
