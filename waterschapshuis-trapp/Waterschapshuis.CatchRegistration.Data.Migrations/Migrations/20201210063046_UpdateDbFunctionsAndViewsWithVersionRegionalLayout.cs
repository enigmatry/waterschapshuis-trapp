using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdateDbFunctionsAndViewsWithVersionRegionalLayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201210063046_UpdateDbFunctionsAndViewsWithVersionRegionalLayout.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201210063046_UpdateDbFunctionsAndViewsWithVersionRegionalLayout.Down.sql");
        }
    }
}
