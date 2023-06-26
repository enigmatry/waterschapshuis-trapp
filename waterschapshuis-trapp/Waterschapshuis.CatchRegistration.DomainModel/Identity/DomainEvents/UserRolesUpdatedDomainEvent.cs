using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.DomainEvents
{
    public class UserRolesUpdatedDomainEvent : AuditableDomainEvent
    {
        public UserRolesUpdatedDomainEvent(Guid id, string email, IEnumerable<Guid> oldRoleIds, IEnumerable<Guid> newRoleIds): base("UserRolesUpdated")
        {
            Id = id;
            Email = email;
            OldRoleIds = String.Join(",", oldRoleIds);
            NewRoleIds = String.Join(",", newRoleIds);
        }

        public Guid Id { get; set; }
        public string Email { get; }
        public string OldRoleIds { get; }
        public string NewRoleIds { get; }

        public override object AuditPayload => new { Id, Email, OldRoleIds, NewRoleIds };
    }
}
