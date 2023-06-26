using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates.DomainEvents;
using Waterschapshuis.CatchRegistration.DomainModel.Templates;

namespace Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates
{
    public class ReportTemplate : Entity<Guid>
    {
        public const int TitleMaxLength = 100;
        public const int GroupMaxLength = 50;
        public const int RouteUriMaxLength = 250;
        public const int KeyMaxLength = 50;
        public const int ExportFileNameMaxLength = 250;

        private ReportTemplate() { }

        public string Title { get; private set; } = String.Empty;
        public string Group { get; private set; } = String.Empty;
        public string RouteUri { get; private set; } = String.Empty;
        public ReportTemplateType Type { get; private set; }
        public string? Key { get; private set; }
        public string? ExportFileName { get; private set; }
        public string Content { get; private set; } = String.Empty;
        public bool Active { get; private set; }
        public bool Exported { get; private set; }
        public ChartType ChartType { get; private set; } = ChartType.None;

        public static ReportTemplate CreateExport(ReportTemplate originTemplate, string title, string content, ChartType chartType)
        {
            var result = originTemplate.Type != ReportTemplateType.GeoMap
                ? CreateDevExtreme(
                    title,
                    String.Empty,
                    originTemplate.RouteUri,
                    originTemplate.Type,
                    originTemplate.Key ?? String.Empty,
                    originTemplate.ExportFileName ?? String.Empty,
                    content,
                    originTemplate.Active,
                    true,
                    chartType)
                : CreateGeoMap(
                    title,
                    String.Empty,
                    originTemplate.RouteUri,
                    content,
                    true,
                    true);
            result.AddDomainEvent(new ReportTemplateExportedDomainEvent(result.Id, result.Title, result.Active));
            return result;
        }

        public static ReportTemplate CreateDevExtreme(
            string title,
            string group,
            string routeUri,
            ReportTemplateType reportTemplateType,
            string key,
            string exportFileName,
            string content,
            bool active,
            bool exported,
            ChartType chartType = ChartType.Line)
        {
            return new ReportTemplate
            {
                Id = GenerateId(),
                Title = title,
                Group = group,
                RouteUri = routeUri,
                Type = reportTemplateType,
                Key = key,
                ExportFileName = exportFileName,
                Content = content,
                Active = active,
                Exported = exported,
                ChartType =  chartType
            };
        }

        public static ReportTemplate CreateGeoMap(
            string title,
            string group,
            string routeUri,
            string content,
            bool active,
            bool exported)
        {
            return new ReportTemplate
            {
                Id = GenerateId(),
                Title = title,
                Group = group,
                RouteUri = routeUri,
                Type = ReportTemplateType.GeoMap,
                Content = content,
                Active = active,
                Exported = exported
            };
        }
    }
}
