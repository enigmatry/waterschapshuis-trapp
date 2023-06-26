using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts
{
    public class VersionRegionalLayout : Entity<Guid>
    {
        public static readonly int NameMaxLength = 30;
        public static readonly Guid V2IndelingId = new Guid("8110E841-F71A-43E5-AB12-8F5EB85676F5");

        private readonly List<SubAreaHourSquare> _subAreaHourSquares = new List<SubAreaHourSquare>();

        private VersionRegionalLayout() { }

        public string Name { get; private set; } = String.Empty;
        public DateTimeOffset StartDate { get; private set; }
        public IReadOnlyCollection<SubAreaHourSquare> SubAreaHourSquares => _subAreaHourSquares.AsReadOnly();


        public static VersionRegionalLayout Create(
            string name,
            DateTimeOffset startDate,
            List<SubAreaHourSquare> subAreaHourSquares)
        {
            var result = new VersionRegionalLayout
            {
                Id = GenerateId(),
                Name = name,
                StartDate = startDate
            }
            .WithSubAreaHourSquares(subAreaHourSquares);

            result.AddDomainEvent(new VersionRegionalLayoutCreateDomainEvent(result));

            return result;
        }

        public List<SubArea> GetSubAreas() => _subAreaHourSquares
            .Select(x => x.SubArea)
            .GroupBy(x => x.Id).Select(x => x.First()).ToList();

        public List<CatchArea> GetCatchAreas() => _subAreaHourSquares
            .Select(x => x.SubArea.CatchArea)
            .GroupBy(x => x.Id).Select(x => x.First()).ToList();

        public List<Rayon> GetRayons() => GetCatchAreas()
            .Select(x => x.Rayon)
            .GroupBy(x => x.Id).Select(x => x.First()).ToList();

        public List<WaterAuthority> GetWaterAuthorities() => _subAreaHourSquares
            .Select(x => x.SubArea.WaterAuthority)
            .GroupBy(x => x.Id).Select(x => x.First()).ToList();

        public string GetNameAndDate() => $"{Name}, {StartDate.DateTime.ToShortDateString()}";

        private VersionRegionalLayout WithSubAreaHourSquares(List<SubAreaHourSquare> subAreaHourSquares)
        {
            _subAreaHourSquares.Clear();
            _subAreaHourSquares.AddRange(subAreaHourSquares);
            return this;
        }
    }
}
