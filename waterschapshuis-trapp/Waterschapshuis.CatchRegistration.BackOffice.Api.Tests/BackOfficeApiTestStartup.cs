using Autofac;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Waterschapshuis.CatchRegistration.Common.Tests.Autofac;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup;
using Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests
{
    [UsedImplicitly]
    public class BackOfficeApiTestStartup
    {
        private readonly Startup _startup;
        private readonly IConfiguration _configuration;

        public BackOfficeApiTestStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _startup = new Startup(configuration, hostEnvironment);
        }

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AppAddMvc(_configuration, _startup.AuthorizationFeatureEnabled, AssemblyFinder.BackOfficeApiAssembly)
                .AddApplicationPart(AssemblyFinder.BackOfficeApiAssembly); // needed only because of tests
            _startup.ConfigureServicesExceptMvc(services, _configuration);
        }

        [UsedImplicitly]
        public void ConfigureContainer(ContainerBuilder builder)
        {
            _startup.ConfigureContainer(builder);
            builder.RegisterModule<TestModule>();// this allows certain components to be overriden
            // Api does not depend on migrations assembly, test are
            builder.RegisterModule(new EntityFrameworkModule {RegisterMigrationsAssembly = true});
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _startup.Configure(app, env);
        }
    }
}
