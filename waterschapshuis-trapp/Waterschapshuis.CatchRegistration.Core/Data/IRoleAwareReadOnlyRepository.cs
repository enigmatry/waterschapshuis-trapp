using System;
using System.Linq;
using System.Linq.Expressions;

namespace Waterschapshuis.CatchRegistration.Core.Data
{
    public interface IRoleAwareReadOnlyRepository<T> where T : Entity, ICurrentUserRoleAwareEntity
    {
        IQueryable<T> QueryAll();
        IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths);
    }
}
