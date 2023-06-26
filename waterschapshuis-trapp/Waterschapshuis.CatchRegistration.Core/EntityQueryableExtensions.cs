using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.Core
{
    public static class EntityQueryableExtensions
    {
        public static IQueryable<T> QueryById<T>(this IQueryable<T> query, Guid id) where T : Entity<Guid>
        {
            return query.Where(e => e.Id == id);
        }

        public static IQueryable<T> QueryExceptWithId<T>(this IQueryable<T> query, Guid? id) where T : Entity<Guid>
        {
            return !id.HasValue ? query : query.Where(e => e.Id != id);
        }

        public static IQueryable<T> QueryByCreator<T>(this IQueryable<T> query, Guid userId) where T: IEntityHasCreated
        {
            return query.Where(e => e.CreatedById == userId);
        }

        public static IQueryable<T> QueryByName<T>(this IQueryable<T> query, string name) where T: IEntityHasName
        {
            return name.IsNotNullOrEmpty() ?
                query.Where(e => e.Name == name) :
                query;
        }

        public static IQueryable<T> QueryByKeyword<T>(this IQueryable<T> query, string keyword) where T : IEntityHasName
        {
            return keyword.IsNotNullOrEmpty() ? 
                query.Where(e => e.Name.Contains(keyword)) : 
                query;
        }
    }
}
