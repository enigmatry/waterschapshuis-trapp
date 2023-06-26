using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.UserImport
{
    [UsedImplicitly]
    public sealed class UserImportTask : PgImportTask
    {
        private static readonly Guid SystemUserId = User.SystemUserId;

        public UserImportTask(
            ILogger<UserImportTask> logger, 
            IConfiguration configuration, 
            IServiceScopeFactory serviceScopeFactory) 
            : base(logger, configuration, serviceScopeFactory)
        { }

        protected override string ConnectionString => Configuration.GetConnectionString("PostgresV2");

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Gebruiker, User>(
                    MapUser,
                    "select id, case when length(tussenvoegsel)= 0 or tussenvoegsel is null then trim(naam) || ', ' || trim(voorletters) else trim(naam) || ', ' || trim(voorletters) || ' ' || trim(tussenvoegsel) end as name, email from public.gebruiker order by id",
                    cancellationToken);
        }

        private Task<User> MapUser(Gebruiker model, CancellationToken cancellationToken)
        {
            var date = Scope.GetService<ITimeProvider>().Now;

            var user = User.Create(model.Name, model.Email, model.Id);
            
            user.SetCreated(date, SystemUserId);
            user.SetUpdated(date, SystemUserId);

            return Task.FromResult(user);
        }
    }
}
