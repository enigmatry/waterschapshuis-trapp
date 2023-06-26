using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddNewColumnsToSubAreaHourSquare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DitchNew",
                table: "SubAreaHourSquare",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KmWaterwayNew",
                table: "SubAreaHourSquare",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WetDitchNew",
                table: "SubAreaHourSquare",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DitchNew",
                table: "SubAreaHourSquare");

            migrationBuilder.DropColumn(
                name: "KmWaterwayNew",
                table: "SubAreaHourSquare");

            migrationBuilder.DropColumn(
                name: "WetDitchNew",
                table: "SubAreaHourSquare");
        }
    }
}
