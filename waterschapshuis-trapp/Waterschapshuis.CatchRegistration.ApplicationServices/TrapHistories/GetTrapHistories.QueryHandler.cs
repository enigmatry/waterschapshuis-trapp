using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TrapHistories
{
    public static partial class GetTrapHistories
    {
        [UsedImplicitly]
        public class GetTrapHistoriesQueryHandler : IRequestHandler<Query, PagedResponse<HistoryItem>>
        {
            private readonly IMapper _mapper;
            private readonly IMappingParametrizationService _mappingParametrizationService;
            private readonly IRepository<TrapHistory> _trapHistoryRepository;

            public GetTrapHistoriesQueryHandler(IMapper mapper,
                IMappingParametrizationService mappingParametrizationService,
                IRepository<TrapHistory> trapHistoryRepository)
            {
                _mapper = mapper;
                _mappingParametrizationService = mappingParametrizationService;
                _trapHistoryRepository = trapHistoryRepository;
            }

            public async Task<PagedResponse<HistoryItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _trapHistoryRepository
                    .QueryAllAsNoTracking()
                    .QueryByTrapId(request.TrapId)
                    .SortBy(request.SortField, request.SortDirection)
                    .ProjectToPagedResponseWithMappingParameters<TrapHistory, HistoryItem>(
                        request.CurrentPage,
                        request.PageSize,
                        _mapper,
                        _mappingParametrizationService,
                        cancellationToken);
            }
        }
    }
}
