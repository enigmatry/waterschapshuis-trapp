using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations
{
    public static class TimeRegistrationEnumerableExtensions
    {
        public static IEnumerable<TimeRegistration> QueryByDate(this IEnumerable<TimeRegistration> query, DateTimeOffset date) =>
            query.QueryByDateRangeExclusiveEnd(date, date.AddDays(1));
        
        public static IEnumerable<TimeRegistration> QueryByDateRangeExclusiveEnd(
            this IEnumerable<TimeRegistration> query,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            var start = startDate.BeginningOfDay();
            var end = endDate.BeginningOfDay();

            return query.Where(x => x.Date >= start && x.Date < end);
        }

        public static IEnumerable<TimeRegistration> NotInSubAreaHourSquare(
            this IEnumerable<TimeRegistration> query,
            Guid? subAreaHourSquareId)
        {
            return query.Where(x =>
                !subAreaHourSquareId.HasValue || x.SubAreaHourSquareId != subAreaHourSquareId);
        }
        
        public static IEnumerable<TimeRegistration> QueryByOptionalSubAreaHourSquareId(
            this IEnumerable<TimeRegistration> query,
            Guid? subAreaHourSquareId)
        {
            return query.Where(x =>
                !subAreaHourSquareId.HasValue || x.SubAreaHourSquareId == subAreaHourSquareId);
        }
    }
}
