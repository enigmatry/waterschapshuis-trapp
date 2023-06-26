using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Areas
{
    public static partial class GetAreaEntities
    {
        [UsedImplicitly]
        public class CatchAreasRequestHandler : IRequestHandler<CatchAreaQuery, Response>
        {
            private readonly IRepository<User> _userRepository;
            private readonly ICurrentUserIdProvider _currentUserIdProvider;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            public CatchAreasRequestHandler(
                IRepository<User> userRepository,
                ICurrentUserIdProvider currentUserIdProvider,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _userRepository = userRepository;
                _currentUserIdProvider = currentUserIdProvider;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Response> Handle(CatchAreaQuery request, CancellationToken cancellationToken)
            {
                var subAreaIds = _currentVersionRegionalLayoutService
                    .QuerySubAreasNoTracking(x => x.SubAreaHourSquares)
                    .Select(x => x.Id).ToList();

                var expression = request.FilterByOrganization
                    ? CreateFilteredByOrganizationCatchAreaQuery(request, subAreaIds)
                    : CreateCatchAreaQuery(request, subAreaIds);

                return await RequestHandler.Handle(
                    _currentVersionRegionalLayoutService.QueryCatchAreasNoTracking(x => x.SubAreas),
                    expression);
            }

            private Expression<Func<CatchArea, bool>> CreateFilteredByOrganizationCatchAreaQuery(CatchAreaQuery request, List<Guid> subAreaIds)
            {
                var userId = _currentUserIdProvider.FindUserId(_userRepository.QueryAll()) ??
                    throw new InvalidOperationException("Cannot find user id.");

                var organizationId = _userRepository.FindById(userId).OrganizationId ??
                    throw new InvalidOperationException("Cannot find users organization id.");

                var rayonsIds = _currentVersionRegionalLayoutService
                    .QueryRayonsNoTracking()
                    .QueryByOrganization(organizationId)
                    .Select(x=> x.Id)
                    .ToList();

                Expression<Func<CatchArea, bool>> predicate = x =>
                    rayonsIds.Contains(x.RayonId) &&
                    x.SubAreas.Any(sa => subAreaIds.Contains(sa.Id)) &&
                    (String.IsNullOrEmpty(request.NameFilter) || x.Name.Contains(request.NameFilter));

                return predicate;
            }

            private static Expression<Func<CatchArea, bool>> CreateCatchAreaQuery(CatchAreaQuery request, List<Guid> subAreaIds)
            {
                Expression<Func<CatchArea, bool>> predicate = x =>
                    (!request.RayonId.HasValue || x.RayonId == request.RayonId) &&
                    x.SubAreas.Any(sa => subAreaIds.Contains(sa.Id)) &&
                    (String.IsNullOrEmpty(request.NameFilter) || x.Name.Contains(request.NameFilter));
                return predicate;
            }
        }
    }
}
