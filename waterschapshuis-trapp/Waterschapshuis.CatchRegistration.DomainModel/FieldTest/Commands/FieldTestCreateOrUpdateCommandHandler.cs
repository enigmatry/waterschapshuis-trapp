using System.Linq;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.FieldTest.Commands
{
    [UsedImplicitly]
    public class
        FieldTestCreateOrUpdateCommandHandler : IRequestHandler<FieldTestCreateOrUpdate.Command, FieldTestCreateOrUpdate.Result>
    {
        private readonly IRepository<FieldTest> _fieldTestRepository;

        public FieldTestCreateOrUpdateCommandHandler(
            IRepository<FieldTest> fieldTestRepository
        )
        {
            _fieldTestRepository = fieldTestRepository;
        }

        public Task<FieldTestCreateOrUpdate.Result> Handle(FieldTestCreateOrUpdate.Command request,
            CancellationToken cancellationToken)
        {
            FieldTest fieldTest;
            if (request.Id.HasValue)
            {
                fieldTest = _fieldTestRepository
                    .QueryAll()
                    .Include(u => u.FieldTestHourSquares)
                    .QueryById(request.Id.Value).Single();
                fieldTest.Update(request);
            }
            else
            {
                fieldTest = FieldTest.Create(request);
                _fieldTestRepository.Add(fieldTest);
            }

            return Task.FromResult(FieldTestCreateOrUpdate.Result.CreateResult(fieldTest.Id));
        }
    }
}
