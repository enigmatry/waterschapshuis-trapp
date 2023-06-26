using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Topology
{
    public class VersionRegionalLayoutBuilder
    {
        private string _name = String.Empty;
        private DateTimeOffset _startDate = DateTimeOffset.Now;
        private List<SubAreaHourSquare> _subAreaHourSquares = new List<SubAreaHourSquare>();

        public VersionRegionalLayoutBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public VersionRegionalLayoutBuilder WithStartDate(DateTimeOffset value)
        {
            _startDate = value;
            return this;
        }

        public VersionRegionalLayoutBuilder WithSubAreaHourSquares(params SubAreaHourSquare[] value)
        {
            _subAreaHourSquares = value.ToList();
            return this;
        }

        public static implicit operator VersionRegionalLayout(VersionRegionalLayoutBuilder builder) => builder.Build();

        private VersionRegionalLayout Build()
        {
            var result = VersionRegionalLayout.Create(
                _name,
                _startDate,
                _subAreaHourSquares
            );
            return result;
        }
    }
}
