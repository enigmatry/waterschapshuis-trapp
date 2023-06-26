using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddActiveColumnToTimeRegistrationCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("72b53fd0-c68f-4207-f6a1-08d8765994e4"));

            migrationBuilder.DeleteData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("cd4647ef-3f8f-4236-f593-08d8765994e4"));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TimeRegistrationCategory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("0ac78057-29d4-40db-f5ba-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("16dd1391-39c9-4e7a-f54d-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("220d84d1-a3b0-4e90-f5ee-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("479a4375-6820-4d46-f61c-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("4a956e92-3d0c-484b-f65d-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("4b983ec7-c03d-4930-f606-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("4d773155-70bc-4c67-f6e3-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("71106f2a-e599-4030-f70e-08d8765994e4"),
                columns: new[] { "Active", "Name" },
                values: new object[] { true, "Ziekte" });

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("82217e2b-2268-461c-f538-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("8ac90922-ed2d-4de1-f6f9-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("935340b4-929e-46ce-f0f7-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("ab2e247d-0d85-493d-f51f-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("fac9ee8d-c03b-40b3-f647-08d8765994e4"),
                column: "Active",
                value: true);

            migrationBuilder.InsertData(
                table: "TimeRegistrationCategory",
                columns: new[] { "Id", "Active", "Name" },
                values: new object[,]
                {
                    { new Guid("b36f9910-3495-4e95-84bd-cb44d1d03da2"), true, "Vangstregistratie" },
                    { new Guid("8c8be6d7-da92-44a1-927b-1db91a03c099"), true, "Reistijd" },
                    { new Guid("5eac706a-9f39-42d9-8d8f-d772d0482cc0"), true, "Opleiding / Training" },
                    { new Guid("1feed3fc-9931-49ec-adbf-d407ec1d9bc4"), true, "Life mica" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("1feed3fc-9931-49ec-adbf-d407ec1d9bc4"));

            migrationBuilder.DeleteData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("5eac706a-9f39-42d9-8d8f-d772d0482cc0"));

            migrationBuilder.DeleteData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("8c8be6d7-da92-44a1-927b-1db91a03c099"));

            migrationBuilder.DeleteData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("b36f9910-3495-4e95-84bd-cb44d1d03da2"));

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TimeRegistrationCategory");

            migrationBuilder.UpdateData(
                table: "TimeRegistrationCategory",
                keyColumn: "Id",
                keyValue: new Guid("71106f2a-e599-4030-f70e-08d8765994e4"),
                column: "Name",
                value: "Ziek");

            migrationBuilder.InsertData(
                table: "TimeRegistrationCategory",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("72b53fd0-c68f-4207-f6a1-08d8765994e4"), "Reistijd Werk-Werk" });

            migrationBuilder.InsertData(
                table: "TimeRegistrationCategory",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("cd4647ef-3f8f-4236-f593-08d8765994e4"), "Cursus" });
        }
    }
}
