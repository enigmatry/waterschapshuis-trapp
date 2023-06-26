using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles.DomainEvents
{
    public class RoleUpdatedDomainEvent : AuditableDomainEvent
    {
        public RoleUpdatedDomainEvent(Guid id, string name, PermissionId[] permissions) : base("RoleUpdated")
        {
            Id = id;
            Name = name;
            Permissions = String.Join(", ", permissions.Select(x => x.GetDescription()));
        }

        public Guid Id { get; set; }
        public string Name { get; }
        public string Permissions { get; }

        public override object AuditPayload => new { Id, Name, Permissions };
    }
}
