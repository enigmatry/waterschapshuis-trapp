using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.ScheduledJobs
{
    public static partial class GetScheduledJobs
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<ScheduledJob> _scheduledJobRepository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<ScheduledJob> scheduledJobRepository,
                IMapper mapper)
            {
                _scheduledJobRepository = scheduledJobRepository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var job = await _scheduledJobRepository
                    .QueryAll()
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefaultAsync(cancellationToken);

                return job != null
                    ? _mapper.Map<Response>(job)
                    : new Response();
            }
        }
    }
}
