using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Traps
{
    public static class GetTrapsQueryableExtensions
    {
        public static IQueryable<GetTrap.TrapItem> QueryByYear(this IQueryable<GetTrap.TrapItem> queryable, int? createdOnYear)
        {
            return createdOnYear.HasValue
                ? queryable.Where(item => item.CreatedOn.Year == createdOnYear.Value)
                : queryable;
        }

        public static IQueryable<GetTrap.TrapItem> QueryByOrganizationName(this IQueryable<GetTrap.TrapItem> queryable, string? organization)
        {
            return organization.IsNotNullOrEmpty()
                ? queryable.Where(item => item.OrganizationName == organization)
                : queryable;
        }
    }
}
