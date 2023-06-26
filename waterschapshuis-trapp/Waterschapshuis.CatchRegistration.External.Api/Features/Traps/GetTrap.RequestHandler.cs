using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Traps
{
    public partial class GetTrap
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Trap> _repository;
            private readonly ICurrentUserProvider _currentUserProvider;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Trap> repository, IMapper mapper, ICurrentUserProvider currentUserProvider)
            {
                _repository = repository;
                _mapper = mapper;
                _currentUserProvider = currentUserProvider;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var hasPermission = _currentUserProvider.UserHasAnyPermission(new List<PermissionId> { PermissionId.ApiPrivate });

                IConfigurationProvider config = new MapperConfiguration(cfg =>
                    cfg.AddProfile(new TrapMappingProfile(hasPermission)));

                var trap = _repository.QueryAll()
                    .QueryById(request.Id)
                    .ProjectTo<TrapItem>(config)
                    .FirstOrDefault();

                return Task.FromResult(new Response(trap));
            }
        }
    }
}
