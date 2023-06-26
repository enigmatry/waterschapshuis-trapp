using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.DomainEvents
{
    public class TimeRegistrationGeneralUpdatedDomainEvent : AuditableDomainEvent
    {
        public TimeRegistrationGeneralUpdatedDomainEvent(Guid id,
            Guid userId,
            double hours,
            TimeRegistrationStatus status,
            Guid timeRegistrationCategoryId) : base("TimeRegistrationGeneralUpdated")
        {
            Id = id;
            UserId = userId;
            Hours = hours;
            Status = status;
            TimeRegistrationCategoryId = timeRegistrationCategoryId;
        }

        public override object AuditPayload =>
            new
            {
                Id,
                UserId,
                Hours,
                Status,
                TimeRegistrationCategoryId
            };

        public Guid Id { get; }
        public Guid UserId { get; }
        public double Hours { get; }
        public TimeRegistrationStatus Status { get; }
        public Guid TimeRegistrationCategoryId { get; }
    
    }
}
