using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class EntityFrameworkQuery<T> : IQueryable<T> where T : Entity
    {
        private readonly IQueryable<T> _queryable;

        public EntityFrameworkQuery(DbContext context)
        {
            _queryable = context.Set<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queryable.GetEnumerator();
        }

        public Type ElementType => _queryable.ElementType;
        public Expression Expression => _queryable.Expression;
        public IQueryProvider Provider => _queryable.Provider;
    }
}
