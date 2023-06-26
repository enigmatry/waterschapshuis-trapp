using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AlterFunction_GetWeekOfYearWithCustomRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210218081613_AlterFunction_GetWeekOfYearWithCustomRule.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210218081613_AlterFunction_GetWeekOfYearWithCustomRule.Down.sql");
        }
    }
}
