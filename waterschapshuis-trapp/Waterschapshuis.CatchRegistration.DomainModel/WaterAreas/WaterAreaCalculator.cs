using System.Collections.Generic;
using System.Linq;
using NetTopologySuite.Precision;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Union;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.WaterAreas
{
    public class WaterAreaCalculator
    {
        private readonly Geometry _geometry;
        private readonly List<WaterPlane> _waterPlanes;
        private readonly List<WaterLine> _waterLines;
        private readonly GeometryPrecisionReducer _pm;

        private WaterAreaCalculator(Geometry geometry, List<WaterPlane> waterPlanes, List<WaterLine> waterLines)
        {
            _pm = new GeometryPrecisionReducer(new PrecisionModel(PrecisionModels.FloatingSingle));
            _geometry = geometry;
            _waterPlanes = waterPlanes;
            _waterLines = waterLines;
        }

        public static WaterAreaCalculator Create(
            Geometry geometry,
            List<WaterPlane> waterPlanes,
            List<WaterLine> waterLines)
        {
            return new WaterAreaCalculator(geometry, waterPlanes, waterLines);
        }

        public (double ditch, double wetDitch) Calculate()
        {
            var a1 = GetMergedWaterLines(TypeWater.DitchesAndDryDitches);
            var a2 = GetMergedWaterLines(TypeWater.LakesPuddlesAndChannels);
            var b1 = GetMergedPolygons(TypeWater.DitchesAndDryDitches);
            var b2 = GetMergedPolygons(TypeWater.LakesPuddlesAndChannels);

            var ditch = _geometry.Intersection(a1).Length + PolygonIntersectionLength(b1);
            var wetDitch = _geometry.Intersection(a2).Length + PolygonIntersectionLength(b2);

            return (ditch, wetDitch);
        }

        private Geometry GetMergedPolygons(string[] typesIncluded)
        {
            var planes = _waterPlanes
                .Where(x => typesIncluded.Contains(x.Type) && x.Geometry.IsValid)
                .Select(x => _pm.Reduce(x.Geometry))
                .ToList();

            var cpu = new CascadedPolygonUnion(planes);

            return cpu.Union() ?? Point.Empty;
        }

        private MultiLineString GetMergedWaterLines(string[] typesIncluded)
        {
            var lines = _waterLines
                .Where(x => typesIncluded.Contains(x.Type) && x.Geometry.IsValid)
                .Select(x => (LineString)_pm.Reduce(x.Geometry))
                .ToArray();

            return GeometryUtil.Factory.CreatePolyline(lines);
        }
        
        private double PolygonIntersectionLength(Geometry containedGeometry)
        {
            var geometriesIntersection = _geometry.Intersection(containedGeometry.Boundary);

            if (geometriesIntersection is LineString || geometriesIntersection is MultiLineString)
            {
                return _geometry.Boundary.Intersection(containedGeometry.Boundary) == geometriesIntersection 
                    ? 0 
                    : geometriesIntersection.Length;
            }
            return geometriesIntersection.Length;
        }

    }
}
