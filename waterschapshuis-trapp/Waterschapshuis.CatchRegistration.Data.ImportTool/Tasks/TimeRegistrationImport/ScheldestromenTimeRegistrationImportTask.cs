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
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport
{
    public class ScheldestromenTimeRegistrationImportTask : JsonImportTask
    {
        public ScheldestromenTimeRegistrationImportTask(
            ILogger<ScheldestromenTimeRegistrationImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory) : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecuteJsonImportsAsync(CancellationToken cancellationToken)
        {
            await ExecuteJsonImportAsync<ScheldestromenTimeRegistrationProperties, TimeRegistration>(
                new Regex("^features\\[[\\d]+]\\.properties"),
                async (data, token) =>
                {
                    if (!Int64.TryParse(data.User, out var userId))
                    {
                        throw new ImportException("Invalid user id.");
                    }

                    SubAreaHourSquare sh = await Scope.ServiceProvider
                                               .GetRequiredService<IRepository<SubAreaHourSquare>>()
                                               .QueryAllIncluding(x => x.SubArea, x => x.HourSquare)
                                               .FirstOrDefaultAsync(x => x.SubArea.Name == data.SubAreaName &&
                                                                         x.HourSquare.Name == data.HourSquareName,
                                                   token) ??
                                           throw ImportException.NotFoundSubAreaHourSquare();

                    User usr = await Scope.ServiceProvider.GetRequiredService<IRepository<User>>()
                                   .QueryAll()
                                   .FirstOrDefaultAsync(x => x.ExternalId == userId, token) ??
                               throw ImportException.NotFoundUser();

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
                        List<(int? Hours, DayOfWeek Day)> loggedHours = value.Where(d => d.Hours > 0).ToList();

                        loggedHours.ForEach(h =>
                        {
                            var tr = TimeRegistration.Create(
                                usr.Id,
                                sh.Id,
                                key,
                                new DateTimeOffset(ISOWeek.ToDateTime(data.Year!.Value, data.Week!.Value, h.Day)),
                                (double)h.Hours,
                                TimeRegistrationStatus.Written,
                                false
                            );

                            tr.PopulateCreatedUpdated(tr.UserId, tr.Date);

                            results.Add(tr);
                        });
                    }

                    if (!results.Any())
                    {
                        throw ImportException.EmptyHours();
                    }

                    if (results.All(r => !r.Date.PassesOrganizationDateConstrain(OrganizationNames.Scheldestromen)))
                    {
                        throw ImportException.InvalidDate();
                    }

                    return results.Where(r => r.Date.PassesOrganizationDateConstrain(OrganizationNames.Scheldestromen));
                }, cancellationToken);
        }
    }
}
