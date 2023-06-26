using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.CatchTypes
{
    public partial class GetCatchType
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<CatchType> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<CatchType> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .QueryById(request.Id)
                    .SingleOrDefaultMappedAsync<CatchType, Response>(_mapper, cancellationToken);
            }
        }
    }
}
