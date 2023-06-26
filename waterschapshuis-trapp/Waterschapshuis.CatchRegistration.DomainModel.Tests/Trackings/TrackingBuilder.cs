using System;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Trackings
{
    public class TrackingBuilder
    {
        private double _longitude;
        private double _latitude;
        private DateTimeOffset _recordedOn;
        private Guid _trappingTypeId = Guid.Empty;
        private Guid _userId = Guid.Empty;
        private Guid _sessionId = Guid.Empty;
        private readonly DateTimeOffset _createdOn = DateTimeOffset.Now;
        private bool _isTimewriting = true;
        private bool _isTrackingMap = true;
        private bool _isTrackingPrivate = false;

        public static implicit operator Tracking(TrackingBuilder builder)
        {
            return builder.Build();
        }

        private Tracking Build()
        {
            var tracking = Tracking.Create(
                new TrackingSync.Command.TrackingItem
                {
                    Id = Guid.NewGuid().ToString(),
                    Longitude = _longitude,
                    Latitude = _latitude,
                    RecordedOn = _recordedOn,
                    TrappingTypeId = _trappingTypeId,
                    SessionId = _sessionId,
                    IsTimewriting = _isTimewriting,
                    IsTrackingMap = _isTrackingMap,
                    IsTrackingPrivate = _isTrackingPrivate
                });

            tracking.SetCreated(_createdOn, _userId);
            tracking.SetUpdated(_createdOn, _userId);

            return tracking;
        }

        public TrackingBuilder WithLocation(double longitude, double latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
            return this;
        }

        public TrackingBuilder WithRecordedOn(DateTimeOffset value)
        {
            _recordedOn = value;
            return this;
        }

        public TrackingBuilder WithTrappingTypeId(Guid value)
        {
            _trappingTypeId = value;
            return this;
        }

        public TrackingBuilder WithUserId(Guid value)
        {
            _userId = value;
            return this;
        }

        public TrackingBuilder WithSessionId(Guid value)
        {
            _sessionId = value;
            return this;
        }

        public TrackingBuilder WithTimewriting(bool value)
        {
            _isTimewriting = value;
            return this;
        }

        public TrackingBuilder WithTrackingMap(bool value)
        {
            _isTrackingMap = value;
            return this;
        }

        public TrackingBuilder WithTrackingPrivacy(bool value)
        {
            _isTrackingPrivate = value;
            return this;
        }
    }
}
