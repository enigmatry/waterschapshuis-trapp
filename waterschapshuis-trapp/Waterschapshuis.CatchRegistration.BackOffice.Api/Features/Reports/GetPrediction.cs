using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    public static partial class GetPrediction
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public Guid HourSquareId { get; set; }

            public int SummerCatches { get; set; }
            public int SpringCatches { get; set; }
            public int AutumnCatches { get; set; }
            public int WinterCatches { get; set; }

            public int SummerHours { get; set; }
            public int SpringHours { get; set; }
            public int AutumnHours { get; set; }
            public int WinterHours { get; set; }
        }

        [PublicAPI]
        public class Response
        {
            public double ModelQuality { get; set; }
            public Item? Prediction { get; set; }
            public IEnumerable<Item> Items { get; set; } = new List<Item>();

            public class Item
            {
                public int? SummerCatches { get; set; }
                public int? SpringCatches { get; set; }
                public int? AutumnCatches { get; set; }
                public int? WinterCatches { get; set; }

                public int? SummerHours { get; set; }
                public int? SpringHours { get; set; }
                public int? AutumnHours { get; set; }
                public int? WinterHours { get; set; }

                public int Year { get; set; }

                public static Item Create(IQueryable<Catch> catchesOfYear, IQueryable<TimeRegistration> timeRegistrationsOfYear, int year, int numberOfYears)
                {
                    var seasonsIncluded = SeasonPeriod.SeasonsIncluded(year, numberOfYears);

                    return new Item
                    {
                        WinterCatches = seasonsIncluded.Contains( (int)Season.Winter) ? catchesOfYear.QueryBySeason(Season.Winter).Sum(x => x.Number) : (int?)null,
                        SpringCatches = seasonsIncluded.Contains((int)Season.Spring) ? catchesOfYear.QueryBySeason(Season.Spring).Sum(x => x.Number) : (int?)null,
                        SummerCatches = seasonsIncluded.Contains((int)Season.Summer) ? catchesOfYear.QueryBySeason(Season.Summer).Sum(x => x.Number) : (int?)null,
                        AutumnCatches = seasonsIncluded.Contains((int)Season.Autumn) ? catchesOfYear.QueryBySeason(Season.Autumn).Sum(x => x.Number) : (int?)null,
                        WinterHours = seasonsIncluded.Contains((int)Season.Winter) ? timeRegistrationsOfYear.QueryBySeason(Season.Winter).Sum(tr => Convert.ToInt32(tr.Hours)) : (int?)null,
                        SpringHours = seasonsIncluded.Contains((int)Season.Spring) ? timeRegistrationsOfYear.QueryBySeason(Season.Spring).Sum(tr => Convert.ToInt32(tr.Hours)) : (int?)null,
                        SummerHours = seasonsIncluded.Contains((int)Season.Summer) ? timeRegistrationsOfYear.QueryBySeason(Season.Summer).Sum(tr => Convert.ToInt32(tr.Hours)) : (int?)null,
                        AutumnHours = seasonsIncluded.Contains((int)Season.Autumn) ? timeRegistrationsOfYear.QueryBySeason(Season.Autumn).Sum(tr => Convert.ToInt32(tr.Hours)) : (int?)null,
                        Year = year
                    };
                }


                public static Item Create(PredictionModel predictionModel, Query request) =>
                    new Item
                    {
                        WinterCatches = predictionModel.CalculateCatches(Season.Winter, request.WinterHours),
                        SpringCatches = predictionModel.CalculateCatches(Season.Spring, request.SpringHours),
                        SummerCatches = predictionModel.CalculateCatches(Season.Summer, request.SummerHours),
                        AutumnCatches = predictionModel.CalculateCatches(Season.Autumn, request.AutumnHours),
                        WinterHours = predictionModel.CalculateHours(Season.Winter, request.WinterCatches),
                        SpringHours = predictionModel.CalculateHours(Season.Spring, request.SpringCatches),
                        SummerHours = predictionModel.CalculateHours(Season.Summer, request.SummerCatches),
                        AutumnHours = predictionModel.CalculateHours(Season.Autumn, request.AutumnCatches)
                    };
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.HourSquareId).Must(x => x.NotEmpty()).WithMessage("HourSquare Id not provided");
                RuleFor(x => x.WinterCatches).GreaterThanOrEqualTo(0);
                RuleFor(x => x.SpringCatches).GreaterThanOrEqualTo(0);
                RuleFor(x => x.SummerCatches).GreaterThanOrEqualTo(0);
                RuleFor(x => x.AutumnCatches).GreaterThanOrEqualTo(0);
                RuleFor(x => x.WinterHours).GreaterThanOrEqualTo(0);
                RuleFor(x => x.SpringHours).GreaterThanOrEqualTo(0);
                RuleFor(x => x.SummerHours).GreaterThanOrEqualTo(0);
                RuleFor(x => x.AutumnHours).GreaterThanOrEqualTo(0);
            }
        }

        private static IQueryable<Catch> BuildInclude(this IQueryable<Catch> query) =>
            query
                .Include(x => x.Trap)
                    .ThenInclude(x => x.SubAreaHourSquare)
                        .ThenInclude(x => x.HourSquare);

        private static IQueryable<TimeRegistration> BuildInclude(this IQueryable<TimeRegistration> query) =>
            query
                .Include(x => x.SubAreaHourSquare)
                    .ThenInclude(x => x.HourSquare);
    }
}
