using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddVRL_ForPreviousYearDataToOwnReportData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210115091625_AddVRL_ForPreviousYearDataToOwnReportData.Up.Sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210115091625_AddVRL_ForPreviousYearDataToOwnReportData.Down.Sql");
        }
    }
}
