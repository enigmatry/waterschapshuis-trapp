using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.DomainModel.Maps;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Templates;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Reports
{
    [Category("integration")]
    public class ReportsControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private ReportTemplate inactiveReportTemplate = null!;
        private ReportTemplate exportedReportTemplate = null!;

        [SetUp]
        public void SetUp()
        {
            inactiveReportTemplate = ReportTemplate
                .CreateGeoMap("Test Inactive", "Inactive Group", "test-uri", "Test Content", false, false);
            exportedReportTemplate = ReportTemplate
                .CreateDevExtreme("Test Exported", String.Empty, "test-uri", ReportTemplateType.DevExtreme, "TestKey", "TestFileName", "Test Content", true, true);

            AddAndSaveChanges(inactiveReportTemplate, exportedReportTemplate);
        }


        [Test]
        public async Task GetReportTemplates()
        {
            var templates = (await Client.GetAsync<GetReportTemplates.Response>("reports/templates")).Items.ToList();

            templates.Count.Should().Be(10);
            templates.Count(i => i.Type == ReportTemplateType.GeoMap).Should().Be(3);
            templates.Count(i => i.Type == ReportTemplateType.DevExtreme).Should().Be(1);
            templates.All(x => x.Active).Should().BeTrue();
            templates.All(x => x.Exported).Should().BeFalse();

            var devExtremeReport = templates.First(i => i.RouteUri == ReportTemplateUriConstants.OwnReport);

            devExtremeReport.Should().NotBeNull();
            devExtremeReport.Title.Should().Be("Eigen rapportage");
            devExtremeReport.Group.Should().Be("Vangstrapportage");
            devExtremeReport.ExportFileName.Should().Be("vangstrapportage");
            devExtremeReport.Key.Should().Be("CatchId");
            devExtremeReport.Content.Should().NotBeNullOrWhiteSpace();

            var geoMapReport = templates.First(i => i.RouteUri == ReportTemplateUriConstants.TrackingLines);

            geoMapReport.Title.Should().Be("Alles");
            geoMapReport.Group.Should().Be("Speurkaart");
            geoMapReport.Content.Should()
                .Be(@"{
                                        ""layer"": """ + LayerConstants.WorkspaceName.V3 + @":" + LayerConstants.OverlayLayerName.TrackingLines + @"""
                                    }");
        }

        [Test]
        public async Task GetReportTemplate()
        {
            var templates = (await Client.GetAsync<GetReportTemplates.Response>("reports/templates")).Items.ToList();
            GetReportTemplates.Response.Item? devExtremeReport = templates.FirstOrDefault(i => i.RouteUri == ReportTemplateUriConstants.OwnReport);

            devExtremeReport.Should().NotBeNull();

            var template = (await Client.GetAsync<GetReportTemplate.Response>($"reports/templates/{devExtremeReport.Id}")).Item;

            template.Id.Should().Equals(devExtremeReport.Id);
            template.RouteUri.Should().Equals(devExtremeReport.RouteUri);
            template.Title.Should().Equals(devExtremeReport.Title);
            template.Group.Should().Equals(devExtremeReport.Group);
            template.Content.Should().Equals(devExtremeReport.Content);

            template = (await Client.GetAsync<GetReportTemplate.Response>($"reports/templates/{exportedReportTemplate.Id}")).Item;

            template.Id.Should().Equals(exportedReportTemplate.Id);

            template = (await Client.GetAsync<GetReportTemplate.Response>($"reports/templates/{inactiveReportTemplate.Id}")).Item;

            template.Should().BeNull();
        }

        [Test]
        public async Task CreateReportTemplate()
        {
            var command = new CreateReportTemplateExport.Command
            {
                ReportUri = ReportTemplateUriConstants.OwnReport,
                TemplateTitle = "New DevExtreme Template Title",
                TemplateContent = "New DevExtreme Template Content"
            };

            var newTemplateId = (await Client.PostAsync<CreateReportTemplateExport.Command, CreateReportTemplateExport.Result>("reports/templates", command)).Id;

            newTemplateId.Should().NotBe(Guid.Empty);

            var newTemplate = (await Client.GetAsync<GetReportTemplate.Response>($"reports/templates/{newTemplateId}")).Item;

            AssertCreatedTemplate(command, newTemplate, ReportTemplateType.DevExtreme);

            command = new CreateReportTemplateExport.Command
            {
                ReportUri = ReportTemplateUriConstants.CatchesByGeoRegion,
                TemplateTitle = "New GeoMap Template Title",
                TemplateContent = "New GeoMap Template Content"
            };

            newTemplateId = (await Client.PostAsync<CreateReportTemplateExport.Command, CreateReportTemplateExport.Result>("reports/templates", command)).Id;

            newTemplateId.Should().NotBe(Guid.Empty);

            newTemplate = (await Client.GetAsync<GetReportTemplate.Response>($"reports/templates/{newTemplateId}")).Item;

            AssertCreatedTemplate(command, newTemplate, ReportTemplateType.GeoMap);
        }

        private static void AssertCreatedTemplate(CreateReportTemplateExport.Command command, GetReportTemplates.Response.Item newTemplate, ReportTemplateType templateType)
        {
            newTemplate.Should().NotBeNull();
            newTemplate.RouteUri.Should().Be(command.ReportUri);
            newTemplate.Title.Should().Be(command.TemplateTitle);
            newTemplate.Content.Should().Be(command.TemplateContent);
            newTemplate.Type.Should().Be(templateType);
            newTemplate.Exported.Should().BeTrue();
            newTemplate.Active.Should().BeTrue();
        }
    }
}
