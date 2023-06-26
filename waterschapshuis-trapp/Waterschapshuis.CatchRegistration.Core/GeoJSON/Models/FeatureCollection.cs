using System;
using System.Collections.Generic;

namespace Waterschapshuis.CatchRegistration.Core.GeoJSON.Models
{
    public class FeatureCollection
    {
        public string Type { get; set; } = String.Empty;
        public List<Feature> Features { get; set; } = new List<Feature>();
    }
}
