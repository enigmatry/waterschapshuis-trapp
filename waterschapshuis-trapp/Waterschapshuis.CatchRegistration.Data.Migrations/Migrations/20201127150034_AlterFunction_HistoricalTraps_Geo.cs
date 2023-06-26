using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AlterFunction_HistoricalTraps_Geo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201127150034_AlterFunction_HistoricalTraps_Geo.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201127150034_AlterFunction_HistoricalTraps_Geo.Down.sql");
        }
    }
}
