using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class DataInitialInsertAndUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200911083527_Geoserver_Views_Pk_Config.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
