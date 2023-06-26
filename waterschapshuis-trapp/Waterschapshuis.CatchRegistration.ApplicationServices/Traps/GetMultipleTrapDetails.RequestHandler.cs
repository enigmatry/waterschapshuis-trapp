using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public static partial class GetMultipleTrapDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<GetTrapDetails.TrapItem>>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<Trap> _repository;
            private readonly IRepository<SubAreaHourSquare> _sahsRepository;
            private readonly IMappingParametrizationService _mappingParametrizationService;

            public RequestHandler(
                IMapper mapper,
                IRepository<Trap> repository,
                IMappingParametrizationService mappingParametrizationService, IRepository<SubAreaHourSquare> sahsRepository)
            {
                _mapper = mapper;
                _repository = repository;
                _mappingParametrizationService = mappingParametrizationService;
                _sahsRepository = sahsRepository;
            }

            public async Task<IEnumerable<GetTrapDetails.TrapItem>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAll()
                    .ContainingIds(request.Ids)
                    .ProjectToListWithMappingParameters<Trap, GetTrapDetails.TrapItem>(_mapper, _mappingParametrizationService, cancellationToken);
            }
        }
    }
}
