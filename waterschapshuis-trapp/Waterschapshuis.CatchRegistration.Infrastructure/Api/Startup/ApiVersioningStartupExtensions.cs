using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Versioning;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class ApiVersioningStartupExtensions
    {
        public static void AppAddApiVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(configuration.ReadAppSettings().ApiVersioning);
        }

        private static void AddApiVersioning(this IServiceCollection services, ApiVersioningSettings settings)
        {
            if (!settings.Enabled)
            {
                return;
            }

            services.AddApiVersioning(options =>
            {
                var latestApiVersion = ApiVersion.Parse(settings.LatestApiVersion);

                options.DefaultApiVersion = latestApiVersion;
                options.ReportApiVersions = true;

                if (settings.UseVersionByNamespaceConvention)
                {
                    options.Conventions.Add(new LatestVersionByNamespaceConvention(latestApiVersion));
                }
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
