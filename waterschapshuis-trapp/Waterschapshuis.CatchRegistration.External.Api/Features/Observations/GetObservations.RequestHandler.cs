using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Observations
{
    public partial class GetObservations
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Observation> _repository;
            private readonly ICurrentUserProvider _currentUserProvider;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Observation> repository, IMapper mapper, ICurrentUserProvider currentUserProvider)
            {
                _repository = repository;
                _mapper = mapper;
                _currentUserProvider = currentUserProvider;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var hasPermission = _currentUserProvider.UserHasAnyPermission(new List<PermissionId> { PermissionId.ApiPrivate });

                IConfigurationProvider config = new MapperConfiguration(cfg =>
                    cfg.AddProfile(new GetObservation.ObservationMappingProfile(hasPermission)));

                var observations = _repository.QueryAll()
                    .AsNoTracking()
                    .ProjectTo<GetObservation.ObservationItem>(config)
                    .QueryByOrganizationName(request.Organization)
                    .QueryByYear(request.CreatedOnYear);

                return Task.FromResult(new Response(observations));
            }
        }
    }
}
