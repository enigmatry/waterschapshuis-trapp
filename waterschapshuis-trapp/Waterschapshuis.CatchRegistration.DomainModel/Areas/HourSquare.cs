using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class HourSquare : Entity<Guid>, IEntityHasName<Guid>
    {
        public const int NameMaxLength = 5;

        private readonly List<SubAreaHourSquare> _subAreaHourSquares = new List<SubAreaHourSquare>();
        private readonly List<FieldTestHourSquare> _fieldTestHourSquare = new List<FieldTestHourSquare>();

        private HourSquare() { }

        public string Name { get; private set; } = String.Empty;
        public Geometry Geometry { get; private set; } = Polygon.Empty;

        public PredictionModel? PredictionModel { get; private set; }

        public IReadOnlyCollection<FieldTestHourSquare> FieldTestHourSquare => _fieldTestHourSquare.AsReadOnly();
        public IReadOnlyCollection<SubAreaHourSquare> SubAreaHourSquares => _subAreaHourSquares.AsReadOnly();

        public static HourSquare Create(string name, Geometry geometry)
        {
            return new HourSquare()
            {
                Id = GenerateId(),
                Name = name,
                Geometry = geometry
            };
        }

        public HourSquare WithPredictionModel(PredictionModel model)
        {
            PredictionModel = model;
            return this;
        }

        public HourSquare AddSubAreaHourSquare(SubAreaHourSquare subAreaHourSquare)
        {
            _subAreaHourSquares.Add(subAreaHourSquare);
            return this;
        }

        public HourSquare AddFieldTestHourSquare(FieldTestHourSquare fieldTestHourSquare)
        {
            _fieldTestHourSquare.Add(fieldTestHourSquare);
            return this;
        }
    }
}
