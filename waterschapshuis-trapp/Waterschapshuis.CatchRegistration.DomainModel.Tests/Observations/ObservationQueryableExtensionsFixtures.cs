using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.BoundingAreas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Observations
{
    [Category("unit")]
    public class ObservationQueryableExtensionsFixtures
    {
        private const string InBBox = "Observation inside bounding box";
        private const string OutOfBBox = "Observation out of bounding box";
        private IQueryable<Observation> _testObservationsQuery = null!;
        private BoundingBox _boundingBox = null!;

        [SetUp]
        public void Setup()
        { ;
            _testObservationsQuery = CreateTestObservationsList();
            _boundingBox = CreateBoundingBox();
        }


        [Test]
        public void TestValidQuery_ByBoundingBox()
        {
            var items = _testObservationsQuery.QueryByOptionalBoundingBox(_boundingBox);
            items.Should().HaveCount(1);
            items.First().Remarks.Should().Be(InBBox);
        }


        private static IQueryable<Observation> CreateTestObservationsList()
        {
            var observationInRange = new ObservationBuilder()
                .WithLocation(60000, 60000)
                .WithRemarks(OutOfBBox);
            ;
            var observationOutOfRange =  new ObservationBuilder()
                .WithLocation(1000, 1000)
                .WithRemarks(InBBox);

            return new List<Observation> { observationInRange, observationOutOfRange }.AsQueryable();
        }

        private BoundingBox CreateBoundingBox()
        {
            var bb = new BoundingBoxBuilder()
                .WithLocation(0,0)
                .WithWidthKilometers(50);
            return  bb;
        }
    }
}
