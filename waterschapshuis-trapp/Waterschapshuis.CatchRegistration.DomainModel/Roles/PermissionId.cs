using System.ComponentModel;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles
{
    public enum PermissionId
    {
        [Description("Kaartoverzicht")]
        MapRead = 1,

        [Description("Bekijken gegevens")]
        MapContentRead = 2,

        [Description("Wijzigen gegevens")]
        MapContentWrite = 3,

        [Description("Vangstrapportage")]
        ReportReadWrite = 4,

        [Description("Urenrapportage")]
        TimeRegistrationPersonalReadWrite = 5,

        [Description("Urenrapportage beheer")]
        TimeRegistrationManagementReadWrite = 6,

        [Description("Gebruikers")]
        UserReadWrite = 7, 

        [Description("Beheer")]
        Management = 8,

        [Description("Beheerder Rechten Toekennen")]
        AssignMaintainerRole = 9,

        [Description("Alleen lezen")]
        ReadOnly = 10,

        [Description("Externe data interface (publiek)")]
        ApiPublic = 11,

        [Description("Externe data interface")]
        ApiPrivate = 12,

        [Description("Mobiel")]
        Mobile = 100,
    }
}
