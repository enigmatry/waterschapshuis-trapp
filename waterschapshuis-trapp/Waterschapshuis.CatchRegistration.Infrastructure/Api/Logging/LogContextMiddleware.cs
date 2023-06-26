using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Logging
{
    [UsedImplicitly]
    public class LogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public LogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [UsedImplicitly]
        public async Task InvokeAsync(HttpContext context)
        {
            using (LogContext.Push(CreateEnrichers(context)))
            {
                await _next.Invoke(context);
            }
        }

        private ILogEventEnricher[] CreateEnrichers(HttpContext context)
        {
            var result = new ILogEventEnricher[]
            {
                new PropertyEnricher("User", context.User.Identity.Name ?? String.Empty),
                new PropertyEnricher("Address", context.Connection.RemoteIpAddress),
            };

            result = TryAddMobileAppVersion(result, context);

            return result;
        }

        private static ILogEventEnricher[] TryAddMobileAppVersion(ILogEventEnricher[] logEventEnrichers, HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("MobileVersion", out StringValues headerValues))
            {
                return logEventEnrichers;
            }

            var version = headerValues.FirstOrDefault();
            var enricher = new PropertyEnricher("MobileVersion", version);
            return logEventEnrichers.Concat(new ILogEventEnricher[] {enricher}).ToArray();
        }
    }
}
