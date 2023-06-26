using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares
{
    public class HourSquareBuilder : EntityWithGeometryBuilderBase
    {
        private string _name = String.Empty;
        private PredictionModel? _predictionModel;

        public static implicit operator HourSquare(HourSquareBuilder builder)
        {
            return builder.Build();
        }

        private HourSquare Build()
        {
            var result = HourSquare.Create(_name, _geometry);

            if (_predictionModel is {})
            {
                result = result.WithPredictionModel(_predictionModel);
            }

            return result;
        }

        public HourSquareBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public HourSquareBuilder WithGeometry(Geometry value)
        {
            _geometry = value;
            return this;
        }

        public HourSquare WithPredictionModel(PredictionModel model)
        {
            _predictionModel = model;
            return this;
        }
    }
}
