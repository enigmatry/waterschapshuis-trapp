using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddActiveSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    AccessToken = table.Column<string>(nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(nullable: false),
                    Origin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveSession_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSession_CreatedById",
                table: "ActiveSession",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSession");
        }
    }
}
