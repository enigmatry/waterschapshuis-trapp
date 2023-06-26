using System.Linq;
using System.Linq.Expressions;

namespace Waterschapshuis.CatchRegistration.Core.Utils
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string property, string direction = "asc")
        {
            var type = typeof(T);
            var prop =
                type.GetProperty(property);

            if (prop == null)
            {
                return source;
            }

            var method = direction == "asc" ? "OrderBy" : "OrderByDescending";
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, prop);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), method, new[] { type, prop.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
