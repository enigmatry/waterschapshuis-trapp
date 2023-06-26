using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.GeoImport
{
    [UsedImplicitly]
    public class DeelgebiedUurhok : GeometryContainer
    {
        public long SubAreaId { get; set; }
        public long HourSquareId { get; set; }
        public long KmWaterway { get; } = 0;
        public long PercentageDitch { get; } = 0;
        public double Ditch { get; } = 0.0;
        public double WetDitch { get; } = 0.0;
    }
}
