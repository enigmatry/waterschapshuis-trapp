using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs
{
    public class ScheduledJob: EntityHasCreated<Guid>
    {
        public static readonly int OutputMaxLength = 30000;

        public ScheduledJobName Name { get; private set; } 
        public ScheduledJobState State { get; private set; } = ScheduledJobState.Scheduled;
        public DateTimeOffset? StartedAt { get; private set; } = null;
        public DateTimeOffset? FinishedAt { get; private set; } = null;
        public string Params { get; private set; } = String.Empty;
        public string Output { get; private set; } = String.Empty;

        private static string ScheduledJobMessage => "Please note, it will take about 5 min to start job.";


        public static ScheduledJob Create(ScheduledJobName name)
        {
            var result = new ScheduledJob
            {
                Id = GenerateId(),
                Name = name,
                State = ScheduledJobState.Scheduled,
                Output = ScheduledJobMessage
            };

            result.AddDomainEvent(eventItem: new ScheduledJobCreatedDomainEvent(result));
            return result;
        }

        public static ScheduledJob Create(ScheduledJobCreate.Command command)
        {
            var result = Create(command.Name);
            return result;
        }

        public void Update(ScheduledJobState state, string output)
        {
            State = state;
            Output = output;
            if (state == ScheduledJobState.Started)
            {
                StartedAt = DateTimeOffset.Now;
            }
            else
            {
                FinishedAt = DateTimeOffset.Now;
            }

            AddDomainEvent(new ScheduledJobUpdatedDomainEvent(this));
        }

        public List<string> GetOutputMessages() => String.IsNullOrWhiteSpace(Output)
            ? new List<string>() : Output.Split(Environment.NewLine).ToList();
    }
}
