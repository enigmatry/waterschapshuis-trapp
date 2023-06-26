﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport.Mura
{
    public class FryslanTimeRegistrationImportTask : MuraTimeRegistrationImportTask
    {
        public FryslanTimeRegistrationImportTask(
            ILogger<FryslanTimeRegistrationImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Urenregistratie, TimeRegistration>(
                CreateTimeRegistrationAsync,
                $@"select u.guid as Id, u.gebruiker as User, u.vg_id as SubArea,
                u.uurhok as HourSquare, u.datum as Date, u.uren as Hours, 
                u.minuten as Minutes, u.bestr_type as TrappingType 
                from urenregistratie u
                inner join organisatie on u.klantcode = organisatie.mura_klantcode
                where organisatie.id = {MuraOrganizationIds.Fryslan} order by datum",
                cancellationToken);
        }

        protected override bool DatePassesOrganizationConstrain(DateTimeOffset date)
        {
            return date.PassesOrganizationDateConstrain(OrganizationNames.Fryslan);
        }
    }
}
