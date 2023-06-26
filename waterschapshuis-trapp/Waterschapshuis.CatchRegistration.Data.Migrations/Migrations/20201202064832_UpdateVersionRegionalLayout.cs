using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdateVersionRegionalLayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatchArea_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "CatchArea");

            migrationBuilder.DropForeignKey(
                name: "FK_Rayon_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "Rayon");

            migrationBuilder.DropForeignKey(
                name: "FK_SubArea_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "SubArea");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterAuthority_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "WaterAuthority");

            migrationBuilder.DropIndex(
                name: "IX_WaterAuthority_VersionRegionalLayoutId",
                table: "WaterAuthority");

            migrationBuilder.DropIndex(
                name: "IX_SubArea_VersionRegionalLayoutId",
                table: "SubArea");

            migrationBuilder.DropIndex(
                name: "IX_Rayon_VersionRegionalLayoutId",
                table: "Rayon");

            migrationBuilder.DropIndex(
                name: "IX_CatchArea_VersionRegionalLayoutId",
                table: "CatchArea");

            migrationBuilder.DropColumn(
                name: "VersionRegionalLayoutId",
                table: "WaterAuthority");

            migrationBuilder.DropColumn(
                name: "VersionRegionalLayoutId",
                table: "SubArea");

            migrationBuilder.DropColumn(
                name: "VersionRegionalLayoutId",
                table: "Rayon");

            migrationBuilder.DropColumn(
                name: "VersionRegionalLayoutId",
                table: "CatchArea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "WaterAuthority",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "SubArea",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "Rayon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "CatchArea",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterAuthority_VersionRegionalLayoutId",
                table: "WaterAuthority",
                column: "VersionRegionalLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_SubArea_VersionRegionalLayoutId",
                table: "SubArea",
                column: "VersionRegionalLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Rayon_VersionRegionalLayoutId",
                table: "Rayon",
                column: "VersionRegionalLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CatchArea_VersionRegionalLayoutId",
                table: "CatchArea",
                column: "VersionRegionalLayoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatchArea_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "CatchArea",
                column: "VersionRegionalLayoutId",
                principalTable: "VersionRegionalLayout",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rayon_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "Rayon",
                column: "VersionRegionalLayoutId",
                principalTable: "VersionRegionalLayout",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubArea_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "SubArea",
                column: "VersionRegionalLayoutId",
                principalTable: "VersionRegionalLayout",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterAuthority_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "WaterAuthority",
                column: "VersionRegionalLayoutId",
                principalTable: "VersionRegionalLayout",
                principalColumn: "Id");
        }
    }
}
