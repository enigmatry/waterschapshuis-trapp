using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class WaterLineWaterPlaneTablesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201218162618_WaterLineWaterPlaneTablesChanges.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
