using FakeItEasy;
using FluentAssertions;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using Quartz;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.Scheduler.Jobs;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests.Jobs
{
    [Category("integration")]
    public class CreateTrackingLinesJobFixture : SchedulerIntegrationFixtureBase
    {
        private static readonly int CurrentDateDeltaInDays = -1;

        private CreateTrackingLinesJob _populateReportTablesJob = null!;
        private IJobExecutionContext _jobContext = null!;

        private readonly Guid _trackingSessionId1 = new Guid("c63f5a50-4e81-49eb-bb04-a5bb4c01a375");
        private readonly Guid _trackingSessionId2 = new Guid("1446c960-68d9-4258-b08d-d51618d5757f");

        [SetUp]
        public void SetUp()
        {
            _populateReportTablesJob = Resolve<CreateTrackingLinesJob>();
            _jobContext = A.Fake<IJobExecutionContext>();
        }

        [TestCase(TestCase.SingleSessionOnDate, ExpectedResult =
            "51.11/41.11 51.22/41.22 51.33/41.33 51.44/41.44 51.55/41.55")]
        [TestCase(TestCase.TwoSessionsOnDate, ExpectedResult =
            "51.11/41.11 51.22/41.22 51.33/41.33 51.44/41.44 51.55/41.55 | 52.11/42.11 52.22/42.22 52.33/42.33")]
        [TestCase(TestCase.NoSessionsOnDate, ExpectedResult = "")]
        [TestCase(TestCase.SessionWithOneTrackingOnDate, ExpectedResult = "")]
        [TestCase(TestCase.NotTrackingMapSessionsOnDate, ExpectedResult = "")]
        public async Task<string> Execute_Create(TestCase testCase)
        {
            AddAndSaveChanges(GetTestCaseTrackings(testCase));
            SetCreatedOnForTrackings();

            await _populateReportTablesJob.Execute(_jobContext);

            var trackingLines = QueryDbSkipCache<TrackingLine>()
                .QueryByDate(DateTimeOffset.Now.AddDays(CurrentDateDeltaInDays))
                .ToArray();

            return TrackingLinesAsString(trackingLines);
        }

        [Test]
        public async Task Execute_Update()
        {
            AddAndSaveChanges(
                new[]
                {
                    new TestCaseData(_trackingSessionId1, 52.22, 42.22, "15:16:00").AsTracking(),
                    new TestCaseData(_trackingSessionId1, 52.33, 42.33, "15:17:00").AsTracking(),
                    new TestCaseData(_trackingSessionId1, 52.11, 42.11, "15:15:00").AsTracking(),
                });
            SetCreatedOnForTrackings();
            await _populateReportTablesJob.Execute(_jobContext);

            var initTrackingLine = LoadTrackingLine(_trackingSessionId1);
            initTrackingLine.Should().NotBeNull();
            initTrackingLine.SessionId.Should().Be(_trackingSessionId1);
            TrackingLinesAsString(initTrackingLine)
                .Should()
                .Be("52.11/42.11 52.22/42.22 52.33/42.33",
                    "3 trackings of same session results in 3 point tracking line");

            // remove all trackings in db
            RemoveFromDb(QueryDb<Tracking>().Where(x => true).ToArray());

            AddAndSaveChanges(
                new[]
                {
                    new TestCaseData(_trackingSessionId1, 51.22, 41.22, "09:00:01").AsTracking(),
                    new TestCaseData(_trackingSessionId1, 51.55, 41.55, "09:00:04").AsTracking(),
                    new TestCaseData(_trackingSessionId1, 51.11, 41.11, "09:00:00").AsTracking(),
                    new TestCaseData(_trackingSessionId1, 51.33, 41.33, "09:00:02").AsTracking(),
                    new TestCaseData(_trackingSessionId1, 51.44, 41.44, "09:00:03").AsTracking(),
                });
            SetCreatedOnForTrackings();

            await _populateReportTablesJob.Execute(_jobContext);

            var updatedTrackingLine = LoadTrackingLine(_trackingSessionId1);
            updatedTrackingLine.Should().NotBeNull();
            updatedTrackingLine.SessionId.Should().Be(_trackingSessionId1);
            TrackingLinesAsString(updatedTrackingLine)
                .Should()
                .Be("51.11/41.11 51.22/41.22 51.33/41.33 51.44/41.44 51.55/41.55",
                    "3 init trackings are removed & 5 new ones are inserted, update init tracking line contains 5 new points");
        }

        #region Helpers
        private Tracking[] GetTestCaseTrackings(TestCase testCase) =>
            new Dictionary<TestCase, Tracking[]>
            {
                {
                    TestCase.SingleSessionOnDate,
                    new[]
                    {
                        new TestCaseData(_trackingSessionId1, 51.22, 41.22, "09:00:01").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.55, 41.55, "09:00:04").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.11, 41.11, "09:00:00").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.33, 41.33, "09:00:02").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.44, 41.44, "09:00:03").AsTracking(),
                    }
                },
                {
                    TestCase.TwoSessionsOnDate,
                    new[]
                    {
                        new TestCaseData(_trackingSessionId1, 51.22, 41.22, "09:00:01").AsTracking(),
                        new TestCaseData(_trackingSessionId2, 52.22, 42.22, "15:16:00").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.55, 41.55, "09:00:04").AsTracking(),
                        new TestCaseData(_trackingSessionId2, 52.33, 42.33, "15:17:00").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.11, 41.11, "09:00:00").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.33, 41.33, "09:00:02").AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.44, 41.44, "09:00:03").AsTracking(),
                        new TestCaseData(_trackingSessionId2, 52.11, 42.11, "15:15:00").AsTracking(),
                    }
                },
                {
                    TestCase.NoSessionsOnDate,
                    new Tracking[] {}
                },
                {
                    TestCase.SessionWithOneTrackingOnDate,
                    new[]
                    {
                        new TestCaseData(_trackingSessionId1, 51.22, 41.22, "09:00:01").AsTracking()
                    }
                },
                {
                    TestCase.NotTrackingMapSessionsOnDate,
                    new[]
                    {
                        new TestCaseData(_trackingSessionId1, 51.22, 41.22, "09:00:01", false).AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.55, 41.55, "09:00:04", false).AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.11, 41.11, "09:00:00", false).AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.33, 41.33, "09:00:02", false).AsTracking(),
                        new TestCaseData(_trackingSessionId1, 51.44, 41.44, "09:00:03", false).AsTracking(),
                    }
                },
            }[testCase];

        public enum TestCase
        {
            SingleSessionOnDate,
            TwoSessionsOnDate,
            NoSessionsOnDate,
            SessionWithOneTrackingOnDate,
            NotTrackingMapSessionsOnDate
        }

        public struct TestCaseData
        {
            public Guid TrackingSessionId { get; private set; }
            public double XCoordinate { get; private set; }
            public double YCoordinate { get; private set; }
            public DateTimeOffset TrackingRecordedOn { get; private set; }
            public bool IsTrackingMap { get; private set; }

            public TestCaseData(
                Guid trackingSessionId,
                double xCoordinate,
                double yCoordinate,
                string recordedOnTime,
                bool isTrackingMap = true)
            {
                TrackingSessionId = trackingSessionId;
                XCoordinate = xCoordinate;
                YCoordinate = yCoordinate;
                var date = DateTimeOffset.Now
                    .AddDays(CurrentDateDeltaInDays)
                    .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                TrackingRecordedOn = $"{date} {recordedOnTime}".AppToDateTimeOffset();
                IsTrackingMap = isTrackingMap;
            }

            public Tracking AsTracking() =>
                new TrackingBuilder()
                    .WithRecordedOn(TrackingRecordedOn)
                    .WithLocation(XCoordinate, YCoordinate)
                    .WithSessionId(TrackingSessionId)
                    .WithTrackingMap(IsTrackingMap)
                    .WithTimewriting(false)
                    .WithTrackingPrivacy(false)
                    .WithTrappingTypeId(TrappingType.BeverratId);
        }

        private static string TrackingLinesAsString(params TrackingLine[] trackingLines) =>
            String.Join(" | ",
                trackingLines.Select(trackingLine => String.Join(" ", LineStringToString(trackingLine.Polyline))));

        private static string[] LineStringToString(Geometry lineString) =>
            lineString.Coordinates
                .Select(c => $"{c.X.ToString(CultureInfo.InvariantCulture)}/{c.Y.ToString(CultureInfo.InvariantCulture)}")
                .ToArray();

        private TrackingLine LoadTrackingLine(Guid sessionId) =>
            QueryDbSkipCache<TrackingLine>().TryFindBySessionId(sessionId);

        private void SetCreatedOnForTrackings()
        {
            QueryDb<Tracking>()
                .ToList()
                .ForEach(x => x.SetCreated(DateTimeOffset.Now.AddDays(-1), x.CreatedById));
            SaveChanges();
        }
        #endregion Helpers
    }
}
