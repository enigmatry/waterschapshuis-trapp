using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddOwnReportColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "CatchingNights",
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
		                    ""caption"": ""Aantal vallen"",
		                    ""dataField"": ""numberOfTraps"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    },
	                    {
		                    ""caption"": ""Uren"",
                            ""dataField"": ""hours"",
		                    ""summaryType"": ""sum"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
                            ""format"": {
                                    ""type"": ""largeNumber"",
			                        ""precision"": 2
                            }
                        },
	                    {
		                    ""caption"": ""Uren VJ"",
                            ""dataField"": ""hoursPreviousYear"",
		                    ""summaryType"": ""sum"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
                            ""format"": {
                                    ""type"": ""largeNumber"",
			                        ""precision"": 2
                            }
                        },
	                    {
		                    ""caption"": ""Vangnachten"",
		                    ""dataField"": ""catchingNights"",
		                    ""dataType"": ""number"",
		                    ""summaryType"": ""sum""
	                    }
                    ]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatchingNights",
                schema: "report",
                table: "OwnReportData");

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
		                    ""summaryType"": ""sum"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
                            ""format"": {
                                    ""type"": ""largeNumber"",
			                        ""precision"": 2
                            }
                        },
	                    {
		                    ""caption"": ""Uren VJ"",
                            ""dataField"": ""hoursPreviousYear"",
		                    ""summaryType"": ""sum"",
		                    ""sortOrder"": ""asc"",
		                    ""dataType"": ""number"",
                            ""format"": {
                                    ""type"": ""largeNumber"",
			                        ""precision"": 2
                            }
                        }
                    ]");
        }
    }
}
