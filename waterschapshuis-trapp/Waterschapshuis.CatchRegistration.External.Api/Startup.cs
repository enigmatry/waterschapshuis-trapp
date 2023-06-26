using Autofac;
using JetBrains.Annotations;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

namespace Waterschapshuis.CatchRegistration.External.Api
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
            AuthorizationFeatureEnabled = _configuration.ReadAppSettings().EasyAuth.Enabled;
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

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<LogContextMiddleware>();// needs to be after authentication/authorization so that we can read current user

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().AppRequireAuthorization(AuthorizationFeatureEnabled);
                endpoints.AppMapHealthChecks();

                endpoints.AppMapODataRoute();
            });

            app.AppUseSwaggerWithEasyAuth();
            app.AppConfigureFluentValidation();
        }

        [UsedImplicitly]
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesExceptMvc(services, _configuration);
            services.AppAddMvc(_configuration, AuthorizationFeatureEnabled, AssemblyFinder.ExternalApiAssembly);
        }

        // this also called by tests. Mvc is configured slightly differently in integration tests
        public void ConfigureServicesExceptMvc(IServiceCollection services, IConfiguration configuration)
        {
            string appDescription = "The External API provides access to the data collected by the Dutch Water Authorities in their muskrat and coypu population management. This description applies to the REST API, " +
                "but there is also an OData interface available at the /odata endpoint. The same object set is available in both API's. The following objects are available: <br>" +
                "- Traps: This object gives insight into types of traps and their position, status and remarks<br>" +
                "- TimeRegistration: Contains field hours made by trappers while trapping<br>" +
                "- Observations: This object contains the observations which are logged by a trapper in the field. Besides the position, it also contains URL link to the image, type (damage or other), " +
                "remarks and whether the observation is archived or not<br>" +
                "- Catches: This object gives insight into amount of (by-)catches distinguished by Muskrat or Coypu, Age, Gender and (By-)Catch<br>" +
                "All objects have identifiers for referring to Organization, Water Authority, Rayon, Catch Area, Sub Area or Hour Square (Atlas Block).<br>" +
                "Coordinates are returned in Rijksdriehoekscoördinaten (EPSG 28992).<br><br>" +
                "For access to this API the user needs to have at least the 'Public external access' role. For access to sensitive information and the TimeRegistration object the user needs to have the 'Private external access' role. " +
                "For user management and access get in touch with the Water Authorities.<br><br>" +
                "In the OData interfaces there is the possibility to retrieve the data based on an Organization or on a Year.";

            services.AddOData();
            services.AddODataQueryFilter();

            services.AddCors();
            services.AddHttpContextAccessor();
            services.AddDbContext<CatchRegistrationDbContext>();
            services.AddApplicationInsightsTelemetry();

            services.AppAddPolly();
            services.AppAddAutoMapper(AssemblyFinder.ExternalApiAssembly);

            services.AppAddEasyAuthAuthentication(configuration, AuthorizationFeatureEnabled);
            services.AppAddAuthorization(AuthorizationFeatureEnabled, _hostEnvironment);

            AppSettings appSettings = configuration.ReadAppSettings();

            services.AppAddHealthChecks(configuration);
            services.AppAddMediatR(AssemblyFinder.ExternalApiAssembly);
            services.AppAddTypedHttpClients(appSettings);
            services.AppAddSwaggerWithEasyAuth("trAPP External API", appDescription, appSettings.ApiVersioning);

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
