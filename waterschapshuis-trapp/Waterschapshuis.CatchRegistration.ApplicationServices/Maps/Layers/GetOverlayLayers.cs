using System;
using System.Collections.Generic;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Maps;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Layers
{
    public static partial class GetOverlayLayers
    {
        [PublicAPI]
        public class Query : IRequest<ListResponse<ResponseItem>>
        {
            /// <summary>
            /// Category of layer
            /// </summary>
            public IEnumerable<OverlayLayerCategoryCode> OrderedLayerCategoryCodes { get; set; } = new List<OverlayLayerCategoryCode>();

            /// <summary>
            /// Specific layers need year on which they get data
            /// </summary>
            public int Year { get; set; } = DateTimeOffset.Now.Year;

            /// <summary>
            /// Maximum years go back in history
            /// </summary>
            public int NumberOfYears { get; set; } = 5;
        }

        [PublicAPI]
        public class ResponseItem
        {
            /// <summary>
            /// Technical name of layer
            /// </summary>
            public string Name { get; set; } = String.Empty;
            /// <summary>
            /// Full name
            /// </summary>
            public string FullName { get; set; } = String.Empty;

            /// <summary>
            /// Name on screen for this layer
            /// </summary>
            public string DisplayName { get; set; } = String.Empty;

            /// <summary>
            /// Category code
            /// </summary>
            public OverlayLayerCategoryCode CategoryCode { get; set; }

            /// <summary>
            /// Category name
            /// </summary>
            public string CategoryDisplayName { get; set; } = String.Empty;

            /// <summary>
            /// Url where layer can be retrieved from
            /// </summary>
            public string Url { get; set; } = String.Empty;

            /// <summary>
            /// Indicator for layers stacking on eachother
            /// </summary>
            public int DisplayZIndex { get; set; }

            /// <summary>
            /// Color to be used for drawing layer
            /// </summary>
            public string Color { get; set; } = String.Empty;

            /// <summary>
            /// Geometry name
            /// </summary>
            public string GeometryFieldName { get; set; } = String.Empty;

            /// <summary>
            /// Strategy for showing layers on screen
            /// </summary>
            public OverlayLayerLookupStrategy LookupStrategy { get; set; }

            /// <summary>
            /// Lookup codes to be used for different maps
            /// </summary>
            public MapStyleLookupKeyCode DefaultMapStyle { get; set; } = null!;

            /// <summary>
            /// How long items should be cached
            /// </summary>
            public OverlayLayerCacheSettings CacheSettings { get; set; } = null!;

            /// <summary>
            /// Width on screen for this layer
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// Indicator whether it is used on backoffice, mobile or both
            /// </summary>
            public OverlayLayerPlatformType PlatformType { get; set; }

            /// <summary>
            /// Overlay type (wms, wfs, wmts)
            /// </summary>
            public OverlayLayerType Type { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() =>
                CreateMap<OverlayLayerLayerCategory, ResponseItem>()
                    .ForMember(dest => dest.FullName, opt => opt.Ignore())
                    .ForMember(dest => dest.Url, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => src.LayerCategory.Code))
                    .ForMember(dest => dest.CategoryDisplayName, opt => opt.MapFrom(src => src.LayerCategory.DisplayName))
                    .ForMember(dest => dest.DefaultMapStyle, opt => opt.MapFrom(src => src.DefaultStyleLookupCode ?? MapStyleLookupKeyCode.TrapType));
        }
    }
}
