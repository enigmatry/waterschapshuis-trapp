using System;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.GeoImport
{
    [UsedImplicitly]
    public class Uurhok : GeometryContainer
    {
        public long Id { get; set; }
        public string Name { get; } = String.Empty;
        public int Type { get; } = 0;
    }
}
