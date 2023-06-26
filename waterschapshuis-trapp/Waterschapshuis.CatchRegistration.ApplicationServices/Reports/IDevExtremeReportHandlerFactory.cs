using Waterschapshuis.CatchRegistration.ApplicationServices.Reports.ReportHandlers;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports
{
    public interface IDevExtremeReportHandlerFactory
    {
        IDevExtremeReportHandler GetReportHandler(string reportUri);
    }
}
