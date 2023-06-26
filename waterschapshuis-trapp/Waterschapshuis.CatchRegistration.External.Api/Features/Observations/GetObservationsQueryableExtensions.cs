using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Observations
{
    public static class GetObservationsQueryableExtensions
    {
        public static IQueryable<GetObservation.ObservationItem> QueryByYear(
            this IQueryable<GetObservation.ObservationItem> queryable,
            int? createdOnYear)
        {
            return createdOnYear.HasValue
                ? queryable.Where(item => item.CreatedOn.Year == createdOnYear.Value)
                : queryable;
        }

        public static IQueryable<GetObservation.ObservationItem> QueryByOrganizationName(
            this IQueryable<GetObservation.ObservationItem> queryable,
            string? organization)
        {
            return organization.IsNotNullOrEmpty()
                ? queryable.Where(item => item.OrganizationName == organization)
                : queryable;
        }
    }
}
