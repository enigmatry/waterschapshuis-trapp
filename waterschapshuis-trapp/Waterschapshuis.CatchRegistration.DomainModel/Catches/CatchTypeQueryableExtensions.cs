using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches
{
    public static class CatchTypeQueryableExtensions
    {
        public static IQueryable<CatchType> QueryByOptionalIsByCatch(this IQueryable<CatchType> query, bool? isByCatch)
        {
            return isByCatch.HasValue ? 
                query.Where(c => c.IsByCatch == isByCatch) : 
                query;
        }

        public static IQueryable<CatchType> ExcludeById(this IQueryable<CatchType> query, Guid id)
        {
            return query.Where(c => c.Id != id);
        }

        public static IQueryable<CatchType> QueryByIds(this IQueryable<CatchType> query, Guid[] ids) =>
            query.Where(x => ids.Contains(x.Id));
    }
}
