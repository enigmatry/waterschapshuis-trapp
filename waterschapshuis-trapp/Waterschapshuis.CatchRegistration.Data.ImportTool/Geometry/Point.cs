using System.Linq;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Geometry
{
    [UsedImplicitly]
    public class Point : IGeometry
    {
        private const double Zero = 0.0;
        private bool Condition => Coordinates.Length >= 2;

        public double[] Coordinates { get; set; } = { Zero, Zero };

        public double X => Condition ? Coordinates[0] : Zero;

        public double Y => Condition ? Coordinates[1] : Zero;

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public SubAreaHourSquare GetSubAreaHourSquareForPointLocation(IRepository<SubAreaHourSquare> repository)
        {
            return repository.QueryAll()
                       .QueryByLongAndLat(X, Y)
                       .SingleOrDefault()
                   ?? throw new ImportException("Cannot find SubAreaHourSquare at Point coordinates.");
        }
    }
}
