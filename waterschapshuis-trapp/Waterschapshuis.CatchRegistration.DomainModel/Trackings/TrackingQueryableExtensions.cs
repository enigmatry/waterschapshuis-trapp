using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings
{
    public static class TrackingQueryableExtensions
    {
        public static IQueryable<Tracking> QueryUserTrackingsForTheDay(this IQueryable<Tracking> query, Guid userId, DateTimeOffset date) =>
            query
                .QueryCreatedBy(userId)
                .QueryByRecordedOnDay(date)
                .QueryRecordedOnBeforeIncludingDate(date);

        public static Tracking? PreviousTrackingForTheDayEntity(this IQueryable<Tracking> query, Guid trackingId, Guid userId, DateTimeOffset date) =>
            query
                .QueryUserTrackingsForTheDay(userId, date)
                .OrderByDescending(x => x.RecordedOn)
                .FirstOrDefault(x => x.Id != trackingId);

        public static IQueryable<Tracking> QueryCreatedBy(this IQueryable<Tracking> query, Guid userId) =>
            query.Where(x => x.CreatedById == userId);

        public static IQueryable<Tracking> QueryByRecordedOnDay(this IQueryable<Tracking> query, DateTimeOffset date) =>
            query.Where(x => x.RecordedOn.Date == date.Date);

        public static IQueryable<Tracking> QueryByCreatedOnDay(this IQueryable<Tracking> query, DateTimeOffset date) =>
            query.Where(x => x.CreatedOn.Date == date.Date);

        public static IQueryable<Tracking> QueryRecordedOnBeforeIncludingDate(this IQueryable<Tracking> query, DateTimeOffset date) =>
            query.Where(x => x.RecordedOn <= date);

        public static IQueryable<Tracking> QueryBySessionId(this IQueryable<Tracking> query, Guid sessionId) =>
            query.Where(x => x.SessionId == sessionId);

        public static IQueryable<Tracking> QueryByIsTrackingMap(this IQueryable<Tracking> query, bool isTrackingMap) =>
            query.Where(x => x.IsTrackingMap == isTrackingMap);

        public static IQueryable<Tracking> QueryByTimeWritting(this IQueryable<Tracking> query) =>
            query.Where(x => x.IsTimewriting);

        public static IQueryable<Tracking> ContainingIds(this IQueryable<Tracking> query, IEnumerable<Guid> ids) =>
            query.Where(t => ids.Contains(t.Id));

        public static IQueryable<Tracking> QueryByGeometry(this IQueryable<Tracking> query, Geometry geometry) =>
            query.Where(x => geometry.Contains(x.Location));
    }
}
