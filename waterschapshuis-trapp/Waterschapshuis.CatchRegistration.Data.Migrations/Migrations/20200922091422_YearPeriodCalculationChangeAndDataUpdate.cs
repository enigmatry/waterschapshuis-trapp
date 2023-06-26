using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class YearPeriodCalculationChangeAndDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200922091422_YearPeriodCalculationChangeAndDataUpdate.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200922091422_YearPeriodCalculationChangeAndDataUpdate.Down.sql");
        }
    }
}
