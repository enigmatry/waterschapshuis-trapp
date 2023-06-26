using NetTopologySuite.Geometries;
using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Common
{
    public interface IHasLocation
    {
        Point Location { get; }

        void InsertNewVersionOfSubAreaHourSquareId(Guid id);
    }
}
