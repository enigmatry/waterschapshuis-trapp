using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public static partial class GetTrapDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, TrapItem>
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

            public async Task<TrapItem> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .QueryById(request.Id)
                    .ProjectToWithMappingParameters<Trap, TrapItem>(_mapper, _mappingParametrizationService)
                    .SingleOrDefaultAsync(cancellationToken);
            }
        }
    }
}
