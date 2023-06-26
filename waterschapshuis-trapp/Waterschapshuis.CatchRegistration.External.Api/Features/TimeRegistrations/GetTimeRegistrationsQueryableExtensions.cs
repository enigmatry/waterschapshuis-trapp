using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations
{
    public static class GetTimeRegistrationsQueryableExtensions
    {
        public static IQueryable<GetTimeRegistration.TimeRegistrationItem> QueryByYear(
            this IQueryable<GetTimeRegistration.TimeRegistrationItem> queryable,
            int? createdOnYear)
        {
            return createdOnYear.HasValue
                ? queryable.Where(item => item.DateOffset.Year == createdOnYear.Value)
                : queryable;
        }

        public static IQueryable<GetTimeRegistration.TimeRegistrationItem> QueryByOrganizationName(
            this IQueryable<GetTimeRegistration.TimeRegistrationItem> queryable,
            string? organization)
        {
            return organization.IsNotNullOrEmpty()
                ? queryable.Where(item => item.OrganizationName == organization)
                : queryable;
        }
    }
}
