using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddIsTrackingMapCheckToTrackingsLayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200916064008_AlterTrackingsLayers_AddIsTrackingMap.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200916064008_AlterTrackingsLayers_AddIsTrackingMap.Down.sql");
        }
    }
}
