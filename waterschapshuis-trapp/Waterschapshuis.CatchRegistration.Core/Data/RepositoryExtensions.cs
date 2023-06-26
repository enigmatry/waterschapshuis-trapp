using System;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Core.Data
{
    public static class RepositoryExtensions
    {
        public static void AddRange<T>(this IRepository<T> repository, IEnumerable<T> entities) where T : Entity
        {
            foreach (T entity in entities)
            {
                repository.Add(entity);
            }
        }

        public static bool EntityExists<T>(this IRepository<T> repository, Guid id) where T : Entity<Guid>
        {
            return repository.QueryAll().Any(x => x.Id == id);
        }
    }
}
