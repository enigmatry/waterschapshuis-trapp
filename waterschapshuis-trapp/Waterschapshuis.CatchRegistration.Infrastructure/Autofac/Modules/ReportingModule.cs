using Autofac;
using Waterschapshuis.CatchRegistration.ApplicationServices.Reports;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public class ReportingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DevExtremeReportHandlerFactory>().As<IDevExtremeReportHandlerFactory>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AssemblyFinder.ApplicationServicesAssembly)
                .Where(type => type.Name.EndsWith("ReportHandler"))
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
