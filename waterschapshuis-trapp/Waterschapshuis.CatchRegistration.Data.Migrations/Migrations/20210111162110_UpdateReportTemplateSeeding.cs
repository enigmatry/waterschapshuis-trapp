using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class UpdateReportTemplateSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("2f87ea3e-e5ac-4834-8448-db36752996be"),
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
                            ""area"": ""data"",
                            ""areaIndex"": 0
	                    },
	                    {
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 16,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
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
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 20,
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
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
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
		                    ""areaIndex"": 4
	                    },
	                    {
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
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
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
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
		                    ""index"": 31,
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
		                    ""_initProperties"": {
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
			                    ""filterType"": ""include""
		                    },
		                    ""index"": 32,
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
		                    ""index"": 33,
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
                    ]");

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
		                    ""allowExpandAll"": false
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },	                    
	                    {
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 16,
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
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
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
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 20,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },                   
	                    {
		                    ""dataField"": ""hoursPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
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
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / uur"",
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
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
		                    ""index"": 31,
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
		                    ""index"": 32,
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
		                    ""index"": 33,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]");

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("c8ca6f77-8ee4-4d1a-baa8-dd12c04589c1"),
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 1,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 2,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 3,
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
			                    ""area"": ""column"",
			                    ""areaIndex"": 0,
			                    ""sortOrder"": ""asc"",
			                    ""filterValues"": [
				                    null,
				                    ""Muskusrat""
			                    ]
		                    },
		                    ""index"": 6,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""column"",
		                    ""areaIndex"": 0,
		                    ""filterValues"": [
			                    ""Muskusrat"",
			                    null
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
			                    ""area"": ""column"",
			                    ""areaIndex"": 1,
			                    ""sortOrder"": ""asc"",
			                    ""filterType"": ""exclude"",
			                    ""filterValues"": null,
			                    ""expanded"": false
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
			                    ""area"": ""column"",
			                    ""areaIndex"": 2,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 4,
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
                            ""area"": ""data"",
			                ""areaIndex"": 0
	                    },
	                    {
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 16,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
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
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
		                        ""areaIndex"": 1
		                    },
		                    ""index"": 20,
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
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 5
		                    },
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 27,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 4
		                    },
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""data"",
		                    ""areaIndex"": 4
	                    },
	                    {
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 3
		                    },
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""data"",
		                    ""areaIndex"": 3
	                    },
	                    {
		                    ""caption"": ""Vangsten / uur"",
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 2
		                    },
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
		                    ""index"": 31,
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
			                    ""filterType"": ""include""
		                    },
		                    ""index"": 32,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 5,
			                    ""sortOrder"": ""asc"",
			                    ""filterValues"": [
				                    null,
				                    ""Velduren""
			                    ]
		                    },
		                    ""index"": 33,
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
                    ]");

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("e48c2e6e-c5fc-402c-ac59-0cea26124d1b"),
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
                            ""area"": ""data"",
                            ""areaIndex"": 0
	                    },
	                    {
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 16,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 17,
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
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 20,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hoursPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
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
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
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
		                    ""areaIndex"": 3
	                    },
	                    {
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
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
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / uur"",
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
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
		                    ""index"": 31,
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
		                    ""_initProperties"": {
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
			                    ""filterType"": ""include""
		                    },
		                    ""index"": 32,
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
		                    ""index"": 33,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("2f87ea3e-e5ac-4834-8448-db36752996be"),
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
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
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 16,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
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
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 20,
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
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
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
		                    ""areaIndex"": 4
	                    },
	                    {
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
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
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
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
		                    ""index"": 31,
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
		                    ""_initProperties"": {
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
			                    ""filterType"": ""include""
		                    },
		                    ""index"": 32,
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
		                    ""index"": 33,
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
                    ]");

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
		                    ""allowExpandAll"": false
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },	                    
	                    {
		                    ""dataField"": ""numberOfByCatches"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 16,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },	                   
	                    {
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
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
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 20,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },                   
	                    {
		                    ""dataField"": ""hoursPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
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
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / uur"",
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
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
		                    ""index"": 31,
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
		                    ""index"": 32,
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
		                    ""index"": 33,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]");

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("c8ca6f77-8ee4-4d1a-baa8-dd12c04589c1"),
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 1,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 2,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 3,
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
			                    ""area"": ""column"",
			                    ""areaIndex"": 0,
			                    ""sortOrder"": ""asc"",
			                    ""filterValues"": [
				                    null,
				                    ""Muskusrat""
			                    ]
		                    },
		                    ""index"": 6,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""column"",
		                    ""areaIndex"": 0,
		                    ""filterValues"": [
			                    ""Muskusrat"",
			                    null
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
			                    ""area"": ""column"",
			                    ""areaIndex"": 1,
			                    ""sortOrder"": ""asc"",
			                    ""filterType"": ""exclude"",
			                    ""filterValues"": null,
			                    ""expanded"": false
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
			                    ""area"": ""column"",
			                    ""areaIndex"": 2,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 4,
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
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
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 16,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
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
		                    ""dataField"": ""numberOfByCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
		                        ""areaIndex"": 1
		                    },
		                    ""index"": 20,
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
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 5
		                    },
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 27,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 4
		                    },
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""data"",
		                    ""areaIndex"": 4
	                    },
	                    {
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Uren / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 3
		                    },
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""data"",
		                    ""areaIndex"": 3
	                    },
	                    {
		                    ""caption"": ""Vangsten / uur"",
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 2
		                    },
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
		                    ""index"": 31,
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
			                    ""filterType"": ""include""
		                    },
		                    ""index"": 32,
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
			                    ""area"": ""filter"",
			                    ""areaIndex"": 5,
			                    ""sortOrder"": ""asc"",
			                    ""filterValues"": [
				                    null,
				                    ""Velduren""
			                    ]
		                    },
		                    ""index"": 33,
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
                    ]");

            migrationBuilder.UpdateData(
                table: "ReportTemplate",
                keyColumn: "Id",
                keyValue: new Guid("e48c2e6e-c5fc-402c-ac59-0cea26124d1b"),
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
		                    ""caption"": ""Vangsten"",
		                    ""summaryType"": ""sum"",
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum"",
                                ""area"": ""data"",
			                    ""areaIndex"": 0
		                    },
		                    ""index"": 15,
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
		                    ""caption"": ""Bijvangsten"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 16,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""numberOfCatchesPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Vangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 17,
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
		                    ""caption"": ""Bijvangsten VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 18,
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
		                    ""index"": 19,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hours"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 20,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""hoursPreviousYear"",
		                    ""dataType"": ""number"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Uren VJ"",
		                    ""summaryType"": ""sum"",
		                    ""isMeasure"": true,
		                    ""visible"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
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
		                    ""caption"": ""Vangsten verschil (%)"",
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Vangsten,Vangsten VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 22,
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
		                    ""dxFunctionPlaceholder"": ""RelativeComparison:Uren,Uren VJ"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": ""percent"",
		                    ""index"": 23,
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
		                    ""index"": 24,
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
		                    ""index"": 25,
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
		                    ""index"": 26,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten,KmWatergangHidden"",
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
		                    ""areaIndex"": 3
	                    },
	                    {
		                    ""caption"": ""Vangsten VJ / km"",
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Vangsten VJ,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 28,
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
		                    ""dxFunctionPlaceholder"": ""DivideByTotal:Uren,KmWatergangHidden"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 29,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""caption"": ""Vangsten / uur"",
		                    ""dxFunctionPlaceholder"": ""Divide:Vangsten,Uren"",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 30,
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
		                    ""index"": 31,
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
		                    ""_initProperties"": {
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
			                    ""filterType"": ""include""
		                    },
		                    ""index"": 32,
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
		                    ""index"": 33,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]");
        }
    }
}
