using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddedMissingDataInSPPopulateOwnReportData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210518161431_AddedMissingDataInSPPopulateOwnReportData.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210518161431_AddedMissingDataInSPPopulateOwnReportData.Down.sql");
        }
    }
}
