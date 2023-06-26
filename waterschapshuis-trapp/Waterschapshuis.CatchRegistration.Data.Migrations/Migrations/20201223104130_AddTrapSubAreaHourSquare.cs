using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddTrapSubAreaHourSquare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrapSubAreaHourSquare",
                columns: table => new
                {
                    TrapId = table.Column<Guid>(nullable: false),
                    SubAreaHourSquareId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrapSubAreaHourSquare", x => new { x.TrapId, x.SubAreaHourSquareId });
                    table.ForeignKey(
                        name: "FK_TrapSubAreaHourSquare_SubAreaHourSquare_SubAreaHourSquareId",
                        column: x => x.SubAreaHourSquareId,
                        principalTable: "SubAreaHourSquare",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrapSubAreaHourSquare_Trap_TrapId",
                        column: x => x.TrapId,
                        principalTable: "Trap",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrapSubAreaHourSquare_SubAreaHourSquareId",
                table: "TrapSubAreaHourSquare",
                column: "SubAreaHourSquareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrapSubAreaHourSquare");
        }
    }
}
