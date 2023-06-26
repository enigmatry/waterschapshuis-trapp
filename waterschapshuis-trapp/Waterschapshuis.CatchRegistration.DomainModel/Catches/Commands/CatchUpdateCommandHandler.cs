using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands
{
    [UsedImplicitly]
    public class CatchUpdateCommandHandler : IRequestHandler<CatchCreateOrUpdate.Command, CatchCreateOrUpdate.Result>
    {
        private readonly IRepository<Catch> _catchRepository;
        private readonly IRepository<CatchType> _catchTypeRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public CatchUpdateCommandHandler(
            IRepository<Catch> catchRepository,
            IRepository<CatchType> catchTypeRepository,
            ICurrentUserProvider currentUserProvider)
        {
            _catchRepository = catchRepository;
            _catchTypeRepository = catchTypeRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<CatchCreateOrUpdate.Result> Handle(CatchCreateOrUpdate.Command request, CancellationToken cancellationToken)
        {
            var catchToUpdate = await _catchRepository.QueryAll()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new InvalidOperationException("Catch with given Id does not exist");

            if (!catchToUpdate.CanUpdateFromBackOffice(_currentUserProvider.RoleIds.ToList(), _currentUserProvider.PermissionIds.ToList()))
                throw new InvalidOperationException("Current user doesn't have enough rights to update catch");

            catchToUpdate.Update(
                request,
                _catchTypeRepository.FindById(request.CatchTypeId)
                    ?? throw new InvalidOperationException("Cannot find catch type"),
                catchToUpdate.Status);

            return CatchCreateOrUpdate.Result.CreateResult(catchToUpdate.Id);
        }
    }
}
