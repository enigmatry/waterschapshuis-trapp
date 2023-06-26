using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public class BackgroundLayer
    {
        public string Name { get; protected set; } = String.Empty;
        public string Id { get; protected set; } = String.Empty;
        public string Url { get; protected set; } = String.Empty;
        public MapServiceType ServiceType { get; protected set; }
        public MapNetworkType NetworkType { get; protected set; }
        public string? DefaultOverlayLayer { get; private set; }

        public static IEnumerable<BackgroundLayer> CreateSampleLayers()
        {
            var result = new List<BackgroundLayer>
            {
                Create("standaard", "Top10 NL", "https://service.pdok.nl/brt/achtergrondkaart/wmts/v2_0?request=getcapabilities&service=wmts", MapServiceType.Wmts, MapNetworkType.Online),
                Create("Actueel_ortho25", "Luchtfoto's", "https://service.pdok.nl/hwh/luchtfotorgb/wmts/v1_0?&request=GetCapabilities&service=wmts", MapServiceType.Wmts, MapNetworkType.Online),
                Create("topoplus", "OpenTopo", "https://topoplus_hwh_dtfbtjx2w2khnsxo7ljn.omgevingsserver.nl/topoplus/map/wmts/1.0.0/WMTSCapabilities.xml", MapServiceType.Wmts, MapNetworkType.Online),
                Create("osm", "Offline OSM", "", MapServiceType.Mvt, MapNetworkType.Offline)
            };
            return result;
        }

        private static BackgroundLayer Create(string id, string name, string url, MapServiceType serviceType, MapNetworkType networkType, string defaultOverlayLayer = null!)
        {
           return new BackgroundLayer
           {
               Id = id,
               Name = name,
               Url = url,
               ServiceType = serviceType,
               NetworkType = networkType,
               DefaultOverlayLayer = defaultOverlayLayer
           };
        }
    }
}
