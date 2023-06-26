using System;
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
using Waterschapshuis.CatchRegistration.Data.ImportTool.Mapping;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport
{
    [UsedImplicitly]
    public sealed class ScheldestromenCatchImportTask : ScheldestromenJsonImportTask
    {
        public ScheldestromenCatchImportTask(
            ILogger<ScheldestromenCatchImportTask> logger, 
            IConfiguration configuration, 
            IServiceScopeFactory serviceScopeFactory) 
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ImportObjectsWorkload(string jsonFilePath, string[] jsonPath, CancellationToken cancellationToken)
        {
            await ImportSingleEntityResultSets<ScheldestromenCatchProperties, Point, Catch>(
                jsonFilePath,
                jsonPath,
                Scope.GetService<IRepository<Catch>>(),
                MapCatch);
        }

        private (Feature<ScheldestromenCatchProperties, Point> model, Catch entity, bool existing) 
            MapCatch(Feature<ScheldestromenCatchProperties, Point> item)
        {
            var date = ConvertUnixDate(item.Properties.DateCreated);

            if (!date.PassesOrganizationDateConstrain(OrganizationNames.Scheldestromen))
            {
                throw ImportException.InvalidDate();
            }

            if (!item.Properties.CatchType.HasValue)
            {
                throw ImportException.InvalidCatchType();
            }

            // find all the Traps at the Catch location
            var traps = 
                Scope.GetService<IRepository<Trap>>()
                    .QueryAll()
                    .Where(x => 
                        item.Geometry.X.Equals(x.Location.X) && 
                        item.Geometry.Y.Equals(x.Location.Y));

            if (!traps.Any())
            {
                throw ImportException.NotFoundTrap();
            }

            Guid? trapId;

            if (item.Properties.TrapType.HasValue)
            {
                // if TrapType is specified for the catch then try to find that Trap of that Type at the exact point Location
                var trapTypeId = TrapTypeMapper.GetTrapTypeGuid(item.Properties.TrapType.Value);
                trapId = traps.FirstOrDefault(x => x.TrapTypeId == trapTypeId)?.Id;
            }
            else
            {
                // if no TrapType is specified then try to find the Conibear trap or if there isn't one use Trap that was first created
                trapId = traps.FirstOrDefault(x =>
                             x.Id == TrapTypeMapper.GetTrapTypeGuid(TrapTypeMapper.ConibearTrapTypeId))?.Id ??
                         traps.OrderBy(x => x.CreatedOn).FirstOrDefault()?.Id;

                if (trapId.HasValue)
                {
                    var trapObjectId = TrapTypeMapper.GetTrapTypeId(trapId.Value);
                    Logger.LogWarning($"Catch Id: { item.Properties.Id } has TrapType value null. Using { trapObjectId } instead.");
                }
            }

            var importCatch = new DomainModel.Catches.Commands.CatchImport(
                item.Properties.Number ?? 0,
                CatchStatus.Written,
                trapId ?? throw new ImportException("Cannot determine the Trap for which to create the catch."),
                CatchTypeMapper.GetCatchTypeGuid(item.Properties.CatchType.Value));

            var @catch = Catch.Create(importCatch);

            @catch.PopulateCreatedUpdatedRecorded(FindSystemUser(item.Properties.User).Id, date);

            return (item, @catch, false);
        }
    }
}
