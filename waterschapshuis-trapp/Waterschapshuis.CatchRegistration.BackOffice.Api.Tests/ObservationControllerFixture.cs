using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Waterschapshuis.CatchRegistration.ApplicationServices.Observations;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Observations;
using GetObservations = Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Observations.GetObservations;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests
{
    [Category("integration")]
    public class ObservationControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private Observation _observation = null!;

        [SetUp]
        public void SetUp()
        {
            var conf = Resolve<IConfiguration>();

            var azureBlobSettingsUrl = conf.GetValue(typeof(string), "App:AzureBlob:Url").ToString();
            var azureBlobSettingsContainer =
                conf.GetValue(typeof(string), "App:AzureBlob:BaseObservationBlobContainer").ToString();

            var azureBlobSettings = azureBlobSettingsUrl != null && azureBlobSettingsContainer != null
                ? new AzureBlobSettings
                {
                    Url = azureBlobSettingsUrl, BaseObservationBlobContainer = azureBlobSettingsContainer
                }
                : null;

            _observation = new ObservationBuilder()
                .WithRemarks("test remarks")
                .WithLocation(4.899431, 52.379189)
                .WithRecordedOn(DateTimeOffset.Now)
                .WithAzureBlobSettings(azureBlobSettings)
                .WithHasPhoto(true)
                .WithId(Guid.NewGuid());

            AddAndSaveChanges(_observation);
        }
        
        [Test]
        public async Task TestGetMultiple()
        {
            string url = QueryHelpers.AddQueryString("observations", "ids", _observation.Id.ToString());
            var response = await Client.GetAsync<IEnumerable<GetObservationDetails.ResponseItem>>(url);

            response = response.ToList();

            response.Should().NotBeNull();
            response.Should().NotBeEmpty();
            response.Count().Should().Be(1);
        }
        
        [Test]
        public async Task TestUpdateObservation()
        {
            var command = new ObservationUpdate.Command
            {
                Id = _observation.Id,
                Archived = true,
                Remarks = "update test",
                Type = ObservationType.Overig
            };

            GetObservationDetails.ResponseItem response =
                await Client.PutAsync<ObservationUpdate.Command, GetObservationDetails.ResponseItem>("observations", command);

            response.Should().NotBeNull();
            response.Archived.Should().Be(command.Archived);
            response.Remarks.Should().Be(command.Remarks);
            response.Type.Should().Be((int)command.Type);
        }
    }
}
