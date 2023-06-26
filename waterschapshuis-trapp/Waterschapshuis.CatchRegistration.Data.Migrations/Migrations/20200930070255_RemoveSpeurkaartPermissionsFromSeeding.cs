using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class RemoveSpeurkaartPermissionsFromSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, new Guid("da236317-eff5-44c4-b668-006ee2914309") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, new Guid("fcf68091-04f0-4714-b889-b9230435feff") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 11, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 11, new Guid("da236317-eff5-44c4-b668-006ee2914309") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 12, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 13, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 13, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 14, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 15, new Guid("da236317-eff5-44c4-b668-006ee2914309") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Gebruikers");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Beheer");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Alleen lezen");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Externe data interface (publiek)");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Externe data interface");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Beheerder Rechten Toekennen");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Urenrapportage beheer");

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 14, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 13, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 10, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 8, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 8, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 9, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 11, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 11, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 12, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("fcf68091-04f0-4714-b889-b9230435feff") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 11, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 11, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 12, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 13, new Guid("da236317-eff5-44c4-b668-006ee2914309") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 14, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Speurkaart bekijken");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Speurkaart wijzigen");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Gebruikers");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Beheer");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Alleen lezen");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Externe data interface (no auth)");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Externe data interface");

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 16, "Urenrapportage beheer" },
                    { 15, "Beheerder Rechten Toekennen" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 12, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 10, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 10, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 11, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 13, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 13, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 14, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 9, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 8, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 9, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 8, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 11, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 10, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 16, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 15, new Guid("da236317-eff5-44c4-b668-006ee2914309") });
        }
    }
}
