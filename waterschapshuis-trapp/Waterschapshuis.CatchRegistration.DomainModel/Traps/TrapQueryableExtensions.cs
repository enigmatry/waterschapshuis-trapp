using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public static class TrapQueryableExtensions
    {
        public static IQueryable<Trap> QueryByLongAndLat(this IQueryable<Trap> query, double longitude, double latitude)
        {
            //Using .Equals() because '==' require tolerance to be defined but when comparing longitude and latitude not tolerance is allowed
            return query.Where(e => e.Location.X.Equals(longitude) && e.Location.Y.Equals(latitude));
        }
        public static IQueryable<Trap> QueryByOptionalBoundingBox(this IQueryable<Trap> query,
            BoundingBox? boundingBox)
        {
            return boundingBox == null ? query : query.Where(x=> boundingBox.Geometry.Contains(x.Location));
        }

        public static IQueryable<Trap> QueryByUserCreatedId(this IQueryable<Trap> query, Guid userId)
        {
            return query.Where(x => x.CreatedById == userId);
        }

        public static IQueryable<Trap> QueryNotRemoved(this IQueryable<Trap> query)
        {
            return query.Where(x => x.Status != TrapStatus.Removed);
        }

        public static IQueryable<Trap> ContainingIds(this IQueryable<Trap> query, IEnumerable<Guid> ids)
        {
            return query.Where(t => ids.Contains(t.Id));
        }

        public static async Task<Trap?> TryFindByCatchId(this IQueryable<Trap> query, Guid catchId, CancellationToken cancellationToken) =>
            await query.SingleOrDefaultAsync(x => x.Catches.Any(c => c.Id == catchId), cancellationToken);

        public static IQueryable<Trap> BuildInclude(this IQueryable<Trap> query) =>
            query
                .Include(x => x.SubAreaHourSquare.SubArea.WaterAuthority);
    }
}
