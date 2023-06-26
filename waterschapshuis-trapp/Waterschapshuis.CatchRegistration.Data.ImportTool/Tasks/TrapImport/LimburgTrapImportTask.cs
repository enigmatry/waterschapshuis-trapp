using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class LimburgTrapImportTask : LimburgJsonImportTask
    {
        public LimburgTrapImportTask(
            ILogger<LimburgTrapImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected override async Task ImportObjectsWorkload(string jsonFilePath,
            string[] jsonPath,
            CancellationToken cancellationToken)
        {
            await ImportSingleEntityResultSets<LimburgTrapProperties, Point, Trap>(
                jsonFilePath,
                jsonPath,
                Scope.GetService<IRepository<Trap>>(),
                MapTrap);
        }

        private (Feature<LimburgTrapProperties, Point> model, Trap entity, bool existing)
            MapTrap(Feature<LimburgTrapProperties, Point> item)
        {
            var date = item.Properties.DateCreated.AsDateTimeOffset();

            if (!date.HasValue)
            {
                throw ImportException.InvalidDate();
            }

            if (!item.Properties.TrapType.HasValue)
            {
                throw ImportException.InvalidTrapType();
            }

            TrapStatus trapStatus;

            if (!item.Properties.Status.HasValue)
            {
                throw ImportException.InvalidTrapStatus();
            }

            try
            {
                trapStatus = (TrapStatus)item.Properties.Status.Value;
            }
            catch (InvalidCastException)
            {
                throw ImportException.InvalidTrapStatus();
            }

            SubAreaHourSquare subAreaHourSquare =
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
                        item.Properties.GlobalId.AsOrganizationPrefixed(OrganizationNames.Limburg)
                    ));

            if (Scope.GetService<IRepository<Trap>>()
                .QueryAll()
                .Any(x => x.ExternalId == trap.ExternalId))
            {
                throw ImportException.ExistsTrap();
            }

            trap.PopulateCreatedUpdatedRecorded(FindSystemUser(item.Properties.User).Id, date.Value);

            return (item, trap, false);
        }
    }
}
