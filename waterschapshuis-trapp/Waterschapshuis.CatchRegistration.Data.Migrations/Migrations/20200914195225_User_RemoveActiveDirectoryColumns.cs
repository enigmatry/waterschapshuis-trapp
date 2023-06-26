using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class User_RemoveActiveDirectoryColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ObjectId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "User",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);
        }
    }
}
