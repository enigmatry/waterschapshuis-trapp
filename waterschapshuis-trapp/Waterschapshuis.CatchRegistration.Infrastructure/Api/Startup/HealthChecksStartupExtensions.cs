using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class HealthChecksStartupExtensions
    {
        public static void AppMapHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                // Specify a custom ResponseWriter, so we can return json with additional information,
                // Otherwise it will just return plain text with the status.
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        new {status = report.Status.ToString(), entries = report.Entries.Select(e => new {key = e.Key, value = e.Value.Status.ToString()})});
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            }).RequireAuthorization();
        }

        public static void AppAddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationInsightsSettings = configuration.ReadApplicationInsightsSettings();
            var healthCheckSettings = configuration.ReadHealthCheckSettings();

            // Here we can configure the different health checks:
            var builder = services.AddHealthChecks()

                // Check the sql server connection
                //.AddSqlServer(configuration["ConnectionStrings:CatchRegistrationContext"], "SELECT 1")
                // Check the EF Core Context
                .AddDbContextCheck<CatchRegistrationDbContext>()

                // Check metrics
                .AddPrivateMemoryHealthCheck(healthCheckSettings.MaximumAllowedMemory, "Available memory test", HealthStatus.Degraded);

            // Check Redis
            //.AddRedis(redisConnectionString: configuration["ConnectionStrings:RedisConnection"],
            //    name: "Redis",
            //    failureStatus: HealthStatus.Degraded)
            // We can also push the results to Application Insights. This will be done every 30 seconds
            // Can be checked from the Azure Portal under metrics, by selecting the azure.applicationinsights namespace.
            if (applicationInsightsSettings.InstrumentationKey.IsNotNullOrEmpty())
            {
                builder.AddApplicationInsightsPublisher(applicationInsightsSettings.InstrumentationKey);
            }
        }
    }
}
