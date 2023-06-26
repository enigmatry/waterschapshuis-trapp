using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdatePopulateOwnReportSP_AddV1Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                schema: "report",
                table: "OwnReportData",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.SqlScript("20201124134331_UpdatePopulateOwnReportSP_AddV1Data.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                schema: "report",
                table: "OwnReportData",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.SqlScript("20201124134331_UpdatePopulateOwnReportSP_AddV1Data.Down.sql");
        }
    }
}
