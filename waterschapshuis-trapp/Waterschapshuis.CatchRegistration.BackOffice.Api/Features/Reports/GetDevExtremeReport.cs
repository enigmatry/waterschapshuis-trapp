using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    public static partial class GetDevExtremeReport
    {
        [PublicAPI]
        [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
        public class Query : DataSourceLoadOptionsBase, IRequest<LoadResult>
        {
            public string ReportUri { get; private set; }

            public Query(string reportUri)
            {
                ReportUri = reportUri;
            }
        }
    }
}
