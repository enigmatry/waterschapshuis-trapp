using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddVersionRegionalLayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "WaterAuthority",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "SubAreaHourSquare",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "SubArea",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "Rayon",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VersionRegionalLayoutId",
                table: "CatchArea",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VersionRegionalLayout",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionRegionalLayout", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WaterAuthority_VersionRegionalLayoutId",
                table: "WaterAuthority",
                column: "VersionRegionalLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreaHourSquare_VersionRegionalLayoutId",
                table: "SubAreaHourSquare",
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

            migrationBuilder.CreateIndex(
                name: "IX_VersionRegionalLayout_Name",
                table: "VersionRegionalLayout",
                column: "Name",
                unique: true);

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
                name: "FK_SubAreaHourSquare_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "SubAreaHourSquare",
                column: "VersionRegionalLayoutId",
                principalTable: "VersionRegionalLayout",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterAuthority_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "WaterAuthority",
                column: "VersionRegionalLayoutId",
                principalTable: "VersionRegionalLayout",
                principalColumn: "Id");

            migrationBuilder.SqlScript("20201127130240_AddVersionRegionalLayout.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201127130240_AddVersionRegionalLayout.Down.sql");

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
                name: "FK_SubAreaHourSquare_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "SubAreaHourSquare");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterAuthority_VersionRegionalLayout_VersionRegionalLayoutId",
                table: "WaterAuthority");

            migrationBuilder.DropTable(
                name: "VersionRegionalLayout");

            migrationBuilder.DropIndex(
                name: "IX_WaterAuthority_VersionRegionalLayoutId",
                table: "WaterAuthority");

            migrationBuilder.DropIndex(
                name: "IX_SubAreaHourSquare_VersionRegionalLayoutId",
                table: "SubAreaHourSquare");

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
                table: "SubAreaHourSquare");

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
    }
}
