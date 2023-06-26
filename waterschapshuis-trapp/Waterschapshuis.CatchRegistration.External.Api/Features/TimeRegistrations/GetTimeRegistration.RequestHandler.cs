using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations
{
    public partial class GetTimeRegistration
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly IMapper _mapper;

            public RequestHandler(ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService, IMapper mapper)
            {
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
                _mapper = mapper;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var timeRegistration = _currentVersionRegionalLayoutService
                    .QueryTimeRegistrations()
                    .QueryById(request.Id)
                    .AsNoTracking()
                    .ProjectTo<TimeRegistrationItem>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();

                return Task.FromResult(new Response(timeRegistration));
            }
        }
    }
}
