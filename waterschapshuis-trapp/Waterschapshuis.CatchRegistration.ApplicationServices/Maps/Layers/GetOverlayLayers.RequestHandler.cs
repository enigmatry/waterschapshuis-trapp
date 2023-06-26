using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Maps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Layers
{
    [UsedImplicitly]
    public static partial class GetOverlayLayers
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, ListResponse<ResponseItem>>
        {
            private readonly IMapper _mapper;
            private readonly GeoServerSettings _geoServerSettings;
            private readonly ICurrentUserProvider _currentUserProvider;

            public RequestHandler(IMapper mapper,
                GeoServerSettings geoServerSettings,
                ICurrentUserProvider currentUserProvider)
            {
                _mapper = mapper;
                _geoServerSettings = geoServerSettings;
                _currentUserProvider = currentUserProvider;
            }

            public Task<ListResponse<ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                var responseItems = new List<ResponseItem>();
                var userId = _currentUserProvider.UserId.GetValueOrDefault();
                var layersInCategories =
                    new OverlayLayerSeed().Build(request.Year, request.NumberOfYears, userId)
                        .ToList();

                foreach (var categoryCode in request.OrderedLayerCategoryCodes)
                {
                    var orderedLayersOfCategory = layersInCategories
                        .Where(x => x.LayerCategory.Code == categoryCode)
                        .OrderBy(x => x.DisplayOrderIndex)
                        .Select(x => CreateResponseItem(x, _mapper, _geoServerSettings));
                    responseItems.AddRange(orderedLayersOfCategory);
                }

                return Task.FromResult(ListResponse<ResponseItem>.Create(responseItems));
            }

            private ResponseItem CreateResponseItem(OverlayLayerLayerCategory layer, IMapper mapper, GeoServerSettings geoServerSettings)
            {
                var response = mapper.Map<ResponseItem>(layer);
                response.Url = layer.BuildLayerUrl(layer.OverlayLayer.Type == OverlayLayerType.Wmts ? LayerConstants.BaseUrl.NationalRegister : geoServerSettings.Url);
                response.FullName = layer.BuildFullName();
                response.Name = layer.OverlayLayer.Name;
                response.Type = layer.OverlayLayer.Type;
                return response;
            }
        }
    }
}
