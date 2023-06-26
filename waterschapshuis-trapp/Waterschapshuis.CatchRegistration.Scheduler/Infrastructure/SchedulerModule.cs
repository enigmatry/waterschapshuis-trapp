using Autofac;
using Autofac.Extras.Quartz;
using Microsoft.Extensions.Configuration;
using Quartz;
using System.Collections.Specialized;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;

namespace Waterschapshuis.CatchRegistration.Scheduler.Infrastructure
{
    public class SchedulerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => new SchedulerSystemUserIdProvider())
                .As<ICurrentUserIdProvider>()
                .InstancePerLifetimeScope();

            RegisterSettings(builder);

            RegisterQuartz(builder);
        }

        private static void RegisterSettings(ContainerBuilder builder)
        {
            builder
                .Register(c => c.Resolve<IConfiguration>().ReadSettingsSection<SchedulerSettings>("SchedulerSettings"))
                .AsSelf()
                .SingleInstance();
        }

        private static void RegisterQuartz(ContainerBuilder builder)
        {
            builder
                .RegisterModule(new QuartzAutofacFactoryModule
                {
                    ConfigurationProvider = c => c.Resolve<IConfiguration>().ReadSettingsSection<NameValueCollection>("quartz")
                });

            builder
                .RegisterModule(new QuartzAutofacJobsModule(AssemblyFinder.SchedulerAssembly));

            builder
                .RegisterAssemblyTypes(AssemblyFinder.SchedulerAssembly)
                .Where(x => !x.IsAbstract && x.GetInterfaces().Contains(typeof(IJob)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
