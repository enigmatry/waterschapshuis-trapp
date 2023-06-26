using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Organizations
{
    public partial class GetOrganizations
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Organization> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Organization> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _repository
                    .QueryAllAsNoTracking()
                    .OrderBy(i => i.Name)
                    .ProjectToList<Organization, Response.Item>(_mapper, cancellationToken);

                return new Response { Items = items };
            }
        }
    }
}
