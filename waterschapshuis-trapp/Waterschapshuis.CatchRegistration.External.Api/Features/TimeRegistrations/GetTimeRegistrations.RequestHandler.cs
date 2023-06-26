using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations
{
    public partial class GetTimeRegistrations
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
                var registrations = _currentVersionRegionalLayoutService
                    .QueryTimeRegistrationsNoTracking()
                    .ProjectTo<GetTimeRegistration.TimeRegistrationItem>(_mapper.ConfigurationProvider)
                    .QueryByOrganizationName(request.Organization)
                    .QueryByYear(request.CreatedOnYear);

                return Task.FromResult(new Response(registrations));
            }
        }
    }
}
