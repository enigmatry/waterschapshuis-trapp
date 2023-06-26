using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Mapping;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TrapImport.Mura
{
    [UsedImplicitly]
    public class MuraTrapImportTask : MuraPgImportTask
    {
        public MuraTrapImportTask(
            ILogger<MuraTrapImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Vanglocatie, Trap>(
                CreateTrapAsync,
                @"SELECT guid as id, fk_vangmiddel as traptype, aantal as numberoftraps, verwijderd as removed, actief as active, 
                opm as remarks, gebruiker as userexternalid, datum as date, ST_Transform(the_geom, 28992) as location 
                FROM public.vanglocaties ORDER BY guid",
                DefaultBatchSize,
                cancellationToken);
        }

        protected Task<Trap> CreateTrapAsync(Vanglocatie item, CancellationToken token)
        {
            var subAreaHourSquare =
                Scope.GetService<IRepository<SubAreaHourSquare>>()
                    .QueryAll()
                    .FindByLongAndLat(item.Location.X, item.Location.Y, Logger)
                ?? throw ImportException.NotFoundSubAreaHourSquare();

            Province province = Scope.GetService<IRepository<Province>>().QueryAll()
                .QueryContainesCoordinates(item.Location.X, item.Location.Y)
                .FirstOrDefault() ?? throw ImportException.NotFoundProvince();

            var trap =
                Trap.Create(
                    new DomainModel.Traps.Commands.TrapImport(
                        item.NumberOfTraps,
                        item.GetStatus(),
                        item.Location.X,
                        item.Location.Y,
                        TrapTypeMapper.GetTrapTypeGuid(item.TrapType),
                        item.Remarks.Truncate(Trap.RemarksMaxLength),
                        subAreaHourSquare,
                        province,
                        GetPrefixedId(item.Id)
                ));

            if (Scope.GetService<IRepository<Trap>>()
                .QueryAll()
                .Any(x => x.ExternalId == trap.ExternalId))
            {
                throw ImportException.ExistsTrap();
            }

            trap.PopulateCreatedUpdatedRecorded(FindSystemUser(item.UserExternalId).Id, item.Date);

            return Task.FromResult(trap);
        }

        protected virtual string GetPrefixedId(string id)
        {
            return id.AsOrganizationPrefixed(OrganizationNames.Mura);
        }
    }
}
