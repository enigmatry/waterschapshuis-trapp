using System.Threading;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data.ResponseModel;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Reports;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    public static partial class GetDevExtremeReport
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, LoadResult>
        {
            private readonly IDevExtremeReportHandlerFactory _devExtremeReportHandlerFactory;

            public RequestHandler(IDevExtremeReportHandlerFactory devExtremeReportHandlerFactory)
            {
                _devExtremeReportHandlerFactory = devExtremeReportHandlerFactory;
            }

            public async Task<LoadResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var reportHandler = _devExtremeReportHandlerFactory.GetReportHandler(request.ReportUri);

                return await reportHandler.GetReport(request, cancellationToken);
            }
        }
    }
}
