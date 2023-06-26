using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts.DomainEvents
{
    public class VersionRegionalLayoutCreateDomainEvent : AuditableDomainEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTimeOffset StartDate { get; }

        public VersionRegionalLayoutCreateDomainEvent(VersionRegionalLayout request)
            : base("VersionRegionalLayoutCreated")
        {
            Id = request.Id;
            Name = request.Name;
            StartDate = request.StartDate;
        }

        public override object AuditPayload =>
            new
            {
                Id,
                Name,
                StartDate,
            };
    }
}
