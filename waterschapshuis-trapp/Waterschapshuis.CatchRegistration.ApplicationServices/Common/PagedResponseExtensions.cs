using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Common
{
    public static class PagedResponseExtensions
    {
        public static async Task<PagedResponse<TDestination>> ProjectToPagedResponse<TSource, TDestination>(
            this IQueryable<TSource> query,
            int currentPage,
            int pageSize,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            return await query
                .ProjectTo<TDestination>(mapper.ConfigurationProvider, cancellationToken)
                .ToPagedResponse(currentPage, pageSize, cancellationToken);
        }
    }
}
