using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class Add_GetWeekOfYearWithCustomRule_Function : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210205091314_Add_GetWeekOfYearWithCustomRule_Fn.Up.Sql");
            migrationBuilder.SqlScript("20210205091314_Alter_PopulateOwnReportData_SP.Up.Sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210205091314_Add_GetWeekOfYearWithCustomRule_Fn.Down.Sql");
            migrationBuilder.SqlScript("20210205091314_Alter_PopulateOwnReportData_SP.Down.Sql");
        }
    }
}
