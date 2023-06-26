using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class Alter_fn_HistoricalTraps_Geo_and_fn_HeatMapReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210406144458_Alter_fn_HistoricalTraps_Geo_and_fn_HeatMapReport.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210406144458_Alter_fn_HistoricalTraps_Geo_and_fn_HeatMapReport.Down.sql");
        }
    }
}
