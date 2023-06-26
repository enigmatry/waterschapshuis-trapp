using FluentAssertions;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Topology;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreas
{
    [Category("unit")]
    public class SubAreaQueryableExtensionsFixture
    {
        VersionRegionalLayout _currentVersionRegionalLayout = null!;
        VersionRegionalLayout _previousVersionRegionalLayout = null!;
        IQueryable<SubArea> _subAreas = null!;

        [SetUp]
        public void Setup()
        {
            _previousVersionRegionalLayout = new VersionRegionalLayoutBuilder()
                .WithName("PreviousVRL")
                .WithStartDate(DateTimeOffset.Now.AddDays(-1));
            _currentVersionRegionalLayout = new VersionRegionalLayoutBuilder()
                .WithName("CurrentVRL")
                .WithStartDate(DateTimeOffset.Now);

            _subAreas = CreateSubAreas(_currentVersionRegionalLayout, _previousVersionRegionalLayout);
        }

        [Test]
        public void QueryByVersionRegionalLayoutId()
        {
            var result = _subAreas
                .QueryByVersionRegionalLayoutId(_currentVersionRegionalLayout.Id)
                .ToList();

            result.Should().NotBeEmpty();
            result.Count.Should().Be(3);
        }

        private IQueryable<SubArea> CreateSubAreas(VersionRegionalLayout currentVrl, VersionRegionalLayout previousVrl)
        {
            var sahsOfCurrentVrl = new SubAreaHourSquareBuilder().WithVersionRegionalLayoutId(currentVrl.Id);
            var sahsOfPreviousVrl = new SubAreaHourSquareBuilder().WithVersionRegionalLayoutId(previousVrl.Id);

            SubArea subArea1 = new SubAreaBuilder().WithName("SubArea1");
            SubArea subArea2 = new SubAreaBuilder().WithName("SubArea2");
            SubArea subArea3 = new SubAreaBuilder().WithName("SubArea3");
            SubArea subArea4 = new SubAreaBuilder().WithName("SubArea4");

            subArea1.AddSubAreaHourSquare(sahsOfCurrentVrl);

            subArea2.AddSubAreaHourSquare(sahsOfCurrentVrl);
            subArea2.AddSubAreaHourSquare(sahsOfPreviousVrl);

            subArea3.AddSubAreaHourSquare(sahsOfPreviousVrl);

            subArea4.AddSubAreaHourSquare(sahsOfPreviousVrl);
            subArea4.AddSubAreaHourSquare(sahsOfCurrentVrl);

            return new List<SubArea> { subArea1, subArea2, subArea3, subArea4 }.AsQueryable();
        }
    }
}
