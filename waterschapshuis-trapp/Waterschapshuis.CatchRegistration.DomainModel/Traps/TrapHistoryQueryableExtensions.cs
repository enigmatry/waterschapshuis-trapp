using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public static class TrapHistoryQueryableExtensions
    {
        public static IQueryable<TrapHistory> QueryByTrapId(this IQueryable<TrapHistory> query, Guid trapId) =>
            query.Where(x => x.TrapId == trapId);

        public static IQueryable<TrapHistory> QueryByTrapAndCatchId(this IQueryable<TrapHistory> query, Guid trapId, Guid catchId) =>
            query.Where(x => x.TrapId == trapId && x.CatchId == catchId);
    }
}
