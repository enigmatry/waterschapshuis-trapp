using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class SubAreaHourSquare : Entity<Guid>
    {
        private readonly List<Trap> _traps = new List<Trap>();
        private readonly List<Observation> _observations = new List<Observation>();
        private readonly List<TimeRegistration> _timeRegistrations = new List<TimeRegistration>();
        private readonly List<TrapSubAreaHourSquare> _previousVersionTraps = new List<TrapSubAreaHourSquare>();

        private SubAreaHourSquare() { }

        public Guid SubAreaId { get; private set; } = Guid.Empty;
        public SubArea SubArea { get; private set; } = null!;
        public Guid HourSquareId { get; private set; } = Guid.Empty;
        public HourSquare HourSquare { get; private set; } = null!;
        public int KmWaterway { get; private set; }
        public short PercentageDitch { get; private set; }
        public double Ditch { get; private set; }
        public double WetDitch { get; private set; }
        public Geometry Geometry { get; private set; } = Polygon.Empty;
        public Guid? VersionRegionalLayoutId { get; private set; }
        public VersionRegionalLayout VersionRegionalLayout { get; private set; } = null!;
        public IReadOnlyCollection<Trap> Traps => _traps.AsReadOnly();
        public IReadOnlyCollection<Observation> Observations => _observations.AsReadOnly();
        public IReadOnlyCollection<TimeRegistration> TimeRegistrations => _timeRegistrations.AsReadOnly();
        public IReadOnlyCollection<TrapSubAreaHourSquare> PreviousVersionTraps => _previousVersionTraps.AsReadOnly();

        public static SubAreaHourSquare Create(
            Guid subAreaId,
            Guid hourSquareId,
            int kmWaterway,
            short percentageDitch,
            double ditch,
            double wetDitch,
            Geometry geometry,
            Guid? versionRegionalLayoutId = null) =>
            new SubAreaHourSquare()
            {
                Id = GenerateId(),
                SubAreaId = subAreaId,
                HourSquareId = hourSquareId,
                KmWaterway = kmWaterway,
                PercentageDitch = percentageDitch,
                Ditch = ditch,
                WetDitch = wetDitch,
                Geometry = geometry,
                VersionRegionalLayoutId = versionRegionalLayoutId
            };

        public static SubAreaHourSquare Create(
            SubArea subArea,
            HourSquare hourSquare,
            int kmWaterway,
            short percentageDitch,
            double ditch,
            double wetDitch,
            Geometry geometry,
            Guid? versionRegionalLayoutId = null) =>
            Create(subArea.Id, hourSquare.Id, kmWaterway, percentageDitch, ditch, wetDitch, geometry, versionRegionalLayoutId)
                .WithSubArea(subArea)
                .WithHourSquare(hourSquare);

        public static SubAreaHourSquare Create(
            SubArea subArea,
            Guid hourSquareId,
            Geometry geometry,
            Guid? versionRegionalLayoutId = null) =>
            Create(subArea.Id, hourSquareId, 0, 0, 0, 0, geometry, versionRegionalLayoutId)
                .WithSubArea(subArea);


        public void Update(double ditchNew, double wetDitchNew)
        {
            Ditch = ditchNew;
            WetDitch = wetDitchNew;
            KmWaterway = (ditchNew >= 0 && wetDitchNew >= 0) ?
                (int)Math.Round((wetDitchNew + (PercentageDitch * ditchNew / 100)) / 1000)
                : -1;
        }

        public SubAreaHourSquare WithSubArea(SubArea subArea)
        {
            SubArea = subArea;
            return this;
        }

        private SubAreaHourSquare WithHourSquare(HourSquare hourSquare)
        {
            HourSquare = hourSquare;
            return this;
        }

        public SubAreaHourSquare AddTrap(Trap trap)
        {
            _traps.Add(trap);
            return this;
        }

        public string GetSubAreaHourSquareName() => $"{SubArea.Name}/{HourSquare.Name}";

        public int GetPolyLabelLocationMatchValue(params Point[] locations)
        {
            var containedInCount = 0;

            if (Geometry.IsValidPolygonOrMultiPolygon())
            {
                var polygons = Geometry.TryExtractPolygons();

                foreach (var location in locations)
                {
                    var matchingPolygon = polygons
                        .SingleOrDefault(polygon => polygon.Contains(location));

                    if (matchingPolygon != null)
                    {
                        ++containedInCount;
                        polygons.Remove(matchingPolygon);
                    }
                }
            }

            return containedInCount;
        }
    }
}
