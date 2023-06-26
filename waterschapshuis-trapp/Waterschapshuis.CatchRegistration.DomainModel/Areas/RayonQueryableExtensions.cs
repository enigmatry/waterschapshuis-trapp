using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public static class RayonQueryableExtensions
    {
        public static IQueryable<Rayon> QueryByOrganization(
              this IQueryable<Rayon> query,
              Guid organizationId)
        {
            return query.Where(x =>
                    x.OrganizationId == organizationId);
        }

        public static IQueryable<Rayon> QueryByVersionRegionalLayoutId(this IQueryable<Rayon> query, Guid id) =>
            query
                .Where(r =>
                    r.CatchAreas.Any(ca => 
                        ca.SubAreas.Any(sa => sa.SubAreaHourSquares.Any(sahs => sahs.VersionRegionalLayoutId == id))
                    )
                );
    }
}
