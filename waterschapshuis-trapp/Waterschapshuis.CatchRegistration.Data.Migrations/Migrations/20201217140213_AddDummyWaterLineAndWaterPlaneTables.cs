using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddDummyWaterLineAndWaterPlaneTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201217140213_AddDummyWaterLineAndWaterPlaneTables.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201217140213_AddDummyWaterLineAndWaterPlaneTables.Down.sql");
        }
    }
}
