using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class SubArea : Entity<Guid>, IEntityHasName<Guid>
    {
        public const int NameMaxLength = 10;

        private readonly List<SubAreaHourSquare> _subAreaHourSquares = new List<SubAreaHourSquare>();

        private SubArea() { }

        public string Name { get; private set; } = String.Empty;
        public Geometry Geometry { get; private set; } = Polygon.Empty;
        public Guid CatchAreaId { get; private set; } = Guid.Empty;
        public CatchArea CatchArea { get; private set; } = null!;
        public Guid WaterAuthorityId { get; private set; } = Guid.Empty;
        public WaterAuthority WaterAuthority { get; private set; } = null!;
        public IReadOnlyCollection<SubAreaHourSquare> SubAreaHourSquares => _subAreaHourSquares.AsReadOnly();

        public static SubArea Create(string name, Guid catchAreaId, Guid waterAuthorityId, Geometry geometry) =>
            new SubArea
            {
                Id = GenerateId(),
                Name = name,
                CatchAreaId = catchAreaId,
                WaterAuthorityId = waterAuthorityId,
                Geometry = geometry
            };

        public static SubArea Create(string name, CatchArea catchArea, WaterAuthority waterAuthority, Geometry geometry) =>
            Create(name, catchArea.Id, waterAuthority.Id, geometry)
                .WithCatchArea(catchArea)
                .WithWaterAuthority(waterAuthority);

        private SubArea WithCatchArea(CatchArea catchArea)
        {
            CatchArea = catchArea;
            return this;
        }

        private SubArea WithWaterAuthority(WaterAuthority waterAuthority)
        {
            WaterAuthority = waterAuthority;
            return this;
        }

        public SubArea AddSubAreaHourSquare(SubAreaHourSquare subAreaHourSquare)
        {
            _subAreaHourSquares.Add(subAreaHourSquare);
            return this;
        }
    }
}
