using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class RemoveSessionIdFromTrackingLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201223135800_RemoveSessionIdToTrackinLine.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201223135800_RemoveSessionIdToTrackinLine.Down.sql");
        }
    }
}
