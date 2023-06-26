using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class ExternalApiRoleSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 14, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 14, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 14, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 14, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") });
        }
    }
}
