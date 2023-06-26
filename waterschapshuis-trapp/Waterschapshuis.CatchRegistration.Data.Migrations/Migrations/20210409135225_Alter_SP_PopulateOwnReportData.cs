using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class Alter_SP_PopulateOwnReportData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210409135225_Alter_SP_PopulateOwnReportData.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210409135225_Alter_SP_PopulateOwnReportData.Down.sql");
        }
    }
}
