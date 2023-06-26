using Microsoft.AspNetCore.Http;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters
{
    public static class HttpResponseExtensions
    {
        public static HttpResponse WithInvalidUserSessionHeader(this HttpResponse response)
        {
            var invalidSessionHeaderKey = "InvalidUserSession";
            var accessControlExposeHeaders = "Access-Control-Expose-Headers";

            response.Headers.AppendCommaSeparatedValues(accessControlExposeHeaders, invalidSessionHeaderKey);

            if (!response.Headers.ContainsKey(invalidSessionHeaderKey))
            {
                response.Headers.Add(invalidSessionHeaderKey, "true");
            }

            return response;
        }
    }
}
