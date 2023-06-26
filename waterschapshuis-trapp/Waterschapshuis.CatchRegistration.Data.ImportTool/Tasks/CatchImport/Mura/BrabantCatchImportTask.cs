using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport.Mura
{
    public class BrabantCatchImportTask : MuraCatchImportTask
    {
        public BrabantCatchImportTask(
            ILogger<BrabantCatchImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Vangst, Catch>(
                CreateCatchAsync,
                $@"select 
	                v.guid as id, 
	                case 
		            -- 1001 = muskusrat
		            when fk_soort = 1001 and leeftijd = 'oud' and sex = 'ram' then 9001
		            when fk_soort = 1001 and leeftijd = 'jong' and sex = 'ram' then 9002
		            when fk_soort = 1001 and leeftijd = 'oud' and sex = 'moer' then 9003
		            when fk_soort = 1001 and leeftijd = 'jong' and sex = 'moer' then 9004
		            when fk_soort = 1001 and (leeftijd is null or leeftijd not in ('jong', 'oud') or 
									            sex is null or sex not in ('moer', 'ram')) then 1001
		            -- 1002 = beverrat
		            when fk_soort = 1002 and leeftijd = 'oud' and sex = 'ram' then 8001
		            when fk_soort = 1002 and leeftijd = 'jong' and sex = 'ram' then 8002
		            when fk_soort = 1002 and leeftijd = 'oud' and sex = 'moer' then 8003
		            when fk_soort = 1002 and leeftijd = 'jong' and sex = 'moer' then 8004
		            when fk_soort = 1002 and (leeftijd is null or leeftijd not in ('jong', 'oud') or 
									            sex is null or sex not in ('moer', 'ram')) then 1002
		            else fk_soort
		            end as catchtype, 
	            v.fk_vangstlocatie as trapid, v.tijdstip as createdon, 
	            v.gebruiker as createdby, v.aantal as number 
	            from vangsten v 
	            inner join vanglocaties on v.fk_vangstlocatie = vanglocaties.guid
	            inner join organisatie on vanglocaties.klantcode = organisatie.mura_klantcode
	            left outer join vangsten_extras on v.guid = vangsten_extras.vangsten_guid
                where organisatie.id = {MuraOrganizationIds.Brabant}",
                cancellationToken);
        }

        protected override string GetPrefixedId(string id)
        {
            return id.AsOrganizationPrefixed(OrganizationNames.Brabant);
        }

        protected override bool DatePassesOrganizationConstrain(DateTimeOffset date)
        {
            return date.PassesOrganizationDateConstrain(OrganizationNames.Brabant);
        }
    }
}
