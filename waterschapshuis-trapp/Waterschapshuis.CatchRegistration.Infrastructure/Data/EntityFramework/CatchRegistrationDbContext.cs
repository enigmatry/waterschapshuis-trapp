using JetBrains.Annotations;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework.MediatR;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class CatchRegistrationDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly ITimeProvider _timeProvider;
        private readonly ICurrentUserIdProvider _currentUserIdProvider;
        private readonly ILogger<CatchRegistrationDbContext> _logger;
        private readonly IDbContextAccessTokenProvider _dbContextAccessTokenProvider;
        public Action<ModelBuilder>? ModelBuilderConfigurator { private get; set; }

        public CatchRegistrationDbContext(
            DbContextOptions options,
            IMediator mediator,
            ITimeProvider timeProvider,
            ICurrentUserIdProvider currentUserIdProvider,
            ILogger<CatchRegistrationDbContext> logger,
            IDbContextAccessTokenProvider dbContextAccessTokenProvider) : base(options)
        {
            _mediator = mediator;
            _timeProvider = timeProvider;
            _currentUserIdProvider = currentUserIdProvider;
            _logger = logger;
            _dbContextAccessTokenProvider = dbContextAccessTokenProvider;

            SetupManagedServiceIdentityAccessToken();
        }

        private void SetupManagedServiceIdentityAccessToken()
        {
            var accessToken = _dbContextAccessTokenProvider.GetAccessTokenAsync().GetAwaiter().GetResult();
            if (accessToken.IsNotNullOrEmpty())
            {
                var connection = (SqlConnection)Database.GetDbConnection();
                connection.AccessToken = accessToken;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

            RegisterEntities(modelBuilder);

            ModelBuilderConfigurator?.Invoke(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void RegisterEntities(ModelBuilder modelBuilder)
        {
            MethodInfo entityMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == "Entity" && m.IsGenericMethod);

            Assembly? entitiesAssembly = Assembly.GetAssembly(typeof(User));
            var types = entitiesAssembly != null ? entitiesAssembly.GetTypes() : Enumerable.Empty<Type>();

            IEnumerable<Type> entityTypes = types
                    .Where(x => x.IsSubclassOf(typeof(Entity)) && !x.IsAbstract);

            foreach (Type type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        public override int SaveChanges()
        {
            var task = Task.Run(async () => await SaveChangesAsync());
            return task.GetAwaiter().GetResult();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Must go before PopulateCreatedUpdated() - so that createdBy/updatedBy are populated
            // Must go before SaveChangesAsync() - so that entries are saved in db (and in same transaction with others)
            var historyDomainEvents = this.GatherHistoryDomainEventsFromContext();
            if (historyDomainEvents.Any())
            {
                await _mediator.DispatchDomainEventsAsync(historyDomainEvents, _logger);
            }

            PopulateCreatedUpdated();

            // we need to gather domain events (except history ones) before saving, so that we include events
            // for deleted entities (otherwise they are lost due to deletion of the object from context)
            var domainEvents = this.GatherExceptHistoryDomainEventsFromContext();

            // clear all domain events
            this.ClearDomainEvents();

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var saved = await base.SaveChangesAsync(cancellationToken);

            if (domainEvents.Any())
            {
                await _mediator.DispatchDomainEventsAsync(domainEvents, _logger);
            }

            return saved;
        }

        private void PopulateCreatedUpdated()
        {
            Guid? userId = _currentUserIdProvider.IsAuthenticated
                ? _currentUserIdProvider.FindUserId(Set<User>())
                : null;

            if (userId.HasValue)
            {
                ChangeTracker
                    .GetChangedEntitiesWithHCreatedUpdated()
                    .ForEach(changedEntity =>
                    {
                        var entityChangedUpdated = (IEntityHasCreatedUpdated)changedEntity.Entity;
                        if (changedEntity.State == EntityState.Added)
                        {
                            entityChangedUpdated.SetCreated(_timeProvider.Now, userId.Value);
                            entityChangedUpdated.SetUpdated(_timeProvider.Now, userId.Value);
                        }
                        if (changedEntity.State == EntityState.Modified)
                        {
                            entityChangedUpdated.SetUpdated(_timeProvider.Now, userId.Value);
                        }
                    });
            }
        }
    }
}
