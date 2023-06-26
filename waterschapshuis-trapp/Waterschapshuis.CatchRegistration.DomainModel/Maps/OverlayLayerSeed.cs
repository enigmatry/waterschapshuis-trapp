using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public class OverlayLayerSeed
    {
        private const string 
            RedColor = "#cc0000",
            BlueColor = "#247AAB",
            GreenColor = "#3B9C14",
            PurpleColor = "#a64d79",
            BlackColor = "#2C3539",
            PinkColor = "#ff0066";

        private Dictionary<string, OverlayLayer> _overlayLayers = null!;
        private Dictionary<OverlayLayerCategoryCode, OverlayLayerCategory> _overlayLayersCategories = null!;

        public IEnumerable<OverlayLayerLayerCategory> Build(int currentYear, int numberOfYears, Guid userId)
        {
            _overlayLayersCategories = BuildCategories().ToDictionary(x => x.Code, x => x);
            _overlayLayers = BuildLayers().ToDictionary(x => x.Name, x => x);

            var startZIndex = 1;

            var staticLayers = CreateMapAreaLayers();
            startZIndex += staticLayers.Count;

            var defaultLayers = CreateDefaultLayers(startZIndex);
            startZIndex += defaultLayers.Count;

            var trapDetailYearLayers = CreateTrapDetailsYearLayers(startZIndex, currentYear, numberOfYears);
            startZIndex += trapDetailYearLayers.Count;

            var observationLayers = CreateObservationLayers(startZIndex, trapDetailYearLayers.Count);
            startZIndex += observationLayers.Count;

            var trackingLinesMapLayers = CreateTrackingLinesMapLayers(currentYear, startZIndex,
                trapDetailYearLayers.Count + observationLayers.Count, userId);

            var reportLayers = CreateReportLayers();

            return staticLayers.Concat(defaultLayers).Concat(trapDetailYearLayers).Concat(observationLayers).Concat(trackingLinesMapLayers).Concat(reportLayers);
        }

        private IEnumerable<OverlayLayerLayerCategory> CreateReportLayers()
        {
            return new List<OverlayLayerLayerCategory>
            {
                // REPORT TRACKING:
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.TrackingLines),
                    FindLayerCategory(OverlayLayerCategoryCode.ReportTracking),
                    "Alles",
                    0, 1, color: "#ff0066"),
                // REPORT GEO REGION CATCHES:
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.SubAreaHourSquareCatches),
                    FindLayerCategory(OverlayLayerCategoryCode.ReportGeoRegionCatches),
                    "Deelgebied-uurhok",
                    0, 1),
                OverlayLayerLayerCategory.Create(
                   FindLayer(LayerConstants.OverlayLayerName.HourSquareCatches),
                   FindLayerCategory(OverlayLayerCategoryCode.ReportGeoRegionCatches),
                   "Uurhok",
                   1, 1),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.SubAreaCatches),
                    FindLayerCategory(OverlayLayerCategoryCode.ReportGeoRegionCatches),
                    "Deelgebied",
                    2, 1),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.CatchAreaCatches),
                    FindLayerCategory(OverlayLayerCategoryCode.ReportGeoRegionCatches),
                    "Vanggebied",
                    3, 1),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.RayonCatches),
                    FindLayerCategory(OverlayLayerCategoryCode.ReportGeoRegionCatches),
                    "Rayon",
                    4, 1),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.OrganizationCatches),
                    FindLayerCategory(OverlayLayerCategoryCode.ReportGeoRegionCatches),
                    "Organisatie",
                    5, 1),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.WaterAuthorityCatches),
                    FindLayerCategory(OverlayLayerCategoryCode.ReportGeoRegionCatches),
                    "Waterschap",
                    6, 1),
                // HEAT MAP REPORT
                 OverlayLayerLayerCategory.Create(FindLayer(LayerConstants.OverlayLayerName.HeatMapOfCatches),
                     FindLayerCategory(OverlayLayerCategoryCode.HeatMapOfCatches),
                     "HeatMapOfCatches",
                     7,1)
            };

        }

        private IEnumerable<OverlayLayerLayerCategory> CreateTrackingLinesMapLayers(in int currentYear, int startZIndex, int startDisplayOrder, Guid userId)
        {
            return new List<OverlayLayerLayerCategory>
            {
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.TrackingLines),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    "Speurkaart",
                    startDisplayOrder + 1, 
                    startZIndex + 1,
                    filterName: "CurrentYear", 
                    filterValue: $"CreatedOnYear={currentYear}",
                    color: PinkColor),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.TrackingsByUser),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    "Speurkaart persoonlijk",
                    startDisplayOrder + 2, 
                    startZIndex + 2,
                    filterName: "CurrentUser", 
                    viewParamsValue: $"userId:{userId}",
                    defaultStyleLookupCode: MapStyleLookupKeyCode.UserTracking,
                    platformType: OverlayLayerPlatformType.MobileOnly),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.TrackingsByTrappers),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    "Speurkaart aktueel",
                    startDisplayOrder + 3, 
                    startZIndex + 3,
                    filterName: "OtherUsers", 
                    filterValue: $"CreatedById<>'{userId}'",
                    defaultStyleLookupCode: MapStyleLookupKeyCode.TrappersTracking,
                    platformType: OverlayLayerPlatformType.MobileOnly,
                    geometryFieldName: "Location",
                    lookupStrategy: OverlayLayerLookupStrategy.Tracking)
            };
        }

        private List<OverlayLayerLayerCategory> CreateObservationLayers(int startZIndex, int startDisplayOrder)
        {
            return new List<OverlayLayerLayerCategory>
            {
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.Observations),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    "Actuele meldingen",
                    startDisplayOrder + 1, 
                    startZIndex + 1,
                    filterName: "Active", 
                    filterValue: "Archived=0",
                    geometryFieldName: "Location", 
                    lookupStrategy:OverlayLayerLookupStrategy.BBox, 
                    defaultStyleLookupCode: MapStyleLookupKeyCode.ObservationLocation),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.Observations),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    "Gearchiveerde meldingen",
                    startDisplayOrder + 2, 
                    startZIndex + 2,
                    filterName: "Inactive", 
                    filterValue: "Archived=1",
                    geometryFieldName: "Location", 
                    lookupStrategy:OverlayLayerLookupStrategy.BBox, 
                    defaultStyleLookupCode: MapStyleLookupKeyCode.ArchivedObservationLocation)
            };
        }

        private List<OverlayLayerLayerCategory> CreateTrapDetailsYearLayers(int startZIndex, in int currentYear, in int numberOfYears)
        {
            var zIndex = startZIndex + numberOfYears;
            var trapDetailsLayers = new List<OverlayLayerLayerCategory>
            {
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.TrapDetails),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    "Vangend/geplaatst vangmiddel",
                    0, zIndex + (int)OverlayLayerDisplayZIndex.ActiveTraps,
                    filterName: $"TrapCreatedYear{currentYear}Active",
                    viewParamsValue: $"status:{(int)TrapStatus.Catching};showPastYearCatchesOnly:1;",
                    geometryFieldName: "Location", lookupStrategy:OverlayLayerLookupStrategy.BBox, defaultStyleLookupCode: MapStyleLookupKeyCode.TrapType),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.TrapDetails),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    "Niet-vangend vangmiddel",
                    1, zIndex + (int)OverlayLayerDisplayZIndex.InactiveTraps,
                    filterName: $"TrapCreatedYear{currentYear}Inactive",
                    viewParamsValue: $"status:{(int)TrapStatus.NotCatching};",
                    geometryFieldName: "Location", lookupStrategy:OverlayLayerLookupStrategy.BBox, defaultStyleLookupCode: MapStyleLookupKeyCode.TrapType),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.TrapDetails),
                    FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                    $"Verwijderd vangm. {currentYear}",
                    2, zIndex + (int)OverlayLayerDisplayZIndex.RemovedTraps,
                    filterName: $"TrapUpdatedYear{currentYear}Removed",
                    viewParamsValue: $"status:{(int)TrapStatus.Removed};trapUpdatedYear:{currentYear};",
                    geometryFieldName:"Location", lookupStrategy:OverlayLayerLookupStrategy.BBox, defaultStyleLookupCode: MapStyleLookupKeyCode.TrapType)
            };

            for (int i = 1; i <= numberOfYears; i++)
            {
                trapDetailsLayers.Add(
                    OverlayLayerLayerCategory.Create(
                        FindLayer(LayerConstants.OverlayLayerName.TrapDetails),
                        FindLayerCategory(OverlayLayerCategoryCode.MapLocations),
                        $"Verwijderd vangm. {currentYear - i}",
                        2 + i, zIndex + (int)OverlayLayerDisplayZIndex.RemovedTraps - i,
                        filterName: $"TrapUpdatedYear{currentYear - i}",
                        viewParamsValue: $"status:{(int)TrapStatus.Removed};trapUpdatedYear:{currentYear - i};",
                        geometryFieldName: "Location", lookupStrategy: OverlayLayerLookupStrategy.BBox, defaultStyleLookupCode: MapStyleLookupKeyCode.TrapType,
                        cacheSettings: OverlayLayerCacheSettings.CacheForDays(30)));
            }

            return trapDetailsLayers;
        }

        private OverlayLayer FindLayer(string name)
        {
            return _overlayLayers[name];
        }

        private OverlayLayerCategory FindLayerCategory(OverlayLayerCategoryCode code)
        {
            return _overlayLayersCategories[code];
        }

        private List<OverlayLayerLayerCategory> CreateMapAreaLayers()
        {
            return new List<OverlayLayerLayerCategory>
            {
                // MAP AREAS:
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.HourSquares),
                    FindLayerCategory(OverlayLayerCategoryCode.MapAreas),
                    "Uurhokken",
                    0, 1, color: RedColor, width: 1, cacheSettings: OverlayLayerCacheSettings.CacheForDays(180)),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.SubAreas),
                    FindLayerCategory(OverlayLayerCategoryCode.MapAreas),
                    "Deelgebieden",
                    1, 2, color: RedColor, width: 4, cacheSettings: OverlayLayerCacheSettings.CacheForDays(180)),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.CatchAreas),
                    FindLayerCategory(OverlayLayerCategoryCode.MapAreas),
                    "Vanggebieden",
                    2, 3, color: GreenColor, width: 5, cacheSettings: OverlayLayerCacheSettings.CacheForDays(180)),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.Rayons),
                    FindLayerCategory(OverlayLayerCategoryCode.MapAreas),
                    "Rayons",
                    3, 4, color: PurpleColor, width: 6, cacheSettings: OverlayLayerCacheSettings.CacheForDays(180)),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.WaterAuthorities),
                    FindLayerCategory(OverlayLayerCategoryCode.MapAreas),
                    "Waterschap",
                    4, 5, color: BlueColor, width: 6, cacheSettings: OverlayLayerCacheSettings.CacheForDays(180)),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.Organizations),
                    FindLayerCategory(OverlayLayerCategoryCode.MapAreas),
                    "Organisatie",
                    5, 6, color: BlackColor, width: 2, cacheSettings: OverlayLayerCacheSettings.CacheForDays(180)),
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.Provinces),
                    FindLayerCategory(OverlayLayerCategoryCode.MapAreas),
                    "Provincie",
                    6, 7, color: BlackColor, width: 6, cacheSettings: OverlayLayerCacheSettings.CacheForDays(180))
            };
        }

        private List<OverlayLayerLayerCategory> CreateDefaultLayers(int startZIndex)
        {
            // Default overlay layers that are shown together with set background layer
            return new List<OverlayLayerLayerCategory>
            { 
                OverlayLayerLayerCategory.Create(
                    FindLayer(LayerConstants.OverlayLayerName.StreetsAndWaterways),
                    FindLayerCategory(OverlayLayerCategoryCode.DefaultLayers),
                    "StreetsAndWaterways",
                    startZIndex + 1, startZIndex + 1)
            };
        }

        private static IEnumerable<OverlayLayer> BuildLayers() =>
            new List<OverlayLayer>
            {
                OverlayLayer.Create(LayerConstants.OverlayLayerName.Organizations),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.Rayons),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.CatchAreas),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.WaterAuthorities),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.SubAreas),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.HourSquares),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.Provinces),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.Observations),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.TrackingLines),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.TrackingsByUser),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.TrackingsByTrappers),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.TrapDetails),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.SubAreaHourSquareCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.SubAreaCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.CatchAreaCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.RayonCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.OrganizationCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.ProvinceCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.WaterAuthorityCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.HourSquareCatches),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.HeatMapOfCatches, type:OverlayLayerType.Wms, format:OverlayLayerFormat.Image, request:OverlayLayerRequest.Image),
                OverlayLayer.Create(LayerConstants.OverlayLayerName.StreetsAndWaterways, type:OverlayLayerType.Wmts, format:OverlayLayerFormat.Image, request:OverlayLayerRequest.Tile, workspace: String.Empty)

            };


        private static IEnumerable<OverlayLayerCategory> BuildCategories() =>
            new List<OverlayLayerCategory>
            {
                OverlayLayerCategory.Create(OverlayLayerCategoryCode.MapAreas, "GEBIEDEN"),
                OverlayLayerCategory.Create(OverlayLayerCategoryCode.MapLocations, "LOCATIES"),
                OverlayLayerCategory.Create(OverlayLayerCategoryCode.ReportTracking, "Speurkaart"),
                OverlayLayerCategory.Create(OverlayLayerCategoryCode.ReportGeoRegionCatches, "Vangsten op kaart"),
                OverlayLayerCategory.Create(OverlayLayerCategoryCode.HeatMapOfCatches, "Heatmap"),
                OverlayLayerCategory.Create(OverlayLayerCategoryCode.DefaultLayers, "NationalGeoRegister")
            };
    }
}
