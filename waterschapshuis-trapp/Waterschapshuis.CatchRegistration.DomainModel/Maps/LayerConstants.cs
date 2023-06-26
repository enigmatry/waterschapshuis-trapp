namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public static class LayerConstants
    {
        public static class WorkspaceName
        {
            public const string V3 = "catch-registration-v3";
            public const string V2 = "catch-registration-v2";
        }

        public static class BaseUrl
        {
            public const string NationalRegister = "https://geodata.nationaalgeoregister.nl/tiles/service";
        }

        public static class OverlayLayerName {
            public const string Organizations = "Organizations";
            public const string Rayons = "Rayons";
            public const string CatchAreas = "CatchAreas";
            public const string WaterAuthorities = "WaterAuthorities";
            public const string SubAreas = "SubAreas";
            public const string HourSquares = "HourSquares";
            public const string Provinces = "Provinces";
            public const string Observations = "Observations";
            public const string TrackingLines = "TrackingLines";
            public const string TrackingsByUser = "TrackingsByUser";
            public const string TrackingsByTrappers = "TrackingsByTrappers";
            public const string TrapDetails = "TrapDetails";
            public const string SubAreaHourSquares = "SubAreaHourSquares";
            public const string SubAreaHourSquareCatches = "SubAreaHourSquareCatches";
            public const string HourSquareCatches = "HourSquareCatches";
            public const string SubAreaCatches = "SubAreaCatches";
            public const string WaterAuthorityCatches = "WaterAuthorityCatches";
            public const string RayonCatches = "RayonCatches";
            public const string ProvinceCatches = "ProvinceCatches";
            public const string OrganizationCatches = "OrganizationCatches";
            public const string CatchAreaCatches = "CatchAreaCatches";
            public const string HeatMapOfCatches = "HeatMapOfCatches";
            public const string StreetsAndWaterways = "lufolabels";
        }
    }
}
