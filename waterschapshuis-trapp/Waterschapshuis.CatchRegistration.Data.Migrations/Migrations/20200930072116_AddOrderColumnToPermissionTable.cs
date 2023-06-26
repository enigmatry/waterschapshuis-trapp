using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddOrderColumnToPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("da236317-eff5-44c4-b668-006ee2914309") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("fcf68091-04f0-4714-b889-b9230435feff") });

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

            migrationBuilder.AddColumn<short>(
                name: "Order",
                table: "Permission",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "Order",
                value: (short)1);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "Order",
                value: (short)2);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "Order",
                value: (short)3);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "Order",
                value: (short)4);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "Order",
                value: (short)5);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 6,
                column: "Order",
                value: (short)6);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 7,
                column: "Order",
                value: (short)7);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Urenrapportage beheer", (short)8 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Gebruikers", (short)9 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Beheer", (short)10 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Beheerder Rechten Toekennen", (short)11 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Alleen lezen", (short)12 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Externe data interface (publiek)", (short)13 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Name", "Order" },
                values: new object[] { "Externe data interface", (short)14 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 100,
                column: "Order",
                value: (short)100);

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 13, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 13, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 10, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 9, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 12, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 11, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 10, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 14, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 8, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, new Guid("fcf68091-04f0-4714-b889-b9230435feff") });

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

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Permission");

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
                    { 8, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 13, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 10, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 8, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 8, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 11, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 11, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 12, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") }
                });
        }
    }
}
