using System.Linq;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class ODataStartupExtensions
    {
        // Workaround: https://github.com/OData/WebApi/issues/1177
        public static void AddODataFormattersSupportedMediaTypes(this MvcOptions options)
        {
            var outputFormatter = options.OutputFormatters.OfType<ODataOutputFormatter>()
                .FirstOrDefault(_ => _.SupportedMediaTypes.Count == 0);

            outputFormatter?.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));

            var inputFormatter = options.InputFormatters.OfType<ODataInputFormatter>()
                .FirstOrDefault(_ => _.SupportedMediaTypes.Count == 0);

            inputFormatter?.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }
    }
}
