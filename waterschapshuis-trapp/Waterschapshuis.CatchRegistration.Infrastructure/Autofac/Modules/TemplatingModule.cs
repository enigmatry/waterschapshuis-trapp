using Autofac;
using Waterschapshuis.CatchRegistration.ApplicationServices;
using Waterschapshuis.CatchRegistration.Infrastructure.Templating;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public class TemplatingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RazorTemplatingEngine>().As<ITemplatingEngine>().InstancePerLifetimeScope();
        }
    }
}
