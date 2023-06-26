using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations
{
    public class TimeRegistrationGeneral : EntityHasCreatedUpdated<Guid>
    {
        private TimeRegistrationGeneral() { }

        public Guid UserId { get; private set; } = Guid.Empty;
        public User User { get; private set; } = null!;
        public Guid TimeRegistrationCategoryId { get; private set; } = Guid.Empty;
        public TimeRegistrationCategory TimeRegistrationCategory { get; private set; } = null!;
        public DateTimeOffset Date { get; set; }
        public double Hours { get; private set; }
        public YearWeekPeriod WeekPeriod { get; private set; } = YearWeekPeriod.Default();
        public TimeRegistrationStatus Status { get; private set; }
        public User CreatedBy { get; private set; } = null!;
        public User UpdatedBy { get; private set; } = null!;

        public static TimeRegistrationGeneral Create(
            Guid userId,
            Guid timeRegistrationCategoryId,
            DateTimeOffset date,
            double hours,
            TimeRegistrationStatus status)
        {
            var result = new TimeRegistrationGeneral
            {
                Id = GenerateId(),
                UserId = userId,
                TimeRegistrationCategoryId = timeRegistrationCategoryId,
                Date = date.Date,
                Hours = hours,
                Status = status,
                WeekPeriod = YearWeekPeriod.Create(date)
            };

            result.AddDomainEvent(new TimeRegistrationGeneralCreatedDomainEvent(result.Id, result.UserId, result.Hours, result.Status,result.TimeRegistrationCategoryId));

            return result;
        }

        public void Update(
            Guid categoryId,
            double hours,
            TimeRegistrationStatus status)
        {
            TimeRegistrationCategoryId = categoryId;
            Hours = hours;
            Status = status;

            AddDomainEvent(new TimeRegistrationGeneralUpdatedDomainEvent(Id, UserId, Hours, Status, TimeRegistrationCategoryId));
        }

        private TimeRegistrationGeneral WithTimeRegistrationCategory(TimeRegistrationCategory category)
        {
            TimeRegistrationCategory = category;
            return this;
        }

        public int GetHours()
        {
            return (int)Math.Truncate(Hours);
        }

        public int GetMinutes()
        {
            return (int)Math.Round((Hours - GetHours()) * 60, 0);
        }
    }
}
