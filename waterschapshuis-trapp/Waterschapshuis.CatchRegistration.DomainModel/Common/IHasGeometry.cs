using NetTopologySuite.Geometries;

namespace Waterschapshuis.CatchRegistration.DomainModel.Common
{
    public interface IHasGeometry
    {
        Geometry Geometry { get; }

        void UpdateGeometry(Geometry value);
    }
}
