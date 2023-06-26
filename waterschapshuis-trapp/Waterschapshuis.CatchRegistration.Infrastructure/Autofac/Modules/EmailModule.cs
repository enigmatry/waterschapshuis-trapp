using Autofac;
using Waterschapshuis.CatchRegistration.Core.Email;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.Infrastructure.Email;
using Microsoft.Extensions.Logging;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public class EmailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
                {
                    var smtpSettings = context.Resolve<SmtpSettings>();

                    IEmailClient emailClient = smtpSettings.UsePickupDirectory
                        ? (IEmailClient) new MailKitPickupDirectoryEmailClient(smtpSettings, context.Resolve<ILogger<MailKitPickupDirectoryEmailClient>>())
                        : new MailKitEmailClient(smtpSettings, context.Resolve<ILogger<MailKitEmailClient>>());

                    return emailClient;
                })
                .InstancePerLifetimeScope();
        }
    }
}
