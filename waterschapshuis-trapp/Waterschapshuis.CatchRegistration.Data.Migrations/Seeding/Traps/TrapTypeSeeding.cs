using System;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Traps
{
    public class TrapTypeSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            createType(TrapType.ConibearMuskratId, 
                "Conibear", 
                TrappingType.MuskusratId, 
                true,
                1, 
                false,
                (TrapStatus.Catching, "trap-conibear.svg"), 
                (TrapStatus.Removed, "trap-removed-conibear.svg"));

            createType(Guid.Parse("264F0093-6056-110B-1DE8-2AEFD1D73C4A"), 
                "Postklem", 
                TrappingType.MuskusratId, 
                true,
                2, 
                false,
                (TrapStatus.Catching, "trap-postklem.svg"),
                (TrapStatus.Removed, "trap-removed-postklem.svg"));

            createType(Guid.Parse("9F91A9D1-77D9-06D9-03A9-18F2EFCC0BCC"), 
                "Duikerafzetting", 
                TrappingType.MuskusratId,
                true, 
                3, 
                true,
                (TrapStatus.Catching, "trap-duikerafzetting.svg"),
                (TrapStatus.Removed, "trap-removed-duikerafzetting.svg"),
                (TrapStatus.NotCatching, "trap-unplaced-duikerafzetting.svg"));

            createType(Guid.Parse("6539ABE5-081D-A060-31B9-1C5C43F74ABB"), 
                "Lokaaskooi", 
                TrappingType.MuskusratId, 
                true,
                4, 
                true,
                (TrapStatus.Catching, "trap-lokaaskooi.svg"), 
                (TrapStatus.Removed, "trap-removed-lokaaskooi.svg"),
                (TrapStatus.NotCatching, "trap-unplaced-lokaaskooi.svg"));

            createType(Guid.Parse("3C881890-4D00-6B96-25BE-67DEC7314B2F"), 
                "Slootafzetting met kooi",
                TrappingType.MuskusratId, 
                true, 
                5, 
                true,
                (TrapStatus.Catching, "trap-slootafzetting.svg"),
                (TrapStatus.Removed, "trap-removed-slootafzetting.svg"),
                (TrapStatus.NotCatching, "trap-unplaced-slootafzetting.svg"));

            createType(Guid.Parse("DC90FAA1-1AD8-A4F2-22C2-582DCC5D4A84"), 
                "Levend vangende kooi",
                TrappingType.MuskusratId, 
                true, 
                6, 
                false,
                (TrapStatus.Catching, "trap-musk-living.svg"),
                (TrapStatus.Removed, "trap-removed-musk-living.svg"));

            createType(Guid.Parse("E887E560-8959-8994-960B-A85FC5C22E4D"), 
                "Levend vangende kooi op vlot",
                TrappingType.MuskusratId, 
                false, 
                7,
                true);

            createType(Guid.Parse("47890978-910D-969A-2499-0B848FA80F8A"), 
                "Levend vangende kooi op oever",
                TrappingType.MuskusratId, 
                false, 
                8,
                true);

            createType(Guid.Parse("10D026ED-0D1E-7B3B-786C-0246C0367222"), 
                "Levend vangende kooi op wissel",
                TrappingType.MuskusratId, 
                false, 
                9,
                true);

            createType(TrapType.GrondklemMuskratId, 
                "Grondklem", 
                TrappingType.MuskusratId, 
                true,
                10, 
                false,
                (TrapStatus.Catching, "trap-grondklem.svg"), 
                (TrapStatus.Removed, "trap-removed-grondklem.svg"));

            createType(Guid.Parse("935A02F4-69B0-8142-29F8-885124DB34BC"), 
                "Slaan en delven", 
                TrappingType.MuskusratId,
                true, 
                11, 
                false,
                (TrapStatus.Catching, "trap-musk-other.svg"),
                (TrapStatus.Removed, "trap-removed-musk-other.svg"));

            createType(Guid.Parse("2FF5402A-96B5-6B49-3EED-E5A4372666FB"), 
                "Klemmenrekje", 
                TrappingType.MuskusratId,
                true, 
                12, 
                false,
                (TrapStatus.Catching, "trap-klemmenrek.svg"),
                (TrapStatus.Removed, "trap-removed-klemmenrek.svg"),
                (TrapStatus.NotCatching, "trap-unplaced-klemmenrek.svg"));

            createType(Guid.Parse("EB992687-4000-6956-A688-2FC9242D2E20"), 
                "Lokaasklem", 
                TrappingType.MuskusratId, 
                true,
                13, 
                false,
                (TrapStatus.Catching, "trap-lokaasklem.svg"), 
                (TrapStatus.Removed, "trap-removed-lokaasklem.svg"));

            createType(Guid.Parse("EB9F0577-A55E-0ED7-2102-F04CEFE54680"), 
                "Lokaasklem (oever)",
                TrappingType.MuskusratId, 
                false, 
                14,
                true);

            createType(Guid.Parse("C3C795B9-49D2-0722-7F4B-E28BF43DA5C4"), 
                "Geweer",
                TrappingType.MuskusratId, 
                true, 
                15,
                false,
                (TrapStatus.Catching, "trap-musk-other.svg"), 
                (TrapStatus.Removed, "trap-removed-musk-other.svg"));

            createType(Guid.Parse("E2BA4C87-65FD-2F70-A1EA-68A6A4549DB6"), 
                "Schijnduiker", 
                TrappingType.MuskusratId,
                true, 
                16, 
                true,
                (TrapStatus.Catching, "trap-schijnduiker.svg"),
                (TrapStatus.Removed, "trap-removed-schijnduiker.svg"),
                (TrapStatus.NotCatching, "trap-unplaced-schijnduiker.svg"));

            createType(Guid.Parse("2FED9C2E-7151-316F-5E5D-644BC5620172"), 
                "Dood aangetroffen",
                TrappingType.MuskusratId, 
                true, 
                17, 
                false,
                (TrapStatus.Catching, "trap-musk-other.svg"),
                (TrapStatus.Removed, "trap-removed-musk-other.svg"));

            createType(Guid.Parse("198BE8A5-569F-233C-93EF-3879AA97120C"), 
                "Duikerkooi (groot/klein)",
                TrappingType.MuskusratId, 
                false, 
                18,
                true);

            createType(Guid.Parse("FF7C880C-9AC6-433E-1B92-3563869A48E2"), 
                "Otter", 
                TrappingType.MuskusratId, 
                false,
                19,
                true);

            createType(Guid.Parse("0D39267F-2EF1-5978-4FB5-68B7EC956F0F"), 
                "Levend vangende kooi op vlot",
                TrappingType.BeverratId, 
                false, 
                20,
                true);

            createType(Guid.Parse("DC56C661-710F-63AB-870B-36EDE6F9204E"), 
                "Levend vangende kooi op oever",
                TrappingType.BeverratId, 
                false, 
                21,
                true);

            createType(Guid.Parse("B73B56E0-9691-6EFF-43C5-0F0F36FF69C2"), 
                "Levend vangende kooi op wissel",
                TrappingType.BeverratId, 
                false, 
                22,
                true);

            createType(Guid.Parse("222B0A31-092A-4978-71DF-1F5C55E41000"), 
                "Levend vangende kooi",
                TrappingType.BeverratId, 
                true, 
                23,
                false,
                (TrapStatus.Catching, "trap-beaver-living.svg"),
                (TrapStatus.Removed, "trap-removed-beaver-living.svg"));

            createType(Guid.Parse("13EB51AC-6984-95E9-04EE-DDAE1927A499"), 
                "Geweer", 
                TrappingType.BeverratId, 
                true, 
                24, 
                false,
                (TrapStatus.Catching, "trap-beaver-other.svg"),
                (TrapStatus.Removed, "trap-removed-beaver-other.svg"));

            createType(Guid.Parse("5FA2DC7F-8C1A-1255-A41E-6BF28B183DEF"), 
                "Slaan en delven", 
                TrappingType.BeverratId,
                true, 
                25, 
                false,
                (TrapStatus.Catching, "trap-beaver-other.svg"),
                (TrapStatus.Removed, "trap-removed-beaver-other.svg"));

            createType(Guid.Parse("CA6C4838-2B63-71C5-3BEC-6DBEFE7678A2"), 
                "Dood aangetroffen", 
                TrappingType.BeverratId,
                true, 
                26, 
                false,
                (TrapStatus.Catching, "trap-beaver-other.svg"),
                (TrapStatus.Removed, "trap-removed-beaver-other.svg"));

            createType(TrapType.ConibearBeverratId, 
                "Conibear", 
                TrappingType.BeverratId, 
                false,
                27,
                true);

            createType(TrapType.GrondklemBeverratId, 
                "Grondklem", 
                TrappingType.BeverratId, 
                false,
                28,
                true);

            createType(Guid.Parse("3B1215F3-05A2-660E-85F4-D27DD5AE115D"), 
                "Overig", 
                TrappingType.BeverratId, 
                false,
                29,
                true);

            void createType(Guid id,
                string name,
                Guid trappingTypeId,
                bool active,
                short order,
                bool allowNotCatching,
                params (TrapStatus status, string iconName)[] iconsPerStatus)
            {
                TrapType trapType = TrapType
                    .Create(name, trappingTypeId, active, order, allowNotCatching)
                    .WithId(id);
                modelBuilder.Entity<TrapType>().HasData(trapType);

                foreach ((TrapStatus status, var iconName) in iconsPerStatus)
                {
                    TrapTypeTrapStatusStyle style = TrapTypeTrapStatusStyle
                        .Create(id, status, iconName);
                    modelBuilder.Entity<TrapTypeTrapStatusStyle>().HasData(style);
                }
            }
        }
    }
}
