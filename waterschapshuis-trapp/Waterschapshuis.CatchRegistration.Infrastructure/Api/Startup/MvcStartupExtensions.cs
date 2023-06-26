using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class MvcStartupExtensions
    {
        public static IMvcBuilder AppAddMvc(
            this IServiceCollection services, 
            IConfiguration configuration, 
            bool enableAuthorization,
            Assembly entryAssembly) 
            => services
                .AddControllers(options => options.ConfigureMvc(configuration, enableAuthorization, entryAssembly))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson()
                .AppAddFluentValidation(entryAssembly);

        private static void ConfigureMvc(this MvcOptions options, IConfiguration configuration, bool enableAuthorization, [NotNull] Assembly entryAssembly)
        {
            options.Filters.Add(new CancelSavingTransactionAttribute());
            options.Filters.Add(new HandleExceptionsFilterAttribute(configuration.AppUseDeveloperExceptionPage()));

            options.AppAddAuthorizationFilters(enableAuthorization);

            if (entryAssembly.FullName != null && entryAssembly.FullName.Contains("BackOffice.Api"))
            {
                options.AppAddBackOfficeAuthorizationFilters(enableAuthorization);
            }

            if (entryAssembly.FullName != null && entryAssembly.FullName.Contains("Mobile.Api"))
            {
                options.AppAddMobileAuthorizationFilters(enableAuthorization);
            }

            if (entryAssembly.FullName != null && entryAssembly.FullName.Contains("External.Api"))
            {
                options.AppAddExternalApiAuthorizationFilters(enableAuthorization);
            }

            options.AddODataFormattersSupportedMediaTypes();
        }
    }
}
