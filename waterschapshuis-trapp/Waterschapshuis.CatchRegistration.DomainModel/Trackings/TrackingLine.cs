using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings
{
    public class TrackingLine : Entity<Guid>
    {
        public TrackingLine() { }

        public DateTimeOffset Date { get; private set; }
        public LineString Polyline { get; private set; } = LineString.Empty;
        public Guid SessionId { get; private set; }

        public static TrackingLine Create(DateTimeOffset date, Guid sessionId, LineString lineString)
        {
            var result = new TrackingLine
            {
                Id = GenerateId(),
                Date = date,
                SessionId = sessionId,
                Polyline = lineString
            };

            result.AddDomainEvent(new TrackingLineCreatedDomainEvent(result.Id, result.Date));

            return result;
        }

        public TrackingLine Update(LineString lineString)
        {
            Polyline = lineString;
            AddDomainEvent(new TrackingLineUpdatedDomainEvent(this.Id, this.Date));
            return this;
        }
    }
}
