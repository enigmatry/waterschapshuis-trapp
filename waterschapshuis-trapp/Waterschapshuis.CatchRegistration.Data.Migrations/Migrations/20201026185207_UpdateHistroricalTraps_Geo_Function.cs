using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdateHistroricalTraps_Geo_Function : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201026185207_UpdateHistroricalTraps_Geo_Function.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201026185207_UpdateHistroricalTraps_Geo_Function.Down.sql");
        }
    }
}
