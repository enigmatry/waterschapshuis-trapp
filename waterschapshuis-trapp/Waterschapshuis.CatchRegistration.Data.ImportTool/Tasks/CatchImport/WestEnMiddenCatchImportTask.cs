using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport
{
    [UsedImplicitly]
    public sealed class WestEnMiddenCatchImportTask : WestEnMiddenJsonImportTask
    {
        public WestEnMiddenCatchImportTask(
            ILogger<WestEnMiddenCatchImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ImportObjectsWorkload(string jsonFilePath,
            string[] jsonPath,
            CancellationToken cancellationToken)
        {
            await ImportSingleEntityListResultSets<WestEnMiddenCatchProperties, Point, Catch>(
                jsonFilePath,
                jsonPath,
                Scope.GetService<IRepository<Catch>>(),
                MapCatches);
        }

        private IEnumerable<(Feature<WestEnMiddenCatchProperties, Point> model, Catch entity, bool existing)>
            MapCatches(Feature<WestEnMiddenCatchProperties, Point> item)
        {
            var date = !String.IsNullOrEmpty(item.Properties.DateControl)
                ? item.Properties.DateControl.AsDateTimeOffset()
                : item.Properties.DateCorrected.AsDateTimeOffset();

            if (!date.HasValue || !date.Value.PassesOrganizationDateConstrain(OrganizationNames.WestEnMidden))
            {
                throw ImportException.InvalidDate();
            }

            var catches = GetCatches(item.Properties).ToList();

            if (!catches.Any())
            {
                throw ImportException.EmptyCatches();
            }

            // find the Trap based on previously saved ExternalId for the Trap
            var trap = Scope.GetService<IRepository<Trap>>()
                .QueryAll()
                .SingleOrDefault(x => item.Properties.TrapObjectId.HasValue &&
                                      x.ExternalId == item.Properties.TrapObjectId.ToString()
                                          .AsOrganizationPrefixed(OrganizationNames.WestEnMidden));

            if (trap == null)
            {
                throw ImportException.NotFoundTrap();
            }

            foreach ((Guid catchTypeId, var number) in catches)
            {
                var importCatch = new DomainModel.Catches.Commands.CatchImport(number, CatchStatus.Written, trap.Id, catchTypeId);

                var @catch = Catch.Create(importCatch);

                @catch.PopulateCreatedUpdatedRecorded(FindSystemUser(item.Properties.User).Id, date.Value);

                yield return (item, @catch, false);
            }
        }

        private static IEnumerable<(Guid catchTypeId, int number)> GetCatches(WestEnMiddenCatchProperties properties)
        {
            return properties.GetCatches()
                    .Union(properties.GetByCatches());
        }
    }
}
