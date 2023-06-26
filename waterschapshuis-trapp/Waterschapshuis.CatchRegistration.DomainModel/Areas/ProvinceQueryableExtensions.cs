using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public static class ProvinceQueryableExtensions
    {
        public static IQueryable<Province> QueryContainesCoordinates(this IQueryable<Province> queryable, double longitude, double latitude)
        {
            return queryable.Where(x => x.Geometry.Contains(GeometryUtil.Factory.CreatePoint(longitude, latitude)));
        }
    }
}
