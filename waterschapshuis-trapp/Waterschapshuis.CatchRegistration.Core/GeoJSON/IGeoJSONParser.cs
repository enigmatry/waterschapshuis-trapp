using Waterschapshuis.CatchRegistration.Core.GeoJSON.Models;

namespace Waterschapshuis.CatchRegistration.Core.GeoJSON
{
    public interface IGeoJsonParser
    {
        FeatureCollection Parse(string fileName);
    }
}
