using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Extensions.Configuration;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure
{
    public class ImportModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            SetupCurrentUser(builder);
            SetupImportTasks(builder);
        }

        private static void SetupImportTasks(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ImportModule).Assembly)
                .Where(type => type.Name.EndsWith("ImportTask"))
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        private static void SetupCurrentUser(ContainerBuilder builder)
        {
            // when importing we do not want to override created or updated users.
            builder.RegisterType<NullCurrentUserIdProvider>()
                .As<ICurrentUserIdProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NullCurrentUserProvider>()
                .As<ICurrentUserProvider>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<IConfiguration>().ReadSettingsSection<DbContextSettings>("DbContext"))
                .AsSelf()
                .SingleInstance();
        }

        private class NullCurrentUserIdProvider : ICurrentUserIdProvider
        {
            public Guid? FindUserId(IQueryable<User> query)
            {
                return null;
            }

            public string Email => String.Empty;
            public bool IsAuthenticated => false;
        }

        private class NullCurrentUserProvider : ICurrentUserProvider
        {
            public Guid? UserId => null;

            public bool IsAuthenticated => true;

            public UserAuthorizationStatus UserAuthorizationStatus => UserAuthorizationStatus.Success;

            public string Email => "import-tool@enigmatry.com";

            public string Name => throw new NotImplementedException();

            public PermissionId[] PermissionIds => Array.Empty<PermissionId>();

            public Guid[] RoleIds => Array.Empty<Guid>();

            public Organization Organization => throw new NotImplementedException();

            public void TryReloadUser()
            {
                throw new NotImplementedException();
            }

            public Guid GetUserId()
            {
                return UserId.GetValueOrDefault();
            }

            public bool UserHasAnyPermission(IEnumerable<PermissionId> permissionIds)
            {
                return true;
            }
        }
    }
}
