using Autofac;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Logging;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        public bool AuthorizationFeatureEnabled { get; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            AuthorizationFeatureEnabled = _configuration.ReadAppSettings().AzureAd.Enabled;
        }

        [UsedImplicitly]

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (_configuration.AppUseDeveloperExceptionPage())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseHsts();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<LogContextMiddleware>(); // needs to be after authentication/authorization so that we can read current user

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().AppRequireAuthorization(AuthorizationFeatureEnabled);
                endpoints.AppMapHealthChecks();
            });

            app.AppUseSwaggerWithAzureAdAuthentication(_configuration.ReadAppSettings().AzureAd);
            app.AppConfigureFluentValidation();
        }

        [UsedImplicitly]

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesExceptMvc(services, _configuration);
            services.AppAddMvc(_configuration, AuthorizationFeatureEnabled, AssemblyFinder.BackOfficeApiAssembly);
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });
        }

        // this also called by tests. Mvc is configured slightly differently in integration tests
        public void ConfigureServicesExceptMvc(IServiceCollection services, IConfiguration configuration)
        {
            string appDescription =
                "The Back-office API provides access to the back-office of the trAPP application and is used by the Dutch Water Authorities in their muskrat and coypu population management. " +
                "The RESTful API consists of the following endpoints: <br>" +
                "- Roles: get overview of all available roles and update permission matrix <br>" +
                "- Users: managing user properties within the trAPP application <br>" +
                "- TrapTypes: managing trap types <br>" +
                "- Traps: read, write, update or delete traps <br>" +
                "- TimeRegistrations: read, write, update or delete time registration entered by trapper <br>" +
                "- Reports: used for getting and storing own definitions of reports <br>" +
                "- Organizations: getting overview of organizations <br>" +
                "- Observations: read, write, update or archive observations <br>" +
                "- Maps: getting available overlay and content layers as well as definitions of map styles <br>" +
                "- Lookups: getting available trap types, trapping types and catch types <br>" +
                "- FieldTests: managing field tests <br>" +
                "- CatchTypes: managing catchtypes <br>" +
                "- Catches: read, write, update or delete catches <br>" +
                "- Areas: retrieve geographic entities based on a given entity id<br>" +
                "- Account: is used for authentication of user accounts in Azure AD <br><br>" +
                "In case of an error most of the methods return ProblemDetails class. ProblemDetails is a .NET Core" +
                "class for standard way of specifying errors in HTTP API responses.";

            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<CatchRegistrationDbContext>();
            services.AddApplicationInsightsTelemetry();

            services.AppAddPolly();
            services.AppAddAutoMapper(AssemblyFinder.BackOfficeApiAssembly);

            services.AppAddAuthentication(configuration, AuthorizationFeatureEnabled);
            services.AppAddAuthorization(AuthorizationFeatureEnabled, _hostEnvironment);

            AppSettings appSettings = configuration.ReadAppSettings();

            services.AppAddHealthChecks(configuration);
            services.AppAddMediatR(AssemblyFinder.BackOfficeApiAssembly);
            services.AppAddTypedHttpClients(appSettings);
            services.AppAddSwaggerWithAzureAdAuthentication("trAPP BackOffice API", appDescription,
                appSettings.ApiVersioning, appSettings.AzureAd);

            // must be PostConfigure due to: https://github.com/aspnet/Mvc/issues/7858
            services.PostConfigure<ApiBehaviorOptions>(options => options.AppAddFluentValidationApiBehaviorOptions());
        }

        [UsedImplicitly]

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AppRegisterModules();
        }
    }
}
