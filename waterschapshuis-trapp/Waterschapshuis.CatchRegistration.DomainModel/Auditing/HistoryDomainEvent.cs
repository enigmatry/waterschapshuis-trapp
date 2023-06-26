using MediatR;
using System;
using System.Collections.Generic;

namespace Waterschapshuis.CatchRegistration.DomainModel.Auditing
{
    public abstract class HistoryDomainEvent : INotification
    {
        public Guid TrackedEntityId { get; }
        public DateTimeOffset RecordedOn { get; }
        public IList<string> Messages { get; } = new List<string>();

        protected HistoryDomainEvent(Guid trackedEntityId, IList<string> messages)
        {
            TrackedEntityId = trackedEntityId;
            RecordedOn = DateTimeOffset.Now;
            Messages = messages;
        }
    }
}
