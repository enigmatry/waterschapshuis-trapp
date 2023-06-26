using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations
{
    public static class TimeRegistrationQueryableExtensions
    {
        public static IQueryable<TimeRegistration> QueryByUser(this IQueryable<TimeRegistration> query, Guid userId) =>
            query.Where(x => x.UserId == userId);

        public static bool AnyNotInStatusWritten(
            this IQueryable<TimeRegistration> query,
            Guid userId,
            DateTimeOffset startDate,
            DateTimeOffset endDate) =>
            query.QueryByUser(userId)
                .QueryByDateRangeExclusiveEnd(startDate, endDate)
                .Any(x => x.Status != TimeRegistrationStatus.Written);

        public static IQueryable<TimeRegistration> QueryByDateRangeExclusiveEnd(
            this IQueryable<TimeRegistration> query,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            var start = startDate.BeginningOfDay();
            var end = endDate.BeginningOfDay();
            return query.Where(x => x.Date >= start && x.Date < end);
        }

        public static IQueryable<TimeRegistration> QueryByDateOfOneDayRange(this IQueryable<TimeRegistration> query, DateTimeOffset date) =>
            query.QueryByDateRangeExclusiveEnd(date,date.AddDays(1));

        public static IQueryable<TimeRegistration> QueryToDateAndStatuses(
            this IQueryable<TimeRegistration> query,
            DateTimeOffset date,
            IEnumerable<TimeRegistrationStatus> availableStatuses)
        {
            var queryDate = date.BeginningOfDay();
            return query.Where(x => queryDate > x.Date && availableStatuses.Contains(x.Status));
        }

        public static TimeRegistration? ExistingTimeRegistrationEntryEntity(
            this IQueryable<TimeRegistration> query,
            Guid userId,
            Guid subAreaHourSquareId,
            Guid trappingTypeId,
            DateTimeOffset date) =>
            query
                .QueryByUser(userId)
                .QueryByDateOfOneDayRange(date)
                .QueryByOptionalSubAreaHourSquareId(subAreaHourSquareId)
                .SingleOrDefault(x => x.TrappingTypeId == trappingTypeId);

        public static IQueryable<TimeRegistration> ExistingTimeRegistrationByWeek(
            this IQueryable<TimeRegistration> query,
            Guid userId,
            DateTimeOffset startDate,
            DateTimeOffset endDate) =>
            query
                .QueryByUser(userId)
                .QueryByDateRangeExclusiveEnd(startDate, endDate);

        public static IQueryable<TimeRegistration> QueryByRayon(this IQueryable<TimeRegistration> query, Guid rayonId) =>
            query.Where(x => x.SubAreaHourSquare.SubArea.CatchArea.Rayon.Id == rayonId);

        public static IQueryable<TimeRegistration> QueryByOrganization(this IQueryable<TimeRegistration> query, Guid organizationId) =>
            query.Where(x => x.SubAreaHourSquare.SubArea.CatchArea.Rayon.OrganizationId == organizationId);

        public static IQueryable<TimeRegistration> QueryByOptionalSubAreaHourSquareId(this IQueryable<TimeRegistration> query, Guid? subAreaHourSquareId) =>
            query.Where(x => !subAreaHourSquareId.HasValue || x.SubAreaHourSquareId == subAreaHourSquareId);

        public static IQueryable<TimeRegistration> QueryByHourSquareId(this IQueryable<TimeRegistration> query, Guid hourSquareId) =>
            query.Where(x => x.SubAreaHourSquare.HourSquareId == hourSquareId);

        public static IQueryable<TimeRegistration> QueryByYear(this IQueryable<TimeRegistration> query, int year) =>
            query.Where(x => 
                (x.WeekPeriod.Year == year && x.WeekPeriod.Period < 13) ||
                (x.WeekPeriod.Year == year - 1 && x.WeekPeriod.Period == 13));

        public static IQueryable<TimeRegistration> QueryBetweenYears(this IQueryable<TimeRegistration> query, int startYear, int endYear) =>
            query.Where(x => 
                (x.WeekPeriod.Year == startYear && x.WeekPeriod.Period == 13) || 
                (x.WeekPeriod.Year > startYear && x.WeekPeriod.Year < endYear) ||
                (x.WeekPeriod.Year == endYear && x.WeekPeriod.Period < 13));

        public static IQueryable<TimeRegistration> QueryBySeason(this IQueryable<TimeRegistration> query, Season season) =>
            query.Where(x => SeasonPeriod.GetSeasonByPeriod(x.WeekPeriod.Period) == (int)season);

        public static IQueryable<TimeRegistration> QueryByVersionRegionalLayoutId(this IQueryable<TimeRegistration> query, Guid id) =>
           query.Where(tr => tr.SubAreaHourSquare.VersionRegionalLayoutId == id);

        public static IQueryable<TimeRegistration> QueryNotCreatedFromTrackings(this IQueryable<TimeRegistration> query) =>
           query.Where(tr => !tr.IsCreatedFromTrackings);

        public static IQueryable<TimeRegistration> QueryCreatedFromTracking(this IQueryable<TimeRegistration> query) =>
           query.Where(tr => tr.IsCreatedFromTrackings);

        public static IQueryable<TimeRegistration> QueryBySubAreaHourSquareIds(this IQueryable<TimeRegistration> query, params Guid[] ids) =>
           query.Where(tr => ids.Contains(tr.SubAreaHourSquareId));
    }
}
