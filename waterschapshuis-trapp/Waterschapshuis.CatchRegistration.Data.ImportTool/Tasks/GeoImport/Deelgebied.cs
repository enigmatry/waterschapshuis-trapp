using System;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.GeoImport
{
    [UsedImplicitly]
    public class Deelgebied : GeometryContainer
    {
        public long Id { get; set; }
        public string Name { get; } = String.Empty;
        public long WaterAuthorityId { get; } = 0;
        public long CatchAreaId { get; } = 0;
    }
}
