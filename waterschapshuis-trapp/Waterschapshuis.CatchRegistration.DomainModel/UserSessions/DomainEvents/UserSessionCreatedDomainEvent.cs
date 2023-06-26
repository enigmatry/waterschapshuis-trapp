using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.DomainEvents
{
    public class UserSessionCreatedDomainEvent : AuditableDomainEvent
    {
        public Guid UserId { get; }
        public Guid SessionId { get; }
        public string SessionOrigin { get; }

        public UserSessionCreatedDomainEvent(Guid userId, Guid sessionId, UserSessionOrigin sessionOrigin) : base("UserSessionCreated")
        {
            UserId = userId;
            SessionId = sessionId;
            SessionOrigin = sessionOrigin.GetDisplayName();
        }

        public override object AuditPayload => new { UserId, SessionId, SessionOrigin };
    }
}
