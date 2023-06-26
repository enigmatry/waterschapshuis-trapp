using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport
{
    public class LimburgTimeRegistrationImportTask : JsonImportTask
    {
        public LimburgTimeRegistrationImportTask(
            ILogger<LimburgTimeRegistrationImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory) 
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecuteJsonImportsAsync(CancellationToken cancellationToken)
        {
            await ExecuteJsonImportAsync<LimburgTimeRegistrationProperties, TimeRegistration>(
                new Regex("^features\\[[\\d]+]\\.properties"),
                async (data, token) =>
                {
                    var user = FindSystemUser(data.User);

                    SubAreaHourSquare subAreaHourSquare =
                        await Scope.ServiceProvider.GetRequiredService<IRepository<SubAreaHourSquare>>()
                            .QueryAllIncluding(sh => sh.SubArea, sh => sh.HourSquare)
                            .FirstOrDefaultAsync(sh => sh.HourSquare.Name == data.HourSquareName &&
                                                       sh.SubArea.Name == data.SubAreaName, token) ??
                        throw ImportException.NotFoundSubAreaHourSquare();

                    if (!data.Year.HasValue || data.Year < 1)
                    {
                        throw ImportException.InvalidYear();
                    }

                    if (!data.Week.HasValue || data.Week < 1 || data.Week > 53)
                    {
                        throw ImportException.InvalidWeek();
                    }

                    var results = new List<TimeRegistration>();

                    foreach ((Guid key, (int? Hours, DayOfWeek Day)[] value) in data.FormatDayHours())
                    {
                        value.Where(d => d.Hours > 0).ToList().ForEach(h =>
                        {
                            var tr = TimeRegistration.Create(
                                user.Id,
                                subAreaHourSquare.Id,
                                key,
                                new DateTimeOffset(ISOWeek.ToDateTime(data.Year!.Value, data.Week!.Value, h.Day)),
                                (double)h.Hours,
                                TimeRegistrationStatus.Written,
                                false);

                            tr.PopulateCreatedUpdated(user.Id, tr.Date);

                            results.Add(tr);
                        });
                    }

                    if (!results.Any())
                    {
                        throw ImportException.EmptyHours();
                    }

                    if (results.All(r => !r.Date.PassesOrganizationDateConstrain(OrganizationNames.Limburg)))
                    {
                        throw ImportException.InvalidDate();
                    }

                    return results.Where(r => r.Date.PassesOrganizationDateConstrain(OrganizationNames.Limburg));
                },
                cancellationToken);
        }
    }
}
