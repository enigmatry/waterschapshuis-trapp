using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class DbObjectsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200911082912_DbObjectsCreate.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
