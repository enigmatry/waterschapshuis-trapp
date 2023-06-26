using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Trackings
{
    public class TrackingLineBuilder
    {
        private Guid _sessionId;
        private List<Tracking> _trackings = new List<Tracking>();

        public static implicit operator TrackingLine(TrackingLineBuilder builder) => builder.Build();

        private TrackingLine Build() => TrackingLine
            .Create(
                _trackings.First().RecordedOn, 
                _sessionId,
                GeometryUtil.Factory.CreateLine(_trackings.Select(tracking => tracking.Location.Coordinate).ToArray())
            ) 
            ?? new TrackingLine();

        public TrackingLineBuilder WithTrackings(List<Tracking> value)
        {
            _trackings = value;
            return this;
        }

        public TrackingLineBuilder WithSessionId(Guid value)
        {
            _sessionId = value;
            return this;
        }
    }
}
