using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public static class SubAreaHourSquareQueryableExtensions
    {
        public static IQueryable<SubAreaHourSquare> QueryByLongAndLat(this IQueryable<SubAreaHourSquare> query, double longitude, double latitude) =>
            query.Where(e => e.Geometry.Contains(GeometryUtil.Factory.CreatePoint(longitude, latitude)));

        public static SubAreaHourSquare? FindBySubAreaAndHourSquare(this IQueryable<SubAreaHourSquare> query, Guid subAreaId, Guid hourSquareId) =>
            query.SingleOrDefault(e => e.SubAreaId == subAreaId && e.HourSquareId == hourSquareId);

        public static bool ExistAtLocation(this IQueryable<SubAreaHourSquare> query, double longitude, double latitude) =>
            query.QueryByLongAndLat(longitude, latitude).Count() == 1;

        public static IQueryable<SubAreaHourSquare> QueryByVersionRegionalLayoutId(this IQueryable<SubAreaHourSquare> query, Guid id) =>
            query.Where(x => x.VersionRegionalLayoutId == id);

        public static IQueryable<SubAreaHourSquare> QueryByLocation(this IQueryable<SubAreaHourSquare> query, Point location) =>
            query.Where(x => x.Geometry.Contains(location));

        public static SubAreaHourSquare? FindByLongAndLat(this IQueryable<SubAreaHourSquare> query, double longitude, double latitude, ILogger logger)
        {
            var result = query.QueryByLongAndLat(longitude, latitude).ToList();

            if (result.Count > 1)
            {
                var overlappingSubAreaHourSquareIds = result.Select(i => i.Id);
                var location = new { Longitude = longitude, Latitude = latitude };
                logger.LogWarning("{@OverlappingSubAreaHourSquareIds} found for {@Location}", overlappingSubAreaHourSquareIds, location);
            }
            return result.FirstOrDefault();
        }

        public static IQueryable<SubAreaHourSquare> QueryByMultiplePoint(this IQueryable<SubAreaHourSquare> query, IEnumerable<Point> points)
        {
            Expression<Func<SubAreaHourSquare, bool>> predicate = PredicateBuilder.False<SubAreaHourSquare>();

            foreach (var point in points)
            {
                predicate = predicate.Or(e => e.Geometry.Contains(point));
            }

            return query.Where(predicate);
        }

        public static SubAreaHourSquare? QueryById(this IQueryable<SubAreaHourSquare> query, Guid sahsId) =>
            query.SingleOrDefault(e => e.Id == sahsId);
    }
}
