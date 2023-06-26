using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Mapping
{
    internal static class TrapTypeMapper
    {
        internal const long ConibearTrapTypeId = 1;

        private static readonly Dictionary<long, Guid> TrapTypeMap = new Dictionary<long, Guid>()
        {
            {1, Guid.Parse("A0A0503E-0CD7-0642-73AB-464E7CA0A28E")},
            {2, Guid.Parse("1620509F-4BB2-90EA-637C-AF77B636964A")},
            {3, Guid.Parse("EB992687-4000-6956-A688-2FC9242D2E20")},
            {4, Guid.Parse("EB9F0577-A55E-0ED7-2102-F04CEFE54680")},
            {5, Guid.Parse("C3C795B9-49D2-0722-7F4B-E28BF43DA5C4")},
            {6, Guid.Parse("E2BA4C87-65FD-2F70-A1EA-68A6A4549DB6")},
            {7, Guid.Parse("198BE8A5-569F-233C-93EF-3879AA97120C")},
            {8, Guid.Parse("9F91A9D1-77D9-06D9-03A9-18F2EFCC0BCC")},
            {9, Guid.Parse("3C881890-4D00-6B96-25BE-67DEC7314B2F")},
            {10, Guid.Parse("6539ABE5-081D-A060-31B9-1C5C43F74ABB")},
            {11, Guid.Parse("DC90FAA1-1AD8-A4F2-22C2-582DCC5D4A84")},
            {12, Guid.Parse("935A02F4-69B0-8142-29F8-885124DB34BC")},
            {21, Guid.Parse("264F0093-6056-110B-1DE8-2AEFD1D73C4A")},
            {22, Guid.Parse("2FF5402A-96B5-6B49-3EED-E5A4372666FB")},
            {23, Guid.Parse("2FED9C2E-7151-316F-5E5D-644BC5620172")},
            {25, Guid.Parse("E887E560-8959-8994-960B-A85FC5C22E4D")},
            {26, Guid.Parse("47890978-910D-969A-2499-0B848FA80F8A")},
            {27, Guid.Parse("10D026ED-0D1E-7B3B-786C-0246C0367222")},
            {30, Guid.Parse("FF7C880C-9AC6-433E-1B92-3563869A48E2")},
            {13, Guid.Parse("586729D8-980E-2A76-81F2-DBB5C57C9D6F")},
            {14, Guid.Parse("54AF411E-25F6-2A11-4BBF-E7547E212E76")},
            {15, Guid.Parse("13EB51AC-6984-95E9-04EE-DDAE1927A499")},
            {16, Guid.Parse("0D39267F-2EF1-5978-4FB5-68B7EC956F0F")},
            {17, Guid.Parse("DC56C661-710F-63AB-870B-36EDE6F9204E")},
            {18, Guid.Parse("B73B56E0-9691-6EFF-43C5-0F0F36FF69C2")},
            {19, Guid.Parse("3B1215F3-05A2-660E-85F4-D27DD5AE115D")},
            {20, Guid.Parse("222B0A31-092A-4978-71DF-1F5C55E41000")},
            {31, Guid.Parse("5FA2DC7F-8C1A-1255-A41E-6BF28B183DEF")},
            {32, Guid.Parse("CA6C4838-2B63-71C5-3BEC-6DBEFE7678A2")}
        };

        internal static Guid GetTrapTypeGuid(long id)
        {
            if (!TrapTypeMap.ContainsKey(id))
            {
                throw ImportException.InvalidTrapType();
            }
            return TrapTypeMap[id];
        }

        internal static long GetTrapTypeId(Guid guid)
        {
            if (!TrapTypeMap.ContainsValue(guid))
            {
                throw ImportException.InvalidTrapType();
            }
            return TrapTypeMap.Single(x => x.Value == guid).Key;
        }
    }
}
