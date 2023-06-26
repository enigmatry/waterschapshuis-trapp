using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class RemoveSubAreHourSquareFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DitchNew",
                table: "SubAreaHourSquare",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KmWaterwayNew",
                table: "SubAreaHourSquare",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WetDitchNew",
                table: "SubAreaHourSquare",
                type: "float",
                nullable: true);
        }
    }
}
