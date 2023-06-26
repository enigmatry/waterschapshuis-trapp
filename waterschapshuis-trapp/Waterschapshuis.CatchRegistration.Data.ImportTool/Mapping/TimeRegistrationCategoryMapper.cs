using System;
using System.Collections.Generic;
using System.Text;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Mapping
{
    public static class TimeRegistrationCategoryMapper
    {
        internal const string MateriaalOnderhoud = "Materiaal/Onderhoud";
        internal const string Reistijd = "Reistijd";
        internal const string Overleg = "Overleg";
        internal const string Voorlichting = "Voorlichting";
        internal const string OpleidingTraining = "Opleiding/Training";
        internal const string Basisverlof = "Basisverlof";
        internal const string Bijzonderverlof = "Bijzonderverlof";
        internal const string Ouderschapsverlof = "Ouderschapsverlof";
        internal const string Ziekte = "Ziekte";
        internal const string KortVerzuim = "KortVerzuim";
        internal const string PlanningVoortgangsrapportages = "PlanningVoortgangsrapportages";
        internal const string Dijkleger = "Dijkleger";
        internal const string Waterschap = "Waterschap";
        internal const string Arbo = "Arbo";
        internal const string Overig = "Overig";
        internal const string Plusuren = "Plusuren";
        internal const string Adv = "Adv";
        internal const string Seniorenverlof = "Seniorenverlof";
        internal const string AdminitratieICT = "Administratie/ICT";
        internal const string LifeMica = "LifeMica";
        internal const string Vangstregistratie = "Vangstregistratie";
        internal const string ORuren = "OR-uren";

        private static readonly Dictionary<string, Guid> TimeRegistrationCategoryMap = new Dictionary<string, Guid>
        {
            {MateriaalOnderhoud, Guid.Parse("220D84D1-A3B0-4E90-F5EE-08D8765994E4")},
            {Reistijd, Guid.Parse("8C8BE6D7-DA92-44A1-927B-1DB91A03C099")},
            {Overleg, Guid.Parse("FAC9EE8D-C03B-40B3-F647-08D8765994E4")},
            {Voorlichting, Guid.Parse("4D773155-70BC-4C67-F6E3-08D8765994E4")},
            {OpleidingTraining, Guid.Parse("5EAC706A-9F39-42D9-8D8F-D772D0482CC0")},
            {Basisverlof,Guid.Parse("82217E2B-2268-461C-F538-08D8765994E4")},
            {Bijzonderverlof,Guid.Parse("16DD1391-39C9-4E7A-F54D-08D8765994E4")},
            {Ouderschapsverlof,Guid.Parse("479A4375-6820-4D46-F61C-08D8765994E4")},
            {Ziekte,Guid.Parse("71106F2A-E599-4030-F70E-08D8765994E4")},
            {KortVerzuim,Guid.Parse("DAD170EE-8AC4-4867-F5CF-08D8765994E4")},
            {PlanningVoortgangsrapportages,Guid.Parse("CAB4DED2-255F-4186-F673-08D8765994E4")},
            {Dijkleger,Guid.Parse("0AC78057-29D4-40DB-F5BA-08D8765994E4")},
            {Waterschap,Guid.Parse("8AC90922-ED2D-4DE1-F6F9-08D8765994E4")},
            {Arbo,Guid.Parse("AB2E247D-0D85-493D-F51F-08D8765994E4")},
            {Overig,Guid.Parse("A20A5F9C-8B41-406C-F632-08D8765994E4")},
            {Plusuren,Guid.Parse("F3E776DF-DF8B-4B95-F688-08D8765994E4")},
            {Adv,Guid.Parse("5972D9DB-1C6A-4D4E-F4DC-08D8765994E4")},
            {Seniorenverlof,Guid.Parse("E6F9E5E6-47DF-4C1B-F6CE-08D8765994E4")},
            {AdminitratieICT, Guid.Parse("935340B4-929E-46CE-F0F7-08D8765994E4")},
            {LifeMica, Guid.Parse("1FEED3FC-9931-49EC-ADBF-D407EC1D9BC4")},
            {Vangstregistratie, Guid.Parse("B36F9910-3495-4E95-84BD-CB44D1D03DA2")},
            {ORuren, Guid.Parse("4B983EC7-C03D-4930-F606-08D8765994E4")}
    };


        public static Guid GetTimeRegistrationCategoryGuid(string name)
        {
            if (!TimeRegistrationCategoryMap.ContainsKey(name))
            {
                throw ImportException.InvalidTimeRegistrationCategory();
            }
            return TimeRegistrationCategoryMap[name];
        }
    }
}
