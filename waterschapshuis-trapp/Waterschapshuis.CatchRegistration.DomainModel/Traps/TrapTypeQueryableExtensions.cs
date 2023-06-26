using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public static class TrapTypeQueryableExtensions
    {
        public static IQueryable<TrapType> QueryActiveOnly(this IQueryable<TrapType> query)
        {
            return query.Where(e => e.Active);
        }

        public static IQueryable<TrapType> QueryByOptionalTrappingType(this IQueryable<TrapType> query,
            Guid? trappingTypeId)
        {
            return !trappingTypeId.HasValue
                ? query
                : query.Where(e => e.TrappingTypeId == trappingTypeId);
        }

        public static bool AllowTrapStatus(this IQueryable<TrapType> query, Guid trapTypeId, TrapStatus trapStatus)
        {
            var trapType = query.SingleOrDefault(x => x.Id == trapTypeId);
            return trapType == null
                ? false : trapType.AllowedStatuses.Contains(trapStatus);
        }
            
    }
}
