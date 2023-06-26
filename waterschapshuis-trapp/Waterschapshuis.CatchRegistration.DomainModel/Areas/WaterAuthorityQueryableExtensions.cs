using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public static class WaterAuthorityQueryableExtensions
    {
        public static IQueryable<WaterAuthority> QueryByVersionRegionalLayoutId(this IQueryable<WaterAuthority> query, Guid id) =>
            query
                .Where(wa =>
                    wa.SubAreas.Any(sa => sa.SubAreaHourSquares.Any(sahs => sahs.VersionRegionalLayoutId == id))
                );
    }
}
