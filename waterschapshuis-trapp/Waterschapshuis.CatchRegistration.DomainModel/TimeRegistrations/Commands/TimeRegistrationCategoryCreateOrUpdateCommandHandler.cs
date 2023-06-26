using System;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    [UsedImplicitly]
    public class
        TimeRegistrationCategoryCreateOrUpdateCommandHandler : IRequestHandler<TimeRegistrationCategoryCreateOrUpdate.Command, TimeRegistrationCategoryCreateOrUpdate.Result>
    {
        private readonly IRepository<TimeRegistrationCategory> _timeRegistrationCategoryRepository;

        public TimeRegistrationCategoryCreateOrUpdateCommandHandler(
            IRepository<TimeRegistrationCategory> timeRegistrationCategoryRepository)
        {
            _timeRegistrationCategoryRepository = timeRegistrationCategoryRepository;
        }

        public async Task<TimeRegistrationCategoryCreateOrUpdate.Result> Handle(
            TimeRegistrationCategoryCreateOrUpdate.Command request,
            CancellationToken cancellationToken)
        {

            TimeRegistrationCategory timeRegistrationCategory;

            if (request.Id.HasValue)
            {
                timeRegistrationCategory = await _timeRegistrationCategoryRepository.FindByIdAsync(request.Id.Value);
                timeRegistrationCategory.Update(request);
            }
            else
            {
                timeRegistrationCategory = TimeRegistrationCategory.Create(request);
                _timeRegistrationCategoryRepository.Add(timeRegistrationCategory);
            }

            return TimeRegistrationCategoryCreateOrUpdate.Result.CreateResult(timeRegistrationCategory.Id);
        }
    }
}
