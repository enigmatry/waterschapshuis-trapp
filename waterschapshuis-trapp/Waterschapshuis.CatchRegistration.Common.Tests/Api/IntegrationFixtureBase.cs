using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Common.Tests.Configuration;
using Waterschapshuis.CatchRegistration.Common.Tests.Database;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Api
{
    public abstract class IntegrationFixtureBase
    {
        protected IConfiguration _configuration = null!;
        protected IServiceScope _testScope = null!;

        protected void BuildConfiguration(Dictionary<string, string> additionalSettings)
        {
            _configuration = new TestConfigurationBuilder()
                .WithAdditionalSettings(additionalSettings)
                .Build();
        }

        protected abstract IServiceScope CreateScope();

        protected void CreateDatabase()
        {
            using IServiceScope scope = CreateScope();
            var dbContext = scope.Resolve<CatchRegistrationDbContext>();

            // On Azure we cannot drop db, we can only delete all tables
            DropAllDbObjects(dbContext.Database);

            // In case that we want to delete db call: dbContext.Database.EnsureDeleted()
            dbContext.Database.Migrate();
        }

        private static void DropAllDbObjects(DatabaseFacade database)
        {
            try
            {
                string dropAllSql = EmbeddedResource.ReadResourceContent(
                    "Waterschapshuis.CatchRegistration.Common.Tests.Database.DropAllSql.sql",
                    typeof(IntegrationFixtureBase).Assembly);
                foreach (var statement in dropAllSql.SplitStatements())

                // WriteLine("Executing: " + statement);
                {
                    database.ExecuteSqlRaw(statement);
                }
            }
            catch (SqlException ex)
            {
                const int cannotOpenDatabaseErrorNumber = 4060;
                if (ex.Number == cannotOpenDatabaseErrorNumber)
                {
                    WriteLine("Error while trying to drop all objects from database. Maybe database does not exist.");
                    WriteLine("Continuing...");
                    WriteLine(ex.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        [TearDown]
        public void Teardown()
        {
            _testScope.Dispose();
        }

        protected void AddCurrentUserToDb()
        {
            using IServiceScope scope = CreateScope();
            var currentUserProvider = scope.Resolve<TestCurrentUserProvider>();
            var dbContext = scope.Resolve<DbContext>();

            dbContext.Add(currentUserProvider.User);

            dbContext.SaveChanges();
        }

        protected void AddAndSaveChanges(params object[] entities)
        {
            var dbContext = Resolve<DbContext>();

            foreach (object entity in entities)
            {
                dbContext.Add(entity);
            }

            dbContext.SaveChanges();
        }

        protected void AddToContext(params object[] entities)
        {
            AddToContext(entities.AsEnumerable());
        }

        private void AddToContext(IEnumerable<object> entities)
        {
            var dbContext = Resolve<DbContext>();

            foreach (object entity in entities)
            {
                dbContext.Add(entity);
            }
        }

        protected void SaveChanges()
        {
            var unitOfWork = Resolve<IUnitOfWork>();
            unitOfWork.SaveChanges();
        }

        protected IQueryable<T> QueryDb<T>() where T : class
        {
            return Resolve<DbContext>().Set<T>();
        }

        protected IQueryable<T> QueryDbSkipCache<T>() where T : class
        {
            return Resolve<DbContext>().Set<T>().AsNoTracking();
        }

        protected int RemoveFromDb<T>(Guid entityId) where T : Entity<Guid>
        {
            var entity = QueryDbSkipCache<T>().Single(x => x.Id == entityId);
            return RemoveFromDb<T>(entity);
        }

        protected int RemoveFromDb<T>(params T[] entity) where T : Entity<Guid>
        {
            Resolve<DbContext>().RemoveRange(entity);
            return Resolve<IUnitOfWork>().SaveChanges();
        }

        protected T Resolve<T>()
        {
            return _testScope.Resolve<T>();
        }

        protected void ClearCurrentUserPermissions()
        {
            ChangeCurrentUserPermissions(new PermissionId[0]);
        }

        protected void ChangeCurrentUserPermissions(PermissionId[] permissionIds)
        {
            var currentUserProvider = _testScope.Resolve<TestCurrentUserProvider>();
            currentUserProvider.ChangePermissions(permissionIds);
        }

        protected void ChangeCurrentUserOrganization(Organization? organization)
        {
            var currentUserProvider = _testScope.Resolve<TestCurrentUserProvider>();
            currentUserProvider.ChangeOrganization(organization);
        }

        protected void ChangeCurrentUserId(Guid id)
        {
            var currentUserProvider = _testScope.Resolve<TestCurrentUserProvider>();
            currentUserProvider.ChangeUserId(id);
        }

        protected void ChangeCurrentUserRoleIds(params Guid[] ids)
        {
            var currentUserProvider = _testScope.Resolve<TestCurrentUserProvider>();
            currentUserProvider.ChangeRoles(ids);
        }

        private static void WriteLine(string message)
        {
            TestContext.WriteLine(message);
        }
    }
}
