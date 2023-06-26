using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Catches
{
    public partial class GetCatches
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Catch> _repository;
            private readonly ICurrentUserProvider _currentUserProvider;

            public RequestHandler(IRepository<Catch> repository, ICurrentUserProvider currentUserProvider)
            {
                _repository = repository;
                _currentUserProvider = currentUserProvider;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var hasPermission = _currentUserProvider.UserHasAnyPermission(new List<PermissionId> { PermissionId.ApiPrivate });

                IConfigurationProvider config = new MapperConfiguration(cfg =>
                    cfg.AddProfile(new GetCatch.CatchMappingProfile(hasPermission)));

                var catches = _repository.QueryAll()
                    .AsNoTracking()
                    .ProjectTo<GetCatch.CatchItem>(config)
                    .QueryByOrganizationName(request.Organization)
                    .QueryByYear(request.CreatedOnYear);

                return Task.FromResult(new Response(catches));
            }
        }
    }
}
