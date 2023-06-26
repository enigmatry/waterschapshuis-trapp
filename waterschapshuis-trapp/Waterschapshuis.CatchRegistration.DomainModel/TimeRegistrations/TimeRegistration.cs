using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.DomainEvents;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations
{
    public class TimeRegistration : EntityHasCreatedUpdated<Guid>
    {
        public const int WeeksEditEnabled = 6;
        private TimeRegistration() { }

        public Guid UserId { get; private set; } = Guid.Empty;
        public User User { get; private set; } = null!;
        public Guid SubAreaHourSquareId { get; private set; } = Guid.Empty;
        public SubAreaHourSquare SubAreaHourSquare { get; private set; } = null!;
        public Guid TrappingTypeId { get; private set; } = Guid.Empty;
        public TrappingType TrappingType { get; private set; } = null!;
        public DateTimeOffset Date { get; set; }
        public double Hours { get; private set; }
        public YearWeekPeriod WeekPeriod { get; private set; } = YearWeekPeriod.Default();
        public TimeRegistrationStatus Status { get; private set; }
        public bool IsCreatedFromTrackings { get; private set; }

        public User CreatedBy { get; private set; } = null!;
        public User UpdatedBy { get; private set; } = null!;

        public static TimeRegistration Create(
            Guid userId,
            Guid subAreaHourSquareId,
            Guid trappingTypeId,
            DateTimeOffset date,
            double hours,
            TimeRegistrationStatus status,
            bool isCreatedFromTrackings)
        {
            var result = new TimeRegistration
            {
                Id = GenerateId(),
                UserId = userId,
                SubAreaHourSquareId = subAreaHourSquareId,
                TrappingTypeId = trappingTypeId,
                Date = date.Date,
                Hours = hours,
                Status = status,
                WeekPeriod = YearWeekPeriod.Create(date),
                IsCreatedFromTrackings = isCreatedFromTrackings
            };

            result.AddDomainEvent(new TimeRegistrationCreatedDomainEvent(result));

            return result;
        }

        public static TimeRegistration Create(
            TimeRegistration original,
            SubAreaHourSquare newSubAreaHourSquare) =>
            Create(original.CreatedById,
                   newSubAreaHourSquare.Id,
                   original.TrappingTypeId,
                   original.Date,
                   original.Hours,
                   original.Status,
                   original.IsCreatedFromTrackings)
                .WithSubAreHourSquare(newSubAreaHourSquare);

        public void Update(
            Guid subAreaHourSquareId,
            Guid trappingTypeId,
            double hours,
            TimeRegistrationStatus status)
        {
            SubAreaHourSquareId = subAreaHourSquareId;
            TrappingTypeId = trappingTypeId;
            Hours = hours;
            Status = status;
            IsCreatedFromTrackings = false;

            AddDomainEvent(new TimeRegistrationUpdatedDomainEvent(this));
        }

        public void AddDeletedEvent()
        {
            AddDomainEvent(new TimeRegistrationDeletedDomainEvent(this));
        }

        public void IncrementHours(double hours)
        {
            Hours += hours;
        }

        public int GetHours()
        {
            return (int)Math.Truncate(Hours);
        }

        public int GetMinutes()
        {
            var hoursIntegralPart = GetHours();
            var minutes = (int)Math.Round((Hours - hoursIntegralPart) * 60, 0);
            if (hoursIntegralPart == 0)
            {
                minutes = Math.Max(1, minutes);
            }
            return minutes;
        }

        public TimeRegistration UpdateStatus(TimeRegistrationStatus status)
        {
            Status = status;
            return this;
        }

        public TimeRegistration WithIsCreatedFromTracking(bool value)
        {
            IsCreatedFromTrackings = value;
            return this;
        }

        private TimeRegistration WithSubAreHourSquare(SubAreaHourSquare subAreaHourSquare)
        {
            SubAreaHourSquare = subAreaHourSquare;
            return this;
        }
    }
}
