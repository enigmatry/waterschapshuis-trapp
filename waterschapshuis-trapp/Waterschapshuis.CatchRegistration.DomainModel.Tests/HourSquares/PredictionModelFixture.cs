using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares
{
    [Category("unit")]
    public class PredictionModelFixture
    {
        private readonly PredictionModel _model = new PredictionModel(
            true, 0.0683069655793815, 0.0847339949526522, 0.837202093541335);

        [TestCase(Season.Winter, 50, 141)]
        [TestCase(Season.Spring, 75, 210)]
        [TestCase(Season.Summer, 100, 318)]
        [TestCase(Season.Autumn, 75, 312)]
        public void CalculateHours_ForGivenSeasonAndNumberOfCatches_ReturnsExpectedNumberOfHours(
            Season season, int catches, int expectedHours)
        {
            
            _model.CalculateHours(season, catches).Should().Be(expectedHours);
        }

        [TestCase(Season.Winter, 50, 18)]
        [TestCase(Season.Spring, 100, 36)]
        [TestCase(Season.Summer, 100, 31)]
        [TestCase(Season.Autumn, 150, 36)]
        public void CalculateCatches_ForGivenSeasonAndNumberOfHours_ReturnsExpectedNumberOfCatches(
            Season season, int hours, int expectedCatches)
        {
            _model.CalculateCatches(season, hours).Should().Be(expectedCatches);
        }
    }
}
