using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport
{
    public class LimburgTimeRegistrationProperties
    {
        [JsonProperty("UREN_VR_BEVER")] public int? BeverHoursFriday { get; set; }
        [JsonProperty("UREN_MA_BEVER")] public int? BeverHoursMonday { get; set; }
        [JsonProperty("UREN_ZA_BEVER")] public int? BeverHoursSaturday { get; set; }
        [JsonProperty("UREN_ZO_BEVER")] public int? BeverHoursSunday { get; set; }
        [JsonProperty("UREN_DO_BEVER")] public int? BeverHoursThursday { get; set; }
        [JsonProperty("UREN_DI_BEVER")] public int? BeverHoursTuesday { get; set; }
        [JsonProperty("UREN_WO_BEVER")] public int? BeverHoursWednesday { get; set; }
        [JsonProperty("UURHOK")] public string HourSquareName { get; set; }
        [JsonProperty("UREN_VR_MUSK")] public int? MuskHoursFriday { get; set; }
        [JsonProperty("UREN_MA_MUSK")] public int? MuskHoursMonday { get; set; }
        [JsonProperty("UREN_ZA_MUSK")] public int? MuskHoursSaturday { get; set; }
        [JsonProperty("UREN_ZO_MUSK")] public int? MuskHoursSunday { get; set; }
        [JsonProperty("UREN_DO_MUSK")] public int? MuskHoursThursday { get; set; }
        [JsonProperty("UREN_DI_MUSK")] public int? MuskHoursTuesday { get; set; }
        [JsonProperty("UREN_WO_MUSK")] public int? MuskHoursWednesday { get; set; }
        [JsonProperty("DEELGEBIED")] public string SubAreaName { get; set; }
        [JsonProperty("BESTRIJDER")] public string User { get; set; }
        [JsonProperty("WEEK")] public int? Week { get; set; }
        [JsonProperty("JAAR")] public int? Year { get; set; }

        public Dictionary<Guid, (int? Hours, DayOfWeek Day)[]> FormatDayHours()
        {
            return new Dictionary<Guid, (int? Hours, DayOfWeek Day)[]>
            {
                {
                    TrappingType.MuskusratId,
                    new[]
                    {
                        (MuskHoursMonday, DayOfWeek.Monday), (MuskHoursTuesday, DayOfWeek.Tuesday),
                        (MuskHoursWednesday, DayOfWeek.Wednesday), (MuskHoursThursday, DayOfWeek.Thursday),
                        (MuskHoursFriday, DayOfWeek.Friday), (MuskHoursSaturday, DayOfWeek.Saturday),
                        (MuskHoursSunday, DayOfWeek.Sunday)
                    }
                },
                {
                    TrappingType.BeverratId,
                    new[]
                    {
                        (BeverHoursMonday, DayOfWeek.Monday), (BeverHoursTuesday, DayOfWeek.Tuesday),
                        (BeverHoursWednesday, DayOfWeek.Wednesday), (BeverHoursThursday, DayOfWeek.Thursday),
                        (BeverHoursFriday, DayOfWeek.Friday), (BeverHoursSaturday, DayOfWeek.Saturday),
                        (BeverHoursSunday, DayOfWeek.Sunday)
                    }
                }
            };
        }
    }
}
