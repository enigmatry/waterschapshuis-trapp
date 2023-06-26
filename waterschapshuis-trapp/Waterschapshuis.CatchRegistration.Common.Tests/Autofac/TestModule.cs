using System.Security.Principal;
using Autofac;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Autofac
{
    [UsedImplicitly]
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            SetupCurrentUser(builder);
        }

        private static void SetupCurrentUser(ContainerBuilder builder)
        {
            var principal = TestPrincipal.CreateDefaultForIntegrationTesting();

            builder.Register(c => principal).As<IPrincipal>().InstancePerLifetimeScope();

            builder.Register(c => new TestCurrentUserIdProvider(principal))
                .As<ICurrentUserIdProvider>()
                .AsSelf()
                .SingleInstance();

            builder.Register(c => new TestCurrentUserProvider(principal, c.Resolve<ITimeProvider>()))
                .AsSelf()
                .As<ICurrentUserProvider>()
                .SingleInstance();// we use singleton so that state of TestCurrentUserProvider is shared between the test and the api
        }
    }
}
