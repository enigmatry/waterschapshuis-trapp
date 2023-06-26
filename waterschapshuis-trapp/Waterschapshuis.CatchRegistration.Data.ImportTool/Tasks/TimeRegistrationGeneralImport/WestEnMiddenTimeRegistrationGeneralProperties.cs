using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Mapping;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationGeneralImport
{
    [UsedImplicitly]
    public class WestEnMiddenTimeRegistrationGeneralProperties
    {
        [JsonProperty("BESTRIJDER")] public string User { get; set; }
        [JsonProperty("INVULDATUM")] public string Date { get; set; }
        [JsonProperty("MATERIAALVOORZIENING")] public double? MateriaalOnderhoudHours { get; set; }
        [JsonProperty("REISTIJD")] public double? ReistijdHours { get; set; }
        [JsonProperty("OVERLEG")] public double? OverlegHours { get; set; }
        [JsonProperty("VOORLICHTING")] public double? VoorlichtingHours { get; set; }
        [JsonProperty("CURSUS")] public double? CursusHours { get; set; }
        [JsonProperty("BASISVERLOF")] public double? BasisverlofHours { get; set; }
        [JsonProperty("BIJZONDERVERLOF")] public double? BijzonderverlofHours { get; set; }
        [JsonProperty("OUDERSCHAPSVERLOF")] public double? OuderschapsverlofHours { get; set; }
        [JsonProperty("ZIEKTE")] public double? ZiekteHours { get; set; }
        [JsonProperty("KORTVERZUIM")] public double? KortVerzuimHours { get; set; }
        [JsonProperty("PLANNING")] public double? PlanningVoortgangsrapportagesHours { get; set; }
        [JsonProperty("DIJKLEGER")] public double? DijklegerHours { get; set; }
        [JsonProperty("WATERSCHAP")] public double? WaterschapHours { get; set; }
        [JsonProperty("ARBO")] public double? ArboHours { get; set; }
        [JsonProperty("OVERIG")] public double? OverigHours { get; set; }
        [JsonProperty("PLUSUREN")] public double? PlusurenHours { get; set; }
        [JsonProperty("ADV")] public double? AdvHours { get; set; }
        [JsonProperty("SENIORENVERLOF")] public double? SeniorenverlofHours { get; set; }
        [JsonProperty("ICT")] public double? AdminitratieICTHours { get; set; }
        [JsonProperty("LIFEMICA")] public double? LifeMicaHours { get; set; }
        [JsonProperty("VANGSTREGISTRATIE")] public double? Vangstregistratie { get; set; }
        [JsonProperty("ONDERNEMINGSRAAD")] public double? ORuren { get; set; }

        internal IEnumerable<(Guid, double)> GetTimeRegistrationCategory()
        {
            if (MateriaalOnderhoudHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.MateriaalOnderhoud),
                 MateriaalOnderhoudHours.Value);
            }
            if (ReistijdHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Reistijd),
                    ReistijdHours.Value);
            }
            if (OverlegHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Overleg),
                    OverlegHours.Value);
            }
            if (VoorlichtingHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Voorlichting),
                    VoorlichtingHours.Value);
            }

            if (CursusHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.OpleidingTraining),
                    CursusHours.Value);
            }
            if (BasisverlofHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Basisverlof),
                    BasisverlofHours.Value);
            }
            if (BijzonderverlofHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Bijzonderverlof),
                    BijzonderverlofHours.Value);
            }
            if (OuderschapsverlofHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Ouderschapsverlof),
                    OuderschapsverlofHours.Value);
            }
            if (ZiekteHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Ziekte),
                    ZiekteHours.Value);
            }
            if (KortVerzuimHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.KortVerzuim),
                    KortVerzuimHours.Value);
            }
            if (PlanningVoortgangsrapportagesHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.PlanningVoortgangsrapportages),
                    PlanningVoortgangsrapportagesHours.Value);
            }
            if (DijklegerHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Dijkleger),
                    DijklegerHours.Value);
            }
            if (WaterschapHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Waterschap),
                    WaterschapHours.Value);
            }
            if (ArboHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Arbo),
                    ArboHours.Value);
            }
            if (OverigHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Overig),
                    OverigHours.Value);
            }
            if (PlusurenHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Plusuren),
                    PlusurenHours.Value);
            }
            if (AdvHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Adv),
                    AdvHours.Value);
            }
            if (SeniorenverlofHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Seniorenverlof),
                    SeniorenverlofHours.Value);
            }
            if (AdminitratieICTHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.AdminitratieICT),
                    AdminitratieICTHours.Value);
            }
            if (LifeMicaHours.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.LifeMica),
                    LifeMicaHours.Value);
            }
            if (Vangstregistratie.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.Vangstregistratie),
                    Vangstregistratie.Value);
            }
            if (ORuren.HasValue)
            {
                yield return (TimeRegistrationCategoryMapper.GetTimeRegistrationCategoryGuid(TimeRegistrationCategoryMapper.ORuren),
                    ORuren.Value);
            }
        }
    }
}
