using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddedReportTemplateChartType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChartType",
                table: "ReportTemplate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("2f87ea3e-e5ac-4834-8448-db36752996be"),
                column: "ChartType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("5dc2fb10-cc66-49f3-be07-f37a0dbdb221"),
                column: "ChartType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("77f0ac51-3e70-4fec-9727-e68246525c22"),
                column: "ChartType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("b1bb6e24-270f-4d1d-9452-2d22fad19242"),
                column: "ChartType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("bb344bf9-b58b-4593-bea4-0a3017c75663"),
                column: "ChartType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("c8ca6f77-8ee4-4d1a-baa8-dd12c04589c1"),
                column: "ChartType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("e48c2e6e-c5fc-402c-ac59-0cea26124d1b"),
                column: "ChartType",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChartType",
                table: "ReportTemplate");
        }
    }
}
