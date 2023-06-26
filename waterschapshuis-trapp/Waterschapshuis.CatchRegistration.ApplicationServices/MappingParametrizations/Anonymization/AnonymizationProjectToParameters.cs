using System;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization
{
    public class AnonymizationProjectToParameters
    {
        public DateTimeOffset AnonymizeBefore { get; }
        public Guid CurrentUserId { get; }
        public Guid CurrentUserOrganizationId { get; }
        public bool IsSeniorUser { get; }
        public bool SkipAnonymizationIfDateConditionIsMeet { get; }

        public AnonymizationProjectToParameters(
            DateTimeOffset anonymizeBefore,
            Guid currentUserId,
            Guid currentUserOrganizationId,
            bool isSeniorUser,
            bool skipAnonymizationIfDateConditionIsMeet)
        {
            AnonymizeBefore = anonymizeBefore;
            CurrentUserId = currentUserId;
            CurrentUserOrganizationId = currentUserOrganizationId;
            IsSeniorUser = isSeniorUser;
            SkipAnonymizationIfDateConditionIsMeet = skipAnonymizationIfDateConditionIsMeet;
        }

        public static AnonymizationProjectToParameters CreateEmpty()
        {
            return new AnonymizationProjectToParameters(DateTimeOffset.MinValue, Guid.Empty, Guid.Empty, false, false);
        }
    }
}
