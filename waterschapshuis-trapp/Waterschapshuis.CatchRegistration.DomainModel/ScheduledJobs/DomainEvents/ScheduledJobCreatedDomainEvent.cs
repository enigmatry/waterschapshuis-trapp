using System;
using System.Collections.Generic;
using System.Text;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.DomainEvents
{
    public class ScheduledJobCreatedDomainEvent : AuditableDomainEvent
    {
        public ScheduledJobCreatedDomainEvent(ScheduledJob scheduledJob) : base("ScheduledJobCreated")
        {
            ScheduledJobId = scheduledJob.Id;
            Name = scheduledJob.Name;
            State = scheduledJob.State;
            Output = scheduledJob.Output;
        }

        public override object AuditPayload =>
            new
            {
                ScheduledJobId,
                Name,
                State,
                Output
            };

        public Guid ScheduledJobId { get; }
        public ScheduledJobName Name { get; }
        public ScheduledJobState State { get; }
        public string Output { get; }
    }
}
