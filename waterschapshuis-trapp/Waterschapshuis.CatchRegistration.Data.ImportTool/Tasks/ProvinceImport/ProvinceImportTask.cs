using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Polygon = Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry.Polygon;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.ProvinceImport
{
    [UsedImplicitly]
    public sealed class ProvinceImportTask : GeoJsonImportTask
    {
        public ProvinceImportTask(
            ILogger<ProvinceImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        { }

        protected override async Task ImportObjectsWorkload(string jsonFilePath, string[] jsonPath, CancellationToken cancellationToken)
        {
            await ImportSingleEntityResultSets<ProvinceProperties, Polygon, Province>(
                jsonFilePath,
                jsonPath,
                Scope.GetService<IRepository<Province>>(),
                MapProvince);
        }

        private (Feature<ProvinceProperties, Polygon> model, Province entity, bool existing)
            MapProvince(Feature<ProvinceProperties, Polygon> item)
        {
            var coordinates = item.Geometry.GetNtsCoordinates();

            NetTopologySuite.Geometries.Geometry geometry;

            if (coordinates.Length == 1)
            {
                geometry = GeometryUtil.Factory.CreatePolygon(coordinates[0]);
            }
            else
            {
                geometry = GeometryUtil.Factory.CreateMultiPolygon(
                    item.Geometry.GetNtsCoordinates().Select(x =>
                            GeometryUtil.Factory.CreatePolygon(x.ToArray()))
                        .ToArray());
            }

            var province = Province.Create(item.Properties.Name, geometry.GetValidatedBuffer());

            return (item, province, false);
        }
    }
}
