using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Users
{
    public partial class GetUserDetails
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<User> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<User> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .BuildInclude()
                    .QueryById(request.Id)
                    .SingleOrDefaultMappedAsync<User, Response>(_mapper, cancellationToken);
            }
        }
    }
}
