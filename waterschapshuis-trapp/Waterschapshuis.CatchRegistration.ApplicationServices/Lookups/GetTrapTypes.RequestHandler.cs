using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Lookups
{
    public partial class GetTrapTypes
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, ListResponse<ResponseItem>>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<TrapType> _repository;

            public RequestHandler(IRepository<TrapType> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ListResponse<ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .QueryActiveOnly()
                    .OrderBy(r => r.Order)
                    .ProjectToListResponse<TrapType, ResponseItem>(_mapper, cancellationToken);
            }
        }
    }
}
