using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.TimeRegistrations
{
    [Category("integration")]
    public class TimeRegistrationsControllerFixture : MobileApiIntegrationFixtureBase
    {
        private ICurrentUserProvider _currentUserProvider = null!;
        private ITimeProvider _timeProvider = null!;

        private SubAreaHourSquare _subAreaHourSquareOne = null!;
        private SubAreaHourSquare _subAreaHourSquareTwo = null!;

        private TimeRegistration _timeRegistrationOne = null!;
        private TimeRegistration _timeRegistrationTwo = null!;
        private TimeRegistration _timeRegistrationThree = null!;

        private DateTimeOffset _yesterday;
        private DateTimeOffset _today;
        private Guid _userId;

        [SetUp]
        public void SetUp()
        {
            _currentUserProvider = Resolve<ICurrentUserProvider>();
            _timeProvider = Resolve<ITimeProvider>();

            _subAreaHourSquareOne = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(4.899432, 52.379190);

            _subAreaHourSquareTwo = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(5.324237, 50.32467);

            _today = _timeProvider.Now;
            _yesterday = _today.AddDays(-1);
            _userId = _currentUserProvider.UserId.GetValueOrDefault();

            _timeRegistrationOne =
                new TimeRegistrationBuilder()
                    .WithSubAreaHourSquareId(_subAreaHourSquareOne.Id)
                    .WithTrappingTypeId(TrappingType.MuskusratId)
                    .WithDate(_yesterday)
                    .WithUserId(_userId)
                    .WithHours(3.75);

            _timeRegistrationTwo =
                new TimeRegistrationBuilder()
                    .WithSubAreaHourSquareId(_subAreaHourSquareTwo.Id)
                    .WithTrappingTypeId(TrappingType.BeverratId)
                    .WithDate(_yesterday)
                    .WithUserId(_userId)
                    .WithHours(2.5);

            _timeRegistrationThree =
                new TimeRegistrationBuilder()
                    .WithSubAreaHourSquareId(_subAreaHourSquareOne.Id)
                    .WithTrappingTypeId(TrappingType.MuskusratId)
                    .WithDate(_today)
                    .WithUserId(_userId)
                    .WithHours(4);

            AddAndSaveChanges(_subAreaHourSquareOne, _subAreaHourSquareTwo, _timeRegistrationOne, _timeRegistrationTwo, _timeRegistrationThree);
        }

        #region GET
        [Test]
        public async Task GivenValidDateGetTimeRegistration_ExistSingle()
        {
            string url = QueryHelpers.AddQueryString("timeregistrations", "date", _today.ToString("yyyy-MM-dd"));
            var timeRegistrations = await Client.GetAsync<GetTimeRegistrations.Response>(url);

            timeRegistrations.Should().NotBeNull();

            timeRegistrations.Date.Date.Should().Be(_today.Date);
            timeRegistrations.Items.Count().Should().Be(1);

            timeRegistrations.Items.First().Hours.Should().Be(4);
            timeRegistrations.Items.First().Minutes.Should().Be(0);
        }

        [Test]
        public async Task GivenValidDateGetTimeRegistration_ExistMultiple()
        {
            string url = QueryHelpers.AddQueryString("timeregistrations", "date", _yesterday.ToString("yyyy-MM-dd"));
            var timeRegistrations = await Client.GetAsync<GetTimeRegistrations.Response>(url);

            timeRegistrations.Should().NotBeNull();

            timeRegistrations.Date.Date.Should().Be(_yesterday.Date);
            timeRegistrations.Items.Count().Should().Be(2);

            timeRegistrations.Items.First().Hours.Should().Be(3);
            timeRegistrations.Items.First().Minutes.Should().Be(45);

            timeRegistrations.Items.Last().Hours.Should().Be(2);
            timeRegistrations.Items.Last().Minutes.Should().Be(30);
        }

        [Test]
        public async Task GivenValidDateGetTimeRegistration_DoesNotExist()
        {
            var date = _yesterday.AddDays(-1);
            string url = QueryHelpers.AddQueryString("timeregistrations", "date", date.ToString("yyyy-MM-dd"));
            var timeRegistrations = await Client.GetAsync<GetTimeRegistrations.Response>(url);

            timeRegistrations.Should().NotBeNull();

            timeRegistrations.Date.Date.Should().Be(date.Date);
            timeRegistrations.Items.Count().Should().Be(0);
        }
        #endregion GET

        #region POST
        [Test]
        public async Task GivenValidDateGetTimeRegistration_EditExistingSingle()
        {
            var command = new TimeRegistrationsEdit.Command();
            command.DaysOfWeek = command.DaysOfWeek.Append(TimeRegistrationsEdit.Command.TimeRegistrationsOfDate
                .Create(
                    _today,
                    new[]
                    {
                        TimeRegistrationsEdit.Command.Item.Create(
                            _timeRegistrationThree.Id,
                            _today,
                            _subAreaHourSquareTwo.SubAreaId,
                            _subAreaHourSquareTwo.HourSquareId,
                            TrappingType.BeverratId,
                            2,
                            30,
                            TimeRegistrationStatus.Written)
                    }));
            command.StartDate = _today;
            command.EndDate = _today.AddDays(1);

            var result = 
                await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrations.Response>("timeregistrations", command);

            result.Should().NotBeNull();

            result.Items.Should().NotBeEmpty();
            result.Items.Count().Should().Be(1);
            result.Items.First().Hours.Should().Be(2);
            result.Items.First().Minutes.Should().Be(30);
            result.Items.First().TrappingType.Id.Should().Be(TrappingType.BeverratId);

            var timeRegistrationFromDb = QueryDbSkipCache<TimeRegistration>().Single(x => x.Id == _timeRegistrationThree.Id);
            timeRegistrationFromDb.IsCreatedFromTrackings.Should().BeFalse();
        }

        [Test]
        public void Create_CurrentDateValidation()
        {
            var command = new TimeRegistrationsEdit.Command();
            command.DaysOfWeek = command.DaysOfWeek.Append(TimeRegistrationsEdit.Command.TimeRegistrationsOfDate
                .Create(
                    _today,
                    new[]
                    {
                        TimeRegistrationsEdit.Command.Item.Create(
                            _timeRegistrationThree.Id,
                            _today,
                            _subAreaHourSquareTwo.SubAreaId,
                            _subAreaHourSquareTwo.HourSquareId,
                            TrappingType.BeverratId,
                            2,
                            30,
                            TimeRegistrationStatus.Written)
                    }));
            command.StartDate = _today.MondayDateInWeekOfDate();
            command.EndDate = _today.MondayDateInWeekOfDate().AddDays(7);

            Assert.ThrowsAsync<HttpOperationException>(async() => 
                await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrations.Response>("timeregistrations", command));
        }

        [Test]
        public void Create_DateRangeMatchingToDaysOfWeekDates()
        {
            var command = new TimeRegistrationsEdit.Command();
            command.DaysOfWeek = command.DaysOfWeek.Append(TimeRegistrationsEdit.Command.TimeRegistrationsOfDate
                .Create(
                    _today,
                    new[]
                    {
                        TimeRegistrationsEdit.Command.Item.Create(
                            _timeRegistrationThree.Id,
                            _today,
                            _subAreaHourSquareTwo.SubAreaId,
                            _subAreaHourSquareTwo.HourSquareId,
                            TrappingType.BeverratId,
                            2,
                            30,
                            TimeRegistrationStatus.Written)
                    }));
            command.StartDate = _today.AddDays(1);
            command.EndDate = _today.AddDays(2);

            Assert.ThrowsAsync<HttpOperationException>(async () =>
                await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrations.Response>("timeregistrations", command));
        }

        [Test]
        public async Task Create()
        {
            var command = new TimeRegistrationsEdit.Command();
            var itemToCreate = TimeRegistrationsEdit.Command.Item.Create(
                null,
                _today,
                _subAreaHourSquareTwo.SubAreaId,
                _subAreaHourSquareTwo.HourSquareId,
                TrappingType.BeverratId,
                6,
                47,
                TimeRegistrationStatus.Written);
            command.DaysOfWeek = command.DaysOfWeek
                .Append(TimeRegistrationsEdit.Command.TimeRegistrationsOfDate.Create(_today, new[] { itemToCreate }));
            command.StartDate = _today;
            command.EndDate = _today.AddDays(1);

            var result = await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrations.Response>("timeregistrations", command);
            SaveChanges();

            result.Should().NotBeNull();
            result.Items.Should().NotBeEmpty();

            var newItemResult = result.Items.SingleOrDefault(x => x.Hours == itemToCreate.Hours && x.Minutes == itemToCreate.Minutes);
            newItemResult.Should().NotBeNull();

            var timeRegistrationFromDb = QueryDbSkipCache<TimeRegistration>().Single(x => x.Id == newItemResult.Id);
            timeRegistrationFromDb.Should().NotBeNull();
            timeRegistrationFromDb.IsCreatedFromTrackings.Should().BeFalse();
        }

        [Test]
        public async Task Update_IsCreatedFromTracking()
        {
            var timeRegistrationInDb = QueryDb<TimeRegistration>().Single(x => x.Id == _timeRegistrationThree.Id);
            timeRegistrationInDb.WithIsCreatedFromTracking(true);
            SaveChanges();

            timeRegistrationInDb = QueryDb<TimeRegistration>().Single(x => x.Id == _timeRegistrationThree.Id);
            timeRegistrationInDb.IsCreatedFromTrackings.Should().BeTrue();

            var command = new TimeRegistrationsEdit.Command();
            command.DaysOfWeek = command.DaysOfWeek.Append(TimeRegistrationsEdit.Command.TimeRegistrationsOfDate
                .Create(
                    _today,
                    new[]
                    {
                        TimeRegistrationsEdit.Command.Item.Create(
                            _timeRegistrationThree.Id,
                            _today,
                            _subAreaHourSquareTwo.SubAreaId,
                            _subAreaHourSquareTwo.HourSquareId,
                            TrappingType.BeverratId,
                            10,
                            00,
                            TimeRegistrationStatus.Written)
                    }));
            command.StartDate = _today;
            command.EndDate = _today.AddDays(1);

            var result = await Client.PostAsync<TimeRegistrationsEdit.Command, GetTimeRegistrationsOfWeek.Response>("timeRegistrations", command);
            SaveChanges();

            var updatedTimeRegistrationInDb = QueryDbSkipCache<TimeRegistration>().Single(x => x.Id == _timeRegistrationThree.Id);
            updatedTimeRegistrationInDb.Hours.Should().Be(10);
            updatedTimeRegistrationInDb.IsCreatedFromTrackings.Should().BeFalse();
        }
        #endregion POST
    }
}
