using System.Threading;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports.ReportHandlers
{
    public interface IDevExtremeReportHandler
    {
        Task<LoadResult> GetReport(DataSourceLoadOptionsBase request, CancellationToken cancellationToken);
    }
}
