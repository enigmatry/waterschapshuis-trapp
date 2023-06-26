using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.DomainEvents
{
    public class UserUpdatedDomainEvent : AuditableDomainEvent
    {
        public UserUpdatedDomainEvent(Guid id, string email, bool oldAuthorized, bool authorized, bool oldConfidentialityConfirmed, bool confidentialityConfirmed, Guid? organizationId) : base("UserUpdated")
        {
            Id = id;
            Email = email;
            OldAuthorized = oldAuthorized;
            Authorized = authorized;
            OrganizationId = organizationId;
            ConfidentialityConfirmed = confidentialityConfirmed;
            OldConfidentialityConfirmed = oldConfidentialityConfirmed;
        }

        public Guid Id { get; }
        public string Email { get; }
        public bool Authorized { get; }
        public bool OldAuthorized { get; }
        public string AuthorizationChangeDescription
        {
            get
            {
                return OldAuthorized == Authorized
                    ? String.Empty
                    : (Authorized ? "User has been authorized" : "User has been unauthorized");
            }
        }
        public bool ConfidentialityConfirmed { get; }
        public bool OldConfidentialityConfirmed { get; }
        public string ConfidentialityConfirmedDescription
        {

            get
            {
                return OldConfidentialityConfirmed == ConfidentialityConfirmed
                    ? String.Empty
                    : (ConfidentialityConfirmed ? "User confirmed confidentiality" : "User didn't confirmed confidentiality");
            }
        }

        public Guid? OrganizationId { get; }

        public override object AuditPayload => new { Id, Email, OldAuthorized, Authorized, AuthorizationChangeDescription, OrganizationId, ConfidentialityConfirmed, ConfidentialityConfirmedDescription };
    }
}
