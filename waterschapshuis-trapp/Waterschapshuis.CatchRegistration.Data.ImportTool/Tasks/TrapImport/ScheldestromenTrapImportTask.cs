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
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TrapImport
{
    [UsedImplicitly]
    public sealed class ScheldestromenTrapImportTask : ScheldestromenJsonImportTask
    {
        public ScheldestromenTrapImportTask(
            ILogger<ScheldestromenTrapImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ImportObjectsWorkload(string jsonFilePath, string[] jsonPath, CancellationToken cancellationToken)
        {
            await ImportSingleEntityResultSets<ScheldestromenTrapProperties, Point, Trap>(
                jsonFilePath,
                jsonPath,
                Scope.GetService<IRepository<Trap>>(),
                MapTrap);
        }

        private (Feature<ScheldestromenTrapProperties, Point> model, Trap entity, bool existing) MapTrap(Feature<ScheldestromenTrapProperties, Point> item)
        {
            var date = ConvertUnixDate(item.Properties.DateCreated);

            if (!item.Properties.TrapType.HasValue)
            {
                throw ImportException.InvalidTrapType();
            }

            if (!item.Properties.Status.HasValue)
            {
                throw ImportException.InvalidTrapStatus();
            }

            TrapStatus trapStatus;

            try
            {
                trapStatus = (TrapStatus)item.Properties.Status.Value;
            }
            catch (InvalidCastException)
            {
                throw ImportException.InvalidTrapStatus();
            }

            var subAreaHourSquare =
                item.Geometry.GetSubAreaHourSquareForPointLocation(Scope.GetService<IRepository<SubAreaHourSquare>>());
            
            Province province = Scope.GetService<IRepository<Province>>().QueryAll()
                .QueryContainesCoordinates(item.Geometry.X, item.Geometry.Y)
                .FirstOrDefault() ?? throw ImportException.NotFoundProvince();

            var trap = 
                Trap.Create(
                    new DomainModel.Traps.Commands.TrapImport(
                        item.Properties.Number ?? 0,
                        trapStatus,
                        item.Geometry.X,
                        item.Geometry.Y,
                        TrapTypeMapper.GetTrapTypeGuid(item.Properties.TrapType.Value),
                        item.Properties.Remarks,
                        subAreaHourSquare,
                        province,
                        item.Properties.Id.ToString().AsOrganizationPrefixed(OrganizationNames.Scheldestromen)
                    ));

            if (Scope.GetService<IRepository<Trap>>()
                .QueryAll()
                .Any(x => x.ExternalId == trap.ExternalId))
            {
                throw ImportException.ExistsTrap();
            }

            trap.PopulateCreatedUpdatedRecorded(FindSystemUser(item.Properties.User).Id, date);

            return (item, trap, false);
        }
    }
}
