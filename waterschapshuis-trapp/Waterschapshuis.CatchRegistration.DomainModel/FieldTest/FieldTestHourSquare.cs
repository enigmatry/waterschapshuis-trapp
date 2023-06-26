using System;
using System.Collections.Generic;
using System.Text;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.DomainModel.FieldTest
{
    public class FieldTestHourSquare
    {
        public Guid FieldTestId { get; private set; }
        public Guid HourSquareId { get; private set; }

        public FieldTest FieldTest { get; private set; } = null!;
        public HourSquare HourSquare { get; private set; } = null!;

        public static FieldTestHourSquare Create(Guid fieldTestId, Guid hourSquareId) => new FieldTestHourSquare() { FieldTestId = fieldTestId, HourSquareId = hourSquareId };
    }
}
