using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.TimeRegistrations
{
    public class TimeRegistrationCategorySeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            createType(Guid.Parse("935340B4-929E-46CE-F0F7-08D8765994E4"), "Administratie / ICT",true);
            createType(Guid.Parse("5972D9DB-1C6A-4D4E-F4DC-08D8765994E4"), "Adv", false);
            createType(Guid.Parse("AB2E247D-0D85-493D-F51F-08D8765994E4"), "Arbo", true);
            createType(Guid.Parse("82217E2B-2268-461C-F538-08D8765994E4"), "Basisverlof",true);
            createType(Guid.Parse("16DD1391-39C9-4E7A-F54D-08D8765994E4"), "Bijzonderverlof", true);
            createType(Guid.Parse("0AC78057-29D4-40DB-F5BA-08D8765994E4"), "Dijkleger", true);
            createType(Guid.Parse("DAD170EE-8AC4-4867-F5CF-08D8765994E4"), "Kort verzuim", false);
            createType(Guid.Parse("1FEED3FC-9931-49EC-ADBF-D407EC1D9BC4"), "Life mica", true);
            createType(Guid.Parse("220D84D1-A3B0-4E90-F5EE-08D8765994E4"), "Materiaal / Onderhoud", true);
            createType(Guid.Parse("5EAC706A-9F39-42D9-8D8F-D772D0482CC0"), "Opleiding / Training", true);
            createType(Guid.Parse("4B983EC7-C03D-4930-F606-08D8765994E4"), "OR-uren", true);
            createType(Guid.Parse("479A4375-6820-4D46-F61C-08D8765994E4"), "Ouderschapsverlof", true);
            createType(Guid.Parse("A20A5F9C-8B41-406C-F632-08D8765994E4"), "Overig", false);
            createType(Guid.Parse("FAC9EE8D-C03B-40B3-F647-08D8765994E4"), "Overleg", true);
            createType(Guid.Parse("4A956E92-3D0C-484B-F65D-08D8765994E4"), "Personeelbijeenkomst", true);
            createType(Guid.Parse("CAB4DED2-255F-4186-F673-08D8765994E4"), "Planning / Voortgangsrapportage", false);
            createType(Guid.Parse("F3E776DF-DF8B-4B95-F688-08D8765994E4"), "Plusuren",false);
            createType(Guid.Parse("8C8BE6D7-DA92-44A1-927B-1DB91A03C099"), "Reistijd", true);
            createType(Guid.Parse("64CBCFEE-A513-4D62-F6B8-08D8765994E4"), "Reistijd Woon-Werk (CAP)",false);
            createType(Guid.Parse("E6F9E5E6-47DF-4C1B-F6CE-08D8765994E4"), "Seniorenverlof", false);
            createType(Guid.Parse("B36F9910-3495-4E95-84BD-CB44D1D03DA2"), "Vangstregistratie", true);
            createType(Guid.Parse("4D773155-70BC-4C67-F6E3-08D8765994E4"), "Voorlichting", true);
            createType(Guid.Parse("8AC90922-ED2D-4DE1-F6F9-08D8765994E4"), "Waterschap", true);
            createType(Guid.Parse("71106F2A-E599-4030-F70E-08D8765994E4"), "Ziekte", true);

            void createType(Guid id, string category, bool active)
            {
                TimeRegistrationCategory timeRegistrationCategory = TimeRegistrationCategory
                    .Create(category, active)
                    .WithId(id);

                modelBuilder.Entity<TimeRegistrationCategory>().HasData(timeRegistrationCategory);
            }
        }
    }
}
