using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class StandardReportTemplatesSeedingAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ReportTemplate",
                columns: new[] { "Id", "Active", "Content", "ExportFileName", "Exported", "Group", "Key", "RouteUri", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("b1bb6e24-270f-4d1d-9452-2d22fad19242"), true, @"[
                	                {
                		                ""dataField"": ""organizationName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Organisatie"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 0,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""waterAuthorityName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Waterschap"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 1,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""rayonName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Rayon"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 2,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""catchAreaName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Vanggebied"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 3,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""row"",
                		                ""areaIndex"": 0
                	                },
                	                {
                		                ""dataField"": ""subAreaName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Deelgebied"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 4,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""provinceName"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Provincie"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 5,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""trappingTypeName"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Bestrijdingssoort"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 6,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""column"",
                		                ""areaIndex"": 0,
                		                ""filterValues"": [
                			                null,
                			                ""Muskusrat""
                		                ]
                	                },
                	                {
                		                ""dataField"": ""trapTypeName"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Vangmiddeltype"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 7,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""catchTypeName"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Diersoort"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 8,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""catchTypeCategoryName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Vangstsoort"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 9,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""recordedOnYear"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Jaar"",
                		                ""sortOrder"": ""asc"",
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 10,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""period"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Periode"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 11,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""ownerName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Persoon"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 12,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""filter"",
                		                ""areaIndex"": 0
                	                },
                	                {
                		                ""dataField"": ""week"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Week"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 13,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""hourSquareName"",
                		                ""dataType"": ""string"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Uurhok"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 120,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 14,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""row"",
                		                ""areaIndex"": 1
                	                },
                	                {
                		                ""dataField"": ""numberOfCatches"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""VangstenHidden"",
                		                ""summaryType"": ""sum"",
                		                ""visible"": false,
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 15,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Vangsten"",
                		                ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""area"": ""data"",
                		                ""index"": 16,
                		                ""_initProperties"": {
                			                ""area"": ""data"",
                			                ""areaIndex"": 0
                		                },
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""areaIndex"": 0
                	                },
                	                {
                		                ""dataField"": ""numberOfByCatches"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""BijvangstenHidden"",
                		                ""summaryType"": ""sum"",
                		                ""isMeasure"": true,
                		                ""visible"": false,
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 17,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Bijvangsten"",
                		                ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""index"": 18,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""numberOfCatchesPreviousYear"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""VangstenVJHidden"",
                		                ""summaryType"": ""sum"",
                		                ""isMeasure"": true,
                		                ""visible"": false,
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 19,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Vangsten VJ"",
                		                ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""index"": 20,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""numberOfByCatchesPreviousYear"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""BijvangstenVJHidden"",
                		                ""summaryType"": ""sum"",
                		                ""isMeasure"": true,
                		                ""visible"": false,
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 21,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Bijvangsten VJ"",
                		                ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""index"": 22,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""numberOfTraps"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Aantal vallen"",
                		                ""summaryType"": ""sum"",
                		                ""isMeasure"": true,
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 23,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""hours"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""UrenHidden"",
                		                ""summaryType"": ""sum"",
                		                ""isMeasure"": true,
                		                ""visible"": false,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 24,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Uren"",
                		                ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""index"": 25,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""data"",
                		                ""areaIndex"": 1
                	                },
                	                {
                		                ""dataField"": ""hoursPreviousYear"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""UrenVJHidden"",
                		                ""summaryType"": ""sum"",
                		                ""isMeasure"": true,
                		                ""visible"": false,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 26,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Uren VJ"",
                		                ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""index"": 27,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Vangsten verschil (%)"",
                		                ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": ""percent"",
                		                ""index"": 28,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Uren verschil (%)"",
                		                ""dxFunctionPlaceholder"": ""RelativeComparison:UrenHidden,UrenVJHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": ""percent"",
                		                ""index"": 29,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""catchingNights"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Vangnachten"",
                		                ""isMeasure"": true,
                		                ""summaryType"": ""sum"",
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 30,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""kmWaterway"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""KmWatergangHidden"",
                		                ""summaryType"": ""sum"",
                		                ""visible"": false,
                		                ""isMeasure"": true,
                		                ""_initProperties"": {
                			                ""summaryType"": ""sum""
                		                },
                		                ""index"": 31,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Km watergang"",
                		                ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""index"": 32,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Vangsten / km"",
                		                ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""index"": 33,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""data"",
                		                ""areaIndex"": 4
                	                },
                	                {
                		                ""caption"": ""Vangsten VJ / km"",
                		                ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenVJHidden,KmWatergangHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""index"": 34,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""caption"": ""Uren / km"",
                		                ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""index"": 35,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""data"",
                		                ""areaIndex"": 3
                	                },
                	                {
                		                ""caption"": ""Vangsten / uur"",
                		                ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                		                ""dataType"": ""number"",
                		                ""isMeasure"": true,
                		                ""format"": {
                			                ""precision"": 2
                		                },
                		                ""index"": 36,
                		                ""_initProperties"": {},
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""data"",
                		                ""areaIndex"": 2
                	                },
                	                {
                		                ""dataField"": ""fieldTestName"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Veldproef"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 250,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 37,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false
                	                },
                	                {
                		                ""dataField"": ""yearAndPeriod"",
                		                ""dataType"": ""number"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Periode + Jaar"",
                		                ""area"": ""column"",
                		                ""allowFiltering"": true,
                		                ""filterType"": ""include"",
                		                ""filterValues"": [
                			                null,
                			                202001,
                			                202002,
                			                202003,
                			                202004,
                			                202005,
                			                202006,
                			                202007,
                			                202008,
                			                202009,
                			                202010,
                			                202011,
                			                202012,
                			                201913
                		                ],
                		                ""_initProperties"": {
                			                ""area"": ""filter"",
                			                ""areaIndex"": 0,
                			                ""filterType"": ""include"",
                			                ""filterValues"": [
                				                null,
                				                202001,
                				                202002,
                				                202003,
                				                202004,
                				                202005,
                				                202006,
                				                202007,
                				                202008,
                				                202009,
                				                202010,
                				                202011,
                				                202012,
                				                201913
                			                ]
                		                },
                		                ""index"": 38,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowExpandAll"": false,
                		                ""areaIndex"": 1
                	                },
                	                {
                		                ""dataField"": ""timeRegistrationType"",
                		                ""displayFolder"": """",
                		                ""caption"": ""Urencategorie"",
                		                ""sortOrder"": ""asc"",
                		                ""width"": 250,
                		                ""_initProperties"": {
                			                ""sortOrder"": ""asc""
                		                },
                		                ""index"": 39,
                		                ""allowSorting"": true,
                		                ""allowSortingBySummary"": true,
                		                ""allowFiltering"": true,
                		                ""allowExpandAll"": false,
                		                ""area"": ""filter"",
                		                ""areaIndex"": 1,
                		                ""filterValues"": [
                			                ""Velduren"",
                			                null
                		                ]
                	                }
                                ]", "vangstrapportage", false, "", "CatchId", "hour-loft-report", "Uurhok bestrijder", (byte)3 },
                    { new Guid("5dc2fb10-cc66-49f3-be07-f37a0dbdb221"), true, @"[
                	                    {
                		                    ""dataField"": ""organizationName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Organisatie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 0,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""waterAuthorityName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Waterschap"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 1,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""rayonName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Rayon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 2,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vanggebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 3,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""subAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Deelgebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 4,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""provinceName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Provincie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 5,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trappingTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Bestrijdingssoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 6,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trapTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangmiddeltype"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 7,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Diersoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 8,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""row"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeCategoryName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangstsoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 9,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""recordedOnYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Jaar"",
                		                    ""sortOrder"": ""asc"",
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 10,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""column"",
                		                    ""areaIndex"": 0,
                		                    ""filterValues"": [
                			                    2020
                		                    ]
                	                    },
                	                    {
                		                    ""dataField"": ""period"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 11,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""column"",
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""dataField"": ""ownerName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Persoon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 12,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""week"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Week"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 13,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hourSquareName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Uurhok"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 14,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 15,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 16,
                		                    ""_initProperties"": {
                			                    ""area"": ""data"",
                			                    ""areaIndex"": 0
                		                    },
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 17,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 18,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 19,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 20,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 21,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 22,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfTraps"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Aantal vallen"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 23,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hours"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 24,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 25,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hoursPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 26,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 27,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 28,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:UrenHidden,UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 29,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchingNights"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangnachten"",
                		                    ""isMeasure"": true,
                		                    ""summaryType"": ""sum"",
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 30,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""kmWaterway"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""KmWatergangHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 31,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Km watergang"",
                		                    ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 32,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 33,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenVJHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 34,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 35,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / uur"",
                		                    ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 36,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""fieldTestName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Veldproef"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 37,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""yearAndPeriod"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode + Jaar"",
                		                    ""allowFiltering"": true,
                		                    ""filterType"": ""include"",
                		                    ""filterValues"": [
                			                    null,
                			                    202001,
                			                    202002,
                			                    202003,
                			                    202004,
                			                    202005,
                			                    202006,
                			                    202007,
                			                    202008,
                			                    202009,
                			                    202010,
                			                    202011,
                			                    202012,
                			                    201913
                		                    ],
                		                    ""_initProperties"": {
                			                    ""area"": ""filter"",
                			                    ""areaIndex"": 0,
                			                    ""filterType"": ""include"",
                			                    ""filterValues"": [
                				                    null,
                				                    202001,
                				                    202002,
                				                    202003,
                				                    202004,
                				                    202005,
                				                    202006,
                				                    202007,
                				                    202008,
                				                    202009,
                				                    202010,
                				                    202011,
                				                    202012,
                				                    201913
                			                    ]
                		                    },
                		                    ""index"": 38,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": -1
                	                    },
                	                    {
                		                    ""dataField"": ""timeRegistrationType"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Urencategorie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 39,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    }
                                    ]", "vangstrapportage", false, "", "CatchId", "bycatches-report", "Bijvangsten per periode", (byte)3 },
                    { new Guid("2f87ea3e-e5ac-4834-8448-db36752996be"), true, @"[
                	                    {
                		                    ""dataField"": ""organizationName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Organisatie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 0,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""waterAuthorityName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Waterschap"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 1,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""rayonName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Rayon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 2,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vanggebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 3,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""row"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""subAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Deelgebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 4,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""row"",
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""dataField"": ""provinceName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Provincie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 5,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trappingTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Bestrijdingssoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 6,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""column"",
                		                    ""areaIndex"": 0,
                		                    ""filterValues"": [
                			                    null,
                			                    ""Muskusrat""
                		                    ]
                	                    },
                	                    {
                		                    ""dataField"": ""trapTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangmiddeltype"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 7,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Diersoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 8,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeCategoryName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangstsoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 9,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""recordedOnYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Jaar"",
                		                    ""sortOrder"": ""asc"",
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 10,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""period"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 11,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""ownerName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Persoon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 12,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""week"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Week"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 13,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hourSquareName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Uurhok"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 14,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": -1
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 15,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""area"": ""data"",
                		                    ""index"": 16,
                		                    ""_initProperties"": {
                			                    ""area"": ""data"",
                			                    ""areaIndex"": 0
                		                    },
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 17,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 18,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 19,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 20,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 21,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 22,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfTraps"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Aantal vallen"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 23,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hours"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 24,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 25,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""dataField"": ""hoursPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 26,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 27,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 28,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:UrenHidden,UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 29,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchingNights"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangnachten"",
                		                    ""isMeasure"": true,
                		                    ""summaryType"": ""sum"",
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 30,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""kmWaterway"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""KmWatergangHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 31,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Km watergang"",
                		                    ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 32,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 33,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 4
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenVJHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 34,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 35,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 3
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / uur"",
                		                    ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 36,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 2
                	                    },
                	                    {
                		                    ""dataField"": ""fieldTestName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Veldproef"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 37,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""yearAndPeriod"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode + Jaar"",
                		                    ""area"": ""column"",
                		                    ""allowFiltering"": true,
                		                    ""filterType"": ""include"",
                		                    ""filterValues"": [
                			                    null,
                			                    202001,
                			                    202002,
                			                    202003,
                			                    202004,
                			                    202005,
                			                    202006,
                			                    202007,
                			                    202008,
                			                    202009,
                			                    202010,
                			                    202011,
                			                    202012,
                			                    201913
                		                    ],
                		                    ""_initProperties"": {
                			                    ""area"": ""filter"",
                			                    ""areaIndex"": 0,
                			                    ""filterType"": ""include"",
                			                    ""filterValues"": [
                				                    null,
                				                    202001,
                				                    202002,
                				                    202003,
                				                    202004,
                				                    202005,
                				                    202006,
                				                    202007,
                				                    202008,
                				                    202009,
                				                    202010,
                				                    202011,
                				                    202012,
                				                    201913
                			                    ]
                		                    },
                		                    ""index"": 38,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""dataField"": ""timeRegistrationType"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Urencategorie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 39,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 1,
                		                    ""filterValues"": [
                			                    ""Velduren"",
                			                    null
                		                    ]
                	                    }
                                    ]", "vangstrapportage", false, "", "CatchId", "sub-area-fighter-report", "Deelgebied bestrijder", (byte)3 },
                    { new Guid("e48c2e6e-c5fc-402c-ac59-0cea26124d1b"), true, @"[
                	                    {
                		                    ""dataField"": ""organizationName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Organisatie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 0,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""row"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""waterAuthorityName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Waterschap"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 1,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""rayonName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Rayon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 2,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vanggebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 3,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""subAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Deelgebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 4,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""provinceName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Provincie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 5,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trappingTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Bestrijdingssoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 6,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trapTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangmiddeltype"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 7,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Diersoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 8,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeCategoryName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangstsoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 9,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""recordedOnYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Jaar"",
                		                    ""sortOrder"": ""asc"",
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 10,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""period"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 11,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""ownerName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Persoon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 12,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""week"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Week"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 13,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hourSquareName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Uurhok"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 14,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 15,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""area"": ""data"",
                		                    ""index"": 16,
                		                    ""_initProperties"": {
                			                    ""area"": ""data"",
                			                    ""areaIndex"": 0
                		                    },
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 17,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 18,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 19,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 20,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 21,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 22,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfTraps"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Aantal vallen"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 23,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hours"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 24,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 25,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hoursPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 26,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 27,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 28,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 2
                	                    },
                	                    {
                		                    ""caption"": ""Uren verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:UrenHidden,UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 29,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchingNights"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangnachten"",
                		                    ""isMeasure"": true,
                		                    ""summaryType"": ""sum"",
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 30,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""kmWaterway"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""KmWatergangHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 31,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Km watergang"",
                		                    ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 32,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 33,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 3
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenVJHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 34,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 4
                	                    },
                	                    {
                		                    ""caption"": ""Uren / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 35,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / uur"",
                		                    ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 36,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""fieldTestName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Veldproef"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 37,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""yearAndPeriod"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode + Jaar"",
                		                    ""area"": ""column"",
                		                    ""allowFiltering"": true,
                		                    ""filterType"": ""include"",
                		                    ""filterValues"": [
                			                    null,
                			                    202001,
                			                    202002,
                			                    202003,
                			                    202004,
                			                    202005,
                			                    202006,
                			                    202007,
                			                    202008,
                			                    202009,
                			                    202010,
                			                    202011,
                			                    202012,
                			                    201913
                		                    ],
                		                    ""_initProperties"": {
                			                    ""area"": ""filter"",
                			                    ""areaIndex"": 0,
                			                    ""filterType"": ""include"",
                			                    ""filterValues"": [
                				                    null,
                				                    202001,
                				                    202002,
                				                    202003,
                				                    202004,
                				                    202005,
                				                    202006,
                				                    202007,
                				                    202008,
                				                    202009,
                				                    202010,
                				                    202011,
                				                    202012,
                				                    201913
                			                    ]
                		                    },
                		                    ""index"": 38,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""timeRegistrationType"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Urencategorie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 39,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    }
                                    ]", "vangstrapportage", false, "", "CatchId", "catches-organisation-report", "Vangsten organisatie", (byte)3 },
                    { new Guid("bb344bf9-b58b-4593-bea4-0a3017c75663"), true, @"[
                	                    {
                		                    ""dataField"": ""organizationName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Organisatie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 0,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""row"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""waterAuthorityName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Waterschap"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 1,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""rayonName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Rayon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 2,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vanggebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 3,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""subAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Deelgebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 4,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""provinceName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Provincie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 5,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trappingTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Bestrijdingssoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 6,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trapTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangmiddeltype"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 7,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Diersoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 8,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeCategoryName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangstsoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 9,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""recordedOnYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Jaar"",
                		                    ""sortOrder"": ""asc"",
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 10,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""period"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 11,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""ownerName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Persoon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 12,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""week"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Week"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 13,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hourSquareName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Uurhok"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 14,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 15,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 16,
                		                    ""_initProperties"": {
                			                    ""area"": ""data"",
                			                    ""areaIndex"": 0
                		                    },
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 17,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 18,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 19,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 20,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 21,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 22,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfTraps"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Aantal vallen"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 23,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hours"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 24,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 25,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""hoursPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 26,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 27,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 28,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:UrenHidden,UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 29,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 2
                	                    },
                	                    {
                		                    ""dataField"": ""catchingNights"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangnachten"",
                		                    ""isMeasure"": true,
                		                    ""summaryType"": ""sum"",
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 30,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""kmWaterway"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""KmWatergangHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 31,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Km watergang"",
                		                    ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 32,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 33,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenVJHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 34,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 35,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / uur"",
                		                    ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 36,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""fieldTestName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Veldproef"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 37,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""yearAndPeriod"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode + Jaar"",
                		                    ""area"": ""column"",
                		                    ""allowFiltering"": true,
                		                    ""filterType"": ""include"",
                		                    ""filterValues"": [
                			                    null,
                			                    202001,
                			                    202002,
                			                    202003,
                			                    202004,
                			                    202005,
                			                    202006,
                			                    202007,
                			                    202008,
                			                    202009,
                			                    202010,
                			                    202011,
                			                    202012,
                			                    201913
                		                    ],
                		                    ""_initProperties"": {
                			                    ""area"": ""filter"",
                			                    ""areaIndex"": 0,
                			                    ""filterType"": ""include"",
                			                    ""filterValues"": [
                				                    null,
                				                    202001,
                				                    202002,
                				                    202003,
                				                    202004,
                				                    202005,
                				                    202006,
                				                    202007,
                				                    202008,
                				                    202009,
                				                    202010,
                				                    202011,
                				                    202012,
                				                    201913
                			                    ]
                		                    },
                		                    ""index"": 38,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""timeRegistrationType"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Urencategorie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 39,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 0,
                		                    ""filterValues"": [
                			                    ""Velduren""
                		                    ]
                	                    }
                                    ]", "vangstrapportage", false, "", "CatchId", "hour-organisation-report", "Uren organisatie", (byte)3 },
                    { new Guid("c8ca6f77-8ee4-4d1a-baa8-dd12c04589c1"), true, @"[
                	                    {
                		                    ""dataField"": ""organizationName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Organisatie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 0,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""waterAuthorityName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Waterschap"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 1,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""rayonName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Rayon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 2,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""dataField"": ""catchAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vanggebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 3,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 2
                	                    },
                	                    {
                		                    ""dataField"": ""subAreaName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Deelgebied"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 4,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 3
                	                    },
                	                    {
                		                    ""dataField"": ""provinceName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Provincie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 5,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""trappingTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Bestrijdingssoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 6,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""column"",
                		                    ""areaIndex"": 0,
                		                    ""filterValues"": [
                			                    null,
                			                    ""Muskusrat""
                		                    ]
                	                    },
                	                    {
                		                    ""dataField"": ""trapTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangmiddeltype"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 7,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Diersoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 8,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchTypeCategoryName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangstsoort"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 9,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""recordedOnYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Jaar"",
                		                    ""sortOrder"": ""asc"",
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 10,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""column"",
                		                    ""areaIndex"": 1,
                		                    ""filterType"": ""exclude"",
                		                    ""filterValues"": null,
                		                    ""expanded"": false
                	                    },
                	                    {
                		                    ""dataField"": ""period"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 11,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""column"",
                		                    ""areaIndex"": 2
                	                    },
                	                    {
                		                    ""dataField"": ""ownerName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Persoon"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 12,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""week"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Week"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 13,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hourSquareName"",
                		                    ""dataType"": ""string"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Uurhok"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 14,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 4
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 15,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""area"": ""data"",
                		                    ""index"": 16,
                		                    ""_initProperties"": {
                			                    ""area"": ""data"",
                			                    ""areaIndex"": 0
                		                    },
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""areaIndex"": 0
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatches"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 17,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 18,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""VangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 19,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 20,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""BijvangstenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 21,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:BijvangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 22,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""numberOfTraps"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Aantal vallen"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 23,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""hours"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 24,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 25,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 1
                	                    },
                	                    {
                		                    ""dataField"": ""hoursPreviousYear"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""UrenVJHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""isMeasure"": true,
                		                    ""visible"": false,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 26,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren VJ"",
                		                    ""dxFunctionPlaceholder"": ""DoNothing:UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 27,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:VangstenHidden,VangstenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 28,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren verschil (%)"",
                		                    ""dxFunctionPlaceholder"": ""RelativeComparison:UrenHidden,UrenVJHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": ""percent"",
                		                    ""index"": 29,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchingNights"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Vangnachten"",
                		                    ""isMeasure"": true,
                		                    ""summaryType"": ""sum"",
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 30,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""kmWaterway"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""KmWatergangHidden"",
                		                    ""summaryType"": ""sum"",
                		                    ""visible"": false,
                		                    ""isMeasure"": true,
                		                    ""_initProperties"": {
                			                    ""summaryType"": ""sum""
                		                    },
                		                    ""index"": 31,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Km watergang"",
                		                    ""dxFunctionPlaceholder"": ""Total:KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""index"": 32,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 5
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 33,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 4
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten VJ / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:VangstenVJHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 34,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""caption"": ""Uren / km"",
                		                    ""dxFunctionPlaceholder"": ""DivideByTotal:UrenHidden,KmWatergangHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 35,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 3
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten / uur"",
                		                    ""dxFunctionPlaceholder"": ""Divide:VangstenHidden,UrenHidden"",
                		                    ""dataType"": ""number"",
                		                    ""isMeasure"": true,
                		                    ""format"": {
                			                    ""precision"": 2
                		                    },
                		                    ""index"": 36,
                		                    ""_initProperties"": {},
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""data"",
                		                    ""areaIndex"": 2
                	                    },
                	                    {
                		                    ""dataField"": ""fieldTestName"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Veldproef"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 37,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""yearAndPeriod"",
                		                    ""dataType"": ""number"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Periode + Jaar"",
                		                    ""allowFiltering"": true,
                		                    ""filterType"": ""include"",
                		                    ""filterValues"": [
                			                    null,
                			                    202001,
                			                    202002,
                			                    202003,
                			                    202004,
                			                    202005,
                			                    202006,
                			                    202007,
                			                    202008,
                			                    202009,
                			                    202010,
                			                    202011,
                			                    202012,
                			                    201913
                		                    ],
                		                    ""_initProperties"": {
                			                    ""area"": ""filter"",
                			                    ""areaIndex"": 0,
                			                    ""filterType"": ""include"",
                			                    ""filterValues"": [
                				                    null,
                				                    202001,
                				                    202002,
                				                    202003,
                				                    202004,
                				                    202005,
                				                    202006,
                				                    202007,
                				                    202008,
                				                    202009,
                				                    202010,
                				                    202011,
                				                    202012,
                				                    201913
                			                    ]
                		                    },
                		                    ""index"": 38,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowExpandAll"": false
                	                    },
                	                    {
                		                    ""dataField"": ""timeRegistrationType"",
                		                    ""displayFolder"": """",
                		                    ""caption"": ""Urencategorie"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 250,
                		                    ""_initProperties"": {
                			                    ""sortOrder"": ""asc""
                		                    },
                		                    ""index"": 39,
                		                    ""allowSorting"": true,
                		                    ""allowSortingBySummary"": true,
                		                    ""allowFiltering"": true,
                		                    ""allowExpandAll"": false,
                		                    ""area"": ""filter"",
                		                    ""areaIndex"": 5,
                		                    ""filterValues"": [
                			                    null,
                			                    ""Velduren""
                		                    ]
                	                    }
                                    ]", "vangstrapportage", false, "", "CatchId", "organisation-histogram-report", "Histogram organisatie", (byte)3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("2f87ea3e-e5ac-4834-8448-db36752996be"));

            migrationBuilder.DeleteData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("5dc2fb10-cc66-49f3-be07-f37a0dbdb221"));

            migrationBuilder.DeleteData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("b1bb6e24-270f-4d1d-9452-2d22fad19242"));

            migrationBuilder.DeleteData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("bb344bf9-b58b-4593-bea4-0a3017c75663"));

            migrationBuilder.DeleteData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("c8ca6f77-8ee4-4d1a-baa8-dd12c04589c1"));

            migrationBuilder.DeleteData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("e48c2e6e-c5fc-402c-ac59-0cea26124d1b"));
        }
    }
}
