using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdateTrapDetailsTVFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201118171346_UpdateTrapDetailsTVFunctions.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201118171346_UpdateTrapDetailsTVFunctions.Down.sql");
        }
    }
}
