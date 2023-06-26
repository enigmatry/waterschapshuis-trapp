using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public static partial class GetTraps
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, PagedResponse<GetTrapDetails.TrapItem>>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<Trap> _repository;
            private readonly IMappingParametrizationService _mappingParametrizationService;

            public RequestHandler(
                IMapper mapper,
                IRepository<Trap> repository,
                IMappingParametrizationService mappingParametrizationService)
            {
                _mapper = mapper;
                _repository = repository;
                _mappingParametrizationService = mappingParametrizationService;
            }

            public async Task<PagedResponse<GetTrapDetails.TrapItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAll()
                    .QueryByOptionalBoundingBox(request.MapToBoundingBox())
                    .OrderBy(x => x.Id)
                    .ProjectToPagedResponseWithMappingParameters<Trap, GetTrapDetails.TrapItem>(
                        request.CurrentPage,
                        request.PageSize,
                        _mapper,
                        _mappingParametrizationService,
                        cancellationToken);
            }
        }
    }
}
