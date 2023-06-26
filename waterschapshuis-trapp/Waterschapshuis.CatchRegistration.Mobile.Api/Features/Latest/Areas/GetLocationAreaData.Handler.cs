using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Areas
{
    public static partial class GetLocationAreaData
    {
        [UsedImplicitly]
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly CatchRegistrationDbContext _dbContext;

            public Handler([NotNull] CatchRegistrationDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = new Response();

                var dataSummaries = await _dbContext.Set<LocationAreaDataSummary>()
                    .FromSqlRaw("EXECUTE GetLocationAreaData {0}, {1}", request.CatchAreaId, request.SubAreaId)
                    .ToListAsync(cancellationToken);

                dataSummaries.ForEach(data => MapResponse(data, response));

                return response;
            }

            private static void MapResponse(LocationAreaDataSummary data, Response response)
            {
                switch (data.SummaryType)
                {
                    case LocationAreaDataSummary.Type.CatchAreaCatchingTraps:
                        response.CatchArea.CatchingTraps.Add(
                            new Response.Area.TrapSummary(data.Value, data.ValueType));
                        break;
                    case LocationAreaDataSummary.Type.SubAreaCatchingTraps:
                        response.SubArea.CatchingTraps.Add(
                            new Response.Area.TrapSummary(data.Value, data.ValueType));
                        break;
                    case LocationAreaDataSummary.Type.CatchAreaLastWeekCatches:
                        response.CatchArea.LastWeekCatches.Add(
                            new Response.Area.TrapSummary(data.Value, data.ValueType));
                        break;
                    case LocationAreaDataSummary.Type.SubAreaLastWeekCatches:
                        response.SubArea.LastWeekCatches.Add(
                            new Response.Area.TrapSummary(data.Value, data.ValueType));
                        break;
                    case LocationAreaDataSummary.Type.CatchAreaLastWeekByCatches:
                        response.CatchArea.LastWeekByCatches.Add(
                            new Response.Area.TrapSummary(data.Value, data.ValueType));
                        break;
                    case LocationAreaDataSummary.Type.SubAreaLastWeekByCatches:
                        response.SubArea.LastWeekByCatches.Add(
                            new Response.Area.TrapSummary(data.Value, data.ValueType));
                        break;
                    case LocationAreaDataSummary.Type.CatchAreaLastWeekHours:
                        response.CatchArea.LastWeekTimeTotal = new Response.Area.TimeSummary(data.Value);
                        break;
                    case LocationAreaDataSummary.Type.SubAreaLastWeekHours:
                        response.SubArea.LastWeekTimeTotal = new Response.Area.TimeSummary(data.Value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
