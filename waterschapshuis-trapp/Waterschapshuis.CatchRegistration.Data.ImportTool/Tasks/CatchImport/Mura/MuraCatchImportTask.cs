using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Mapping;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport.Mura
{
    [UsedImplicitly]
    public class MuraCatchImportTask : MuraPgImportTask
    {
        public MuraCatchImportTask(
            ILogger<MuraCatchImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        { }

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Vangst, Catch>(
                CreateCatchAsync,
                @"SELECT guid as id, fk_soort as catchtype, fk_vangstlocatie as trapid, tijdstip as createdon, 
                gebruiker as createdby, aantal as number FROM public.vangsteny ORDER BY guid",
                DefaultBatchSize,
                cancellationToken);
        }

        protected async Task<Catch> CreateCatchAsync(Vangst item, CancellationToken cancellationToken)
        {
            if (!DatePassesOrganizationConstrain(item.CreatedOn))
            {
                throw ImportException.InvalidDate();
            }

            // find the Trap based on previously saved ExternalId for the Trap
            var trap = await Scope.GetService<IRepository<Trap>>()
                .QueryAll()
                .SingleOrDefaultAsync(x =>
                    x.ExternalId == GetPrefixedId(item.TrapId), cancellationToken)
                ?? throw ImportException.NotFoundTrap();


            var importCatch = new DomainModel.Catches.Commands.CatchImport(
                item.Number, 
                CatchStatus.Written, 
                trap.Id, 
                CatchTypeMapper.GetCatchTypeGuid(item.CatchType));

            var @catch = Catch.Create(importCatch);

            @catch.PopulateCreatedUpdatedRecorded(FindSystemUser(item.CreatedBy).Id, item.CreatedOn);

            return @catch;
        }

        protected virtual string GetPrefixedId(string id)
        {
            return id.AsOrganizationPrefixed(OrganizationNames.Mura);
        }
    }
}
