using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddConstraintsToTimeRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimeRegistration_UserId",
                table: "TimeRegistration");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_UserId_TrappingTypeId_SubAreaHourSquareId_Date",
                table: "TimeRegistration",
                columns: new[] { "UserId", "TrappingTypeId", "SubAreaHourSquareId", "Date" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimeRegistration_UserId_TrappingTypeId_SubAreaHourSquareId_Date",
                table: "TimeRegistration");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_UserId",
                table: "TimeRegistration",
                column: "UserId");
        }
    }
}
