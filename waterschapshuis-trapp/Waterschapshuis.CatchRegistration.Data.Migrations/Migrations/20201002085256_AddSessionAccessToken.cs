using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddSessionAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSession");

            migrationBuilder.CreateTable(
                name: "UserSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(nullable: true),
                    Origin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSession_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionAccessToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Expired = table.Column<bool>(nullable: false),
                    UserSessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAccessToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionAccessToken_UserSession_UserSessionId",
                        column: x => x.UserSessionId,
                        principalTable: "UserSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionAccessToken_UserSessionId",
                table: "SessionAccessToken",
                column: "UserSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_CreatedById",
                table: "UserSession",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionAccessToken");

            migrationBuilder.DropTable(
                name: "UserSession");

            migrationBuilder.CreateTable(
                name: "ActiveSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Origin = table.Column<int>(type: "int", nullable: false)
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
    }
}
