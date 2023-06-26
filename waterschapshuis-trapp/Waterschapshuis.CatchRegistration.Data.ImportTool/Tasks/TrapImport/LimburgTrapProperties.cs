using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TrapImport
{
    public class LimburgTrapProperties : IProperties
    {
        [JsonProperty("CREATED_DATE")]
        public string DateCreated { get; set; }

        [JsonProperty("GLOBALID")]
        public string GlobalId { get; set; }

        [JsonProperty("OPM_VANGMIDDEL")]
        public string Remarks { get; set; }

        [JsonProperty("AANTAL_VANGMIDDELEN")]
        public int? Number { get; set; }

        [JsonProperty("SOORT_COMBINATIE")]
        public int? TrapType { get; set; }

        [JsonProperty("STATUS_CODE")]
        public short? Status { get; set; }

        [JsonProperty("BESTRIJDER_VANGMIDDEL")]
        public string User { get; set; }
    }
}
