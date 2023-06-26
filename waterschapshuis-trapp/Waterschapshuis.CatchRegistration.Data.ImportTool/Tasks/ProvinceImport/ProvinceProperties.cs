using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.ProvinceImport
{
    [UsedImplicitly]
    public class ProvinceProperties : IProperties
    {
        [UsedImplicitly]
        [JsonProperty("Provincienaam")]
        public string Name { get; set; } = String.Empty;
    }
}
