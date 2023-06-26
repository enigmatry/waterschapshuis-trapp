using System.Collections.Generic;
using System.Text.RegularExpressions;
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
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport
{
    [UsedImplicitly]
    public class WestEnMiddenTimeRegistrationImportTask : JsonImportTask
    {
        public WestEnMiddenTimeRegistrationImportTask(ILogger<WestEnMiddenTimeRegistrationImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory) : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecuteJsonImportsAsync(CancellationToken cancellationToken)
        {
           await ExecuteJsonImportAsync<WestEnMiddenTimeRegistrationProperties, TimeRegistration>(
                new Regex("^features\\[[\\d]+]\\.properties"),
                async (data, token) =>
                {
                    var date = data.Date.AsDateTimeOffset();

                    if (!date.HasValue || !date.Value.PassesOrganizationDateConstrain(OrganizationNames.WestEnMidden))
                    {
                        throw ImportException.InvalidDate();
                    }

                    User user = FindSystemUser(data.User);

                    SubAreaHourSquare subAreaHourSquare =
                        await Scope.ServiceProvider.GetRequiredService<IRepository<SubAreaHourSquare>>()
                            .QueryAllIncluding(sh => sh.SubArea, sh => sh.HourSquare)
                            .FirstOrDefaultAsync(sh => sh.HourSquare.Name == data.HourSquareName &&
                                                       sh.SubArea.Name == data.SubAreaName, token) ??
                        throw ImportException.NotFoundSubAreaHourSquare();

                    if (!(data.MuskHours > 0 || data.BeverHours > 0))
                    {
                        throw ImportException.EmptyHours();
                    }

                    var results = new List<TimeRegistration>();

                    if (data.MuskHours > 0)
                    {
                        var tr = TimeRegistration.Create(
                            user.Id,
                            subAreaHourSquare.Id,
                            TrappingType.MuskusratId,
                            date.Value,
                            data.MuskHours.Value / 10,
                            TimeRegistrationStatus.Written,
                            false);

                        tr.PopulateCreatedUpdated(tr.UserId, tr.Date);

                        results.Add(tr);
                    }

                    if (data.BeverHours > 0)
                    {
                        var tr = TimeRegistration.Create(
                            user.Id,
                            subAreaHourSquare.Id,
                            TrappingType.BeverratId,
                            date.Value,
                            data.BeverHours.Value / 10,
                            TimeRegistrationStatus.Written,
                            false);

                        tr.PopulateCreatedUpdated(tr.UserId, tr.Date);

                        results.Add(tr);
                    }

                    return results;
                },
                cancellationToken);
        }
    }
}
