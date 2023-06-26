using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Observations;
using Waterschapshuis.CatchRegistration.External.Api.Features.Observations;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests.Observations
{
    [Category("integration")]
    public class ObservationControllerFixture : ExternalApiIntegrationFixtureBase
    {
        private Observation _observation = null!;
        private double _latitude;
        private double _longitude;

        [SetUp]
        public void SetUp()
        {
            _latitude = 4.899431;
            _longitude = 52.379189;

            _observation = new ObservationBuilder()
                .WithRemarks("test remarks")
                .WithLocation(_longitude, _latitude)
                .WithRecordedOn(DateTimeOffset.Now)
                .WithHasPhoto(true)
                .WithId(Guid.NewGuid());

            AddAndSaveChanges(_observation);
        }

        [Test]
        public async Task TestGetObservations()
        {
            var response = await Client.GetAsync<PagedResponse<GetObservation.ObservationItem>>("observations");

            response.Should().NotBeNull();
            response.Items.Should().NotBeEmpty();
            response.Items.Count().Should().Be(1);
        }

        [Test]
        public async Task TestUpdateObservation()
        {
            var command = new UpdateStatusAndRemarks.Command
            {
                Id = _observation.Id,
                Archived = true, 
                Remarks = "update test"
            };

            GetObservation.ObservationItem response =
                await Client.PutAsync<UpdateStatusAndRemarks.Command, GetObservation.ObservationItem>("observations", command);

            response.Should().NotBeNull();
            response.Archived.Should().Be(command.Archived);
            response.Remarks.Should().Be(command.Remarks);
        }
    }
}
