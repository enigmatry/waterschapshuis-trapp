using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddBaitCageNotCatchingIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TrapTypeTrapStatusStyle",
                columns: new[] { "TrapTypeId", "TrapStatus", "IconName" },
                values: new object[] { new Guid("6539abe5-081d-a060-31b9-1c5c43f74abb"), (byte)2, "trap-unplaced-lokaaskooi.svg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TrapTypeTrapStatusStyle",
                keyColumns: new[] { "TrapTypeId", "TrapStatus" },
                keyValues: new object[] { new Guid("6539abe5-081d-a060-31b9-1c5c43f74abb"), (byte)2 });
        }
    }
}
