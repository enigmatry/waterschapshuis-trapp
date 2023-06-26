using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddOwnReportData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "report");

            migrationBuilder.CreateTable(
                name: "OwnReportData",
                schema: "report",
                columns: table => new
                {
                    CreatedOn = table.Column<DateTimeOffset>(nullable: true),
                    RecordedOn = table.Column<DateTimeOffset>(nullable: true),
                    RecordedOnYear = table.Column<int>(nullable: true),
                    NumberOfCatches = table.Column<int>(nullable: true),
                    NumberOfByCatches = table.Column<int>(nullable: true),
                    NumberOfCatchesPreviousYear = table.Column<int>(nullable: true),
                    NumberOfByCatchesPreviousYear = table.Column<int>(nullable: true),
                    Period = table.Column<int>(nullable: true),
                    Week = table.Column<int>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    OwnerName = table.Column<string>(maxLength: 255, nullable: true),
                    NumberOfTraps = table.Column<int>(nullable: true),
                    TrapStatus = table.Column<string>(maxLength: 20, nullable: true),
                    TrapTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    TrappingTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    IsByCatch = table.Column<bool>(nullable: true),
                    CatchTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    OrganizationName = table.Column<string>(maxLength: 50, nullable: false),
                    WaterAuthorityName = table.Column<string>(maxLength: 60, nullable: false),
                    RayonName = table.Column<string>(maxLength: 50, nullable: false),
                    CatchAreaName = table.Column<string>(maxLength: 50, nullable: false),
                    SubAreaName = table.Column<string>(maxLength: 10, nullable: false),
                    HourSquareName = table.Column<string>(maxLength: 5, nullable: false),
                    ProvinceName = table.Column<string>(maxLength: 50, nullable: true),
                    FieldTestName = table.Column<string>(maxLength: 250, nullable: true),
                    Hours = table.Column<double>(nullable: true),
                    HoursPreviousYear = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.SqlScript("20200920063300_AddOwnReportData.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200920063300_AddOwnReportData.Up.sql");

            migrationBuilder.DropTable(
                name: "OwnReportData",
                schema: "report");
        }
    }
}
