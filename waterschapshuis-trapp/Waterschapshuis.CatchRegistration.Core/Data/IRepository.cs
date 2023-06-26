using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Core.Data
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T item);

        void Delete(T item);

        void DeleteRange(IEnumerable<T> entities);

        IQueryable<T> QueryAll();

        IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths);

        void Delete<TId>(TId id);

        T FindById<TId>(TId id);

        Task<T> FindByIdAsync<TId>(TId id);

        IQueryable<T> ExecuteRawSql<TId>(string sqlQuery, params object[] parameters);
    }
}
