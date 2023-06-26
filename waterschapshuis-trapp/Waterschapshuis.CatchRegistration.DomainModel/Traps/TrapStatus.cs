using System.ComponentModel;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public enum TrapStatus
    {
        [Description("Vangend")]
        Catching = 1,
        [Description("Niet-vangend")]
        NotCatching = 2,
        [Description("Verwijderd")]
        Removed = 3
    }
}
