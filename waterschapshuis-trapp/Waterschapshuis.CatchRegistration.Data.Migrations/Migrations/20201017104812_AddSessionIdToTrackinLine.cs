using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddSessionIdToTrackinLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "TrackingLine",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.SqlScript("20201017104812_AddSessionIdToTrackinLine.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201017104812_AddSessionIdToTrackinLine.Down.sql");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "TrackingLine");
        }
    }
}
