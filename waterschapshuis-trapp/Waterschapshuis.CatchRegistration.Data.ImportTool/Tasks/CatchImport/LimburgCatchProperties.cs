using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Mapping;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport
{
    public class LimburgCatchProperties : IProperties
    {
        [JsonProperty("BIJVANGST_1_VOGELS_AANTAL")] public int? ByCatchBirdsNumber { get; set; }
        [JsonProperty("BIJVANGST_1_VOGELS_SOORT")] public int? ByCatchBirdsType { get; set; }
        [JsonProperty("BIJVANGST_1_VISSEN_AANTAL")] public int? ByCatchFish1Number { get; set; }
        [JsonProperty("BIJVANGST_1_VISSEN_SOORT")] public int? ByCatchFish1Type { get; set; }
        [JsonProperty("BIJVANGST_2_VISSEN_AANTAL")] public int? ByCatchFish2Number { get; set; }
        [JsonProperty("BIJVANGST_2_VISSEN_SOORT")] public int? ByCatchFish2Type { get; set; }
        [JsonProperty("BIJVANGST_3_VISSEN_AANTAL")] public int? ByCatchFish3Number { get; set; }
        [JsonProperty("BIJVANGST_3_VISSEN_SOORT")] public int? ByCatchFish3Type { get; set; }
        [JsonProperty("BIJVANGST_1_ZOOGDIER_AANTAL")] public int? ByCatchMamalNumber { get; set; }
        [JsonProperty("BIJVANGST_1_ZOOGDIER_SOORT")] public int? ByCatchMamalType { get; set; }
        [JsonProperty("BIJVANGST_2_OVERIG_AANTAL")] public int? ByCatchOtherNumber { get; set; }
        [JsonProperty("BIJVANGST_2_OVERIG_SOORT")] public int? ByCatchOtherType { get; set; }
        [JsonProperty("CONTROLEDATUM")] public string DateControl { get; set; }
        [JsonProperty("CORRECTIEDATUM")] public string DateCorrected { get; set; }
        [JsonProperty("GLOBALID")] public string GlobalId { get; set; }
        [JsonProperty("BEVERRAT")] public int? BeverNumber { get; set; }
        [JsonProperty("MUSKUSRAT_MOER_OUD")] public int? NumberMuskusFemaleOld { get; set; }
        [JsonProperty("MUSKUSRAT_MOER_JONG")] public int? NumberMuskusFemaleYoung { get; set; }
        [JsonProperty("MUSKUSRAT_RAM_OUD")] public int? NumberMuskusMaleOld { get; set; }
        [JsonProperty("MUSKUSRAT_RAM_JONG")] public int? NumberMuskusMaleYoung { get; set; }
        [JsonProperty("VANGMIDDELEN_GLOBALID")] public string TrapGlobalId { get; set; }
        [JsonProperty("SOORT_COMBINATIE")] public int? TrapType { get; set; }
        [JsonProperty("BESTRIJDER")] public string User { get; set; }

        internal IEnumerable<(Guid catchTypeId, int number)> GetCatches()
        {
            if (NumberMuskusMaleOld.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(CatchTypeMapper.MuskusMaleOldCatchId),
                    NumberMuskusMaleOld.Value);
            }
            
            if (NumberMuskusMaleYoung.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(CatchTypeMapper.MuskusMaleYoungCatchId),
                    NumberMuskusMaleYoung.Value);
            }
            
            if (NumberMuskusFemaleOld.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(CatchTypeMapper.MuskusFemaleOldCatchId),
                    NumberMuskusFemaleOld.Value);
            }
            
            if (NumberMuskusFemaleYoung.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(CatchTypeMapper.MuskusFemaleYoungCatchId),
                    NumberMuskusFemaleYoung.Value);
            }
            
            if (BeverNumber.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(CatchTypeMapper.BeaverCatchId), BeverNumber.Value);
            }

            if (ByCatchMamalType.HasValue && ByCatchMamalNumber.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(ByCatchMamalType.Value), ByCatchMamalNumber.Value);
            }
            
            if (ByCatchBirdsType.HasValue && ByCatchBirdsNumber.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(ByCatchBirdsType.Value), ByCatchBirdsNumber.Value);
            }
            
            if (ByCatchFish1Type.HasValue && ByCatchFish1Number.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(ByCatchFish1Type.Value), ByCatchFish1Number.Value);
            }
            
            if (ByCatchFish2Type.HasValue && ByCatchFish2Number.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(ByCatchFish2Type.Value), ByCatchFish2Number.Value);
            }
            
            if (ByCatchFish3Type.HasValue && ByCatchFish3Number.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(ByCatchFish3Type.Value), ByCatchFish3Number.Value);
            }
            
            if (ByCatchOtherType.HasValue && ByCatchOtherNumber.HasValue)
            {
                yield return (CatchTypeMapper.GetCatchTypeGuid(ByCatchOtherType.Value), ByCatchOtherNumber.Value);
            }
        }
    }
}
