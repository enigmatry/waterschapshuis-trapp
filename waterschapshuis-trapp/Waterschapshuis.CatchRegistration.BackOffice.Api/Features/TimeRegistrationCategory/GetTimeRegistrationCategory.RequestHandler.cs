using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TimeRegistrationCategory
{
    public partial class GetTimeRegistrationCategory
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<DomainModel.TimeRegistrations.TimeRegistrationCategory> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<DomainModel.TimeRegistrations.TimeRegistrationCategory> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .QueryById(request.Id)
                    .SingleOrDefaultMappedAsync<DomainModel.TimeRegistrations.TimeRegistrationCategory, Response>(_mapper, cancellationToken);
            }
        }
    }
}
