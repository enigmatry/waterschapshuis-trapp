using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Common
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> QueryAllAsNoTracking<TEntity>(
            this IRepository<TEntity> repository, 
            params Expression<Func<TEntity, object>>[] paths) where TEntity : Entity =>
           paths.Any()
                ? repository.QueryAllIncluding(paths).AsNoTracking()
                : repository.QueryAll().AsNoTracking();
    }
}
