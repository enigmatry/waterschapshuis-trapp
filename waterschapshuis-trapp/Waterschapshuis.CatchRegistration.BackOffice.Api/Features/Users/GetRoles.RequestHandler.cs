using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users
{
    public partial class GetRoles
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Role> _repository;
            private readonly IMapper _mapper;
            private readonly ICurrentUserProvider _currentUserProvider;

            public RequestHandler(IRepository<Role> repository, IMapper mapper, ICurrentUserProvider currentUserProvider)
            {
                _repository = repository;
                _mapper = mapper;
                _currentUserProvider = currentUserProvider;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                // Required, in case when roles permissions are updated in same Request scope!
                _currentUserProvider.TryReloadUser();

                var items = await _repository
                    .QueryAllAsNoTracking()
                    .BuildInclude()
                    .OrderBy(x => x.DisplayOrderIndex)
                    .ToListMappedAsync<Role, Response.Item>(_mapper, cancellationToken);

                return new Response { Items = items };
            }
        }
    }
}
