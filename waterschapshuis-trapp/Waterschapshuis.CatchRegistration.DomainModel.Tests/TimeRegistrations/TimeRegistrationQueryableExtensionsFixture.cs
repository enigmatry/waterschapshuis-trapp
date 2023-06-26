using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.TimeRegistrations
{
    [Category("unit")]
    public class TimeRegistrationQueryableExtensionsFixture
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInvalidQuery_ByDay()
        {
            var userId = Guid.NewGuid();
            var trappingTypeId = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var today = DateTimeOffset.Now;
            var yesterday = today.AddDays(-1);

            var timeRegistrations =
                CreateTimeRegistrationsByDate(userId, trappingTypeId, subAreaHourSquareId, today, yesterday, false);

            Action todayTimeReg = () =>
                timeRegistrations.ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeId, today);

            todayTimeReg.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Sequence contains more than one matching element");

            Action yesterdayTimeReg = () =>
                timeRegistrations.ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeId,
                    yesterday);

            yesterdayTimeReg.Should().NotThrow();
        }

        [Test]
        public void TestInvalidQuery_ByUser()
        {
            var userIdOne = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();
            var trappingTypeId = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var date = DateTimeOffset.Now;

            var timeRegistrations =
                CreateTimeRegistrationsByUser(userIdOne, userIdTwo, trappingTypeId, subAreaHourSquareId, date, false);

            Action todayTimeReg = () =>
                timeRegistrations.ExistingTimeRegistrationEntryEntity(userIdOne, subAreaHourSquareId, trappingTypeId, date);

            todayTimeReg.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Sequence contains more than one matching element");

            Action yesterdayTimeReg = () =>
                timeRegistrations.ExistingTimeRegistrationEntryEntity(userIdTwo, subAreaHourSquareId, trappingTypeId, date);

            yesterdayTimeReg.Should().NotThrow();
        }

        [Test]
        public void TestInvalidQuery_ByTrappingType()
        {
            var userIdOne = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();
            var trappingTypeIdOne = Guid.NewGuid();
            var trappingTypeIdTwo = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var dateOne = DateTimeOffset.Now;
            var dateTwo = DateTimeOffset.Now.AddDays(-1);

            var timeRegistrationsOne =
                CreateTimeRegistrationsByTrappingType(userIdOne, trappingTypeIdOne, trappingTypeIdTwo, subAreaHourSquareId, dateOne, false);
            var timeRegistrationsTwo =
                CreateTimeRegistrationsByTrappingType(userIdTwo, trappingTypeIdOne, trappingTypeIdTwo, subAreaHourSquareId, dateOne, false);
            var timeRegistrationsThree =
                CreateTimeRegistrationsByTrappingType(userIdTwo, trappingTypeIdOne, trappingTypeIdTwo, subAreaHourSquareId, dateTwo, true);
            var timeRegistrationsFour =
                CreateTimeRegistrationsByTrappingType(userIdTwo, trappingTypeIdOne, trappingTypeIdTwo, subAreaHourSquareId, dateTwo, true);

            var timeRegistrations = 
                timeRegistrationsOne
                    .Union(timeRegistrationsTwo)
                    .Union(timeRegistrationsThree)
                    .Union(timeRegistrationsFour);

            var userOneTimeRegForDayOne =
                timeRegistrations.QueryByUser(userIdOne).QueryByDateOfOneDayRange(dateOne);

            var userTwoTimeRegForDayOne =
                timeRegistrations.QueryByUser(userIdTwo).QueryByDateOfOneDayRange(dateOne);

            var userTwoTimeRegForDayTwo =
                timeRegistrations.QueryByUser(userIdTwo).QueryByDateOfOneDayRange(dateTwo);

            Action trappingOneUserOneDayOneTimeReg = () =>
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdOne, subAreaHourSquareId, trappingTypeIdOne, dateOne);

            Action trappingTwoUserOneDayOneTimeReg = () =>
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdOne, subAreaHourSquareId, trappingTypeIdTwo, dateOne);

            Action trappingOneUserTwoDayOneTimeReg = () =>
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdTwo, subAreaHourSquareId, trappingTypeIdOne, dateOne);

            Action trappingTwoUserTwoDayOneTimeReg = () =>
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdTwo, subAreaHourSquareId, trappingTypeIdTwo, dateOne);

            Action trappingOneUserTwoDayTwoTimeReg = () =>
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdTwo, subAreaHourSquareId, trappingTypeIdOne, dateTwo);

            Action trappingTwoUserTwoDayTwoTimeReg = () =>
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdTwo, subAreaHourSquareId, trappingTypeIdTwo, dateTwo);

            userOneTimeRegForDayOne.Should().NotBeEmpty();
            userOneTimeRegForDayOne.Count().Should().Be(2);

            userTwoTimeRegForDayOne.Should().NotBeEmpty();
            userTwoTimeRegForDayOne.Count().Should().Be(2);

            userTwoTimeRegForDayTwo.Should().NotBeEmpty();
            userTwoTimeRegForDayTwo.Count().Should().Be(4);

            trappingOneUserOneDayOneTimeReg.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Sequence contains more than one matching element");

            trappingTwoUserOneDayOneTimeReg.Should().NotThrow();

            trappingOneUserTwoDayOneTimeReg.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Sequence contains more than one matching element");

            trappingTwoUserTwoDayOneTimeReg.Should().NotThrow();

            trappingOneUserTwoDayTwoTimeReg.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Sequence contains more than one matching element");

            trappingTwoUserTwoDayTwoTimeReg.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Sequence contains more than one matching element");
        }

        [Test]
        public void TestValidQuery_ByDay()
        {
            var userId = Guid.NewGuid();
            var trappingTypeId = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var today = DateTimeOffset.Now;
            var yesterday = today.AddDays(-1);
            var tomorrow = today.AddDays(1);

            var timeRegistrations =
                CreateTimeRegistrationsByDate(userId, trappingTypeId, subAreaHourSquareId, today, yesterday, true);

            var todayTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeId, today);

            var yesterdayTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeId, yesterday);

            var tomorrowTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeId, tomorrow);

            todayTimeReg.Should().NotBeNull();
            todayTimeReg?.Date.Should().Be(today.Date);
            todayTimeReg?.UserId.Should().Be(userId);
            todayTimeReg?.TrappingTypeId.Should().Be(trappingTypeId);
            todayTimeReg?.SubAreaHourSquareId.Should().Be(subAreaHourSquareId);

            yesterdayTimeReg.Should().NotBeNull();
            yesterdayTimeReg?.Date.Should().Be(yesterday.Date);
            yesterdayTimeReg?.UserId.Should().Be(userId);
            yesterdayTimeReg?.TrappingTypeId.Should().Be(trappingTypeId);
            yesterdayTimeReg?.SubAreaHourSquareId.Should().Be(subAreaHourSquareId);

            tomorrowTimeReg.Should().BeNull();
        }

        [Test]
        public void TestValidQuery_ByDateRangeExclusiveEnd()
        {
            var userId = Guid.NewGuid();
            var trappingTypeId = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var today = DateTimeOffset.Now;
            var yesterday = today.AddDays(-1);
            var dayBeforeYesterday = yesterday.AddDays(-1);
            var tomorrow = today.AddDays(1);

            var timeRegistrations =
                CreateTimeRegistrationsByDate(userId, trappingTypeId, subAreaHourSquareId, today, yesterday, true);

            var yesterdaysTimeRegistrations = timeRegistrations.QueryByDateRangeExclusiveEnd(yesterday,today);
            var dayBeforeYesterdayTimeRegistrations = timeRegistrations.QueryByDateRangeExclusiveEnd(dayBeforeYesterday, yesterday);
            var yesterdaysAndTodaysTimeRegistrations = timeRegistrations.QueryByDateRangeExclusiveEnd(yesterday, tomorrow);
            var todaysTimeRegistrations = timeRegistrations.QueryByDateRangeExclusiveEnd(today, tomorrow);

            yesterdaysTimeRegistrations.Should().NotBeNull();
            yesterdaysTimeRegistrations.Count().Should().Be(1);

            dayBeforeYesterdayTimeRegistrations.Should().NotBeNull();
            dayBeforeYesterdayTimeRegistrations.Count().Should().Be(0);

            yesterdaysAndTodaysTimeRegistrations.Should().NotBeNull();
            yesterdaysAndTodaysTimeRegistrations.Count().Should().Be(2);

            todaysTimeRegistrations.Should().NotBeNull();
            todaysTimeRegistrations.Count().Should().Be(1);
        }

        [Test]
        public void TestValidQuery_QueryByToDateAndStatuses()
        {
            var userId = Guid.NewGuid();
            var trappingTypeId = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var today = DateTimeOffset.Now;
            var yesterday = today.AddDays(-1);
            var dayBeforeYesterday = yesterday.AddDays(-1);
            var tomorrow = today.AddDays(1);

            var timeRegistrations =
                CreateTimeRegistrationsByDate(userId, trappingTypeId, subAreaHourSquareId, today, yesterday, true);

            var yesterdaysTimeRegistrations = timeRegistrations
                .QueryToDateAndStatuses(today,new List<TimeRegistrationStatus>
                {
                    TimeRegistrationStatus.Written
                });
            var yesterdaysTimeRegistrationsInClosedStatus = timeRegistrations
                .QueryToDateAndStatuses(today, new List<TimeRegistrationStatus>
                {
                    TimeRegistrationStatus.Closed
                });


            var beforeYesterdayTimeRegistrations = timeRegistrations
                .QueryToDateAndStatuses(yesterday, new List<TimeRegistrationStatus>
                {
                    TimeRegistrationStatus.Written
                });


            yesterdaysTimeRegistrations.Should().NotBeNull();
            yesterdaysTimeRegistrations.Count().Should().Be(1);
            yesterdaysTimeRegistrationsInClosedStatus.Count().Should().Be(0);
            beforeYesterdayTimeRegistrations.Count().Should().Be(0);
        }


        [Test]
        public void TestValidQuery_ByUser()
        {
            var userIdOne = Guid.NewGuid();
            var userIdTwo = Guid.NewGuid();
            var userIdThree = Guid.NewGuid();
            var trappingTypeId = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var date = DateTimeOffset.Now;

            var timeRegistrations =
                CreateTimeRegistrationsByUser(userIdOne, userIdTwo, trappingTypeId, subAreaHourSquareId, date, true);

            var userOneTimeRegForDay =
                timeRegistrations.QueryByUser(userIdOne).QueryByDateOfOneDayRange(date);

            var userTwoTimeRegForDay =
                timeRegistrations.QueryByUser(userIdTwo).QueryByDateOfOneDayRange(date);

            var userThreeTimeRegForDay =
                timeRegistrations.QueryByUser(userIdThree).QueryByDateOfOneDayRange(date);

            userOneTimeRegForDay.Should().NotBeEmpty();
            userOneTimeRegForDay.Count().Should().Be(1);

            userTwoTimeRegForDay.Should().NotBeEmpty();
            userTwoTimeRegForDay.Count().Should().Be(1);

            userThreeTimeRegForDay.Should().BeEmpty();

            var userOneTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdOne, subAreaHourSquareId, trappingTypeId, date);

            var userTwoTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdTwo, subAreaHourSquareId, trappingTypeId, date);

            var userThreeTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userIdThree, subAreaHourSquareId, trappingTypeId, date);

            userOneTimeReg.Should().NotBeNull();
            userOneTimeReg?.Date.Should().Be(date.Date);
            userOneTimeReg?.UserId.Should().Be(userIdOne);
            userOneTimeReg?.TrappingTypeId.Should().Be(trappingTypeId);
            userOneTimeReg?.SubAreaHourSquareId.Should().Be(subAreaHourSquareId);

            userTwoTimeReg.Should().NotBeNull();
            userTwoTimeReg?.Date.Should().Be(date.Date);
            userTwoTimeReg?.UserId.Should().Be(userIdTwo);
            userTwoTimeReg?.TrappingTypeId.Should().Be(trappingTypeId);
            userTwoTimeReg?.SubAreaHourSquareId.Should().Be(subAreaHourSquareId);

            userThreeTimeReg.Should().BeNull();
        }

        [Test]
        public void TestValidQuery_ByTrappingType()
        {
            var userId = Guid.NewGuid();
            var trappingTypeIdOne = Guid.NewGuid();
            var trappingTypeIdTwo = Guid.NewGuid();
            var trappingTypeIdThree = Guid.NewGuid();
            var subAreaHourSquareId = Guid.NewGuid();
            var date = DateTimeOffset.Now;

            var timeRegistrations =
                CreateTimeRegistrationsByTrappingType(userId, trappingTypeIdOne, trappingTypeIdTwo, subAreaHourSquareId, date, true);

            var userTimeRegForDay =
                timeRegistrations.QueryByUser(userId).QueryByDateOfOneDayRange(date);

            var trappingOneTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeIdOne, date);

            var trappingTwoTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeIdTwo, date);

            var trappingThreeTimeReg =
                timeRegistrations
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeIdThree, date);

            userTimeRegForDay.Should().NotBeEmpty();
            userTimeRegForDay.Count().Should().Be(2);

            trappingOneTimeReg.Should().NotBeNull();
            trappingOneTimeReg?.Date.Should().Be(date.Date);
            trappingOneTimeReg?.UserId.Should().Be(userId);
            trappingOneTimeReg?.TrappingTypeId.Should().Be(trappingTypeIdOne);
            trappingOneTimeReg?.SubAreaHourSquareId.Should().Be(subAreaHourSquareId);

            trappingTwoTimeReg.Should().NotBeNull();
            trappingTwoTimeReg?.Date.Should().Be(date.Date);
            trappingTwoTimeReg?.UserId.Should().Be(userId);
            trappingTwoTimeReg?.TrappingTypeId.Should().Be(trappingTypeIdTwo);
            trappingTwoTimeReg?.SubAreaHourSquareId.Should().Be(subAreaHourSquareId);

            trappingThreeTimeReg.Should().BeNull();
        }

        private TimeRegistration CreateTimeRegistration(
            Guid userId,
            Guid trappingTypeId,
            Guid subAreaHourSquareId,
            DateTimeOffset date,
            double hours)
        {
            TimeRegistration timeRegistration =
                new TimeRegistrationBuilder()
                    .WithUserId(userId)
                    .WithTrappingTypeId(trappingTypeId)
                    .WithSubAreaHourSquareId(subAreaHourSquareId)
                    .WithDate(date)
                    .WithHours(hours);

            return timeRegistration;
        }

        private IQueryable<TimeRegistration> CreateTimeRegistrationsByDate(
            Guid userId,
            Guid trappingTypeId,
            Guid subAreaHourSquareId,
            DateTimeOffset today,
            DateTimeOffset yesterday,
            bool valid)
        {
            return new List<TimeRegistration>
                {
                    CreateTimeRegistration(userId, trappingTypeId, subAreaHourSquareId, today, 3.5),
                    CreateTimeRegistration(userId, trappingTypeId, subAreaHourSquareId, valid ? yesterday : today, 8)
                }
                .AsQueryable();
        }

        private IQueryable<TimeRegistration> CreateTimeRegistrationsByUser(
            Guid userIdOne,
            Guid userIdTwo,
            Guid trappingTypeId,
            Guid subAreaHourSquareId,
            DateTimeOffset date,
            bool valid)
        {
            return new List<TimeRegistration>
                {
                    CreateTimeRegistration(userIdOne, trappingTypeId, subAreaHourSquareId, date, 3.5),
                    CreateTimeRegistration(valid ? userIdTwo : userIdOne, trappingTypeId, subAreaHourSquareId, date, 8)
                }
                .AsQueryable();
        }

        private IQueryable<TimeRegistration> CreateTimeRegistrationsByTrappingType(
            Guid userId,
            Guid trappingTypeIdOne,
            Guid trappingTypeIdTwo,
            Guid subAreaHourSquareId,
            DateTimeOffset date,
            bool valid)
        {
            return new List<TimeRegistration>
                {
                    CreateTimeRegistration(userId, trappingTypeIdOne, subAreaHourSquareId, date, 3.5),
                    CreateTimeRegistration(userId, valid ? trappingTypeIdTwo : trappingTypeIdOne, subAreaHourSquareId, date, 8)
                }
                .AsQueryable();
        }
    }
}
