using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public partial class GetCatchDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, CatchItem>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<Catch> _repository;
            private readonly IMappingParametrizationService _mappingParametrizationService;

            public RequestHandler(IMapper mapper,
                IRepository<Catch> repository,
                IMappingParametrizationService mappingParametrizationService)
            {
                _mapper = mapper;
                _repository = repository;
                _mappingParametrizationService = mappingParametrizationService;
            }

            public async Task<CatchItem> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAll()
                    .BuildInclude()
                    .QueryById(request.Id)
                    .ProjectToWithMappingParameters<Catch, CatchItem>(_mapper, _mappingParametrizationService)
                    .SingleOrDefaultAsync(cancellationToken);
            }
        }
    }
}
