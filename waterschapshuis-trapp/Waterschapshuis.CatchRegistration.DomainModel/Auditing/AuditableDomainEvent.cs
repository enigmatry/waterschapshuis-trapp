using MediatR;

namespace Waterschapshuis.CatchRegistration.DomainModel.Auditing
{
    public abstract class AuditableDomainEvent : INotification
    {
        protected AuditableDomainEvent(string eventName)
        {
            EventName = eventName;
            AuditEnabled = true;
        }

        public bool AuditEnabled { get; private set; }
        public string EventName { get; }

        public abstract object AuditPayload { get; }

        protected void DoNotAudit()
        {
            AuditEnabled = false;
        }
    }
}
