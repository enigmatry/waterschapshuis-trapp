using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations
{
    public class MappingParametrizationService : IMappingParametrizationService
    {
        private const int AnonymizeOlderThanYears = 5;

        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITimeProvider _timeProvider;

        public MappingParametrizationService(
            ICurrentUserProvider currentUserProvider,
            ITimeProvider timeProvider)
        {
            _currentUserProvider = currentUserProvider;
            _timeProvider = timeProvider;
        }

        public AnonymizationProjectToParameters CreateAnonymizationParameters()
        {
            var anonymizeOlderThanDate = _timeProvider.Now.AddYears(-AnonymizeOlderThanYears);
            var currentUserId = _currentUserProvider.UserId ?? Guid.Empty;
            var currentUserOrganizationId = _currentUserProvider.Organization?.Id ?? Guid.Empty;

            return new AnonymizationProjectToParameters(
                anonymizeOlderThanDate,
                currentUserId,
                currentUserOrganizationId,
                _currentUserProvider.RoleIds.Contains(Role.SeniorUserId),
                ShouldSkipAnonymizationIfDateConditionIsMeet);
        }

        public CanBeEditedProjectToParameters CreateCanBeEditedParameters() =>
            new CanBeEditedProjectToParameters(
                _currentUserProvider.RoleIds.Contains(Role.TrapperRoleId),
                _currentUserProvider.PermissionIds.Contains(PermissionId.MapContentWrite));

        private bool ShouldSkipAnonymizationIfDateConditionIsMeet => _currentUserProvider.RoleIds.Intersect(new[] { Role.MaintainerRoleId }).Any();
    }
}
