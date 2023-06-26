using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Lookups
{
    public static partial class GetTrappingTypes
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, ListResponse<ResponseItem>>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<TrappingType> _repository;

            public RequestHandler(IRepository<TrappingType> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ListResponse<ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .OrderByDescending(r => r.Name)
                    .ProjectToListResponse<TrappingType, ResponseItem>(_mapper, cancellationToken);
            }
        }
    }
}
