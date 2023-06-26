using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdatePopulateOwnReportSP_AddMissingTrapData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201123111250_UpdatePopulateOwnReportSP_AddMissingTrapData.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201123111250_UpdatePopulateOwnReportSP_AddMissingTrapData.Down.sql");
        }
    }
}
