using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Swagger;
namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class SwaggerStartupExtensions
    {
        public static void AppUseSwaggerWithAzureAdAuthentication(this IApplicationBuilder app, AzureAdSettings settings)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(c =>
            {
                c.Path = "/swagger";
                c.OAuth2Client = new NSwag.AspNetCore.OAuth2ClientSettings
                {
                    
                    ClientId = settings.ClientId,
                    ClientSecret = "",
                    UsePkceWithAuthorizationCodeGrant = true
                };
            });
        }

        public static void AppUseSwaggerWithEasyAuth(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(c => c.Path = "");
        }

        public static void AppAddSwaggerWithAzureAdAuthentication(this IServiceCollection services,
            string appTitle,
            string appDescription,
            ApiVersioningSettings versioning, AzureAdSettings adSettings)
        {
            services.AppAddSwagger(appTitle, appDescription, versioning, document =>
            {
                var authorizationUrl = $"{adSettings.Instance}/{adSettings.TenantId}/oauth2/v2.0/authorize";
                var tokenUrl = $"{adSettings.Instance}/{adSettings.TenantId}/oauth2/v2.0/token";
                var scopes = new Dictionary<string, string> {{adSettings.ApiScopes, ""}};
                document.AddSecurity("oauth2", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = authorizationUrl,
                            TokenUrl = tokenUrl,
                            Scopes = scopes
                        }
                    }
                });
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("oauth2"));
            });
        }

        public static void AppAddSwaggerWithEasyAuth(this IServiceCollection services,
            string appTitle,
            string appDescription,
            ApiVersioningSettings versioning)
        {
            services.AppAddSwagger(appTitle, appDescription, versioning, document =>
            {
            });
        }

        private static void AppAddSwagger(this IServiceCollection services,
            string appTitle,
            string appDescription,
            ApiVersioningSettings versioning, Action<AspNetCoreOpenApiDocumentGeneratorSettings> configureSettings)
        {
            string[] versions = versioning.Enabled
                ? new []{ versioning.LatestApiVersion }
                : new[] {"1"};

            foreach (string version in versions)
            {
                services.AddOpenApiDocument(appTitle, appDescription, version, versioning.Enabled, configureSettings);
            }
        }

        private static void AddOpenApiDocument(this IServiceCollection services,
            string appTitle,
            string appDescription,
            string version,
            bool versioningEnabled, Action<AspNetCoreOpenApiDocumentGeneratorSettings> configureSettings)
        {
            services
                .AddOpenApiDocument(document =>
                {
                    document.DocumentName = $"v{version}";
                    document.Title = appTitle;
                    document.Description = appDescription;
                    document.Version = $"v{version}";
                    document.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();

                    if (versioningEnabled)
                    {
                        document.ApiGroupNames = new[] {version};
                    }

                    configureSettings(document);
                });
        }
    }
}
