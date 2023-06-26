using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class PredictionModel : ValueObject
    {
        private const double MinimumCredibleCpe = 0.01;

        [UsedImplicitly]
        private PredictionModel()
        {
        }

        public PredictionModel(
            bool populationPresent,
            double cpeRecent,
            double cpeAb,
            double? r2)
        {
            PopulationPresent = populationPresent;
            CpeRecent = cpeRecent;
            CpeAB = cpeAb;
            R2 = r2;
        }

        public double CpeAB { get; }
        public double CpeRecent { get; }
        public bool PopulationPresent { get; }
        public double? R2 { get; }

        protected override IEnumerable<object?> GetValues()
        {
            yield return PopulationPresent;
            yield return CpeRecent;
            yield return CpeAB;
            yield return R2;
        }

        public int CalculateHours(Season season, int catches) =>
            PopulationPresent
                ? (int)Math.Round(catches / Math.Max(MinimumCredibleCpe,
                    CalculateCatchesPerHour(season) + CpeAB + CpeRecent))
                : 0;

        public int CalculateCatches(Season season, int hours) =>
            PopulationPresent
                ? (int)Math.Round(Math.Max(MinimumCredibleCpe, 
                    CalculateCatchesPerHour(season) + CpeAB + CpeRecent) * hours)
                : 0;

        private double CalculateCatchesPerHour(Season season) => season switch
        {
            Season.Spring => 0.203490736794677,
            Season.Summer => 0.161298806595384,
            Season.Autumn => 0.0870381520905144,
            Season.Winter => 0.202715283315667,
            _ => throw new ArgumentOutOfRangeException(nameof(season))
        };
    }
}
