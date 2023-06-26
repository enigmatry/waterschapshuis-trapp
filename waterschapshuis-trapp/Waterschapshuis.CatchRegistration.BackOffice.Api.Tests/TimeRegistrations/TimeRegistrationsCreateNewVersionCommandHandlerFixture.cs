using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.TimeRegistrations
{
    [Category("integration")]
    public class TimeRegistrationsCreateNewVersionCommandHandlerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private IMediator _mediator = null!;

        private const int TrackingDiffInMin = 30;
        private DateTimeOffset _currentDateTime;
        private Guid _userId = TestPrincipal.TestUserId;
        private Guid _trappingTypeId = TrappingType.BeverratId;
        private HourSquare _commonHourSquare = null!;

        [SetUp]
        public void SetUp()
        {
            _mediator = Resolve<IMediator>();
            _currentDateTime = new DateTimeOffset(
                DateTimeOffset.Now.Year, 
                DateTimeOffset.Now.Month, 
                DateTimeOffset.Now.Day,
                0, 0, 1, TimeSpan.FromSeconds(0));
            _commonHourSquare = new HourSquareBuilder().WithName("HSQ");
            AddAndSaveChanges(_commonHourSquare);
        }

        [Test]
        public async Task UnchangedSAHSes_With_2TrackingTRs_And_1ManualTR()
        {
            var initState = await CreateInitSAHSesAndTRs(true, true, false, false);

            var newSAHSes = GetNeighbouringSAHSes();
            var command = TimeRegistrationsCreateNewVersion.Command.Create(newSAHSes.ToList());
            var result = await _mediator.Send(command);

            result.Succeed.Should().BeTrue();
            result.TimeRegistrations.Should().NotBeEmpty();
            result.TimeRegistrations
                .Count(x => x.IsCreatedFromTrackings)
                .Should().Be(initState.TimeRegistrations.Count(x => x.IsCreatedFromTrackings));
            result.TimeRegistrations
                .Count(x => !x.IsCreatedFromTrackings)
                .Should().Be(initState.TimeRegistrations.Count(x => !x.IsCreatedFromTrackings));
            result.TimeRegistrations
                .Where(x => x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(initState.TotalHoursFromTracking);
            result.TimeRegistrations
                .Where(x => !x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(initState.TotalHoursNotFromTracking);
            result.TimeRegistrations
                .All(tr => newSAHSes.Select(x => x.Id).Contains(tr.SubAreaHourSquareId))
                .Should().BeTrue("New TRs must belong to 3 new SAHSes");
        }

        [Test]
        public async Task Last2SAHSesMerged_With_2TrackingTRs_And_1ManualTR()
        {
            var initState = await CreateInitSAHSesAndTRs(true, true, false, false);

            // 2 - so second (last) SAHS.Geomatry includes second + third SAHS.Geomatry of initState
            var newSAHSes = GetNeighbouringSAHSes(1, 2); 
            var command = TimeRegistrationsCreateNewVersion.Command.Create(newSAHSes.ToList());
            var result = await _mediator.Send(command);

            result.Succeed.Should().BeTrue();
            result.TimeRegistrations.Should().NotBeEmpty();
            result.TimeRegistrations
                .Count(x => x.IsCreatedFromTrackings)
                .Should().Be(1, "SAHS including manual-TR expands over SAHS of second tracking-TR");
            result.TimeRegistrations
                .Count(x => !x.IsCreatedFromTrackings)
                .Should().Be(1);
            result.TimeRegistrations
                .Where(x => x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(1);
            result.TimeRegistrations
                .Where(x => !x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(2);
            result.TimeRegistrations
                .All(tr => newSAHSes.Select(x => x.Id).Contains(tr.SubAreaHourSquareId))
                .Should().BeTrue("New TRs must belong to 2 new SAHSes");
        }

        [Test]
        public async Task TwoSAHSesExpanded_With_2TrackingTRs_And_1ManualTR()
        {
            var initState = await CreateInitSAHSesAndTRs(true, true, false, false);

            // middle SAHS of initState is shared, but not on the middle because each SAHS has tracking in the middle
            var newSAHSes = GetNeighbouringSAHSes(1.6, 1.4);
            var command = TimeRegistrationsCreateNewVersion.Command.Create(newSAHSes.ToList());
            var result = await _mediator.Send(command);

            result.Succeed.Should().BeTrue();
            result.TimeRegistrations.Should().NotBeEmpty();
            result.TimeRegistrations
                .Count(x => x.IsCreatedFromTrackings)
                .Should().Be(1);
            result.TimeRegistrations
                .Count(x => !x.IsCreatedFromTrackings)
                .Should().Be(1);
            result.TimeRegistrations
                .Where(x => x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(1.5);
            result.TimeRegistrations
                .Where(x => !x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(1.5);
            result.TimeRegistrations
                .All(tr => newSAHSes.Select(x => x.Id).Contains(tr.SubAreaHourSquareId))
                .Should().BeTrue("New TRs must belong to 2 new SAHSes");
        }

        [Test]
        public async Task OneSAHSExpanded_With_3ManualTRs()
        {
            var initState = await CreateInitSAHSesAndTRs(false, false, false, false);

            // 1 SAHS that will expand over all initially seeded SAHses
            var newSAHSes = GetNeighbouringSAHSes(10);
            var command = TimeRegistrationsCreateNewVersion.Command.Create(newSAHSes.ToList());
            var result = await _mediator.Send(command);

            result.Succeed.Should().BeTrue();
            result.TimeRegistrations.Should().NotBeEmpty();
            result.TimeRegistrations
                .Count(x => x.IsCreatedFromTrackings)
                .Should().Be(0);
            result.TimeRegistrations
                .Count(x => !x.IsCreatedFromTrackings)
                .Should().Be(1);
            result.TimeRegistrations
                .Where(x => x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(0);
            result.TimeRegistrations
                .Where(x => !x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(3);
            result.TimeRegistrations
                .All(tr => newSAHSes.Select(x => x.Id).Contains(tr.SubAreaHourSquareId))
                .Should().BeTrue("New TRs must belong to 1 new SAHSes");
        }

        [Test]
        public async Task UnchangedSAHSes__With_2TrackingTRs_And_1ManualTR_And_RandomTrackingSession()
        {
            var initState = await CreateInitSAHSesAndTRs(true, true, false, true);

            var newSAHSes = GetNeighbouringSAHSes();
            var command = TimeRegistrationsCreateNewVersion.Command.Create(newSAHSes.ToList());
            var result = await _mediator.Send(command);

            result.Succeed.Should().BeTrue();
            result.TimeRegistrations.Should().NotBeEmpty();
            result.TimeRegistrations
                .Count(x => x.IsCreatedFromTrackings)
                .Should().Be(initState.TimeRegistrations.Count(x => x.IsCreatedFromTrackings));
            result.TimeRegistrations
                .Count(x => !x.IsCreatedFromTrackings)
                .Should().Be(initState.TimeRegistrations.Count(x => !x.IsCreatedFromTrackings));
            result.TimeRegistrations
                .Where(x => x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(initState.TotalHoursFromTracking);
            result.TimeRegistrations
                .Where(x => !x.IsCreatedFromTrackings).Sum(x => x.Hours)
                .Should().Be(initState.TotalHoursNotFromTracking);
            result.TimeRegistrations
                .All(tr => newSAHSes.Select(x => x.Id).Contains(tr.SubAreaHourSquareId))
                .Should().BeTrue("New TRs must belong to 3 new SAHSes");
        }


        #region Helpers
        /// <summary>
        /// Creates 3 SAHSes, 1 tracking session lasting for 3h (1h per SAHS), 3 TRs
        /// </summary>
        /// <param name="isFirstTRFromTracking">Is first SAHS TR originated from tracking or user input</param>
        /// <param name="isSecondTRFromTracking">Is second SAHS TR originated from tracking or user input</param>
        /// <param name="isThridTRFromTracking">Is third SAHS TR originated from tracking or user input</param>
        /// <param name="withRandomTrackingSession">Is tracking session random in number of trackings and duration</param>
        /// <returns></returns>
        private async Task<InitState> CreateInitSAHSesAndTRs(
            bool isFirstTRFromTracking,
            bool isSecondTRFromTracking,
            bool isThridTRFromTracking,
            bool withRandomTrackingSession)
        {
            var sahses = GetNeighbouringSAHSes();
            AddAndSaveChanges(sahses);

            var trackings = withRandomTrackingSession
                ? GetRandomTrackingSession(_currentDateTime, sahses)
                : GetTrackingSession(_currentDateTime, sahses);
            AddAndSaveChanges(trackings);

            var trackingsForTimeRegistrationCreation = trackings
                .Where(x =>
                    (isFirstTRFromTracking ? sahses[0].Geometry.Contains(x.Location) : false) ||
                    (isSecondTRFromTracking ? sahses[1].Geometry.Contains(x.Location) : false) ||
                    (isThridTRFromTracking ? sahses[2].Geometry.Contains(x.Location) : false)
                ).ToList();

            if (trackingsForTimeRegistrationCreation.Any())
                await CreateTimeRegistrationFromTracking(trackingsForTimeRegistrationCreation);

            if (!isFirstTRFromTracking)
                CreateTimeRegistrationNotFromTracking(sahses[0], 1);
            if (!isSecondTRFromTracking)
                CreateTimeRegistrationNotFromTracking(sahses[1], 1);
            if (!isThridTRFromTracking)
                CreateTimeRegistrationNotFromTracking(sahses[2], 1);

            var TRs = QueryDb<TimeRegistration>().ToList();
            var initState = new InitState
            {
                SubAreaHourSquares = sahses.ToList(),
                Trackings = trackings.ToList(),
                TimeRegistrations = TRs,
                TotalHoursFromTracking = TRs.Where(x => x.IsCreatedFromTrackings).Sum(x => x.Hours),
                TotalHoursNotFromTracking = TRs.Where(x => !x.IsCreatedFromTrackings).Sum(x => x.Hours)
            };

            // Assert:
            TRs.Should().NotBeEmpty();
            TRs.Count.Should().Be(sahses.Count());
            TRs.All(x => x.IsCreatedFromTrackings).Should().BeFalse();
            TRs.Sum(x => x.Hours).Should()
                .Be(initState.TotalHoursFromTracking + initState.TotalHoursNotFromTracking);

            return initState;
        }

        private SubAreaHourSquare[] GetNeighbouringSAHSes(double sahs1Dimension = 1, double? sahs2Dimension = 1, double? sahs3Dimension = 1)
        {
            var result = new List<SubAreaHourSquare>();

            var sahs1 = GetSAHS(
                sahs1Dimension,
                sahs1Dimension,
                sahs1Dimension);
            result.Add(sahs1);

            if (sahs2Dimension.HasValue)
            {
                var sahs2 = GetSAHS(
                    sahs1.Geometry.Centroid.X + sahs1Dimension + sahs2Dimension.Value,
                    sahs2Dimension.Value,
                    sahs2Dimension.Value);
                result.Add(sahs2);

                if (sahs3Dimension.HasValue)
                {
                    var sahs3 = GetSAHS(
                        sahs2.Geometry.Centroid.X + sahs2Dimension.Value + sahs3Dimension.Value,
                        sahs3Dimension.Value,
                        sahs3Dimension.Value);
                    result.Add(sahs3);
                }
            }

            return result.ToArray();
        }

        private SubAreaHourSquare GetSAHS(double x, double y, double dimension) =>
            new SubAreaHourSquareBuilder()
                .WithGeoHierarchy("org", "ray", "car", "sar", "wat", "hsq")
                .WithHourSquare(_commonHourSquare)
                .WithStartingCoordinate(x, y, dimension);

        private Tracking[] GetTrackingSession(DateTimeOffset startTime, params SubAreaHourSquare[] sahses)
        {
            var trackings = new List<Tracking>();
            var sessionId = Guid.NewGuid();
            var currentTime = startTime;

            foreach (var sahs in sahses)
            {
                var nearMinX = sahs.Geometry.Coordinates.Select(x => x.X).Min() + 0.1;
                Tracking tracking1 = new TrackingBuilder()
                    .WithLocation(nearMinX, sahs.Geometry.Centroid.Y)
                    .WithSessionId(sessionId)
                    .WithRecordedOn(currentTime)
                    .WithUserId(_userId)
                    .WithTimewriting(true)
                    .WithTrappingTypeId(_trappingTypeId);

                currentTime = currentTime.AddMinutes(TrackingDiffInMin);

                Tracking tracking2 = new TrackingBuilder()
                    .WithLocation(sahs.Geometry.Centroid.X, sahs.Geometry.Centroid.Y)
                    .WithSessionId(sessionId)
                    .WithRecordedOn(currentTime)
                    .WithUserId(_userId)
                    .WithTimewriting(true)
                    .WithTrappingTypeId(_trappingTypeId);

                currentTime = currentTime.AddMinutes(TrackingDiffInMin);

                var nearMaxX = sahs.Geometry.Coordinates.Select(x => x.X).Max() - 0.1;
                Tracking tracking3 = new TrackingBuilder()
                    .WithLocation(nearMaxX, sahs.Geometry.Centroid.Y)
                    .WithSessionId(sessionId)
                    .WithRecordedOn(currentTime)
                    .WithUserId(_userId)
                    .WithTimewriting(true)
                    .WithTrappingTypeId(_trappingTypeId);

                currentTime = currentTime.AddSeconds(1);

                trackings.Add(tracking1);
                trackings.Add(tracking2);
                trackings.Add(tracking3);
            }

            return trackings.OrderBy(x => x.RecordedOn).ToArray();
        }

        private Tracking[] GetRandomTrackingSession(DateTimeOffset startTime, params SubAreaHourSquare[] sahses)
        {
            var trackings = new List<Tracking>();
            var sessionId = Guid.NewGuid();
            var currentTime = startTime;
            var random = new Random();

            foreach (var sahs in sahses)
            {
                var trackingCount = random.Next(2, 5);

                for (int i = 0; i < trackingCount; i++)
                {
                    var point = sahs.Geometry.InteriorPoint;
                    Tracking tracking = new TrackingBuilder()
                        .WithLocation(point.X, point.Y)
                        .WithSessionId(sessionId)
                        .WithRecordedOn(currentTime)
                        .WithUserId(_userId)
                        .WithTimewriting(true)
                        .WithTrappingTypeId(_trappingTypeId);
                    trackings.Add(tracking);
                    currentTime = currentTime.AddSeconds(random.Next(1, 360));
                }
            }

            return trackings.OrderBy(x => x.RecordedOn).ToArray();
        }

        private async Task CreateTimeRegistrationFromTracking(List<Tracking> trackings)
        {
            var timeRegistrationsCreateCommand =
                TimeRegistrationsCreate.Command.Create(trackings.OrderBy(x => x.RecordedOn).ToList(), TestPrincipal.TestUserId);
            await _mediator.Send(timeRegistrationsCreateCommand);
            SaveChanges();
        }

        private void CreateTimeRegistrationNotFromTracking(SubAreaHourSquare sahs, double hours)
        {
            var timeRegistration = TimeRegistration.Create(
                _userId,
                sahs.Id,
                _trappingTypeId,
                _currentDateTime,
                hours,
                TimeRegistrationStatus.Written,
                false
            );
            AddAndSaveChanges(timeRegistration);
        }

        private class InitState
        {
            public List<SubAreaHourSquare> SubAreaHourSquares { get; set; } = new List<SubAreaHourSquare>();
            public List<Tracking> Trackings { get; set; } = new List<Tracking>();
            public List<TimeRegistration> TimeRegistrations { get; set; } = new List<TimeRegistration>();
            public double TotalHoursFromTracking { get; set; } = 0;
            public double TotalHoursNotFromTracking { get; set; } = 0;
        }
        #endregion Helpers
    }
}
