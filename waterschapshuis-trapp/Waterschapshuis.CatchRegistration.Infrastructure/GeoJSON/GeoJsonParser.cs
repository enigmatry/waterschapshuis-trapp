using Newtonsoft.Json;
using System;
using System.IO;
using Waterschapshuis.CatchRegistration.Core.GeoJSON;
using Waterschapshuis.CatchRegistration.Core.GeoJSON.Models;

namespace Waterschapshuis.CatchRegistration.Infrastructure.GeoJSON
{
    public class GeoJsonParser : IGeoJsonParser, IDisposable
    {
        public FeatureCollection Parse(string fileName)
        {
            var data = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<FeatureCollection>(data);
        }

        public void Dispose()
        {
            
        }
    }
}
