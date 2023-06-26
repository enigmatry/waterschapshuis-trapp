using System;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry
{
    [UsedImplicitly]
    public class MultiPolygon : IGeometry
    {
        public Polygon[] Polygons { get; set; } = Array.Empty<Polygon>();
    }
}
