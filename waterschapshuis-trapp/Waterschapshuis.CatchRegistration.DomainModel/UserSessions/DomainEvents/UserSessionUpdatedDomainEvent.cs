using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.DomainEvents
{
    public class UserSessionUpdatedDomainEvent : AuditableDomainEvent
    {
        public Guid UserId { get; }
        public Guid SessionId { get; }
        public string SessionOrigin { get; }

        public UserSessionUpdatedDomainEvent(Guid userId, Guid sessionId, UserSessionOrigin sessionOrigin) : base("UserSessionUpdated")
        {
            UserId = userId;
            SessionId = sessionId;
            SessionOrigin = sessionOrigin.GetDisplayName();
        }

        public override object AuditPayload => new { UserId, SessionId, SessionOrigin };
    }
}
