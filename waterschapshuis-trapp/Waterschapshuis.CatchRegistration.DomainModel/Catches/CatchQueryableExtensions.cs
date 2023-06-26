using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches
{
    public static class CatchQueryableExtensions
    {
        public static IQueryable<Catch> QueryByUserCreatedId(this IQueryable<Catch> query, Guid userId)
        {
            return query.Where(x => x.CreatedById == userId);
        }

        public static IQueryable<Catch> QueryFromDateRecorded(this IQueryable<Catch> query, DateTimeOffset date)
        {
            var start = date.BeginningOfDay();
            return query.Where(x => x.RecordedOn >= start);
        }

        public static IQueryable<Catch> QueryByIsByCatch(this IQueryable<Catch> query, bool isByCatch)
        {
            return query.Where(x => x.CatchType.IsByCatch == isByCatch);
        }

        public static IQueryable<Catch> QueryToDateAndStatuses(
            this IQueryable<Catch> query,
            DateTimeOffset date,
            IEnumerable<CatchStatus> availableStatuses)
        {
            var queryDate = date.BeginningOfDay();
            return query.Where(x => queryDate > x.RecordedOn && availableStatuses.Contains(x.Status));
        }
        public static IQueryable<Catch> QueryByDateRecordedRangeExclusiveEnd(
           this IQueryable<Catch> query,
           DateTimeOffset startDate,
           DateTimeOffset endDate)
        {
            var start = startDate.BeginningOfDay();
            var end = endDate.BeginningOfDay();
            return query.Where(x => x.RecordedOn >= start && x.RecordedOn < end);
        }
        public static IQueryable<Catch> QueryByRayon(
           this IQueryable<Catch> query,
           Guid rayonId)
        {
            return query.Where(x => x.Trap.SubAreaHourSquare.SubArea.CatchArea.Rayon.Id == rayonId);
        }
        public static IQueryable<Catch> QueryByOrganization(
            this IQueryable<Catch> query,
            Guid organizationId)
        {
            return query.Where(x => x.Trap.SubAreaHourSquare.SubArea.CatchArea.Rayon.OrganizationId == organizationId);
        }

        public static IQueryable<Catch> QueryByHourSquare(
            this IQueryable<Catch> query, 
            Guid hoursquareId)
        {
            return query.Where(x => x.Trap.SubAreaHourSquare.HourSquareId == hoursquareId);
        }

        public static IQueryable<Catch> QueryByYear(
            this IQueryable<Catch> query, 
            int year)
        {
            return query.Where(x => (x.WeekPeriod.Year == year && x.WeekPeriod.Period < 13) ||
                                    (x.WeekPeriod.Year == year - 1 && x.WeekPeriod.Period == 13));
        }

        public static IQueryable<Catch> QueryBetweenYears(
            this IQueryable<Catch> query,
            int startYear, int endYear)
        {
            return query.Where(x => (x.WeekPeriod.Year == startYear && x.WeekPeriod.Period == 13)
                                    || (x.WeekPeriod.Year > startYear && x.WeekPeriod.Year < endYear) ||
                                    (x.WeekPeriod.Year == endYear && x.WeekPeriod.Period < 13));
        }

        public static IQueryable<Catch> QueryBySeason(
            this IQueryable<Catch> query,
            Season season)
        {
            return query.Where(x => SeasonPeriod.GetSeasonByPeriod(x.WeekPeriod.Period) == (int)season);
        }

        public static bool AnyNotInStatusWritten(
            this IQueryable<Catch> query,
            Guid userId,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return query.QueryByUserCreatedId(userId)
                .QueryByDateRecordedRangeExclusiveEnd(startDate, endDate)
                .Any(x => x.Status != CatchStatus.Written);
        }

        public static bool Exists(this IQueryable<Catch> query, params Guid[] ids) =>
            query.Count(x => ids.Contains(x.Id)) == ids.Count();
    }
}
