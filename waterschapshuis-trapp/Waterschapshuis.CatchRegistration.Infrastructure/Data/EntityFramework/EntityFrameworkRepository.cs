using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        public EntityFrameworkRepository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        protected DbSet<T> DbSet { get; }

        protected DbContext DbContext { get; }

        public IQueryable<T> QueryAll()
        {
            return DbSet;
        }

        public IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            return paths.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Add(item);
        }

        public void Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Remove(item);
        }

        public void Delete<TId>(TId id)
        {
            T item = FindById(id);
            if (item != null)
            {
                Delete(item);
            }
        }

        public T FindById<TId>(TId id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> FindByIdAsync<TId>(TId id)
        {
            return await DbSet.FindAsync(id);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public IQueryable<T> ExecuteRawSql<TId>(string sqlQuery, params object[] parameters)
        {
            return DbSet.FromSqlRaw(sqlQuery, parameters);
        }
    }
}
