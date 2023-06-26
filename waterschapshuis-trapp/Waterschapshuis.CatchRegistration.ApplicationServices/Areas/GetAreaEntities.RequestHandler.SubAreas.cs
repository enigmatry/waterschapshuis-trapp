using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Areas
{
    public static partial class GetAreaEntities
    {
        [UsedImplicitly]
        public class SubAreasRequestHandler : IRequestHandler<SubAreaQuery, Response>
        {
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            public SubAreasRequestHandler(ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Response> Handle(SubAreaQuery request, CancellationToken cancellationToken)
            {
                var subAreaIds = _currentVersionRegionalLayoutService
                    .QuerySubAreasNoTracking(x => x.SubAreaHourSquares)
                    .Select(x => x.Id).ToList();

                return await RequestHandler.Handle(
                    _currentVersionRegionalLayoutService.QuerySubAreasNoTracking(), 
                    CreateCatchAreaQuery(request, subAreaIds));
            }

            private static Expression<Func<SubArea, bool>> CreateCatchAreaQuery(SubAreaQuery request, List<Guid> subAreaIds)
            {
                Expression<Func<SubArea, bool>> predicate = x =>
                    (!request.CatchAreaId.HasValue || x.CatchAreaId == request.CatchAreaId) &&
                    subAreaIds.Contains(x.Id) &&
                    (String.IsNullOrEmpty(request.NameFilter) || x.Name.Contains(request.NameFilter));
                return predicate;
            }
        }
    }
}
