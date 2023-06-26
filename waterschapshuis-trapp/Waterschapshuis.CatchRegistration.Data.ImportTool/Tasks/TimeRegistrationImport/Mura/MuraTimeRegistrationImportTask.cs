using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport.Mura
{
    public class MuraTimeRegistrationImportTask : MuraPgImportTask
    {
        public MuraTimeRegistrationImportTask(
            ILogger<MuraTimeRegistrationImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Urenregistratie, TimeRegistration>(
                CreateTimeRegistrationAsync,
                @"SELECT guid as Id, gebruiker as User, vg_id as SubArea, uurhok as HourSquare, datum as Date, uren as Hours, 
                minuten as Minutes,  bestr_type as TrappingType
                FROM public.urenregistratie ORDER BY datum",
                DefaultBatchSize,
                cancellationToken);
        }

        protected Task<TimeRegistration> CreateTimeRegistrationAsync(Urenregistratie import, CancellationToken cancellationToken)
        {
            if (!import.Date.HasValue || !DatePassesOrganizationConstrain(import.Date.Value))
            {
                throw ImportException.InvalidDate();
            }

            var tr = TimeRegistration.Create(
                FindSystemUser(import.User).Id,
                FindSubAreaHourSquare(import.SubArea, import.HourSquare),
                FormatTrappingType(import.TrappingType),
                import.Date.Value,
                FormatHours(import.Hours, import.Minutes),
                TimeRegistrationStatus.Written,
                false);

            tr.PopulateCreatedUpdated(tr.UserId, tr.Date);

            return Task.FromResult(tr);
        }

        private Guid FindSubAreaHourSquare(string subArea, string hourSquare)
        {
            return Scope.GetService<IRepository<SubAreaHourSquare>>()
                       .QueryAllIncluding(sh => sh.SubArea, sh => sh.HourSquare)
                       .FirstOrDefault(sh =>
                           sh.SubArea.Name == subArea &&
                           sh.HourSquare.Name == hourSquare)?
                       .Id
                   ?? throw ImportException.NotFoundSubAreaHourSquare();
        }

        private double FormatHours(int? hours, int? minutes)
        {
            if (!hours.HasValue && !minutes.HasValue)
            {
                throw ImportException.EmptyHours();
            }

            var formattedHours = TimeFormatter.ToHours(hours ?? 0, minutes ?? 0);
            return formattedHours > 0 ? formattedHours : throw ImportException.EmptyHours();
        }

        private Guid FormatTrappingType(string type)
        {
            return type switch
            {
                "M" => TrappingType.MuskusratId,
                "B" => TrappingType.BeverratId,
                _ => throw ImportException.InvalidTrapType()
            };
        }
    }
}
