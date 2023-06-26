using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations
{
    public static class TimeRegistrationGeneralQueryableExtensions
    {
        public static IQueryable<TimeRegistrationGeneral> QueryByUser(
            this IQueryable<TimeRegistrationGeneral> query,
            Guid userId)
        {
            return query.Where(x =>
                x.UserId == userId);
        }

        public static bool AnyNotInStatusWritten(
            this IQueryable<TimeRegistrationGeneral> query,
            Guid userId,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return query.QueryByUser(userId)
                .QueryByDateRangeExclusiveEnd(startDate, endDate)
                .Any(x => x.Status != TimeRegistrationStatus.Written);
        }

        public static IQueryable<TimeRegistrationGeneral> QueryByDateRangeExclusiveEnd(
            this IQueryable<TimeRegistrationGeneral> query,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            var start = startDate.BeginningOfDay();
            var end = endDate.BeginningOfDay();

            return query.Where(x => x.Date >= start && x.Date < end);
        }

        public static IQueryable<TimeRegistrationGeneral> QueryByOrganization(
            this IQueryable<TimeRegistrationGeneral> query,
            Guid organizationId)
        {
            return query.Where(x => x.User.OrganizationId == organizationId);
        }

        public static IQueryable<TimeRegistrationGeneral> QueryByDate(
            this IQueryable<TimeRegistrationGeneral> query,
            DateTimeOffset date)
        {
            return query.QueryByDateRangeExclusiveEnd(date, date.AddDays(1));
        }
    }
}
