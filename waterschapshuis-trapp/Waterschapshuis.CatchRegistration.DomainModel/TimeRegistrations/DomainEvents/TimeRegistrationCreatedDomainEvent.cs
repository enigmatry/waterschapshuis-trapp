using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.DomainEvents
{
    public class TimeRegistrationCreatedDomainEvent : AuditableDomainEvent
    {
        public TimeRegistrationCreatedDomainEvent(TimeRegistration timeRegistration) : base("TimeRegistrationCreated")
        {
            Id = timeRegistration.Id;
            UserId = timeRegistration.UserId;
            Hours = timeRegistration.Hours;
            Date = timeRegistration.Date;
            Status = timeRegistration.Status;
            IsCreatedFromTrackings = timeRegistration.IsCreatedFromTrackings;
            SubAreaHourSquareId = timeRegistration.SubAreaHourSquareId;
            TrappingTypeId = timeRegistration.TrappingTypeId;
        }

        public override object AuditPayload =>
            new
            {
                Id,
                UserId,
                Hours,
                Date,
                Status,
                IsCreatedFromTrackings,
                SubAreaHourSquareId,
                TrappingTypeId
            };

        public Guid Id { get; }
        public Guid UserId { get; }
        public double Hours { get; }
        public DateTimeOffset Date { get; }
        public TimeRegistrationStatus Status { get; }
        public bool IsCreatedFromTrackings { get; }
        public Guid SubAreaHourSquareId { get; }
        public Guid TrappingTypeId { get; }
    }
}
