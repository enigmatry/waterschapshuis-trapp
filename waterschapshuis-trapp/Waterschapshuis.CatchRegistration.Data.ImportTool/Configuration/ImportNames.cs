namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration
{
    public static class ImportNames
    {
        private const string Traps = nameof(Traps);
        private const string Catches = nameof(Catches);
        private const string TimeRegistrations = nameof(TimeRegistrations);
        private const string TimeRegistrationGeneral = nameof(TimeRegistrationGeneral);

        public const string Geo = nameof(Geo);
        public const string Users = nameof(Users);
        public const string Provinces = nameof(Provinces);

        // Trap import per organization.
        public static readonly string TrapsScheldestromen = $"{Traps}:{OrganizationNames.Scheldestromen}";
        public static readonly string TrapsWestEnMidden = $"{Traps}:{OrganizationNames.WestEnMidden}";
        public static readonly string TrapsMura = $"{Traps}:{OrganizationNames.Mura}";
        public static readonly string TrapsLimburg = $"{Traps}:{OrganizationNames.Limburg}";
        public static readonly string TrapsBrabant = $"{Traps}:{OrganizationNames.Brabant}";
        public static readonly string TrapsFryslan = $"{Traps}:{OrganizationNames.Fryslan}";
        public static readonly string TrapsMrb = $"{Traps}:{OrganizationNames.Rivierenland}";
        public static readonly string TrapsNoordoostnederland = $"{Traps}:{OrganizationNames.Noordoostnederland}";
        public static readonly string TrapsZuiderzeeland = $"{Traps}:{OrganizationNames.Zuiderzeeland}";

        // Cache import per organization.
        public static readonly string CatchesScheldestromen = $"{Catches}:{OrganizationNames.Scheldestromen}";
        public static readonly string CatchesWestEnMidden = $"{Catches}:{OrganizationNames.WestEnMidden}";
        public static readonly string CatchesMura = $"{Catches}:{OrganizationNames.Mura}";
        public static readonly string CatchesLimburg = $"{Catches}:{OrganizationNames.Limburg}";
        public static readonly string CatchesBrabant = $"{Catches}:{OrganizationNames.Brabant}";
        public static readonly string CatchesFryslan = $"{Catches}:{OrganizationNames.Fryslan}";
        public static readonly string CatchesMrb = $"{Catches}:{OrganizationNames.Rivierenland}";
        public static readonly string CatchesNoordoostnederland = $"{Catches}:{OrganizationNames.Noordoostnederland}";
        public static readonly string CatchesZuiderzeeland = $"{Catches}:{OrganizationNames.Zuiderzeeland}";

        // Time registration import per organization.
        public static readonly string TimeRegistrationsScheldestromen = $"{TimeRegistrations}:{OrganizationNames.Scheldestromen}";
        public static readonly string TimeRegistrationsWestEnMidden = $"{TimeRegistrations}:{OrganizationNames.WestEnMidden}";
        public static readonly string TimeRegistrationsLimburg = $"{TimeRegistrations}:{OrganizationNames.Limburg}";
        public static readonly string TimeRegistrationsMura = $"{TimeRegistrations}:{OrganizationNames.Mura}";
        public static readonly string TimeRegistrationsBrabant = $"{TimeRegistrations}:{OrganizationNames.Brabant}";
        public static readonly string TimeRegistrationsFryslan = $"{TimeRegistrations}:{OrganizationNames.Fryslan}";
        public static readonly string TimeRegistrationsMrb = $"{TimeRegistrations}:{OrganizationNames.Rivierenland}";
        public static readonly string TimeRegistrationsNoordoostnederland = $"{TimeRegistrations}:{OrganizationNames.Noordoostnederland}";
        public static readonly string TimeRegistrationsZuiderzeeland = $"{TimeRegistrations}:{OrganizationNames.Zuiderzeeland}";

        // Time registration general import per organization.
        public static readonly string TimeRegistrationGeneralWestEnMidden = $"{TimeRegistrationGeneral}:{OrganizationNames.WestEnMidden}";
    }
}
