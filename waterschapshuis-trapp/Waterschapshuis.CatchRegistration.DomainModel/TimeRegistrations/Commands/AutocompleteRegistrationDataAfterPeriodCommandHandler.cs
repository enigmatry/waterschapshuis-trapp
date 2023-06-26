using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    [UsedImplicitly]
    public class AutocompleteRegistrationDataAfterPeriodCommandHandler : IRequestHandler<AutocompleteRegistrationDataAfterPeriod.Command, AutocompleteRegistrationDataAfterPeriod.Result>
    {
        private readonly IRepository<Catch> _catchRepository;
        private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

        public AutocompleteRegistrationDataAfterPeriodCommandHandler(
            IRepository<Catch> catchRepository,
            ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
        {
            _catchRepository = catchRepository;
            _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
        }

        public async Task<AutocompleteRegistrationDataAfterPeriod.Result> Handle(AutocompleteRegistrationDataAfterPeriod.Command request, CancellationToken cancellationToken)
        {
            var availableTimeRegistrationStatuses =
                new List<TimeRegistrationStatus> { TimeRegistrationStatus.Written, TimeRegistrationStatus.Closed };
            var availableCatchStatuses =
                new List<CatchStatus> { CatchStatus.Written, CatchStatus.Closed };

            var timeRegistrations = await _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .QueryToDateAndStatuses(request.Date, availableTimeRegistrationStatuses)
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);

            var catches = await _catchRepository
                .QueryAll()
                .QueryToDateAndStatuses(request.Date, availableCatchStatuses)
                .OrderBy(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            foreach (var timeRegistration in timeRegistrations)
            {
                timeRegistration.UpdateStatus(request.TimeRegistrationStatus);
            }
            foreach (var catchRegistration in catches)
            {
                catchRegistration.UpdateStatus(request.CatchStatus);
            }

            return await Task.FromResult(AutocompleteRegistrationDataAfterPeriod.Result.CreateResult());
        }
    }
}
