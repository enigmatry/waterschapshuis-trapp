using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Areas
{
    public static partial class GetLocationAreaDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IMapper _mapper;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            public RequestHandler(
                IMapper mapper,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _mapper = mapper;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _currentVersionRegionalLayoutService
                    .QuerySubAreaHourSquaresNoTracking()
                    .QueryByLongAndLat(request.Longitude, request.Latitude)
                    .ProjectToFirstOrDefault<SubAreaHourSquare, Response>(_mapper, cancellationToken);
            }
        }
    }
}
