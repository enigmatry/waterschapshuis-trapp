using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Maps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Layers
{
    public static partial class GetBackgroundLayers
    {
        [PublicAPI]
        public class Query : IRequest<ListResponse<ResponseItem>>
        {
        }

        [PublicAPI]
        public class ResponseItem
        {
            /// <summary>
            /// Id of the layer
            /// </summary>
            public string Id { get; set; } = String.Empty;

            /// <summary>
            /// Name of the layer
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// URL where background layer is retrieved from
            /// </summary>
            public string Url { get; set; } = String.Empty;

            /// <summary>
            /// Service type (wmts, mvt)
            /// </summary>
            public MapServiceType ServiceType { get; set; }

            /// <summary>
            /// Network type online of offline
            /// </summary>
            public MapNetworkType NetworkType { get; set; }

            /// <summary>
            /// Overlay type (wms, wfs, wmts)
            /// </summary>
            public string? DefaultOverlayLayer { get; set; } = String.Empty;

        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<BackgroundLayer, ResponseItem>();
        }
    }
}
