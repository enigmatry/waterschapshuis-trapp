using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddTimeRegistrationTypeToOwnReportDataAndUpdateTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "WaterAuthorityName",
               schema: "report",
               table: "OwnReportData",
               maxLength: 75,
               nullable: true,
               oldClrType: typeof(string),
               oldType: "nvarchar(75)",
               oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "SubAreaName",
                schema: "report",
                table: "OwnReportData",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "RayonName",
                schema: "report",
                table: "OwnReportData",
                maxLength: 75,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationName",
                schema: "report",
                table: "OwnReportData",
                maxLength: 75,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "HourSquareName",
                schema: "report",
                table: "OwnReportData",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "CatchAreaName",
                schema: "report",
                table: "OwnReportData",
                maxLength: 75,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "TimeRegistrationType",
                schema: "report",
                table: "OwnReportData",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("77f0ac51-3e70-4fec-9727-e68246525c22"),
                column: "Content",
                value: @"[
	                    {
		                    ""caption"": ""Organisatie"",
		                    ""dataField"": ""organizationName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Waterschap"",
		                    ""dataField"": ""waterAuthorityName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Rayon"",
		                    ""dataField"": ""rayonName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vanggebied"",
		                    ""dataField"": ""catchAreaName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Deelgebied"",
		                    ""dataField"": ""subAreaName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Provincie"",
		                    ""dataField"": ""provinceName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Bestrijdingssoort"",
		                    ""dataField"": ""trappingTypeName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vangmiddeltype"",
		                    ""dataField"": ""trapTypeName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Diersoort"",
		                    ""dataField"": ""catchTypeName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vangstsoort"",
		                    ""dataField"": ""catchTypeCategoryName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Jaar"",
		                    ""dataField"": ""recordedOnYear"",
		                    ""dataType"": ""number"",
		                    ""sortOrder"": ""asc""
	                    },
	                    {
		                    ""caption"": ""Periode"",
		                    ""dataField"": ""period"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Persoon"",
		                    ""dataField"": ""ownerName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Week"",
		                    ""dataField"": ""week"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Uurhok"",
		                    ""dataField"": ""hourSquareName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""VangstenHidden"",
		                    ""dataField"": ""numberOfCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Vangsten"",
                            ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""area"": ""data""
                        },
	                    {
		                    ""caption"": ""BijvangstenHidden"",
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true,
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Bijvangsten"",
                            ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
                        },
	                    {
		                    ""caption"": ""VangstenVJHidden"",
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true,
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Vangsten VJ"",
                            ""dxFunctionPlaceholder"": ""DoNothing:VangstenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
                        },
	                    {
		                    ""caption"": ""BijvangstenVJHidden"",
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true,
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Bijvangsten VJ"",
                            ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
                        },
	                    {
		                    ""caption"": ""Aantal vallen"",
		                    ""dataField"": ""numberOfTraps"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true
	                    },
	                    {
		                    ""caption"": ""UrenHidden"",
                            ""dataField"": ""hours"",
		                    ""summaryType"": ""sum"",
		                    ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Uren"",
                            ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
	                    {
		                    ""caption"": ""UrenVJHidden"",
                            ""dataField"": ""hoursPreviousYear"",
		                    ""summaryType"": ""sum"",
		                    ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
{
                            ""caption"": ""Uren VJ"",
                            ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Vangsten verschil (%)"",
                            ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": ""percent""
                        },
                        {
		                    ""caption"": ""Vangnachten"",
		                    ""dataField"": ""catchingNights"",
		                    ""dataType"": ""number"",
                            ""isMeasure"": true,
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true
	                    },
                        {
		                    ""caption"": ""KmWatergangHidden"",
		                    ""dataField"": ""kmWaterway"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""visible"": false,
                            ""isMeasure"": true
	                    },
                        {
		                    ""caption"": ""Km watergang"",
                            ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Vangsten / km"",
                            ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Uren / km"",
                            ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Vangsten / uur"",
                            ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
						{
		                    ""caption"": ""Veldproef"",
		                    ""dataField"": ""fieldTestName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 250
	                    },
                        {
                            ""caption"": ""Periode + Jaar"",
                            ""dataField"": ""yearAndPeriod"",
		                    ""dataType"": ""number"",
                            ""area"": ""filter"",
                            ""allowFiltering"": true,
                            ""filterType"": ""include""
                        },
						{
		                    ""caption"": ""Urencategorie"",
		                    ""dataField"": ""timeRegistrationType"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 250
	                    }
                    ]");

            migrationBuilder.SqlScript("20201204133423_AddTimeRegistrationTypeToOwnReportDataAndUpdateTemplate.Up.Sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeRegistrationType",
                schema: "report",
                table: "OwnReportData");

            migrationBuilder.AlterColumn<string>(
                name: "WaterAuthorityName",
                schema: "report",
                table: "OwnReportData",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubAreaName",
                schema: "report",
                table: "OwnReportData",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RayonName",
                schema: "report",
                table: "OwnReportData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationName",
                schema: "report",
                table: "OwnReportData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HourSquareName",
                schema: "report",
                table: "OwnReportData",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CatchAreaName",
                schema: "report",
                table: "OwnReportData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("77f0ac51-3e70-4fec-9727-e68246525c22"),
                column: "Content",
                value: @"[
	                    {
		                    ""caption"": ""Organisatie"",
		                    ""dataField"": ""organizationName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Waterschap"",
		                    ""dataField"": ""waterAuthorityName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Rayon"",
		                    ""dataField"": ""rayonName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vanggebied"",
		                    ""dataField"": ""catchAreaName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Deelgebied"",
		                    ""dataField"": ""subAreaName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Provincie"",
		                    ""dataField"": ""provinceName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Bestrijdingssoort"",
		                    ""dataField"": ""trappingTypeName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vangmiddeltype"",
		                    ""dataField"": ""trapTypeName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Diersoort"",
		                    ""dataField"": ""catchTypeName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vangstsoort"",
		                    ""dataField"": ""catchTypeCategoryName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Jaar"",
		                    ""dataField"": ""recordedOnYear"",
		                    ""dataType"": ""number"",
		                    ""sortOrder"": ""asc""
	                    },
	                    {
		                    ""caption"": ""Periode"",
		                    ""dataField"": ""period"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Persoon"",
		                    ""dataField"": ""ownerName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Week"",
		                    ""dataField"": ""week"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Uurhok"",
		                    ""dataField"": ""hourSquareName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""VangstenHidden"",
		                    ""dataField"": ""numberOfCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Vangsten"",
                            ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""area"": ""data""
                        },
	                    {
		                    ""caption"": ""BijvangstenHidden"",
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true,
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Bijvangsten"",
                            ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
                        },
	                    {
		                    ""caption"": ""VangstenVJHidden"",
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true,
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Vangsten VJ"",
                            ""dxFunctionPlaceholder"": ""DoNothing:VangstenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
                        },
	                    {
		                    ""caption"": ""BijvangstenVJHidden"",
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true,
                            ""visible"": false
	                    },
                        {
                            ""caption"": ""Bijvangsten VJ"",
                            ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
                        },
	                    {
		                    ""caption"": ""Aantal vallen"",
		                    ""dataField"": ""numberOfTraps"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true
	                    },
	                    {
		                    ""caption"": ""UrenHidden"",
                            ""dataField"": ""hours"",
		                    ""summaryType"": ""sum"",
		                    ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Uren"",
                            ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
	                    {
		                    ""caption"": ""UrenVJHidden"",
                            ""dataField"": ""hoursPreviousYear"",
		                    ""summaryType"": ""sum"",
		                    ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
{
                            ""caption"": ""Uren VJ"",
                            ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Vangsten verschil (%)"",
                            ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": ""percent""
                        },
                        {
		                    ""caption"": ""Vangnachten"",
		                    ""dataField"": ""catchingNights"",
		                    ""dataType"": ""number"",
                            ""isMeasure"": true,
		                    ""summaryType"": ""sum"",
                            ""isMeasure"": true
	                    },
                        {
		                    ""caption"": ""KmWatergangHidden"",
		                    ""dataField"": ""kmWaterway"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
                            ""visible"": false,
                            ""isMeasure"": true
	                    },
                        {
		                    ""caption"": ""Km watergang"",
                            ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
	                    },
                        {
                            ""caption"": ""Vangsten / km"",
                            ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 3
                            }
                        },
                        {
                            ""caption"": ""Uren / km"",
                            ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 3
                            }
                        },
                        {
                            ""caption"": ""Vangsten / uur"",
                            ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 3
                            }
                        },
						{
		                    ""caption"": ""Veldproef"",
		                    ""dataField"": ""fieldTestName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 250
	                    },
                        {
                            ""caption"": ""Periode + Jaar"",
                            ""dataField"": ""yearAndPeriod"",
		                    ""dataType"": ""number"",
                            ""area"": ""filter"",
                            ""allowFiltering"": true,
                            ""filterType"": ""include""
                        }
                    ]");

            migrationBuilder.SqlScript("20201204133423_AddTimeRegistrationTypeToOwnReportDataAndUpdateTemplate.Down.Sql");
        }
    }
}
