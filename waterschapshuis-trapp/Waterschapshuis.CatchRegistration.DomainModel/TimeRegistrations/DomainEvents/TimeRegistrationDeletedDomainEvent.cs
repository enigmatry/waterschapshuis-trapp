using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.DomainEvents
{
    public class TimeRegistrationDeletedDomainEvent : AuditableDomainEvent
    {
        public TimeRegistrationDeletedDomainEvent(TimeRegistration timeRegistration) : base("TimeRegistrationDeleted")
        {
            TimeRegistrationId = timeRegistration.Id;
            UserId = timeRegistration.UserId;
            Hours = timeRegistration.Hours;
            Date = timeRegistration.Date;
            Status = timeRegistration.Status;
            SubAreaHourSquareId = timeRegistration.SubAreaHourSquareId;
            TrappingTypeId = timeRegistration.TrappingTypeId;
            IsCreatedFromTrackings = timeRegistration.IsCreatedFromTrackings;
        }

        public override object AuditPayload =>
            new
            {
                TimeRegistrationId,
                UserId,
                Hours,
                Date,
                Status,
                IsCreatedFromTrackings,
                SubAreaHourSquareId,
                TrappingTypeId
            };

        public double Hours { get; }
        public DateTimeOffset Date { get; }
        public TimeRegistrationStatus Status { get; }
        public bool IsCreatedFromTrackings { get; }
        public Guid SubAreaHourSquareId { get; }
        public Guid TimeRegistrationId { get; }
        public Guid TrappingTypeId { get; }
        public Guid UserId { get; }
    }
}
