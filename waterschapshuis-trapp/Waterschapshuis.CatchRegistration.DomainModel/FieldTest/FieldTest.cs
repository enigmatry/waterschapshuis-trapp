using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Collections;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.FieldTest
{
    public class FieldTest : Entity<Guid>, IEntityHasName<Guid>
    {
        public static readonly int NameMaxLength = 250;

        private readonly List<FieldTestHourSquare> _fieldTestHourSquares = new List<FieldTestHourSquare>();

        public string Name { get; private set; } = String.Empty;

        public YearPeriod StartPeriod { get; private set; } = YearPeriod.Default();
        public YearPeriod EndPeriod { get; private set; } = YearPeriod.Default();

        public IReadOnlyCollection<FieldTestHourSquare> FieldTestHourSquares => _fieldTestHourSquares.AsReadOnly();

        public static FieldTest Create(string name, string startPeriod, string endPeriod)
        {
            var result = new FieldTest()
            {
                Id = GenerateId(),
                Name = name,
                StartPeriod = YearPeriod.Parse(startPeriod),
                EndPeriod = YearPeriod.Parse(endPeriod)
            };

            result.AddDomainEvent(eventItem: new FieldTestCreatedDomainEvent(result.Id, result.Name, result.StartPeriod.YearPeriodValue, result.EndPeriod.YearPeriodValue));

            return result;
        }

        public static FieldTest Create(FieldTestCreateOrUpdate.Command command)
        {

            var result =
                Create(command.Name, command.StartPeriod, command.EndPeriod)
                    .WithHourSquares(command.HourSquareIds);

            return result;
        }

        public void Update(FieldTestCreateOrUpdate.Command command)
        {
            Name = command.Name;
            StartPeriod = YearPeriod.Parse(command.StartPeriod);
            EndPeriod = YearPeriod.Parse(command.EndPeriod);
            WithHourSquares(command.HourSquareIds);

            AddDomainEvent(new FieldTestUpdatedDomainEvent(Id, Name, StartPeriod.YearPeriodValue, EndPeriod.YearPeriodValue));
        }

        public FieldTest AddFieldTestHourSquare(FieldTestHourSquare fieldTestHourSquare)
        {
            _fieldTestHourSquares.Add(fieldTestHourSquare);
            return this;
        }

        private FieldTest WithHourSquares(Guid[] hourSquareIds)
        {
            _fieldTestHourSquares.UpdateWith(hourSquareIds,
                (hourSquareId, fieldTestHourSquare) => hourSquareId == fieldTestHourSquare.HourSquareId,
                (hourSquareId) => FieldTestHourSquare.Create(Id, hourSquareId),
                (hourSquareId, fieldTestHourSquare) => { });

            return this;
        }
    }
}
