using Autofac;
using Waterschapshuis.CatchRegistration.ApplicationServices.Identity;
using Waterschapshuis.CatchRegistration.ApplicationServices.Identity.Claims;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CurrentUserProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ClaimsPrincipalProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
