using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ProjNet.CoordinateSystems.Transformations;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class GeoJsonImportTask : ImportTask
    {
        private static readonly string[] FeaturesJsonPath = {"features"};

        protected GeoJsonImportTask(
            ILogger logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        public override async Task ExecuteImportAsync(CancellationToken cancellationToken)
        {
            await ExecuteImport(ImportObjectsWorkload, cancellationToken);
        }

        protected static double[] TransformCoordinates(double[] coordinates,
            ICoordinateSystem fromCoordinateSystem,
            ICoordinateSystem toCoordinateSystem)
        {
            var factory = new CoordinateTransformationFactory();

            ICoordinateTransformation transformation =
                factory.CreateFromCoordinateSystems(fromCoordinateSystem, toCoordinateSystem);
            return transformation.MathTransform.Transform(coordinates);
        }

        protected abstract Task ImportObjectsWorkload(string jsonFilePath,
            string[] jsonPath,
            CancellationToken cancellationToken);

        protected async
            Task<Dictionary<(Feature<TProperties, TGeometry> model, TEntity entity), TEntity>>
            ImportSingleEntityListResultSets<TProperties, TGeometry, TEntity>(
                string jsonFilePath,
                string[] jsonPath,
                IRepository<TEntity> repository,
                Func<Feature<TProperties, TGeometry>, IEnumerable<(Feature<TProperties, TGeometry> model, TEntity entity
                    , bool existing)>> entityMap,
                Func<double[], double[]> transformCoordsFunc = null)
            where TProperties : IProperties
            where TGeometry : IGeometry
            where TEntity : Entity
        {
            var dictionary = new Dictionary<(Feature<TProperties, TGeometry> model, TEntity entity), TEntity>();
            var imported = new HashSet<TEntity>();
            IEnumerable<JToken> results =
                await JsonObjectParser.ReadJTokenChildrenAtPath(jsonFilePath, jsonPath);

            foreach (JToken result in results)
            {
                Feature<TProperties, TGeometry> item = default;

                try
                {
                    item = result.ToObject<Feature<TProperties, TGeometry>>();
                    HandleFeatureCoordinates(item, transformCoordsFunc);

                    IEnumerable<(Feature<TProperties, TGeometry> model, TEntity entity, bool existing)> mapped =
                        entityMap(item);

                    foreach (var tuple in mapped)
                    {
                        imported.Add(tuple.entity);
                        dictionary.Add((tuple.model, tuple.entity), tuple.entity);
                    }
                }
                catch (ImportException ex)
                {
                    RegisterImportException(ex, result.ToString());
                }
            }

            AddEntities(imported.ToArray());
            return dictionary;
        }

        protected async
            Task<Dictionary<Feature<TProperties, TGeometry>, TEntity>> ImportSingleEntityResultSets<TProperties,
                TGeometry, TEntity>(
                string jsonFilePath,
                string[] jsonPath,
                IRepository<TEntity> repository,
                Func<Feature<TProperties, TGeometry>, (Feature<TProperties, TGeometry> model, TEntity entity, bool
                    existing)> entityMap,
                Func<double[], double[]> transformCoordsFunc = null)
            where TProperties : IProperties
            where TGeometry : IGeometry
            where TEntity : Entity
        {
            var dictionary = new Dictionary<Feature<TProperties, TGeometry>, TEntity>();
            var imported = new HashSet<TEntity>();

            IEnumerable<JToken> results =
                await JsonObjectParser.ReadJTokenChildrenAtPath(jsonFilePath, jsonPath);

            foreach (JToken result in results)
            {
                Feature<TProperties, TGeometry> item = default;

                try
                {
                    item = result.ToObject<Feature<TProperties, TGeometry>>();
                    HandleFeatureCoordinates(item, transformCoordsFunc);

                    (Feature<TProperties, TGeometry> model, TEntity entity, var existing) = entityMap(item);

                    dictionary.Add(model, entity);
                    imported.Add(entity);
                }
                catch (ImportException ex)
                {
                    RegisterImportException(ex, result.ToString());
                }
            }

            AddEntities(imported.ToArray());
            return dictionary;
        }

        private async Task ExecuteImport(Func<string, string[], CancellationToken, Task> importFunction,
            CancellationToken cancellationToken)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", Configuration.GetFileName());

            if (!File.Exists(jsonFilePath))
            {
                throw new InvalidOperationException(
                    $"JSON file '{Configuration.GetFileName()}' does not exist in the Resources subfolder of the application.");
            }

            await using var context = Scope.GetService<CatchRegistrationDbContext>();

            Logger.LogCatchRegistrationDbContextInfo(context);

            await importFunction(jsonFilePath, FeaturesJsonPath, cancellationToken);

            await FinalizeImportAsync(cancellationToken);
        }

        private void HandleFeatureCoordinates<TProperties, TGeometry>(
            Feature<TProperties, TGeometry> item,
            Func<double[], double[]> transformCoordsFunc)
            where TProperties : IProperties
            where TGeometry : IGeometry
        {
            void transformPolygonCoordinates(Polygon polygon)
            {
                IEnumerable<IEnumerable<double[]>> transformed =
                    polygon.Coordinates.Select(x =>
                        x.Select(
                            transformCoordsFunc));
                polygon.Coordinates =
                    transformed.Select(x =>
                            x.ToArray())
                        .ToArray();
            }

            if (transformCoordsFunc == null)
            {
                return;
            }

            switch (item.Geometry)
            {
                case Point point:
                    point.Coordinates = transformCoordsFunc(point.Coordinates);
                    break;
                case Polygon polygon:
                    transformPolygonCoordinates(polygon);
                    break;
                case MultiPolygon multiPolygon:
                    foreach (Polygon polygon in multiPolygon.Polygons)
                    {
                        transformPolygonCoordinates(polygon);
                    }
                    break;
            }
        }
    }
}
