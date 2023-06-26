using MaximeRouiller.Azure.AppService.EasyAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Waterschapshuis.CatchRegistration.External.Api
{
    public static class AuthenticationStartupExtensions
    {
        // Adds EasyAuth authentication (Azure App Service Authentication)
        // https://docs.microsoft.com/en-us/azure/app-service/overview-authentication-authorization
        // https://github.com/MaximRouiller/MaximeRouiller.Azure.AppService.EasyAuth

        public static void AppAddEasyAuthAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            bool enable)
        {
            if (!enable)
            {
                return;
            }

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = "EasyAuth";
                    options.DefaultChallengeScheme = "EasyAuth";
                }
            ).AddEasyAuthAuthentication(_ => { });
        }
    }
}
