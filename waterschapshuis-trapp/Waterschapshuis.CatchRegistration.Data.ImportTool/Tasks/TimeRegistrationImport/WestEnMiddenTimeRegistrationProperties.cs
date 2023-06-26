using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport
{
    [UsedImplicitly]
    public class WestEnMiddenTimeRegistrationProperties
    {
        [JsonProperty("UURHOK")] public string HourSquareName { get; set; }
        [JsonProperty("DEELGEBIED")] public string SubAreaName { get; set; }
        [JsonProperty("VELDUREN_MUSKUSRAT")] public double? MuskHours { get; set; }
        [JsonProperty("VELDUREN_BEVERRAT")] public double? BeverHours { get; set; }
        [JsonProperty("BESTRIJDER")] public string User { get; set; }
        [JsonProperty("INVULDATUM")] public string Date { get; set; }
    }
}
