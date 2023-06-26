using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations
{
    public static class QueryableProjectToExtensions
    {
        public static IQueryable<TDestination> ProjectToWithMappingParameters<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            IMappingParametrizationService mappingParametrizationService) =>
            query
                .ProjectToWithMappingParameters<TSource, TDestination>(
                    mapper,
                    mappingParametrizationService.CreateAnonymizationParameters(),
                    mappingParametrizationService.CreateCanBeEditedParameters()
                );

        public static async Task<List<TDestination>> ProjectToListWithMappingParameters<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            IMappingParametrizationService mappingParametrizationService,
            CancellationToken cancellationToken = default) =>
            await query
                .ProjectToWithMappingParameters<TSource, TDestination>(
                    mapper,
                    mappingParametrizationService.CreateAnonymizationParameters(),
                    mappingParametrizationService.CreateCanBeEditedParameters()
                )
                .ToListAsync(cancellationToken);

        public static async Task<PagedResponse<TDestination>> ProjectToPagedResponseWithMappingParameters<TSource, TDestination>(
            this IQueryable<TSource> query,
            int currentPage,
            int pageSize,
            IMapper mapper,
            IMappingParametrizationService mappingParametrizationService,
            CancellationToken cancellationToken = default) =>
            await query
                .ProjectToWithMappingParameters<TSource, TDestination>(
                    mapper,
                    mappingParametrizationService.CreateAnonymizationParameters(),
                    mappingParametrizationService.CreateCanBeEditedParameters()
                )
                .ToPagedResponse(currentPage, pageSize, cancellationToken);


        private static IQueryable<TDestination> ProjectToWithMappingParameters<TSource, TDestination>(
            this IQueryable<TSource> query,
            IMapper mapper,
            AnonymizationProjectToParameters anonymizationParameters,
            CanBeEditedProjectToParameters canBeEditedParameters) =>
            query
                .ProjectTo<TDestination>(
                    mapper.ConfigurationProvider,
                    new
                    {
                        parameters = new MappingParameters
                        (
                            anonymizationParameters,
                            canBeEditedParameters
                        )
                    });
    }
}
