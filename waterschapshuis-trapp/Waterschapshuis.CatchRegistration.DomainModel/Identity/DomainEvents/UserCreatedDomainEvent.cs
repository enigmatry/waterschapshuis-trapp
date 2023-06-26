using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.DomainEvents
{
    public class UserCreatedDomainEvent : AuditableDomainEvent
    {
        public UserCreatedDomainEvent(Guid id, string email, bool authorized, Guid? organizationId, bool isAutoCreated) : base(
            "UserCreated")
        {
            Id = id;
            Email = email;
            Authorized = authorized;
            OrganizationId = organizationId;
            IsAutoCreated = isAutoCreated;
        }

        public Guid Id { get; }
        public string Email { get; }
        public bool Authorized { get; }
        public Guid? OrganizationId { get; }
        public bool IsAutoCreated { get; }

        public override object AuditPayload => new {Id, Email, Authorized, OrganizationId, IsAutoCreated};
    }
}
