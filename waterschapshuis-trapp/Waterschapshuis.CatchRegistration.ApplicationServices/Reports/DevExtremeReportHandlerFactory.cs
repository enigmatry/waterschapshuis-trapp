using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.ApplicationServices.Reports.ReportHandlers;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports
{
    public class DevExtremeReportHandlerFactory : IDevExtremeReportHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, IDevExtremeReportHandler> _devExtremeReportHandlers;

        public DevExtremeReportHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _devExtremeReportHandlers = GetReportHandlers();
        }

        public IDevExtremeReportHandler GetReportHandler(string reportUri) =>
            _devExtremeReportHandlers.ContainsKey(reportUri)
                ? _devExtremeReportHandlers[reportUri]
                : throw new InvalidOperationException($"Report Uri '{reportUri}' doesn't exist.");

        private Dictionary<string, IDevExtremeReportHandler> GetReportHandlers()
        {
            OwnReportReportHandler ownReportReportHandler = _serviceProvider.GetService<OwnReportReportHandler>();
            return new Dictionary<string, IDevExtremeReportHandler>
            {
                {
                    ReportTemplateUriConstants.OwnReport,
                    ownReportReportHandler
                },
                {
                    ReportTemplateUriConstants.BycatchesReport,
                    ownReportReportHandler
                },
                {
                    ReportTemplateUriConstants.HourSquareReport,
                    ownReportReportHandler
                },
                {
                    ReportTemplateUriConstants.CatchesOrganisationReport,
                    ownReportReportHandler
                },
                {
                    ReportTemplateUriConstants.HourOrganisationReport,
                    ownReportReportHandler
                },
                {
                    ReportTemplateUriConstants.OrganisationHistogramReport,
                    ownReportReportHandler
                },
                {
                    ReportTemplateUriConstants.SubAreaTrackerReport,
                    ownReportReportHandler
                },
            };
        }
    }
}
