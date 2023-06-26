using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.DomainEvents
{
    public class ScheduledJobUpdatedDomainEvent : AuditableDomainEvent
    {
        public ScheduledJobUpdatedDomainEvent(ScheduledJob scheduledJob) : base("ScheduledJobUpdated")
        {
            ScheduledJobId = scheduledJob.Id;
            Name = scheduledJob.Name;
            State = scheduledJob.State;
            StartedAt = scheduledJob.StartedAt;
            FinishedAt = scheduledJob.FinishedAt;
            Output = scheduledJob.Output;
        }

        public override object AuditPayload =>
            new
            {
                ScheduledJobId,
                Name,
                State,
                StartedAt,
                FinishedAt,
                Output
            };

        public Guid ScheduledJobId { get; }
        public ScheduledJobName Name { get; }
        public ScheduledJobState State { get; }
        public DateTimeOffset? StartedAt { get; } 
        public DateTimeOffset? FinishedAt { get; }
        public string Output { get; }
    }
}
