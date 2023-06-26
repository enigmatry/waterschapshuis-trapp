using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AlterFunction_HistoricalTraps_FilterTrapsWithoutCatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201229105259_AlterFunction_HistoricalTraps_FilterTrapsWithoutCatches.Up.Sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201229105259_AlterFunction_HistoricalTraps_FilterTrapsWithoutCatches.Down.sql");
        }
    }
}
