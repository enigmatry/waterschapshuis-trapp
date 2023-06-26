using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Rest;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.CatchAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Rayons;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.TimeRegistrations
{
    [Category("integration")]
    public class TimeRegistrationsControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private const string ApiEndpointDateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
        private DateTimeOffset _createdOn;
        private DateTimeOffset _createdOnMoreThanFiveYearsAgo;
        private DateTimeOffset _createdOnOneDayBeforeFiveYears;

        private TimeRegistration _timeRegistrationInDb = null!;
        private TimeRegistration _timeRegistrationInDifferentOrganization = null!;

        private TrappingType _trappingType = null!;
        private Catch _catchInDb = null!;
        private CatchType _catchType = null!;

        private Rayon _rayon = null!;
        private CatchArea _catchArea = null!;
        private SubArea _subArea = null!;
        private HourSquare _hourSquare = null!;
        private SubAreaHourSquare _subAreaHourSquare = null!;

        [SetUp]
        public void SetUp()
        {
            _createdOn = Resolve<ITimeProvider>().Now;
            _createdOnMoreThanFiveYearsAgo = _createdOn.AddYears(-5);
            _createdOnOneDayBeforeFiveYears = _createdOn.AddYears(-5).AddDays(-1);


            _trappingType = QueryDb<TrappingType>().Single(x => x.Id == TrappingType.MuskusratId);
            _catchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.BeverratMoerOudId);

            Organization organization = QueryDb<Organization>().Single(x => x.Id == TestPrincipal.TestUserOrganizationId);
            _rayon = new RayonBuilder().WithOrganization(organization).WithName("Test rayon");
            _catchArea = new CatchAreaBuilder().WithRayon(_rayon);
            _subArea = new SubAreaBuilder().WithCatchAreaId(_catchArea);
            _hourSquare = new HourSquareBuilder();
            _subAreaHourSquare = new SubAreaHourSquareBuilder().WithSubArea(_subArea).WithHourSquare(_hourSquare);

            Organization differentOrganization = new OrganizationBuilder().WithName("Organization 2").WithShortName("Org 2");
            Rayon differentRayon = new RayonBuilder().WithOrganization(differentOrganization).WithName("Test rayon 2");
            CatchArea differentCatchArea = new CatchAreaBuilder().WithRayon(differentRayon);
            SubArea differentSubArea = new SubAreaBuilder().WithCatchAreaId(differentCatchArea);
            HourSquare differentHourSquare = new HourSquareBuilder();
            SubAreaHourSquare differentSubAreaHourSquare = new SubAreaHourSquareBuilder().WithSubArea(differentSubArea).WithHourSquare(differentHourSquare);

            _timeRegistrationInDb =
                new TimeRegistrationBuilder()
                    .WithUserId(TestPrincipal.TestUserId)
                    .WithTrappingTypeId(_trappingType.Id)
                    .WithSubAreaHourSquareId(_subAreaHourSquare.Id)
                    .WithDate(_createdOn)
                    .WithHours(3.5);

            _timeRegistrationInDifferentOrganization =
                new TimeRegistrationBuilder()
                    .WithUserId(TestPrincipal.TestUserId)
                    .WithTrappingTypeId(_trappingType.Id)
                    .WithSubAreaHourSquareId(differentSubAreaHourSquare.Id)
                    .WithDate(_createdOn)
                    .WithHours(2);

            TimeRegistration oldTimeRegistrationInDb =
                new TimeRegistrationBuilder()
                    .WithUserId(TestPrincipal.TestUserId)
                    .WithTrappingTypeId(_trappingType.Id)
                    .WithSubAreaHourSquareId(_subAreaHourSquare.Id)
                    .WithDate(_createdOnMoreThanFiveYearsAgo)
                    .WithHours(5);

            TimeRegistration olderTimeRegistrationInDb =
                new TimeRegistrationBuilder()
                    .WithUserId(TestPrincipal.TestUserId)
                    .WithTrappingTypeId(_trappingType.Id)
                    .WithSubAreaHourSquareId(_subAreaHourSquare.Id)
                    .WithDate(_createdOnOneDayBeforeFiveYears)
                    .WithHours(7);

            _catchInDb = new CatchBuilder()
                .WithId(Guid.NewGuid())
                .WithRecordedOn(_createdOn)
                .WithCatchType(_catchType)
                .WithNumberOfCatches(5)
                .WithStatus(CatchStatus.Written)
                .WithTrap(new TrapBuilder());

            AddAndSaveChanges(
                _subAreaHourSquare,
                differentSubAreaHourSquare, 
                _timeRegistrationInDb, 
                _timeRegistrationInDifferentOrganization, 
                oldTimeRegistrationInDb, 
                olderTimeRegistrationInDb, 
                _catchInDb);
        }

        #region GET
        [Test]
        public async Task GivenExistingDateRange_Get_ReturnsTimeRegistration()
        {
            var queryStrings = CreateFormatedQueryStringDates(_createdOn, _createdOn.AddDays(1));
            string url = QueryHelpers.AddQueryString("timeRegistrations", queryStrings);

            var response = await Client.GetAsync<GetTimeRegistrationsOfWeek.Response>(url);

            response.Should().NotBeNull();
            response.DaysOfWeek.Count().Should().Be(1);

            var obtainedTimeRegistration = response.DaysOfWeek.FirstOrDefault()?
                                                   .TimeRegistrations.FirstOrDefault(x=> x.Id == _timeRegistrationInDb.Id);
            obtainedTimeRegistration?.Date.Should().Be(_timeRegistrationInDb.Date);
            obtainedTimeRegistration?.TrappingType.Id.Should().Be(_timeRegistrationInDb.TrappingTypeId);
            obtainedTimeRegistration?.Hours.Should().Be(3);
            obtainedTimeRegistration?.Minutes.Should().Be(30);
        }

        [Test]
        public async Task GivenNoneExistingDateRange_Get_ReturnsNoTimeRegistration()
        {
            var queryStrings = CreateFormatedQueryStringDates(_createdOn.AddDays(-2), _createdOn.AddDays(-1));
            string url = QueryHelpers.AddQueryString("timeRegistrations", queryStrings);

            var response = await Client.GetAsync<GetTimeRegistrationsOfWeek.Response>(url);

            response.Should().NotBeNull();
            response.DaysOfWeek.Count().Should().Be(0);
        }

        [Test]
        public async Task GivenExistingDateRange_GetUsersWhoHaveRegisteredTimePerRayon_ReturnsUsersWithRegisteredTimeInRayon()
        {
            var queryStrings = CreateFormatedQueryStringDates(_createdOn, _createdOn.AddDays(1));
            string url = QueryHelpers.AddQueryString("timeRegistrations/users-per-rayon", queryStrings);

            var response = await Client.GetAsync<GetTimeRegistrationPerRayon.Response>(url);

            response.Should().NotBeNull();
            response.UsersWithRegisteredTimePerRayon.Select(x => x.Users).Count().Should().BeGreaterThan(0);
            response.UsersWithRegisteredTimePerRayon.FirstOrDefault()?.RayonName.Should().Be("Test rayon");
            response.UsersWithRegisteredTimePerRayon.FirstOrDefault()?.Users.FirstOrDefault()?.Name.Should().Be(TestPrincipal.TestUserName);
        }

        [Test]
        public async Task GivenExistingDateRange_GetUsersWhoHaveRegisteredTimePerRayon_ReturnsAnonymisedUsersWithRegisteredTimeInRayon()
        {
            var queryString = CreateFormatedQueryStringDates(_createdOnMoreThanFiveYearsAgo.AddDays(-7), _createdOnMoreThanFiveYearsAgo);
            string url = QueryHelpers.AddQueryString("timeRegistrations/users-per-rayon", queryString);

            var response = await Client.GetAsync<GetTimeRegistrationPerRayon.Response>(url);

            response.Should().NotBeNull();
            response.UsersWithRegisteredTimePerRayon.Select(x => x.Users).Count().Should().BeGreaterThan(0);
            response.UsersWithRegisteredTimePerRayon.FirstOrDefault()?.RayonName.Should().Be("Test rayon");
            response.UsersWithRegisteredTimePerRayon.FirstOrDefault()?.Users.FirstOrDefault()?.Name.Should().Be(User.AnonymizedName);
        }

        [Test]
        public async Task GivenExistingDateRange_GetUsersWhoHaveRegisteredTimePerRayon_ReturnsNonAnonymisedUsers()
        {
            var queryString = CreateFormatedQueryStringDates(_createdOnOneDayBeforeFiveYears.AddDays(-7), _createdOnOneDayBeforeFiveYears);
            string url = QueryHelpers.AddQueryString("timeRegistrations/users-per-rayon", queryString);

            var response = await Client.GetAsync<GetTimeRegistrationPerRayon.Response>(url);

            response.Should().NotBeNull();
            response.UsersWithRegisteredTimePerRayon.Select(x => x.Users).Count().Should().BeGreaterThan(0);
            response.UsersWithRegisteredTimePerRayon.FirstOrDefault()?.RayonName.Should().Be("Test rayon");
            response.UsersWithRegisteredTimePerRayon.FirstOrDefault()?.Users.FirstOrDefault()?.Name.Should().Be(TestPrincipal.TestUserName);
        }

        #endregion

        #region POST
        [Test]
        public async Task Update()
        {
            var command = CreateTimeRegistrationUpdateCommand(4, TimeRegistrationStatus.Closed);

            GetTimeRegistrationsOfWeek.Response timeRegistrations =
                await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrationsOfWeek.Response>("timeRegistrations", command);

            timeRegistrations.Should().NotBeNull();

            var updatedTimeRegistration = timeRegistrations.DaysOfWeek.FirstOrDefault().TimeRegistrations.FirstOrDefault();

            updatedTimeRegistration.Hours.Should().Be(4);
            updatedTimeRegistration.Status.Should().Be(TimeRegistrationStatus.Closed);
        }

        [Test]
        public void Create_DateRangeMatchingToDaysOfWeekDates()
        {
            var command = CreateTimeRegistrationUpdateCommand(4, TimeRegistrationStatus.Written);
            command.StartDate = _createdOn.AddDays(10);
            command.StartDate = _createdOn.AddDays(20);

            Assert.ThrowsAsync<HttpOperationException>(async () =>
                await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrations.Response>("timeregistrations", command));
        }

        [Test]
        public async Task ApprovingTimeRegistrationStatus_PostForUser_DoesNotRemoveUsersTimeRegistrationsMadeForOtherOrganizations()
        {
            var command = CreateTimeRegistrationUpdateCommand(6, TimeRegistrationStatus.Completed);

            string url = QueryHelpers.AddQueryString("timeRegistrations/management", new Dictionary<string, string>
            {{"userId",  TestPrincipal.TestUserId.ToString()}, {"rayonId", _rayon.Id.ToString()}});
            GetTimeRegistrationsOfWeek.Response timeRegistrations =
                await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrationsOfWeek.Response>(url, command);
            timeRegistrations.Should().NotBeNull();

            var remainingItemsInDbAfterUpdate = QueryDbSkipCache<TimeRegistration>()
                .QueryByUser(TestPrincipal.TestUserId)
                .QueryByDateRangeExclusiveEnd(command.StartDate, command.EndDate).ToList();

            remainingItemsInDbAfterUpdate.Count().Should().Be(2);

            var updatedTimeRegistration = remainingItemsInDbAfterUpdate.FirstOrDefault(x => x.Id == _timeRegistrationInDb.Id);
            updatedTimeRegistration.Hours.Should().Be(6);
            updatedTimeRegistration.Status.Should().Be(TimeRegistrationStatus.Completed);
            updatedTimeRegistration.IsCreatedFromTrackings.Should().BeFalse();

            var timeRegistrationFromOtherOrganization = remainingItemsInDbAfterUpdate.FirstOrDefault(x => x.Id == _timeRegistrationInDifferentOrganization.Id);
            timeRegistrationFromOtherOrganization.Hours.Should().Be(2);
            timeRegistrationFromOtherOrganization.Status.Should().Be(TimeRegistrationStatus.Written);
        }

        [Test]
        public async Task Update_IsCreatedFromTracking()
        {
            var timeRegistrationInDb = QueryDb<TimeRegistration>().Single(x => x.Id == _timeRegistrationInDb.Id);
            timeRegistrationInDb.WithIsCreatedFromTracking(true);
            SaveChanges();

            timeRegistrationInDb = QueryDb<TimeRegistration>().Single(x => x.Id == _timeRegistrationInDb.Id);
            timeRegistrationInDb.IsCreatedFromTrackings.Should().BeTrue();

            var command = CreateTimeRegistrationUpdateCommand(10, TimeRegistrationStatus.Closed);

            var result = await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrationsOfWeek.Response>("timeRegistrations", command);
            SaveChanges();

            var updatedTimeRegistrationInDb = QueryDbSkipCache<TimeRegistration>().Single(x => x.Id == _timeRegistrationInDb.Id);
            updatedTimeRegistrationInDb.Hours.Should().Be(10);
            updatedTimeRegistrationInDb.IsCreatedFromTrackings.Should().BeFalse();
        }
        #endregion

        private static string FormatDateForApiEndpoint(DateTimeOffset date)
        {
            return date.ToString(ApiEndpointDateFormat);
        }

        private static Dictionary<string,string> CreateFormatedQueryStringDates(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return new Dictionary<string, string>
            {
                {"startDate", FormatDateForApiEndpoint(startDate)},
                {"endDate",  FormatDateForApiEndpoint(endDate)}
            };
        }

        private TimeRegistrationsEdit.Command CreateTimeRegistrationUpdateCommand(int hours, TimeRegistrationStatus status)
        {
            return new TimeRegistrationsEdit.Command
            {
                DaysOfWeek = new List<TimeRegistrationsEdit.Command.TimeRegistrationsOfDate>(
                    new List<TimeRegistrationsEdit.Command.TimeRegistrationsOfDate>
                    {
                        new TimeRegistrationsEdit.Command.TimeRegistrationsOfDate
                        {
                            Date = _createdOn,
                            Items = new List<TimeRegistrationsEdit.Command.Item>
                            {
                                new TimeRegistrationsEdit.Command.Item
                                {
                                    Id = _timeRegistrationInDb.Id,
                                    Hours = hours,
                                    Status = status,
                                    SubAreaId = _subArea.Id,
                                    HourSquareId = _hourSquare.Id,
                                    TrappingTypeId = _trappingType.Id
                                }
                            }
                        }
                    }),
                StartDate = _createdOn.MondayDateInWeekOfDate(),
                EndDate = _createdOn.MondayDateInWeekOfDate().AddDays(7)
            };
        }
    }
}
