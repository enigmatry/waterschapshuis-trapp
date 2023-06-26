using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddOwnReportView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200915122311_AddOwnReportView.Up.sql");

            migrationBuilder.DeleteData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("8d2dfefb-166c-48c2-a3de-477cb5cd5ec4"));

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
		                    ""caption"": ""Datum"",
		                    ""dataField"": ""recordedOnYear"",
		                    ""dataType"": ""number"",
		                    ""sortOrder"": ""asc""
	                    },
	                    {
		                    ""caption"": ""Periode"",
		                    ""dataField"": ""period"",
		                    ""sortOrder"": ""asc"",
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
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vangsten"",
		                    ""dataField"": ""numberOfCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
		                    ""area"": ""data""
	                    },
	                    {
		                    ""caption"": ""Bijvangsten"",
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
	                    {
		                    ""caption"": ""Vangsten VJ"",
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
	                    {
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
                        {
                            ""caption"": ""Vangsten / Bijvangsten"",
                            ""dxFunctionPlaceholder"": ""Divide:Vangsten,Bijvangsten"",
                            ""dataType"": ""number"",
                            ""format"": {
                                ""type"": ""largeNumber"",
                                ""precision"": 3
                            }
                        },
	                    {
		                    ""caption"": ""Aantal vallen"",
		                    ""dataField"": ""numberOfTraps"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
	                    {
		                    ""caption"": ""Uren"",
                            ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""sortOrder"": ""asc""
                        },
	                    {
		                    ""caption"": ""Uren VJ"",
                            ""dataField"": ""hoursPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""sortOrder"": ""asc""
                        }
                    ]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20200915122311_AddOwnReportView.Down.sql");

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
		                    ""dataField"": ""trappingType"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vangmiddeltype"",
		                    ""dataField"": ""trapType"",
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
		                    ""dataField"": ""catchType"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Datum"",
		                    ""dataField"": ""catchRecordedOn"",
		                    ""dataType"": ""date"",
		                    ""sortOrder"": ""asc"",
		                    ""groupInterval"": ""year""
	                    },
	                    {
		                    ""caption"": ""Periode"",
		                    ""dataField"": ""period"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Persoon"",
		                    ""dataField"": ""userFulName"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Week"",
		                    ""dataField"": ""week"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Vangsten"",
		                    ""dataField"": ""numberOfCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
		                    ""area"": ""data""
	                    },
	                    {
		                    ""caption"": ""Bijvangsten"",
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
                        {
                            ""caption"": ""Vangsten / Bijvangsten"",
                            ""dxFunctionPlaceholder"": ""Divide:Vangsten,Bijvangsten"",
                            ""dataType"": ""number"",
                            ""format"": {
                                ""type"": ""largeNumber"",
                                ""precision"": 3
                            }
                        },
	                    {
		                    ""caption"": ""Km waterweg"",
		                    ""dataField"": ""kmOfWaterways"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
	                    {
		                    ""caption"": ""Aantal vallen"",
		                    ""dataField"": ""numberOfTraps"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
	                    {
		                    ""dataField"": ""catchId"",
		                    ""visible"": false
	                    },
	                    {
		                    ""dataField"": ""catchRecordedOn"",
		                    ""visible"": false
	                    }
                    ]");

            migrationBuilder.InsertData(
                table: "ReportTemplate",
                columns: new[] { "Id", "Active", "Content", "ExportFileName", "Exported", "Group", "Key", "RouteUri", "Title", "Type" },
                values: new object[] { new Guid("8d2dfefb-166c-48c2-a3de-477cb5cd5ec4"), true, @"[
	                    {
		                    ""caption"": ""Organisatie"",
		                    ""dataField"": ""organizationName"",
		                    ""sortOrder"": ""asc"",
		                    ""area"": ""row"",
		                    ""width"": 120
	                    },
	                    {
		                    ""caption"": ""Datum"",
		                    ""dataField"": ""catchRecordedOn"",
		                    ""dataType"": ""date"",
		                    ""sortOrder"": ""asc"",
		                    ""groupInterval"": ""year"",
		                    ""area"": ""column""
	                    },
	                    {
		                    ""caption"": ""Totaal"",
		                    ""dataField"": ""numberOfCatches"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum"",
		                    ""area"": ""data""
	                    },
	                    {
		                    ""dataField"": ""catchId"",
		                    ""visible"": false
	                    },
	                    {
		                    ""dataField"": ""catchRecordedOn"",
		                    ""visible"": false
	                    }
                    ]", "vangstrapportage", false, "Vangstrapportage", "CatchId", "catches-per-organization", "Aantal vangsten per organisatie", (byte)1 });
        }
    }
}
