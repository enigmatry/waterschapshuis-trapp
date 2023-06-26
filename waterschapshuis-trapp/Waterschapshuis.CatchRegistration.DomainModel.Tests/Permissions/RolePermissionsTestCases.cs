using NUnit.Framework;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Permissions
{
    public class RolePermissionsTestCases
    {
        public static readonly object[] RolePermissions =
        {
            new TestCaseData("Super Admin", 2),
            new TestCaseData("Trapper", 1)
        };
    }
}
