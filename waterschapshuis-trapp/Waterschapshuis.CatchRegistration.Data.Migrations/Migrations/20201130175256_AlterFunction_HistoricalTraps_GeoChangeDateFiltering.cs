using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AlterFunction_HistoricalTraps_GeoChangeDateFiltering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201130175256_AlterFunction_HistoricalTraps_GeoChangeDateFiltering.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201130175256_AlterFunction_HistoricalTraps_GeoChangeDateFiltering.Down.sql");
        }
    }
}
