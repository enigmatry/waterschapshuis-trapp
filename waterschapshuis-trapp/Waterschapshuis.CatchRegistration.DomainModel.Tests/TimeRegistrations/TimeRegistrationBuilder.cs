using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.TimeRegistrations
{
    public class TimeRegistrationBuilder
    {
        private Guid _id = Guid.Empty;
        private Guid _userId = Guid.Empty;
        private Guid _subAreaHourSquareId = Guid.Empty;
        private Guid _trappingTypeId = Guid.Empty;
        private DateTimeOffset _date;
        private double _hours;
        private bool _isCreatedFromTrackings;

        public static implicit operator TimeRegistration(TimeRegistrationBuilder builder)
        {
            return builder.Build();
        }

        private TimeRegistration Build()
        {
            var result = TimeRegistration.Create(
                _userId,
                _subAreaHourSquareId,
                _trappingTypeId,
                _date,
                _hours,
                TimeRegistrationStatus.Written,
                _isCreatedFromTrackings);

            if (_id != Guid.Empty) result.WithId(_id);

            return result;
        }

        public TimeRegistrationBuilder WithUserId(Guid value)
        {
            _userId = value;
            return this;
        }

        public TimeRegistrationBuilder WithSubAreaHourSquareId(Guid value)
        {
            _subAreaHourSquareId = value;
            return this;
        }

        public TimeRegistrationBuilder WithTrappingTypeId(Guid value)
        {
            _trappingTypeId = value;
            return this;
        }

        public TimeRegistrationBuilder WithDate(DateTimeOffset value)
        {
            _date = value;
            return this;
        }

        public TimeRegistrationBuilder WithHours(double value)
        {
            _hours = value;
            return this;
        }

        public TimeRegistrationBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TimeRegistrationBuilder WithIsCreatedFromTrackings(bool value)
        {
            _isCreatedFromTrackings = value;
            return this;
        }
    }
}
