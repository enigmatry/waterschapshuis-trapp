using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Common
{
    public static class GeometryUtil
    {
        public static readonly GeometryFactory Factory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 28992);

        public static Point CreatePoint(this GeometryFactory factory, double longitude, double latitude) =>
            factory.CreatePoint(new Coordinate(longitude, latitude));

        public static LineString CreateLine(this GeometryFactory factory, Coordinate[] coordinates) =>
            factory.CreateLineString(coordinates);

        public static MultiLineString CreatePolyline(this GeometryFactory factory, LineString[] lineStrings) =>
            factory.CreateMultiLineString(lineStrings);

        public static Geometry CreatePolygon(this GeometryFactory factory, Polygon polygon) =>
            factory.CreatePolygon(polygon.Coordinates);

        public static Geometry CreateMultiPolygon(this GeometryFactory factory, List<Polygon> polygons) =>
            factory.CreateMultiPolygon(polygons.ToArray());

        public static Geometry CreateRectangle(this GeometryFactory factory, double x, double y, double distance) =>
            factory.CreatePolygon(GetRectangleCoordinates(x, y, distance));

        public static bool IsValidPolygonOrMultiPolygon(this Geometry geometry)
            => geometry.IsValidPolygon() || geometry.IsValidMultiPolygon();

        public static bool IsValidPolygon(this Geometry geometry)
            => geometry.IsValid && geometry.GetType() == typeof(Polygon);

        public static bool IsGeometryCollection(this Geometry geometry)
            => geometry.GetType() == typeof(GeometryCollection);

        /// <summary>
        /// Tries to extract valid polygon (or multi-polygon) from geometry collection.
        /// Returns null otherwise.
        /// </summary>
        public static Geometry? TryExtractPolygonOrMultiPolygon(this GeometryCollection geometryCollection)
        {
            var validPolygons = geometryCollection.Geometries
                .Where(geometry => geometry.IsValidPolygon())
                .Select(geometry => (Polygon)geometry).ToList();

            if (!validPolygons.Any())
                return null;

            return validPolygons.Count == 1
                ? Factory.CreatePolygon(validPolygons.Single())
                : Factory.CreateMultiPolygon(validPolygons);
        }

        /// <summary>
        /// Tries to extract list of valid polygons from geometry of type Polygon/MultiPolygon.
        /// Returns empty list otherwise.
        /// </summary>
        public static List<Polygon> TryExtractPolygons(this Geometry geometry)
        {
            if (!geometry.IsValidPolygonOrMultiPolygon())
                return new List<Polygon>();

            return geometry.IsValidPolygon()
                ? new List<Polygon> { (Polygon)geometry }
                : ((MultiPolygon)geometry).Geometries.Select(x => (Polygon)x).ToList();
        }

        /// <summary>
        /// Tries to remove sliver from geometry of type Polygon/MultiPolygon.  
        /// Returns null otherwise. 
        /// </summary>
        public static Geometry? TryRemoveSliver(this Geometry geometry)
        {
            if (!geometry.IsValidPolygonOrMultiPolygon())
                return null;

            if (geometry.IsValidPolygon())
                return RemoveSliver((Polygon)geometry);

            return RemoveSliver((MultiPolygon)geometry);
        }




        private static bool IsValidMultiPolygon(this Geometry geometry)
            => geometry.IsValid && geometry.GetType() == typeof(MultiPolygon);

        private static Polygon RemoveSliver(this Polygon polygon)
        {
            var exteriorRing = (LinearRing)polygon.ExteriorRing;
            var interiorRings = polygon.InteriorRings
                .Cast<LinearRing>()
                .Where(interiorRing => interiorRing.IsValidPolygon()).ToArray();
            return Factory.CreatePolygon(exteriorRing, interiorRings);
        }

        private static MultiPolygon RemoveSliver(this MultiPolygon union)
        {
            var polygonsWithoutSlivers = union
                .Geometries
                .Select(geometry => RemoveSliver((Polygon)geometry))
                .ToList();
            return (MultiPolygon)Factory.CreateMultiPolygon(polygonsWithoutSlivers);
        }

        private static Coordinate[] GetRectangleCoordinates(double x, double y, double distance) =>
            new List<Coordinate>
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
