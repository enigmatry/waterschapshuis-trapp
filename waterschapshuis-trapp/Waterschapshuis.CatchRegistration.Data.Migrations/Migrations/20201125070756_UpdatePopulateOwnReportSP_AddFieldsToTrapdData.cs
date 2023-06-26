using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdatePopulateOwnReportSP_AddFieldsToTrapdData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201125070756_UpdatePopulateOwnReportSP_AddFieldsToTrapData.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201125070756_UpdatePopulateOwnReportSP_AddFieldsToTrapData.Down.sql");
        }
    }
}
