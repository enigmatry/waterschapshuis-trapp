using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Maps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Layers
{
    public partial class GetBackgroundLayers
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, ListResponse<ResponseItem>>
        {
            private readonly IMapper _mapper;

            public RequestHandler(IMapper mapper)
            {
                _mapper = mapper;
            }

            public Task<ListResponse<ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(
                    BackgroundLayer.CreateSampleLayers()
                        .AsQueryable()
                        .ProjectToListResponse<BackgroundLayer, ResponseItem>(_mapper)
                    );
            }
        }
    }
}
