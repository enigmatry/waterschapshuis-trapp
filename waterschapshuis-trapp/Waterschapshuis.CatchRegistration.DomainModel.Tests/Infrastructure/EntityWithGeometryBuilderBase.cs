using System.Collections.Generic;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure
{
    public abstract class EntityWithGeometryBuilderBase
    {
        private const double CoordinatePointsDistance = 2000;
        protected Geometry _geometry = Polygon.Empty;

        public static Geometry CreatePolygon(Coordinate[] coordinates)
        {
            return GeometryUtil.Factory.CreatePolygon(coordinates);
        }

        public static Geometry CreateRectangle(double x, double y, double distance = CoordinatePointsDistance)
        {
            return CreatePolygon(GetRectangleCoordinates(x, y, distance));
        }

        public void SetGeometry(Geometry value)
        {
            _geometry = value;
        }

        private static Coordinate[] GetRectangleCoordinates(double x, double y, double distance)
        {
            return new List<Coordinate>
                {
                    new Coordinate(x - distance, y - distance),
                    new Coordinate(x + distance, y - distance),
                    new Coordinate(x + distance, y + distance),
                    new Coordinate(x - distance, y + distance),
                    new Coordinate(x - distance, y - distance)
                }
                .ToArray();
        }
    }
}
