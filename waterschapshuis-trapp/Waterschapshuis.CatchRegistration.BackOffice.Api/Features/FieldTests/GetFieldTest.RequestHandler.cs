using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.FieldTests
{
    public partial class GetFieldTest
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<FieldTest> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<FieldTest> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .QueryById(request.Id)
                    .ProjectTo<Response>(_mapper.ConfigurationProvider, cancellationToken)
                    .SingleOrDefaultAsync(cancellationToken);
            }
        }
    }
}
