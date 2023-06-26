using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings
{
    public static class TrackingLineQueryableExtensions
    {
        public static IQueryable<TrackingLine> QueryByDate(this IQueryable<TrackingLine> query, DateTimeOffset requestDate) =>
            query.Where(x => x.Date.Date == requestDate.Date);

        public static TrackingLine TryFindBySessionId(this IQueryable<TrackingLine> query, Guid sessionId) =>
            query.SingleOrDefault(x => x.SessionId == sessionId);
    }
}
