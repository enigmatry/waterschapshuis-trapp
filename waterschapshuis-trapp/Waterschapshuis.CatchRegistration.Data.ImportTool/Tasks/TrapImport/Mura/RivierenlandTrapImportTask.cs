using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TrapImport.Mura
{
    public class RivierenlandTrapImportTask : MuraTrapImportTask
    {
        public RivierenlandTrapImportTask(
            ILogger<RivierenlandTrapImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Vanglocatie, Trap>(
                CreateTrapAsync,
                @$"select v.guid as id, v.fk_vangmiddel as traptype, v.aantal as numberoftraps, 
                v.verwijderd as removed, v.actief as active, 
                v.opm as remarks, v.gebruiker as userexternalid, 
                v.datum as date, ST_Transform(v.the_geom, 28992) as location 
                from vanglocaties v
                inner join organisatie on v.klantcode = organisatie.mura_klantcode
                where organisatie.id = {MuraOrganizationIds.Rivierenland}",
                cancellationToken);
        }

        protected override string GetPrefixedId(string id)
        {
            return id.AsOrganizationPrefixed(OrganizationNames.Rivierenland);
        }

        protected override bool DatePassesOrganizationConstrain(DateTimeOffset date)
        {
            return date.PassesOrganizationDateConstrain(OrganizationNames.Rivierenland);
        }
    }
}
