using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    [UsedImplicitly]
    public partial class GetPrediction
    {
        [UsedImplicitly]
        public class Handler : IRequestHandler<Query, Response>
        {
            public const int NumberOfPreviousYearsToShow = 3;

            private readonly IRepository<HourSquare> _hourSquareRepository;
            private readonly IRepository<Catch> _catchRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            private static List<int> HistoricalYears
            {
                get
                {
                    var yearsList = new List<int>();
                    for (int i = 0; i <= NumberOfPreviousYearsToShow; i++)
                    {
                        yearsList.Add(DateTimeOffset.Now.Year - i);
                    }
                    return yearsList;
                }
            }

            public Handler(
                IRepository<HourSquare> hourSquareRepository,
                IRepository<Catch> catchRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _hourSquareRepository = hourSquareRepository ?? throw new ArgumentNullException(nameof(hourSquareRepository));
                _catchRepository = catchRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var hourSquare = await _hourSquareRepository.FindByIdAsync(request.HourSquareId);
                var catches = await GetHourSquareCatches(request.HourSquareId);
                var timeRegistrations = await GetHourSquareHours(request.HourSquareId, cancellationToken);

                var items = new List<Response.Item>();
                HistoricalYears
                    .ForEach(year =>
                    {
                        items.Add(Response.Item.Create(
                            catches.AsQueryable().QueryByYear(year),
                            timeRegistrations.AsQueryable().QueryByYear(year),
                            year,
                            NumberOfPreviousYearsToShow
                        ));
                    });


                var prediction = hourSquare?.PredictionModel is null
                    ? null
                    : Response.Item.Create(hourSquare.PredictionModel, request);

                return new Response
                {
                    ModelQuality = hourSquare?.PredictionModel?.R2 ?? 0,
                    Prediction = prediction,
                    Items = items
                };
            }

            private async Task<List<Catch>> GetHourSquareCatches(Guid hourSquareId) =>
                await _catchRepository.QueryAll()
                    .BuildInclude()
                    .QueryByHourSquare(hourSquareId)
                    .QueryBetweenYears(HistoricalYears.Min()-1, HistoricalYears.Max())
                    .ToListAsync();

            private async Task<List<TimeRegistration>> GetHourSquareHours(Guid hourSquareId, CancellationToken cancellationToken) =>
                await _currentVersionRegionalLayoutService
                    .QueryTimeRegistrations()
                    .BuildInclude()
                    .QueryByHourSquareId(hourSquareId)
                    .QueryBetweenYears(HistoricalYears.Min()-1, HistoricalYears.Max())
                    .ToListAsync(cancellationToken);
        }
    }
}
