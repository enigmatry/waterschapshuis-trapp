using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationGeneralImport
{
    [UsedImplicitly]
    public class WestEnMiddenTimeRegistrationGeneralImportTask : JsonImportTask
    {
        public WestEnMiddenTimeRegistrationGeneralImportTask(ILogger<WestEnMiddenTimeRegistrationImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory) : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ExecuteJsonImportsAsync(CancellationToken cancellationToken)
        {
            await ExecuteJsonImportAsync<WestEnMiddenTimeRegistrationGeneralProperties, TimeRegistrationGeneral>(
                new Regex("^features\\[[\\d]+]\\.properties"),
                (data, token) =>
                {
                    var date = data.Date.AsDateTimeOffset();

                    if (!date.HasValue || !date.Value.PassesOrganizationDateConstrain(OrganizationNames.WestEnMidden))
                    {
                        throw ImportException.InvalidDate();
                    }

                    User user = FindSystemUser(data.User);

                    var results = new List<TimeRegistrationGeneral>();
                    foreach (var result in data.GetTimeRegistrationCategory())
                    {
                        var tr = TimeRegistrationGeneral.Create(
                            user.Id,
                            result.Item1,
                            date.Value,
                            result.Item2 / 10,
                            TimeRegistrationStatus.Written);

                        tr.PopulateCreatedUpdated(tr.UserId, tr.Date);
                        results.Add(tr);
                    }

                    return Task.FromResult<IEnumerable<TimeRegistrationGeneral>>(results);
                }, cancellationToken);
        }
    }
}
