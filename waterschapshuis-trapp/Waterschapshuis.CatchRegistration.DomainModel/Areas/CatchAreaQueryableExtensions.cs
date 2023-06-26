using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public static class CatchAreaQueryableExtensions
    {
        public static IQueryable<CatchArea> QueryAtCoordinates(
            this IQueryable<CatchArea> query, double longitude, double latitude)
        {
            return query.Where(ca => ca.Geometry.Contains(GeometryUtil.Factory.CreatePoint(longitude, latitude)));
        }

        public static IQueryable<CatchArea> QueryByVersionRegionalLayoutId(this IQueryable<CatchArea> query, Guid id) =>
            query
                .Where(ca =>
                    ca.SubAreas.Any(sa => sa.SubAreaHourSquares.Any(sahs => sahs.VersionRegionalLayoutId == id))
                );
    }
}
