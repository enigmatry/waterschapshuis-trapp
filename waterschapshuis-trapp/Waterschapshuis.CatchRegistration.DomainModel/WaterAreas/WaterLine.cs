using NetTopologySuite.Geometries;
using System;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.WaterAreas
{
    public class WaterLine: Entity<Guid>
    {
        public long LocalId { get; private set; }
        public LineString Geometry { get; private set; } = LineString.Empty;
        public string Type { get; private set; } = String.Empty;


        public static WaterLine Create(LineString geometry)
        {
            return new WaterLine() { Geometry = geometry };
        }

        public WaterLine WithType(string type)
        {
            Type = type;
            return this;
        }
    }
}
