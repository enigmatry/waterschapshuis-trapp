using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Reports
{
    [Category("integration")]
    public class ReportsControllerGetPredictionFixture
        : BackOfficeApiIntegrationFixtureBase
    {
        private Guid _hourSquareId;
        private const string Path = "/reports/prediction";

        [SetUp]
        public void Setups()
        {
            HourSquare hourSquare = new HourSquareBuilder()
                .WithName("test")
                .WithPredictionModel(
                    new PredictionModel(true, 0.0683069655793815, 0.0847339949526522, 0.837202093541335));

            AddAndSaveChanges(hourSquare);
            _hourSquareId = hourSquare.Id;
        }

        [Test]
        public async Task GetHourSquarePredictions_ValidInput_ReturnsExpectedValues()
        {
            var request = new GetPrediction.Query
            {
                HourSquareId = _hourSquareId,
                WinterCatches = 50,
                SpringCatches = 75,
                SummerCatches = 100,
                AutumnCatches = 75,
                WinterHours = 50,
                SpringHours = 100,
                SummerHours = 100,
                AutumnHours = 150
            };
            var expectedResponse = new GetPrediction.Response()
            {
                Items = new List<GetPrediction.Response.Item>(){
                    new GetPrediction.Response.Item(){
                WinterCatches = 18,
                SpringCatches = 36,
                SummerCatches = 31,
                AutumnCatches = 36,
                WinterHours = 141,
                SpringHours = 210,
                SummerHours = 318,
                AutumnHours = 312
                }
                }
            };
            var actualResponse = await Client.GetAsync<GetPrediction.Response>(
                Path + BuildQueryString(request));

            AssertResponse(expectedResponse, actualResponse);
        }

        private string BuildQueryString(GetPrediction.Query request) =>
            $"?{nameof(GetPrediction.Query.HourSquareId)}={request.HourSquareId}" +
            $"&{nameof(GetPrediction.Query.WinterCatches)}={request.WinterCatches}" +
            $"&{nameof(GetPrediction.Query.SpringCatches)}={request.SpringCatches}" +
            $"&{nameof(GetPrediction.Query.SummerCatches)}={request.SummerCatches}" +
            $"&{nameof(GetPrediction.Query.AutumnCatches)}={request.AutumnCatches}" +
            $"&{nameof(GetPrediction.Query.WinterHours)}={request.WinterHours}" +
            $"&{nameof(GetPrediction.Query.SpringHours)}={request.SpringHours}" +
            $"&{nameof(GetPrediction.Query.SummerHours)}={request.SummerHours}" +
            $"&{nameof(GetPrediction.Query.AutumnHours)}={request.AutumnHours}";

        private void AssertResponse(GetPrediction.Response expected,
            GetPrediction.Response actual)
        {
            Assert.AreEqual(expected.Items.First().WinterCatches, actual.Prediction?.WinterCatches);
            Assert.AreEqual(expected.Items.First().SpringCatches, actual.Prediction?.SpringCatches);
            Assert.AreEqual(expected.Items.First().AutumnCatches, actual.Prediction?.AutumnCatches);
            Assert.AreEqual(expected.Items.First().SummerCatches, actual.Prediction?.SummerCatches);
            Assert.AreEqual(expected.Items.First().WinterHours, actual.Prediction?.WinterHours);
            Assert.AreEqual(expected.Items.First().SpringHours, actual.Prediction?.SpringHours);
            Assert.AreEqual(expected.Items.First().SummerHours, actual.Prediction?.SummerHours);
            Assert.AreEqual(expected.Items.First().AutumnHours, actual.Prediction?.AutumnHours);
        }
    }
}
