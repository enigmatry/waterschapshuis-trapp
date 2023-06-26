using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates.DomainEvents
{
    public class ReportTemplateExportedDomainEvent : AuditableDomainEvent
    {
        public ReportTemplateExportedDomainEvent(Guid exportedReportTemplateId, string title, bool active) : base("ReportTemplateExported")
        {
            ExportedReportTemplateId = exportedReportTemplateId;
            Title = title;
            Active = active;
        }

        public Guid ExportedReportTemplateId { get; }
        public string Title { get; }
        public bool Active { get; }

        public override object AuditPayload => new
        {
            ExportedReportTemplateId,
            Title,
            Active
        };
    }
}
