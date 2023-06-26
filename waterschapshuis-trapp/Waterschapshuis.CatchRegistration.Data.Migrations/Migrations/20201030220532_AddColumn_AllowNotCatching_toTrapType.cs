using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddColumn_AllowNotCatching_toTrapType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowNotCatching",
                table: "TrapType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("0d39267f-2ef1-5978-4fb5-68b7ec956f0f"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("10d026ed-0d1e-7b3b-786c-0246c0367222"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("198be8a5-569f-233c-93ef-3879aa97120c"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("3b1215f3-05a2-660e-85f4-d27dd5ae115d"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("3c881890-4d00-6b96-25be-67dec7314b2f"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("47890978-910d-969a-2499-0b848fa80f8a"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("54af411e-25f6-2a11-4bbf-e7547e212e76"),
                columns: new[] { "Active", "AllowNotCatching" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("586729d8-980e-2a76-81f2-dbb5c57c9d6f"),
                columns: new[] { "Active", "AllowNotCatching" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("6539abe5-081d-a060-31b9-1c5c43f74abb"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("9f91a9d1-77d9-06d9-03a9-18f2efcc0bcc"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("b73b56e0-9691-6eff-43c5-0f0f36ff69c2"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("dc56c661-710f-63ab-870b-36ede6f9204e"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("dc90faa1-1ad8-a4f2-22c2-582dcc5d4a84"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("e2ba4c87-65fd-2f70-a1ea-68a6a4549db6"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("e887e560-8959-8994-960b-a85fc5c22e4d"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("eb9f0577-a55e-0ed7-2102-f04cefe54680"),
                column: "AllowNotCatching",
                value: true);

            migrationBuilder.UpdateData(
                table: "TrapType",
                keyColumn: "Id",
                keyValue: new Guid("ff7c880c-9ac6-433e-1b92-3563869a48e2"),
                column: "AllowNotCatching",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowNotCatching",
                table: "TrapType");

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
        }
    }
}
