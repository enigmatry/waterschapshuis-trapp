using Newtonsoft.Json;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry
{
    public abstract class GeometryContainer
    {
        // Ignore serialization during logging.
        [JsonIgnore]
        public NetTopologySuite.Geometries.Geometry Geometry { get; private set; } = 
            NetTopologySuite.Geometries.Polygon.Empty;
    }
}
