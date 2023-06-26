using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations
{
    public class TimeRegistrationCategory : Entity<Guid>, IEntityHasName<Guid>
    {
        private TimeRegistrationCategory()
        {

        }

        public string Name { get; private set; } = String.Empty;
        public bool Active { get; private set; }

        public static TimeRegistrationCategory Create(string name, bool active)
        {
            return new TimeRegistrationCategory
            {
                Id = GenerateId(),
                Name = name,
                Active = active
            };
        }

        public static TimeRegistrationCategory Create(TimeRegistrationCategoryCreateOrUpdate.Command command)
        {
            var result = new TimeRegistrationCategory
            {
                Id = GenerateId(),
                Name = command.Name,
                Active = command.Active
            };

            result.AddDomainEvent(new TimeRegistrationCategoryCreatedDomainEvent(result.Id, result.Name, result.Active));

            return result;
        }

        public void Update(TimeRegistrationCategoryCreateOrUpdate.Command command)
        {
            Name = command.Name;
            Active = command.Active;

            AddDomainEvent(new TimeRegistrationCategoryUpdatedDomainEvent(Id, Name, Active));
        }
    }
}
