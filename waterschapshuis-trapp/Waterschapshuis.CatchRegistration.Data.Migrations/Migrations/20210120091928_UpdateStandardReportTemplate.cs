using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdateStandardReportTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("5dc2fb10-cc66-49f3-be07-f37a0dbdb221"),
                column: "Content",
                value: @"[
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
			                    ""area"": ""row"",
			                    ""areaIndex"": 0,
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
		                    ""allowExpandAll"": false,
                            ""area"": ""filter"",
                            ""filterType"": ""include"",
                            ""filterValues"": [
			                    null,
			                    ""Bijvangst""
		                    ]
	                    },
	                    {
		                    ""dataField"": ""recordedOnYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Jaar"",
		                    ""sortOrder"": ""asc"",
		                    ""_initProperties"": {
			                    ""area"": ""column"",
			                    ""areaIndex"": 0,
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 10,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""filter"",
		                    ""areaIndex"": 0
	                    },
	                    {
		                    ""dataField"": ""period"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Periode"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120,
		                    ""_initProperties"": {
			                    ""area"": ""column"",
			                    ""areaIndex"": 1,
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 11,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""column"",
		                    ""areaIndex"": 0
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
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
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
		                    ""_initProperties"": {
			                    ""areaIndex"": -1,
			                    ""filterType"": ""include""
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
	                    },
                        {
                            ""caption"": ""Regionale versie"",
		                    ""dataField"": ""versionRegionalLayout"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120,
                            ""area"": ""filter"",
                            ""allowFiltering"": true,
                            ""filterType"": ""include""
                        }
                    ]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("5dc2fb10-cc66-49f3-be07-f37a0dbdb221"),
                column: "Content",
                value: @"[
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
			                    ""area"": ""row"",
			                    ""areaIndex"": 0,
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
		                    ""allowExpandAll"": false,
                            ""area"": ""filter"",
                            ""filterType"": ""include"",
                            ""filterValues"": [
			                    null,
			                    ""Bijvangst""
		                    ]
	                    },
	                    {
		                    ""dataField"": ""recordedOnYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Jaar"",
		                    ""sortOrder"": ""asc"",
		                    ""_initProperties"": {
			                    ""area"": ""column"",
			                    ""areaIndex"": 0,
			                    ""sortOrder"": ""asc"",
			                    ""filterValues"": [
				                    2020
			                    ]
		                    },
		                    ""index"": 10,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""filter"",
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
			                    ""area"": ""column"",
			                    ""areaIndex"": 1,
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 11,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""column"",
		                    ""areaIndex"": 0
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
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
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
		                    ""_initProperties"": {
			                    ""areaIndex"": -1,
			                    ""filterType"": ""include""
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
	                    },
                        {
                            ""caption"": ""Regionale versie"",
		                    ""dataField"": ""versionRegionalLayout"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120,
                            ""area"": ""filter"",
                            ""allowFiltering"": true,
                            ""filterType"": ""include""
                        }
                    ]");
        }
    }
}
