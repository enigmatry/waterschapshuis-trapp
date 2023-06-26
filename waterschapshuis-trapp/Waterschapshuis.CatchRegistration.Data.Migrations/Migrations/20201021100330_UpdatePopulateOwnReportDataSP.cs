using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdatePopulateOwnReportDataSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201021100330_UpdatePopulateOwnReportData.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201021100330_UpdatePopulateOwnReportData.Down.sql");
        }
    }
}
