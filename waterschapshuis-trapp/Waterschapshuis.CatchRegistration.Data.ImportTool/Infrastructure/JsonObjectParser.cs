using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure
{
    public static class JsonObjectParser
    {
        public static async Task<IEnumerable<JToken>> ReadJTokenChildrenAtPath(string filePath, string[] jsonPath)
        {
            using var streamReader = File.OpenText(filePath);
            using JsonReader jsonReader = new JsonTextReader(streamReader);
            var jObject = await JObject.LoadAsync(jsonReader);

            return jObject[String.Join(':', jsonPath)].Children();
        }
    }
}
