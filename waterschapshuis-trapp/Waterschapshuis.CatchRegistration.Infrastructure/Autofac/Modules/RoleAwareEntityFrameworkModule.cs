using Autofac;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.RoleAwareFiltering;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    [UsedImplicitly]
    public class RoleAwareEntityFrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RoleAwareReadOnlyEntityFrameworkRepository<>))
                .As(typeof(IRoleAwareReadOnlyRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AssemblyFinder.DomainAssembly)
                .Where(type => typeof(IFilterEntityByCurrentUserRoleQueryExpression<>).ImplementsInterface(type))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
