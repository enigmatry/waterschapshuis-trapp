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
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Catches
{
    public partial class GetCatch
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
                    cfg.AddProfile(new CatchMappingProfile(hasPermission)));

                var catchItem = _repository.QueryAll()
                    .QueryById(request.Id)
                    .ProjectTo<CatchItem>(config)
                    .FirstOrDefault();

                return Task.FromResult(new Response(catchItem));
            }
        }
    }
}
