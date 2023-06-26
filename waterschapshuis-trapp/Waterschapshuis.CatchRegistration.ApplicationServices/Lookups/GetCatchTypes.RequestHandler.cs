using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Lookups
{
    public static partial class GetCatchTypes
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, ListResponse<ResponseItem>>
        {
            private readonly IRepository<CatchType> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<CatchType> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ListResponse<ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .ExcludeById(CatchType.HistoryMuskusratId)
                    .ExcludeById(CatchType.HistoryBeverratId)
                    .OrderBy(x => x.Order)
                    .ProjectToListResponse<CatchType, ResponseItem>(_mapper, cancellationToken);
            }
        }
    }
}
