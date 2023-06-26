using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Common
{
    public static class QueryableMappingExtensions
    {
        public static async Task<TDestination> SingleOrDefaultMappedAsync<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            TSource item = await query.SingleOrDefaultAsync(cancellationToken);
            return mapper.Map<TDestination>(item);
        }

        public static async Task<List<TDestination>> ToListMappedAsync<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            List<TSource> items = await query.ToListAsync(cancellationToken);
            return mapper.Map<List<TDestination>>(items);
        }

        public static List<TDestination> ToListMapped<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper)
        {
            List<TSource>? items = query.ToList();
            return mapper.Map<List<TDestination>>(items);
        }

        public static async Task<List<TDestination>> ProjectToList<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            CancellationToken cancellationToken)
            => await query
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        public static List<TDestination> ProjectToList<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper)
            => query
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .ToList();

        public static async Task<ListResponse<TDestination>> ProjectToListResponse<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            CancellationToken cancellationToken)
            => await ListResponse<TDestination>
                .Create(query
                    .ProjectToList<TSource, TDestination>(mapper, cancellationToken)
                );

        public static ListResponse<TDestination> ProjectToListResponse<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper)
            => ListResponse<TDestination>
                .Create(query
                    .ProjectToList<TSource, TDestination>(mapper)
                );

        public static async Task<TDestination> ProjectToSingleOrDefault<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            return await query
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public static async Task<TDestination> ProjectToFirstOrDefault<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            return await query
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public static IQueryable<TDestination> ProjectToWithParameter<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            object parameterObject)
            => query.ProjectTo<TDestination>(mapper.ConfigurationProvider, new { parameter = parameterObject });
    }
}
