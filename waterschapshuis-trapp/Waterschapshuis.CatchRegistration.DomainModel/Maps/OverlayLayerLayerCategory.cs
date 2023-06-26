using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public class OverlayLayerLayerCategory
    {
        public Guid OverlayLayerId { get; private set; } = Guid.Empty;
        public OverlayLayer OverlayLayer { get; private set; } = null!;
        public Guid LayerCategoryId { get; private set; } = Guid.Empty;
        public OverlayLayerCategory LayerCategory { get; private set; } = null!;
        public string DisplayName { get; private set; } = String.Empty;
        public int DisplayOrderIndex { get; private set; } = 0;
        public int DisplayZIndex { get; private set; } = 0;
        public string FilterName { get; private set; } = String.Empty;
        public string FilterValue { get; private set; } = String.Empty;
        public string GeometryFieldName { get; private set; } = String.Empty;
        public MapStyleLookupKeyCode? DefaultStyleLookupCode { get; private set; }
        public OverlayLayerLookupStrategy LookupStrategy { get; set; }
        public OverlayLayerCacheSettings CacheSettings { get; set; } = null!;

        public string Color { get; private set; } = String.Empty;
        public int Width { get; private set; }
        public string? ViewParamsValue { get; private set; } = String.Empty;
        public OverlayLayerPlatformType PlatformType { get; set; }

        public static OverlayLayerLayerCategory Create(
            OverlayLayer overlayLayer,
            OverlayLayerCategory layerCategory,
            string displayName,
            int displayOrderIndex,
            int displayZIndex = 0,
            string filterName = "",
            string filterValue = "",
            string geometryFieldName = "",
            OverlayLayerLookupStrategy lookupStrategy = OverlayLayerLookupStrategy.All,
            MapStyleLookupKeyCode? defaultStyleLookupCode = null,
            string color = "",
            OverlayLayerCacheSettings cacheSettings = null!,
            int width = 1,
            string viewParamsValue = "",
            OverlayLayerPlatformType platformType = OverlayLayerPlatformType.All)
        {
            var result = new OverlayLayerLayerCategory
            {
                OverlayLayer = overlayLayer,
                OverlayLayerId = overlayLayer.Id,
                LayerCategory = layerCategory,
                LayerCategoryId = layerCategory.Id,
                DisplayName = displayName,
                DisplayOrderIndex = displayOrderIndex,
                DisplayZIndex = displayZIndex == 0 ? displayOrderIndex + 1 : displayZIndex,
                FilterName = filterName,
                FilterValue = filterValue,
                GeometryFieldName = geometryFieldName,
                LookupStrategy = lookupStrategy,
                DefaultStyleLookupCode = defaultStyleLookupCode,
                Color = color,
                CacheSettings = cacheSettings,
                Width = width,
                ViewParamsValue = viewParamsValue,
                PlatformType = platformType
            };

            return result;
        }

        public string BuildLayerUrl(string baseUrl)
        {
            var layerTypeName = OverlayLayer.Type.ToString().ToLower();
            var layerFormat = OverlayLayer.Format.GetDisplayName();
            var layerRequest = OverlayLayer.Request.GetDisplayName();

            return
                $"{baseUrl}/" +
                (OverlayLayer.Type == OverlayLayerType.Wfs ? 
                ($"{OverlayLayer.Workspace}/{layerTypeName}?typeName={OverlayLayer.Workspace}:{OverlayLayer.Name}&outputFormat={layerFormat}")
                : $"{layerTypeName}?Service={OverlayLayer.Type}&Format={layerFormat}")+
                $"&request={layerRequest}" 
                + (String.IsNullOrWhiteSpace(FilterValue) ? "" : $"&CQL_FILTER={FilterValue}")
                + (String.IsNullOrWhiteSpace(ViewParamsValue) ? "" : $"&viewparams={ViewParamsValue}");
        }

        public string BuildFullName() => $"{OverlayLayer.Workspace}:{OverlayLayer.Name}"
            + (String.IsNullOrWhiteSpace(FilterName)? "" : $":{FilterName}");
    }
}
