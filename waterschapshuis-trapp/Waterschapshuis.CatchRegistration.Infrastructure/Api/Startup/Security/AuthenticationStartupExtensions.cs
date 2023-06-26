using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Settings;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security
{
    public static class AuthenticationStartupExtensions
    {
        public static void AppAddAuthentication(this IServiceCollection services, IConfiguration configuration, bool enable)
        {
            if (!enable)
            {
                return;
            }

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddAzureAdBearer(options => configuration.Bind("App:AzureAd", options));
        }

        private static void AddAzureAdBearer(this AuthenticationBuilder builder, Action<AzureAdSettings> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAzureOptions>();
            builder.AddJwtBearer();
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AzureAdSettings _azureAdSettings;
            private readonly Dictionary<string, string> _validIssuers;
            private const string UpnClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

            [UsedImplicitly]
            public ConfigureAzureOptions(IOptions<AzureAdSettings> azureSettings)
            {
                _azureAdSettings = azureSettings.Value;
                _validIssuers = _azureAdSettings.AllowedTenantIds
                    .Select(tid => $"https://sts.windows.net/{tid}/")
                    .ToDictionary(x => x, x => x);
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                options.Authority = $"{_azureAdSettings.Instance}/organizations/v2.0";
                options.TokenValidationParameters.ValidAudiences = new[]
                {
                    _azureAdSettings.ClientId,
                    $"api://{_azureAdSettings.ClientId}",
                    "https://graph.windows.net"
                };
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.IssuerValidator = ValidateSpecificIssuers;
                options.Events = new JwtBearerEvents
                {
                    OnForbidden = context =>
                    {
                        Claim upnClaims = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == UpnClaimType);
                        var logger = context.HttpContext.RequestServices.GetService<ILogger<ConfigureAzureOptions>>();
                        logger.LogWarning("Forbidden request, user: {UpnClaims}", upnClaims?.Value);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetService<ILogger<ConfigureAzureOptions>>();
                        logger.LogWarning("Authentication failed. ${Exception}", context.Exception);
                        return Task.CompletedTask;
                    }
                };
            }

            private string ValidateSpecificIssuers(
                string issuer,
                SecurityToken securityToken,
                TokenValidationParameters validationParameters)
            {
                if (IsValidIssuer(issuer))
                {
                    return issuer;
                }

                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    var user = jwtSecurityToken.ReadValueFromPayload("unique_name");

                    Log.Warning("Invalid {Issuer}, {User}", issuer, user);
                }

                throw new SecurityTokenInvalidIssuerException(
                    "The sign-in user's account does not belong to one of the tenants that this Web App accepts users from.");
            }

            private bool IsValidIssuer(string issuer)
            {
                return _validIssuers.ContainsKey(issuer);
            }
        }
    }
}
