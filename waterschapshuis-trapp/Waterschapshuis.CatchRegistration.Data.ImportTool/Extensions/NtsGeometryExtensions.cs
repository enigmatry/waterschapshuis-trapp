namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class NtsGeometryExtensions
    {
        public static NetTopologySuite.Geometries.Geometry GetValidatedBuffer(this NetTopologySuite.Geometries.Geometry geometry)
        {
            return geometry.IsValid ? geometry : geometry.Buffer(0);
        }
    }
}
