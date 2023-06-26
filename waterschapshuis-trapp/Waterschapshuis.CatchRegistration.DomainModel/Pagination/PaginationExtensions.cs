using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.Core.Utils;

namespace Waterschapshuis.CatchRegistration.DomainModel.Pagination
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResponse<T>> ToPagedResponse<T>(
            this IQueryable<T> query,
            int currentPage,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var pagedItems = await query
                .Paginate(currentPage, pageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PagedResponse<T>
            {
                Items = pagedItems,
                ItemsTotalCount = totalCount,
                CurrentPage = currentPage,
                PageSize = pageSize
            };
        }

        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, string sortField, string sortDirection)
        {
            if (!sortField.IsNotNullOrEmpty())
            {
                return query;
            }

            var propertyInfo = typeof(T)
                .GetProperties()
                .FirstOrDefault(x => x.Name.Equals(sortField, StringComparison.InvariantCultureIgnoreCase));

            return propertyInfo == null ? query : query.OrderByProperty(propertyInfo.Name, sortDirection);
        }

        private static IQueryable<T> Paginate<T>(this IQueryable<T> query, int currentPage, int pageSize)
        {
            return query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
