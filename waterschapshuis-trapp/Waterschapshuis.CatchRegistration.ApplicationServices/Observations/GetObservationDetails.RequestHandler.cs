using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Observations
{
    public static partial class GetObservationDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, ResponseItem>
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

            public async Task<ResponseItem> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.QueryAll()
                    .QueryById(request.Id)
                    .ProjectToWithMappingParameters<Observation, ResponseItem>(_mapper, _mappingParametrizationService)
                    .SingleOrDefaultAsync(cancellationToken);
            }
        }
    }
}
