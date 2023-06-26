using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Observations
{
    public partial class GetObservation
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Observation> _repository;
            private readonly ICurrentUserProvider _currentUserProvider;

            public RequestHandler(IRepository<Observation> repository, ICurrentUserProvider currentUserProvider)
            {
                _repository = repository;
                _currentUserProvider = currentUserProvider;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var hasPermission = _currentUserProvider.UserHasAnyPermission(new List<PermissionId> { PermissionId.ApiPrivate });

                IConfigurationProvider config = new MapperConfiguration(cfg =>
                    cfg.AddProfile(new ObservationMappingProfile(hasPermission)));

                var observation = _repository.QueryAll()
                    .QueryById(request.Id)
                    .ProjectTo<ObservationItem>(config)
                    .FirstOrDefault();

                return Task.FromResult(new Response(observation));
            }
        }
    }
}
