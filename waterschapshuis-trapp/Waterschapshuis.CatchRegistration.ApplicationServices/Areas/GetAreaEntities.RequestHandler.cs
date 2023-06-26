using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Areas
{
    public static partial class GetAreaEntities
    {
        private static class RequestHandler
        {
            public static Task<Response> Handle<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate)
                where TEntity : Entity<Guid>, IEntityHasName<Guid>
            {
                return Task.FromResult(new Response
                {
                    Items = query
                        .Where(predicate)
                        .OrderBy(x => x.Name)
                        .Select(x => NamedEntity.Item.Create(x))
                });
            }
        }
    }
}
