using System;
using GeoAPI.CoordinateSystems;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjNet.CoordinateSystems;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class ScheldestromenJsonImportTask : GeoJsonImportTask
    {
        private static readonly ICoordinateSystem FromCoordinateSystem = ProjectedCoordinateSystem.WebMercator;
        private static readonly ICoordinateSystem ToCoordinateSystem = SridReader.GetCoordinateSystemById(SridAmersfoort);

        protected ScheldestromenJsonImportTask(
            ILogger logger, 
            IConfiguration configuration, 
            IServiceScopeFactory serviceScopeFactory) 
            : base(logger, configuration, serviceScopeFactory)
        {
        }

        protected static double[] TransformWebMercatorToAmersfoortCoordinates(double[] coordinates)
        {
            return TransformCoordinates(coordinates, FromCoordinateSystem, ToCoordinateSystem);
        }

        protected DateTimeOffset ConvertUnixDate(string date)
        {
            // Date is represented as UnixTimeMilliseconds.
            if (!Int64.TryParse(date, out var date64))
            {
                throw ImportException.InvalidDate();
            }

            return date64.AsDateTimeOffset();
        }
    }
}
