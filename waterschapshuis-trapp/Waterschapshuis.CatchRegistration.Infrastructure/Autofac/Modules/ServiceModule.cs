
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public class ServiceModule : Module
    {
        public Assembly[] Assemblies { private get; set; } = new Assembly[0];

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assemblies)
                .Where(
                    type => type.Name.EndsWith("Service") || type.Name.EndsWith("Provider")
                )
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
