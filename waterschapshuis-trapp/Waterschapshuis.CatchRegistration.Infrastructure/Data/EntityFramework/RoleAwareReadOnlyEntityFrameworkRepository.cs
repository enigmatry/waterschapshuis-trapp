using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.RoleAwareFiltering;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class RoleAwareReadOnlyEntityFrameworkRepository<T> : IRoleAwareReadOnlyRepository<T>
        where T : Entity, ICurrentUserRoleAwareEntity
    {
        protected DbSet<T> DbSet { get; }
        protected DbContext DbContext { get; }
        private readonly IFilterEntityByCurrentUserRoleQueryExpression<T> _expression;
        private readonly ICurrentUserProvider _currentUserProvider;

        public RoleAwareReadOnlyEntityFrameworkRepository(DbContext context,
            IFilterEntityByCurrentUserRoleQueryExpression<T> expression,
            ICurrentUserProvider currentUserProvider)
        {
            DbContext = context;
            DbSet = context.Set<T>();
            _expression = expression;
            _currentUserProvider = currentUserProvider;
        }

        public IQueryable<T> QueryAll()
        {
            return DbSet.Where(_expression.GetExpression(_currentUserProvider));
        }

        public IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            return paths.Aggregate(QueryAll(), (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
