using System;
using System.Linq;
using JetBrains.Annotations;
using NetTopologySuite.Geometries;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry
{
    [UsedImplicitly]
    public class Polygon : IGeometry
    {
        public double[][][] Coordinates { get; set; } = Array.Empty<double[][]>();

        internal Coordinate[][] GetNtsCoordinates()
        {
            return Coordinates.Select(x =>
                    x.Select(coordinate =>
                            new Coordinate(coordinate[0], coordinate[1]))
                        .ToArray())
                .ToArray();
        }
    }
}
