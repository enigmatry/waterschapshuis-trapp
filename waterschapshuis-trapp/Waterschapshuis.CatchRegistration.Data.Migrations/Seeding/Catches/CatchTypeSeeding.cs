using System;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Catches
{
    public class CatchTypeSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            // ByCatch Types:
            createType(Guid.Parse("A4A5A1F2-A14D-9590-1D81-E29D858A1475"), "woelrat", true, AnimalType.Mammal, 1);
            createType(Guid.Parse("30BD14AC-9FBA-8858-55D6-5AFA921916ED"), "bruine rat", true, AnimalType.Mammal, 2);
            createType(Guid.Parse("A800C291-6104-A0F6-6BCE-50F180D18CBE"), "bunzing", true, AnimalType.Mammal, 3);
            createType(Guid.Parse("A2B00E4D-1360-4F79-0844-A19D41528B90"), "amerikaanse nerts", true, AnimalType.Mammal, 4);
            createType(Guid.Parse("2A241941-61B6-2D0A-2D3F-6C09760EA2BD"), "hermelijn", true, AnimalType.Mammal, 5);
            createType(Guid.Parse("EB989ECC-74D3-4642-48D7-00A512FE2304"), "zwarte rat", true, AnimalType.Mammal, 6);
            createType(Guid.Parse("9D04C71C-251F-9BA3-98A6-871977F68055"), "wezel", true, AnimalType.Mammal, 7);
            createType(Guid.Parse("82BB23BE-6BAB-45A5-83FC-AC1C81FA963F"), "bever", true, AnimalType.Mammal, 8);
            createType(Guid.Parse("D4B917F0-50A6-1992-2C9A-DB686C578FB3"), "fret", true, AnimalType.Mammal, 9);
            createType(Guid.Parse("24873FD8-7B41-8A8B-7C81-621FDE45520A"), "mol", true, AnimalType.Mammal, 10);
            createType(Guid.Parse("2F61EB42-26B2-1E00-A70B-D0B4098D1D05"), "egel", true, AnimalType.Mammal, 11);
            createType(Guid.Parse("7ABA1245-13D7-0317-8307-4009EB3B51BF"), "konijn", true, AnimalType.Mammal, 12);
            createType(Guid.Parse("A7EFD4DF-703E-8ABF-6767-F24760CB2C1D"), "haas", true, AnimalType.Mammal, 13);
            createType(Guid.Parse("CA585BDF-495B-6BA2-62DC-A68D66202A6F"), "eekhoorn", true, AnimalType.Mammal, 14);
            createType(Guid.Parse("E9B0B7E7-3D23-034F-9999-A7F9C4AD1CD5"), "steenmarter", true, AnimalType.Mammal, 15);
            createType(Guid.Parse("5B2131FE-743D-2467-477A-B7AFBF181E81"), "das", true, AnimalType.Mammal, 16);
            createType(Guid.Parse("B2F24FB5-359D-2116-9520-5BD38A0522FE"), "vos", true, AnimalType.Mammal, 17);
            createType(Guid.Parse("C1A5586A-5416-442E-A6D4-E8D001302C14"), "boommarter", true, AnimalType.Mammal, 18);
            createType(Guid.Parse("50CC209C-9168-905C-391C-8641EC5A887A"), "otter", true, AnimalType.Mammal, 19);

            createType(Guid.Parse("053210C7-1491-0255-A36A-223FAB8780BD"), "wilde eend", true, AnimalType.Bird, 20);
            createType(Guid.Parse("843D3051-8F82-1DF3-6E73-BFBA9CBA15CF"), "aalscholver", true, AnimalType.Bird, 21);
            createType(Guid.Parse("E2406E40-63B0-8926-5F49-185513C03E90"), "waterhoen", true, AnimalType.Bird, 22);
            createType(Guid.Parse("52529727-0E5C-07C0-2F00-173F081834D2"), "meerkoet", true, AnimalType.Bird, 23);
            createType(Guid.Parse("386C68BB-76E0-5847-8717-E876399E0D61"), "grauwe gans", true, AnimalType.Bird, 24);
            createType(Guid.Parse("EB6D4C1D-333A-538F-4228-92151F6C11AA"), "fuut", true, AnimalType.Bird, 25);
            createType(Guid.Parse("B7691621-97FA-26F9-86BC-D2D537E92D59"), "dodaars", true, AnimalType.Bird, 26);
            createType(Guid.Parse("DFCF25F4-37B7-85AF-0486-8CC7F76475A1"), "waterral", true, AnimalType.Bird, 27);
            createType(Guid.Parse("1FA69307-35CD-24E0-62CB-83D21E6F706D"), "blauwe reiger", true, AnimalType.Bird, 28);
            createType(Guid.Parse("49F3FA88-8C51-A558-667F-E79DA80D7495"), "tamme eend", true, AnimalType.Bird, 29);
            createType(Guid.Parse("C0D3288C-24B4-57B4-30E7-9D37DE42909E"), "zwarte kraai", true, AnimalType.Bird, 30);
            createType(Guid.Parse("79B23520-2380-49E2-1E23-B226971A579B"), "ekster", true, AnimalType.Bird, 31);
            createType(Guid.Parse("D1F172CE-15C8-88E8-92EC-3E8997F97374"), "vlaamse gaai", true, AnimalType.Bird, 32);
            createType(Guid.Parse("7B4E5DD0-696B-24E2-1F68-685500CF477C"), "mandarijneend", true, AnimalType.Bird, 33);
            createType(Guid.Parse("0E3644B2-04F1-0BDD-2FB2-D673416D944C"), "nonnetje", true, AnimalType.Bird, 34);
            createType(Guid.Parse("F7BACD7D-63DC-5EFB-71DA-556C77E54768"), "roek", true, AnimalType.Bird, 35);
            createType(Guid.Parse("0EFF39DB-3937-6A4B-4526-8472B88CA537"), "spreeuw", true, AnimalType.Bird, 36);
            createType(Guid.Parse("680C7557-4AD8-66B9-9C2D-1310B7D107CB"), "vink", true, AnimalType.Bird, 37);
            createType(Guid.Parse("50A63929-570A-614B-7635-AA5ADFC39C80"), "wintertaling", true, AnimalType.Bird, 38);
            createType(Guid.Parse("4F6E48E1-8213-24AE-80FF-658183378727"), "zomertaling", true, AnimalType.Bird, 39);
            createType(Guid.Parse("0F44982F-5F41-517A-8EAC-38EA36541345"), "blauwborst", true, AnimalType.Bird, 40);
            createType(Guid.Parse("2E8A45E5-90EF-3DA9-1657-9485A719762A"), "merel", true, AnimalType.Bird, 41);
            createType(Guid.Parse("0691A0C0-11ED-0CD0-81DA-0A962BC13352"), "meeuw", true, AnimalType.Bird, 42);
            createType(Guid.Parse("5DFAC5DA-54EA-3B6D-8390-745BAAA97C1F"), "fazant", true, AnimalType.Bird, 43);
            createType(Guid.Parse("399EAF18-256A-0AE9-4F27-EAD861D68F3A"), "kuifeend", true, AnimalType.Bird, 44);
            createType(Guid.Parse("651014A7-422D-5F82-75F6-12F816AC40AD"), "koperwiek", true, AnimalType.Bird, 45);
            createType(Guid.Parse("3A737D68-0316-8DE4-A349-90AEEEB44186"), "roodborst", true, AnimalType.Bird, 46);
            createType(Guid.Parse("F251FDE0-5A5C-7426-7A24-15C5976D0C9F"), "watersnip", true, AnimalType.Bird, 47);
            createType(Guid.Parse("C815BC1E-5FDF-3B4C-23BF-1A5DED45717A"), "zanglijster", true, AnimalType.Bird, 48);
            createType(Guid.Parse("41051A0F-3060-1036-50A8-797F0D246223"), "grote zaagbek", true, AnimalType.Bird, 49);
            createType(Guid.Parse("64A42C1A-7D1B-121D-9A04-927941D6457D"), "middelste zaagbek", true, AnimalType.Bird, 50);
            createType(Guid.Parse("63223BED-466F-75B8-4569-6D6B2A306204"), "slobeend", true, AnimalType.Bird, 51);
            createType(Guid.Parse("8C3DE473-2E36-5F98-2BD8-7DE7CC9424ED"), "porseleinhoen", true, AnimalType.Bird, 52);
            createType(Guid.Parse("570ABD85-A4F2-2216-0218-BF2547D3A47C"), "holenduif", true, AnimalType.Bird, 53);
            createType(Guid.Parse("7F446720-5862-2800-89FE-F0C3B28D573D"), "kleine zilverreiger", true, AnimalType.Bird, 54);
            createType(Guid.Parse("5CD8F246-327F-4A4F-067C-E1573B2B7F94"), "tortelduif", true, AnimalType.Bird, 55);
            createType(Guid.Parse("D36F55C5-05A9-68BA-18BA-BEA7FC3FA3B8"), "knobbelzwaan", true, AnimalType.Bird, 56);
            createType(Guid.Parse("615BB03E-5756-0E85-2BDC-41952FBA4BAB"), "nijlgans", true, AnimalType.Bird, 57);
            createType(Guid.Parse("75283EE9-5706-9A47-2C13-3ABA63AF2AAE"), "rotgans", true, AnimalType.Bird, 58);
            createType(Guid.Parse("7DE9739C-3B0B-80A4-25A2-25CED3895635"), "brandgans", true, AnimalType.Bird, 59);
            createType(Guid.Parse("08304BE0-03D9-0419-9D95-7DDA70BF117F"), "tamme (boeren) gans", true, AnimalType.Bird, 60);
            createType(Guid.Parse("448B7701-491F-9D9E-1D8F-399E9FC74EC2"), "roerdomp", true, AnimalType.Bird, 61);
            createType(Guid.Parse("4047A8C6-35A2-30BA-3F1A-51B479AA2BDD"), "kauw", true, AnimalType.Bird, 62);
            createType(Guid.Parse("CD343CBF-9F02-4955-340B-DAF38C3C95FD"), "grutto", true, AnimalType.Bird, 63);
            createType(Guid.Parse("3F4F77F6-19F1-90C0-534C-223C58D164B5"), "zwaan", true, AnimalType.Bird, 64);
            createType(Guid.Parse("F6D9670B-4FCC-A0BD-00F3-43DAFB6C5F3B"), "bruine kiekendief", true, AnimalType.Bird, 65);
            createType(Guid.Parse("59FB893B-9F4D-5F83-59E2-1748451739DC"), "tafeleend", true, AnimalType.Bird, 66);
            createType(Guid.Parse("8B563A94-516A-19DB-4D96-3DFDA633042B"), "zwarte zwaan", true, AnimalType.Bird, 67);
            createType(Guid.Parse("6BC3A885-66A0-8F35-1142-4D06703A4F46"), "smient", true, AnimalType.Bird, 68);
            createType(Guid.Parse("FE25DB3D-2BF6-A7A6-6272-7B50468F1AA7"), "geoorde fuut", true, AnimalType.Bird, 69);
            createType(Guid.Parse("F296F546-13F4-2172-51DD-6394577831BC"), "canadese gans", true, AnimalType.Bird, 70);

            createType(Guid.Parse("F863A895-35C3-6432-7F93-235AD27456F5"), "snoek", true, AnimalType.Fish, 71);
            createType(Guid.Parse("045164E9-6606-7C9A-8976-05C6F62A4C95"), "zeelt", true, AnimalType.Fish, 72);
            createType(Guid.Parse("7F11F101-04D3-4EC5-3924-A379686B5542"), "blankvoorn", true, AnimalType.Fish, 73);
            createType(Guid.Parse("67DD1253-A69F-8755-50EC-4A8E431089BC"), "voorn", true, AnimalType.Fish, 74);
            createType(Guid.Parse("B3142F85-881E-58D1-9550-1F8BC58A1D3E"), "ruisvoorn", true, AnimalType.Fish, 75);
            createType(Guid.Parse("FD1A6EEA-28B5-8718-4C9F-DF5822301E64"), "aal", true, AnimalType.Fish, 76);
            createType(Guid.Parse("6AA19E7F-74FE-42A8-3256-1EF358538C6F"), "brasem", true, AnimalType.Fish, 77);
            createType(Guid.Parse("7C16C5CD-93B9-315D-2096-D214301B5D30"), "baars", true, AnimalType.Fish, 78);
            createType(Guid.Parse("79EE016D-92C3-6F74-5E4D-0405346F92FA"), "kolblei", true, AnimalType.Fish, 79);
            createType(Guid.Parse("AA139422-996C-7970-3F81-CC52421F42DB"), "grote modderkruiper", true, AnimalType.Fish, 80);
            createType(Guid.Parse("2094367B-41CF-0357-3985-37BE1759918B"), "inheemse meerval", true, AnimalType.Fish, 81);
            createType(Guid.Parse("CB478141-A50F-0A25-2EEE-DE00EAE81410"), "snoekbaars", true, AnimalType.Fish, 82);
            createType(Guid.Parse("3005E270-97AE-68FF-919A-99347B9D949F"), "karper", true, AnimalType.Fish, 83);
            createType(Guid.Parse("4B29436B-92EC-3A3F-105D-4F9C53B092D8"), "schol", true, AnimalType.Fish, 84);
            createType(Guid.Parse("FC3A6849-43FF-1750-1E6F-291EAB227230"), "bot", true, AnimalType.Fish, 85);
            createType(Guid.Parse("FC5D5C74-34F1-1263-571F-170A1E6F483E"), "kwabaal", true, AnimalType.Fish, 86);
            createType(Guid.Parse("06D350B0-378C-8D23-3C7B-31AB92D65D39"), "pos", true, AnimalType.Fish, 87);
            createType(Guid.Parse("3FBC8687-644C-09B5-1F38-32B03F0C39E3"), "bruine amerikaanse", true, AnimalType.Fish, 88);
            createType(Guid.Parse("57FAAF49-108B-78E9-6373-97A021C3062F"), "amer. hondsvis", true, AnimalType.Fish, 89);
            createType(Guid.Parse("1370EE41-7FCA-A1D9-6B68-ECA6CC076BC3"), "bittervoorn", true, AnimalType.Fish, 90);
            createType(Guid.Parse("8B124EB4-8C67-91E4-55CD-95CF3E406937"), "kopvoorn", true, AnimalType.Fish, 91);
            createType(Guid.Parse("37CEEC1C-7A34-7927-5932-B03448247B44"), "spiegelkarper", true, AnimalType.Fish, 92);
            createType(Guid.Parse("AE71B03C-0F45-010B-2966-E860104C698E"), "graskarper", true, AnimalType.Fish, 93);
            createType(Guid.Parse("BDD0CCF0-4EF4-4E18-6F0E-3DB98690A6DD"), "zilverkarper", true, AnimalType.Fish, 94);
            createType(Guid.Parse("BDBF0CAA-64B5-64CB-87B1-A489C6598AF3"), "kroeskarper", true, AnimalType.Fish, 95);
            createType(Guid.Parse("5C71EAAE-3A9E-3276-7E35-1DEC90B19AB4"), "kleine modderkruiper", true, AnimalType.Fish, 96);
            createType(Guid.Parse("B4302D39-5657-5C64-63AC-91F7F1BA9D51"), "riviergrondel", true, AnimalType.Fish, 97);

            createType(Guid.Parse("E31E1410-8CBF-0BBD-29B7-E2290F811C04"), "Amerikaanese rivierkreeft", true, AnimalType.Other, 98);
            createType(Guid.Parse("856E5B58-14F3-6131-3EAA-976715F8A642"), "wolhandkrab", true, AnimalType.Other, 99);
            createType(Guid.Parse("7BBEF5B0-0A32-74F1-44EE-A2395DBA74D8"), "roodwangschildpad", true, AnimalType.Other, 100);
            createType(Guid.Parse("81A64EB4-335A-0D5F-A378-96C3CB5847D8"), "brulkikker", true, AnimalType.Other, 101);
            createType(Guid.Parse("D913AACB-1E93-37B5-6E36-926D319E84B2"), "groene kikker", true, AnimalType.Other, 102);
            createType(Guid.Parse("7FC8C7C6-8DB1-8B20-A566-ED0C83AD94BA"), "gewone pad", true, AnimalType.Other, 103);
            createType(Guid.Parse("78BC4319-368C-1324-8E8E-711E40210508"), "bruine kikker", true, AnimalType.Other, 104);
            createType(Guid.Parse("7D2E51C9-36CB-4F2B-6859-D65CC3EE0124"), "zoetwatermossel", true, AnimalType.Other, 105);
            createType(Guid.Parse("D0994E03-6D7F-9309-574D-687C36EB1499"), "inheemse rivierkreeft", true, AnimalType.Other, 106);
            createType(Guid.Parse("2db0b44e-8ec5-41f2-befc-89fe1c05c17c"), "Turkse rivierkreeft", true, AnimalType.Other, 107);
            createType(Guid.Parse("0042cfb2-1624-4af2-90dd-149d46f7c683"), "Europese rivierkreeft", true, AnimalType.Other, 108);
            createType(Guid.Parse("c6e6943d-89aa-42e3-a096-f18911968095"), "Gestreepte Amerikaanse rivierkreeft", true, AnimalType.Other, 109);
            createType(Guid.Parse("2497dea4-f0c8-40ba-94d6-13cc3dd60682"), "Rode Amerikaanse rivierkreeft", true, AnimalType.Other, 110);
            createType(Guid.Parse("f6b62b18-7dc6-4548-8a0d-5151f2e41ec4"), "Californische rivierkreeft", true, AnimalType.Other, 111);
            createType(Guid.Parse("67800503-61ce-48b1-bb9d-37d1943a545b"), "Gevlekte Amerikaanse rivierkreeft", true, AnimalType.Other, 112);
            createType(Guid.Parse("76fa1418-960d-4f33-adfe-d2603d047348"), "Geknobbelde Amerikaanse rivierkreeft", true, AnimalType.Other, 113);
            createType(Guid.Parse("7dc99bf1-251c-4bd0-a359-9a168f6a6af4"), "Marmerkreeft", true, AnimalType.Other, 114);

            // Catch Types:
            createType(Guid.Parse("8957CB9D-936C-29CB-8511-A9C9A7EC6A7E"), "Beverrat ram oud (>1jr)", false, AnimalType.Mammal, 501);
            createType(Guid.Parse("2539B02A-9298-7B9F-4273-3E8AC99D7C63"), "Beverrat ram jong (<1jr)", false, AnimalType.Mammal, 502);
            createType(Guid.Parse("85803328-15E7-92EF-528F-00E91B6D4815"), "Beverrat moer oud (>1jr)", false, AnimalType.Mammal, 503);
            createType(Guid.Parse("7A8199E8-21DF-7556-1F0A-549E94645B6F"), "Beverrat moer jong (<1jr)", false, AnimalType.Mammal, 504);
            createType(Guid.Parse("49B51935-918B-5A38-2493-A4141FEF8C52"), "Beverrat", false, AnimalType.Mammal, 505);
            createType(Guid.Parse("3D1358F4-61D4-21D8-9438-90096EEEA47E"), "Muskusrat ram oud (>1jr)", false, AnimalType.Mammal, 506);
            createType(Guid.Parse("E72CCB01-65BB-A1AA-A5E8-EB909FE77374"), "Muskusrat ram jong (<1jr)", false, AnimalType.Mammal, 507);
            createType(Guid.Parse("645F7089-7F21-50C5-30C4-5FE30CC693F1"), "Muskusrat moer oud (>1jr)", false, AnimalType.Mammal, 508);
            createType(Guid.Parse("44711E96-25B8-0AF6-669B-CCDC8ABA9017"), "Muskusrat moer jong (<1jr)", false, AnimalType.Mammal, 509);
            createType(Guid.Parse("C8783519-41C6-5654-1977-F6956ABA2EF4"), "Muskusrat", false, AnimalType.Mammal, 510);




            void createType(Guid id, string name, bool isByCatch, AnimalType animalType, short order)
            {
                var catchType = CatchType
                    .Create(name, isByCatch, animalType, order)
                    .WithId(id);

                modelBuilder.Entity<CatchType>().HasData(catchType);
            }
        }
    }
}
