using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.Core
{
    public abstract class Entity
    {
        // needs to be private so that EF does not map the field
        private readonly List<INotification> _domainEvents = new List<INotification>();

        public IEnumerable<INotification> DomainEvents => _domainEvents;

        protected void AddDomainEvent(INotification? eventItem)
        {
            if (eventItem == null)
                return;
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void ClearDomainEvents<TEvent>() where  TEvent : INotification
        {
            var toRemove = _domainEvents.OfType<TEvent>().ToList();

            foreach(var item in toRemove)
                _domainEvents.Remove(item);
        }

        protected static Guid GenerateId()
        {
            return SequentialGuidGenerator.Generate();
        }
    }

    public abstract class Entity<TId> : Entity, IEntity
    {
        public TId Id { get; set; } = default!;
    }
}
