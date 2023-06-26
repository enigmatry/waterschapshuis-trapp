using NetTopologySuite.Geometries;
using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.WaterAreas
{
    public class WaterPlane : Entity<Guid>
    {
        public long LocalId { get; private set; }
        public Polygon Geometry { get; private set; } = Polygon.Empty;
        public string Type { get; private set; } = String.Empty;


        public static WaterPlane Create()
        {
            return new WaterPlane();
        }

        public static WaterPlane Create(Polygon geometry)
        {
            return new WaterPlane(){Geometry = geometry};
        }

        public WaterPlane WithType(string type)
        {
            Type = type;
            return this;
        }

        public WaterPlane WithGeometry(double x, double y, double distance)
        {
            Geometry = (Polygon)GeometryUtil.Factory.CreateRectangle(x, y, distance);
            return this;
        }
    }
}
