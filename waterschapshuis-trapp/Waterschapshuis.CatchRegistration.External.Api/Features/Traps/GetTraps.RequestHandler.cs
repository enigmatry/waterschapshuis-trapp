using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Traps
{
    public partial class GetTraps
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Trap> _repository;
            private readonly ICurrentUserProvider _currentUserProvider;

            public RequestHandler(IRepository<Trap> repository, ICurrentUserProvider currentUserProvider)
            {
                _repository = repository;
                _currentUserProvider = currentUserProvider;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var hasPermission = _currentUserProvider.UserHasAnyPermission(new List<PermissionId> { PermissionId.ApiPrivate });

                IConfigurationProvider config = new MapperConfiguration(cfg =>
                    cfg.AddProfile(new GetTrap.TrapMappingProfile(hasPermission)));

                var traps = _repository.QueryAll()
                    .AsNoTracking()
                    .ProjectTo<GetTrap.TrapItem>(config)
                    .QueryByOrganizationName(request.Organization)
                    .QueryByYear(request.CreatedOnYear);

                return Task.FromResult(new Response(traps));
            }
        }
    }
}
