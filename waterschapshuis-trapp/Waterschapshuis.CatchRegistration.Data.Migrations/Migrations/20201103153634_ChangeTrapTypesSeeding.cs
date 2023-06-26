using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class ChangeTrapTypesSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("54af411e-25f6-2a11-4bbf-e7547e212e76"),
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("586729d8-980e-2a76-81f2-dbb5c57c9d6f"),
                column: "Active",
                value: false);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("dc90faa1-1ad8-a4f2-22c2-582dcc5d4a84"),
                column: "AllowNotCatching",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("54af411e-25f6-2a11-4bbf-e7547e212e76"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("586729d8-980e-2a76-81f2-dbb5c57c9d6f"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("dc90faa1-1ad8-a4f2-22c2-582dcc5d4a84"),
                column: "AllowNotCatching",
                value: true);
        }
    }
}
