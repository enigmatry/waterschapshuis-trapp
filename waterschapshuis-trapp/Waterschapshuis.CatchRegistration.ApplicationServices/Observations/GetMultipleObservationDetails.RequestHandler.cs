using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Observations
{
    public static partial class GetMultipleObservationDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<GetObservationDetails.ResponseItem>>
        {
            private readonly IRepository<Observation> _repository;
            private readonly IMapper _mapper;
            private readonly IMappingParametrizationService _mappingParametrizationService;

            public RequestHandler(IRepository<Observation> repository, IMapper mapper, IMappingParametrizationService mappingParametrizationService)
            {
                _repository = repository;
                _mapper = mapper;
                _mappingParametrizationService = mappingParametrizationService;
            }

            public async Task<IEnumerable<GetObservationDetails.ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .ContainingIds(request.Ids)
                    .ProjectToListWithMappingParameters<Observation, GetObservationDetails.ResponseItem>(_mapper, _mappingParametrizationService, cancellationToken);
            }
        }
    }
}
