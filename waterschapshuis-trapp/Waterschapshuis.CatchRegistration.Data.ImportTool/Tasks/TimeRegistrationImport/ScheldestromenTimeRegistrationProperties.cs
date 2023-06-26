using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class ScheldestromenTimeRegistrationProperties : IProperties
    {
        [JsonProperty("UREN_VR_BR")] public int? BeverHoursFriday { get; set; }
        [JsonProperty("UREN_MA_BR")] public int? BeverHoursMonday { get; set; }
        [JsonProperty("UREN_ZA_BR")] public int? BeverHoursSaturday { get; set; }
        [JsonProperty("UREN_ZO_BR")] public int? BeverHoursSunday { get; set; }
        [JsonProperty("UREN_DO_BR")] public int? BeverHoursThursday { get; set; }
        [JsonProperty("UREN_DI_BR")] public int? BeverHoursTuesday { get; set; }
        [JsonProperty("UREN_WO_BR")] public int? BeverHoursWednesday { get; set; }
        [JsonProperty("UURHOK")] public string HourSquareName { get; set; }
        [JsonProperty("UREN_VR")] public int? MuskHoursFriday { get; set; }
        [JsonProperty("UREN_MA")] public int? MuskHoursMonday { get; set; }
        [JsonProperty("UREN_ZA")] public int? MuskHoursSaturday { get; set; }
        [JsonProperty("UREN_ZO")] public int? MuskHoursSunday { get; set; }
        [JsonProperty("UREN_DO")] public int? MuskHoursThursday { get; set; }
        [JsonProperty("UREN_DI")] public int? MuskHoursTuesday { get; set; }
        [JsonProperty("UREN_WO")] public int? MuskHoursWednesday { get; set; }
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
