using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations
{
    public static class ObservationQueryableExtension
    {
        public static IQueryable<Observation> QueryByKeyword(this IQueryable<Observation> query, string keyword)
        {
            return keyword.IsNotNullOrEmpty() ?
                query.Where(e => e.CreatedBy.Name.Contains(keyword)) :
                query;
        }

        public static IQueryable<Observation> QueryByOptionalBoundingBox(this IQueryable<Observation> query,
            BoundingBox? boundingBox)
        {
            return boundingBox == null ? query : query.Where(x => boundingBox.Geometry.Contains(x.Location));
        }

        public static IQueryable<Observation> ContainingIds(this IQueryable<Observation> query, IEnumerable<Guid> ids)
        {
            return query.Where(t => ids.Contains(t.Id));
        }
    }
}
