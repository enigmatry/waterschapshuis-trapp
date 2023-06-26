using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Catches
{
    public static class GetCatchesQueryableExtensions
    {
        public static IQueryable<GetCatch.CatchItem> QueryByYear(this IQueryable<GetCatch.CatchItem> queryable, int? createdOnYear)
        {
            return createdOnYear.HasValue
                ? queryable.Where(item => item.CreatedOn.Year == createdOnYear.Value)
                : queryable;
        }

        public static IQueryable<GetCatch.CatchItem> QueryByOrganizationName(this IQueryable<GetCatch.CatchItem> queryable, string? organization)
        {
            return organization.IsNotNullOrEmpty()
                ? queryable.Where(item => item.OrganizationName == organization)
                : queryable;
        }
    }
}
