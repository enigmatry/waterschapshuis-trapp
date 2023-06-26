using System;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry
{
    [UsedImplicitly]
    public class Feature<TProperties, TGeometry>
        where TProperties : IProperties
        where TGeometry : IGeometry
    {
        [UsedImplicitly]
        public string Type { get; set; } = String.Empty;
        [UsedImplicitly]
        public TGeometry Geometry { get; set; }
        [UsedImplicitly]
        public TProperties Properties { get; set; }
    }
}
