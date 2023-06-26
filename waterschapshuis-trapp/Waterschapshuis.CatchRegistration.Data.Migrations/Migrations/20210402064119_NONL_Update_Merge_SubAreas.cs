using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class NONL_Update_Merge_SubAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210402064119_NONL_Merge_SubAreas.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
