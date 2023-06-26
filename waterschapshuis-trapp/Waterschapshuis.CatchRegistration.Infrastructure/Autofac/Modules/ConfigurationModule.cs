using Autofac;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => c.Resolve<IConfiguration>().ReadAppSettings())
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<IConfiguration>().ReadSettingsSection<DbContextSettings>("DbContext"))
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().Smtp)
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().GeoServer)
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().ApiConfiguration)
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().AzureBlob)
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().UserSessions)
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<IConfiguration>().ReadSettingsSection<ApplicationInsightsSettings>("ApplicationInsights"))
                .AsSelf()
                .SingleInstance();
        }
    }
}
