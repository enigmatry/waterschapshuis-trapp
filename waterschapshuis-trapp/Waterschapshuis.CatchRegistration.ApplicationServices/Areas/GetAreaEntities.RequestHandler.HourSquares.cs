using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Areas
{
    public static partial class GetAreaEntities
    {
        [UsedImplicitly]
        public class HourSquaresRequestHandler : IRequestHandler<HourSquareQuery, Response>
        {
            private readonly IRepository<HourSquare> _repository;

            public HourSquaresRequestHandler(IRepository<HourSquare> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(HourSquareQuery request, CancellationToken cancellationToken)
            {
                return await RequestHandler.Handle(
                    _repository.QueryAll().AsNoTracking(), 
                    CreateCatchAreaQuery(request));
            }

            private static Expression<Func<HourSquare, bool>> CreateCatchAreaQuery(HourSquareQuery request)
            {
                Expression<Func<HourSquare, bool>> predicate = x =>
                    (!request.SubAreaId.HasValue || x.SubAreaHourSquares.Any(s => s.SubArea.Id == request.SubAreaId)) && 
                    (String.IsNullOrEmpty(request.NameFilter) || x.Name.Contains(request.NameFilter));
                return predicate;
            }
        }
    }
}
