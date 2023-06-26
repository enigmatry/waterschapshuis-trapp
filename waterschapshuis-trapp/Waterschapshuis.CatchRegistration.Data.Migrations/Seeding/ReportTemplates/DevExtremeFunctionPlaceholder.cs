namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.ReportTemplates
{
    public static class DevExtremeFunctionPlaceholder
    {
        public static readonly string RelativeComparisonThisYearAndLastYearCatches = "RelativeComparison:VangstenHidden,VangstenVJHidden";
        public static readonly string RelativeComparisonThisYearAndLastYearHours = "RelativeComparison:UrenHidden,UrenVJHidden";
        public static readonly string DivideCatchesByKilometers = "DivideByTotal:VangstenHidden,KmWatergangHidden";
        public static readonly string DivideLastYearCatchesByKilometers = "DivideByTotal:VangstenVJHidden,KmWatergangHidden";
        public static readonly string DivideHoursByKilometers = "DivideByTotal:UrenHidden,KmWatergangHidden";
        public static readonly string DivideCatchesByHours = "Divide:VangstenHidden,UrenHidden";
        public static readonly string TotalKmWaterways = "Total:KmWatergangHidden";
        public static readonly string DoNothingCatches = "DoNothing:VangstenHidden";
        public static readonly string DoNothingByCatches = "DoNothing:BijvangstenHidden";
        public static readonly string DoNothingLastYearCatches = "DoNothing:VangstenVJHidden";
        public static readonly string DoNothingLastYearByCatches = "DoNothing:BijvangstenVJHidden";
        public static readonly string DoNothingHours = "DoNothing:UrenHidden";
        public static readonly string DoNothingHoursLastYear = "DoNothing:UrenVJHidden";
        public static readonly string DoNothingHoursOther = "DoNothing:UrenOverigHidden";
    }
}
