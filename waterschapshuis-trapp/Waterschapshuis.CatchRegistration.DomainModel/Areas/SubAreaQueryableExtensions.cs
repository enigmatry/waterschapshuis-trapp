using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public static class SubAreaQueryableExtensions
    {
        public static IQueryable<SubArea> QueryByVersionRegionalLayoutId(this IQueryable<SubArea> query, Guid id) =>
            query
                .Where(sa => sa.SubAreaHourSquares.Any(sahs => sahs.VersionRegionalLayoutId == id));
    }
}
