using Microsoft.EntityFrameworkCore;
using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Maps;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;
using Waterschapshuis.CatchRegistration.DomainModel.Templates;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.ReportTemplates
{
    public class ReportTemplateTypeSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var trackingLinesReport = ReportTemplate
                .CreateGeoMap(
                    "Alles",
                    "Speurkaart",
                    ReportTemplateUriConstants.TrackingLines,
                    @"{
                        ""layer"": """ + LayerConstants.WorkspaceName.V3 + @":" + LayerConstants.OverlayLayerName.TrackingLines + @"""
                    }",
                    true,
                    false)
                .WithId(new Guid("40A95CAD-F942-4F99-A836-9C63CB47E4F8"));

            modelBuilder.Entity<ReportTemplate>().HasData(trackingLinesReport);

            var subAreaHourSquareCatchesReport = ReportTemplate
                .CreateGeoMap(
                    "Vangsten en uren op kaart",
                    "Vangsten en uren op kaart",
                    ReportTemplateUriConstants.CatchesByGeoRegion,
                    @"{
                        ""layer"": """ + LayerConstants.WorkspaceName.V3 + @":" + LayerConstants.OverlayLayerName.OrganizationCatches + @""",
                        ""measurement"": 0,
                        ""yearFrom"": """",
                        ""periodFrom"": """",
                        ""yearTo"": """",
                        ""periodTo"": """",
                        ""trappingType"": """"
                    }",
                    true,
                    false)
                .WithId(new Guid("33B0648D-724B-4BE9-8ED9-5ECEA181A5DE"));

            modelBuilder.Entity<ReportTemplate>().HasData(subAreaHourSquareCatchesReport);

            var heatMapReport = ReportTemplate
                .CreateGeoMap(
                    "Heatmap",
                    "Heatmap",
                    ReportTemplateUriConstants.HeatMapRegion,
                    @"{
                        ""startDate"": """",
                        ""endDate"": """",
                        ""catchType"": """",
                        ""organization"": """"
                    }",
                    true,
                    false)
                .WithId(new Guid("a4337297-00d2-41ad-9da7-d58d6f073801"));

            modelBuilder.Entity<ReportTemplate>().HasData(heatMapReport);

            var ownReport = ReportTemplate
                .CreateDevExtreme(
                    "Eigen rapportage",
                    "Vangstrapportage",
                    ReportTemplateUriConstants.OwnReport,
                    ReportTemplateType.DevExtreme,
                    "CatchId",
                    "vangstrapportage",
                    @"[
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
                            ""sortingMethod"": ""sortDescending"",
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
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingCatches + @""",
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
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingByCatches + @""",
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
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearCatches + @""",
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
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearByCatches + @""",
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
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHours + @""",
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
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursLastYear + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
			                        ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Vangsten verschil (%)"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearCatches + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": ""percent""
                        },
                        {
                            ""caption"": ""Uren verschil (%)"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearHours + @""",
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
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.TotalKmWaterways + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true
	                    },
                        {
                            ""caption"": ""Vangsten / km"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByKilometers + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Vangsten VJ / km"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideLastYearCatchesByKilometers + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Uren / km"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideHoursByKilometers + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Vangsten / uur"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByHours + @""",
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
                            ""sortingMethod"": ""sortDescending"",
                            ""filterType"": ""include""
                        },
						{
		                    ""caption"": ""Urencategorie Overig"",
		                    ""dataField"": ""timeRegistrationType"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 250
	                    },
                        {
		                    ""caption"": ""Regionale versie"",
		                    ""dataField"": ""versionRegionalLayout"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120,
                            ""area"": ""filter"",
                            ""allowFiltering"": true,
                            ""filterType"": ""include""
	                    },
                        {
                            ""caption"": ""UrenOverigHidden"",
                            ""dataField"": ""hoursOther"",
                            ""summaryType"": ""sum"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
                        {
                            ""caption"": ""Uren Overig"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursOther + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            }
                        },
                        {
		                    ""caption"": ""Status Vangmiddel"",
		                    ""dataField"": ""trapStatus"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120
	                    }
                    ]",
                    true,
                    false)
                .WithId(new Guid("77F0AC51-3E70-4FEC-9727-E68246525C22"));

            var hourSquareReport = ReportTemplate
                .CreateDevExtreme(
                    "Uurhok bestrijder",
                    String.Empty,
                    ReportTemplateUriConstants.HourSquareReport,
                    ReportTemplateType.StandardReport,
                    "CatchId",
                    "vangstrapportage",
                    @"[
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingCatches + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingByCatches + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingByCatches + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearByCatches + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHours + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursLastYear + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearCatches + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearHours + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.TotalKmWaterways + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByKilometers + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideLastYearCatchesByKilometers + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideHoursByKilometers + @""",
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
		                ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByHours + @""",
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
		                ""_initProperties"": {
			                ""area"": ""filter"",
			                ""areaIndex"": 0,
			                ""filterType"": ""include""
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
		                ""caption"": ""Urencategorie Overig"",
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
	                },
                    {
                        ""caption"": ""Regionale versie"",
		                ""dataField"": ""versionRegionalLayout"",
		                ""sortOrder"": ""asc"",
		                ""width"": 120,
                        ""area"": ""filter"",
                        ""allowFiltering"": true,
                        ""filterType"": ""include""
                    },
                    {
                        ""dataField"": ""hoursOther"",
                        ""dataType"": ""number"",
                        ""caption"": ""UrenOverigHidden"",
		                ""displayFolder"": """",
                        ""summaryType"": ""sum"",
                        ""isMeasure"": true,
                        ""visible"": false,
                        ""format"": {
                            ""precision"": 2
                        },
                        ""_initProperties"": {
			                ""summaryType"": ""sum""
		                },
		                ""index"": 40,
		                ""allowSorting"": true,
		                ""allowSortingBySummary"": true,
		                ""allowFiltering"": true,
		                ""allowExpandAll"": false
                    },
                    {
                        ""caption"": ""Uren Overig"",
                        ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursOther + @""",
                        ""dataType"": ""number"",
                        ""isMeasure"": true,
                        ""format"": {
                            ""precision"": 2
                        },
		                ""index"": 41,
		                ""_initProperties"": {},
		                ""allowSorting"": true,
		                ""allowSortingBySummary"": true,
		                ""allowFiltering"": true,
		                ""allowExpandAll"": false
                    },
                    {
		                ""dataField"": ""trapStatus"",
		                ""dataType"": ""string"",
		                ""displayFolder"": """",
		                ""caption"": ""Status Vangmiddel"",
		                ""sortOrder"": ""asc"",
		                ""_initProperties"": {
			                ""sortOrder"": ""asc""
		                },
		                ""index"": 42,
		                ""allowSorting"": true,
		                ""allowSortingBySummary"": true,
		                ""allowFiltering"": true,
		                ""allowExpandAll"": false
	                }
                ]",
                    true,
                    false)
                .WithId(new Guid("B1BB6E24-270F-4D1D-9452-2D22FAD19242"));

            var byCatchesReport = ReportTemplate
                .CreateDevExtreme(
                    "Bijvangsten per periode",
                    String.Empty,
                    ReportTemplateUriConstants.BycatchesReport,
                    ReportTemplateType.StandardReport,
                    "CatchId",
                    "vangstrapportage",
                    @"[
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursLastYear + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.TotalKmWaterways + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideLastYearCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideHoursByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByHours + @""",
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
		                    ""caption"": ""Urencategorie Overig"",
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
                        },
                        {
                            ""caption"": ""UrenOverigHidden"",
                            ""dataField"": ""hoursOther"",
		                    ""displayFolder"": """",
                            ""summaryType"": ""sum"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 40,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
                            ""caption"": ""Uren Overig"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursOther + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            },
	                    ""index"": 41,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
		                    ""dataField"": ""trapStatus"",
		                    ""dataType"": ""string"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Status Vangmiddel"",
		                    ""sortOrder"": ""asc"",
		                    ""_initProperties"": {
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 42,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]",
                true,
                false)
            .WithId(new Guid("5DC2FB10-CC66-49F3-BE07-F37A0DBDB221"));

            var subAreaFighterReport = ReportTemplate
                .CreateDevExtreme(
                    "Deelgebied bestrijder",
                    String.Empty,
                    ReportTemplateUriConstants.SubAreaTrackerReport,
                    ReportTemplateType.StandardReport,
                    "CatchId",
                    "vangstrapportage",
                    @"[
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursLastYear + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.TotalKmWaterways + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideLastYearCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideHoursByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByHours + @""",
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
		                    ""_initProperties"": {
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
			                    ""filterType"": ""include""
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
		                    ""caption"": ""Urencategorie Overig"",
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
	                    },
                        {
                            ""caption"": ""Regionale versie"",
		                    ""dataField"": ""versionRegionalLayout"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120,
                            ""area"": ""filter"",
                            ""allowFiltering"": true,
                            ""filterType"": ""include""
                        },
                        {
                            ""caption"": ""UrenOverigHidden"",
		                    ""displayFolder"": """",
                            ""dataField"": ""hoursOther"",
                            ""summaryType"": ""sum"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 40,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
                            ""caption"": ""Uren Overig"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursOther + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""index"": 41,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
		                    ""dataField"": ""trapStatus"",
		                    ""dataType"": ""string"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Status Vangmiddel"",
		                    ""sortOrder"": ""asc"",
		                    ""_initProperties"": {
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 42,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]",
                true,
                false)
            .WithId(new Guid("2F87EA3E-E5AC-4834-8448-DB36752996BE"));

            var catchOrganisationReport = ReportTemplate
                .CreateDevExtreme(
                    "Vangsten organisatie",
                    String.Empty,
                    ReportTemplateUriConstants.CatchesOrganisationReport,
                    ReportTemplateType.StandardReport,
                    "CatchId",
                    "vangstrapportage",
                    @"[
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursLastYear + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.TotalKmWaterways + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideLastYearCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideHoursByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByHours + @""",
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
		                    ""_initProperties"": {
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
			                    ""filterType"": ""include""
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
		                    ""caption"": ""Urencategorie Overig"",
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
                        },
                        {
                            ""caption"": ""UrenOverigHidden"",
		                    ""displayFolder"": """",
                            ""dataField"": ""hoursOther"",
                            ""summaryType"": ""sum"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 40,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
                            ""caption"": ""Uren Overig"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursOther + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""index"": 41,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
		                    ""dataField"": ""trapStatus"",
		                    ""dataType"": ""string"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Status Vangmiddel"",
		                    ""sortOrder"": ""asc"",
		                    ""_initProperties"": {
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 42,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]",
                true,
                false)
            .WithId(new Guid("E48C2E6E-C5FC-402C-AC59-0CEA26124D1B"));

            var hourOrganisationReport = ReportTemplate
               .CreateDevExtreme(
                   "Uren organisatie",
                   String.Empty,
                   ReportTemplateUriConstants.HourOrganisationReport,
                    ReportTemplateType.StandardReport,
                   "CatchId",
                   "vangstrapportage",
                   @"[
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursLastYear + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.TotalKmWaterways + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideLastYearCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideHoursByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByHours + @""",
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
		                    ""_initProperties"": {
			                    ""area"": ""filter"",
			                    ""areaIndex"": 0,
			                    ""filterType"": ""include""
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
		                    ""caption"": ""Urencategorie Overig"",
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
                        },
                        {
                            ""caption"": ""UrenOverigHidden"",
		                    ""displayFolder"": """",
                            ""dataField"": ""hoursOther"",
                            ""summaryType"": ""sum"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 40,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
                            ""caption"": ""Uren Overig"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursOther + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""index"": 41,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false,
		                    ""area"": ""data"",
		                    ""areaIndex"": 1
                        },
                        {
		                    ""dataField"": ""trapStatus"",
		                    ""dataType"": ""string"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Status Vangmiddel"",
		                    ""sortOrder"": ""asc"",
		                    ""_initProperties"": {
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 42,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]",
               true,
               false)
           .WithId(new Guid("BB344BF9-B58B-4593-BEA4-0A3017C75663"));

            var organisationHistogramReport = ReportTemplate
               .CreateDevExtreme(
                   "Histogram organisatie",
                   String.Empty,
                   ReportTemplateUriConstants.OrganisationHistogramReport,
                    ReportTemplateType.StandardReport,
                   "CatchId",
                   "vangstrapportage",
                   @"[
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingLastYearByCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHours + @""",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 25,
		                    ""_initProperties"": {
			                    ""area"": ""data"",
			                    ""areaIndex"": 1
		                    },
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursLastYear + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearCatches + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.RelativeComparisonThisYearAndLastYearHours + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.TotalKmWaterways + @""",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""index"": 32,
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByKilometers + @""",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 33,
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideLastYearCatchesByKilometers + @""",
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideHoursByKilometers + @""",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 35,
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
		                    ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DivideCatchesByHours + @""",
		                    ""dataType"": ""number"",
		                    ""isMeasure"": true,
		                    ""format"": {
			                    ""precision"": 2
		                    },
		                    ""index"": 36,
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
			                    ""filterType"": ""include""
		                    },
		                    ""index"": 38,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowExpandAll"": false
	                    },
	                    {
		                    ""dataField"": ""timeRegistrationType"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Urencategorie Overig"",
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
	                    },
                        {
                            ""caption"": ""Regionale versie"",
		                    ""dataField"": ""versionRegionalLayout"",
		                    ""sortOrder"": ""asc"",
		                    ""width"": 120,
                            ""area"": ""filter"",
                            ""allowFiltering"": true,
                            ""filterType"": ""include""
                        },
                        {
                            ""caption"": ""UrenOverigHidden"",
		                    ""displayFolder"": """",
                            ""dataField"": ""hoursOther"",
                            ""summaryType"": ""sum"",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""visible"": false,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""_initProperties"": {
			                    ""summaryType"": ""sum""
		                    },
		                    ""index"": 40,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
                            ""caption"": ""Uren Overig"",
                            ""dxFunctionPlaceholder"": """ + DevExtremeFunctionPlaceholder.DoNothingHoursOther + @""",
                            ""dataType"": ""number"",
                            ""isMeasure"": true,
                            ""format"": {
                                ""precision"": 2
                            },
		                    ""index"": 41,
		                    ""_initProperties"": {},
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
                        },
                        {
		                    ""dataField"": ""trapStatus"",
		                    ""dataType"": ""string"",
		                    ""displayFolder"": """",
		                    ""caption"": ""Status Vangmiddel"",
		                    ""sortOrder"": ""asc"",
		                    ""_initProperties"": {
			                    ""sortOrder"": ""asc""
		                    },
		                    ""index"": 42,
		                    ""allowSorting"": true,
		                    ""allowSortingBySummary"": true,
		                    ""allowFiltering"": true,
		                    ""allowExpandAll"": false
	                    }
                    ]",
               true,
               false)
           .WithId(new Guid("C8CA6F77-8EE4-4D1A-BAA8-DD12C04589C1"));

            modelBuilder.Entity<ReportTemplate>().HasData(ownReport, hourSquareReport, byCatchesReport, subAreaFighterReport, catchOrganisationReport, hourOrganisationReport, organisationHistogramReport);
        }
    }
}
