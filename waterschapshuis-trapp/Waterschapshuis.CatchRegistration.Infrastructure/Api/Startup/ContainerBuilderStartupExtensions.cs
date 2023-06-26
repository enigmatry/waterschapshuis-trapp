using System.Security.Claims;
using System.Security.Principal;
using Autofac;
using Microsoft.AspNetCore.Http;
using Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class ContainerBuilderStartupExtensions
    {
        public static void AppRegisterModules(this ContainerBuilder builder)
        {
            builder.Register(GetPrincipal).As<IPrincipal>().InstancePerLifetimeScope();
            AppRegisterCommonModules(builder);
        }

        private static void AppRegisterCommonModules(this ContainerBuilder builder)
        {
            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule(new ServiceModule()
            {
                Assemblies = new[]
                {
                    AssemblyFinder.ApplicationServicesAssembly, AssemblyFinder.InfrastructureAssembly
                }
            });
            builder.RegisterModule<EntityFrameworkModule>();
            builder.RegisterModule<RoleAwareEntityFrameworkModule>();
            builder.RegisterModule<IdentityModule>();
            builder.RegisterModule<EmailModule>();
            builder.RegisterModule<TemplatingModule>();
            builder.RegisterModule<BlobStorageModule>();
            builder.RegisterModule<ReportingModule>();
        }

        private static ClaimsPrincipal GetPrincipal(IComponentContext c)
        {
            var httpContextAccessor = c.Resolve<IHttpContextAccessor>();
            return httpContextAccessor.HttpContext.User;
        }
    }
}
