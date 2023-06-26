using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Styles
{
    public partial class GetMapStylesLookups
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<TrapType> _trapTypeRepository;

            public RequestHandler(IRepository<TrapType> trapTypeRepository)
            {
                _trapTypeRepository = trapTypeRepository;
            }

            public Task<Response> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var response = new Response();
                var styles = GetMapStyles();

                response.Items = styles;
                return Task.FromResult(response);
            }

            private static IEnumerable<MapStyleLookup> CreatePredefinedStylesLookups()
            {
                return new[]
                {
                    MapStyleLookup.Create(MapStyleLookupKey.CreateForObservationLocation(),
                        "notification.svg"),
                    MapStyleLookup.Create(MapStyleLookupKey.CreateForArchivedObservationLocation(),
                        "notification-archived.svg"),
                    MapStyleLookup.Create(MapStyleLookupKey.CreateForUserTracking(),
                        "tracking-user.svg"),
                    MapStyleLookup.Create(MapStyleLookupKey.CreateForTrappersTracking(),
                        "tracking-trappers.svg")
                };
            }

            private static IEnumerable<MapStyleLookup> Map(TrapType trapType)
            {
                return trapType.TrapTypeTrapStatusStyles.Select(ts =>
                    MapStyleLookup.Create(MapStyleLookupKey.CreateForTrapType(ts.TrapTypeId, ts.TrapStatus),
                        ts.IconName));
            }

            private IEnumerable<MapStyleLookup> GetMapStyles()
            {
                var allTrapTypes = _trapTypeRepository.QueryAll()
                    .Include(ms => ms.TrapTypeTrapStatusStyles)
                    .ToList();

                var genericStyles = CreatePredefinedStylesLookups();
                var nonGenericStyles = allTrapTypes.SelectMany(Map);
                return genericStyles.Concat(nonGenericStyles);
            }
        }
    }
}
