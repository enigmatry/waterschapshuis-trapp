using Microsoft.AspNetCore.Http;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters
{
    public static class HttpRequestExtensions
    {
        public static bool IsTrusted(this HttpRequest request, bool useDeveloperExceptionPage)
        {
            return useDeveloperExceptionPage;
        }
    }
}
