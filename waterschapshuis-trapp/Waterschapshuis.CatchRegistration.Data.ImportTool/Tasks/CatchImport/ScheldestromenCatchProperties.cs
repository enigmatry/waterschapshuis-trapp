using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport
{
    public class ScheldestromenCatchProperties : IProperties
    {
        [JsonProperty("OBJECTID")]
        public virtual long? Id { get; set; }

        [JsonProperty("Vangstdatum")]
        public string DateCreated { get; set; }

        [JsonProperty("Aantal")]
        public int? Number { get; set; }

        [JsonProperty("Soort")]
        public int? CatchType { get; set; }

        [JsonProperty("Bestrijder")]
        public string User { get; set; }

        [JsonProperty("intVangmiddel")]
        public int? TrapType { get; set; }
    }
}
