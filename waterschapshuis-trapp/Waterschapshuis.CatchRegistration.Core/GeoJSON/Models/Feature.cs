using System;

namespace Waterschapshuis.CatchRegistration.Core.GeoJSON.Models
{
    public class Feature
    {
        public string Type { get; set; } = String.Empty;
        public Geometry Geometry { get; set; } = Geometry.Create();
        public Properties Properties { get; set; } = Properties.Create();
    }
}
