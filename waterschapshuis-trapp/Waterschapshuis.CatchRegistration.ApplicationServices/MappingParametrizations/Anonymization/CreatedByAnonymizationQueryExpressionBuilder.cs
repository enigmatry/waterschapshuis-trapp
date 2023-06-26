using System;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.DomainModel.Anonymization;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization
{
    // we need to build EF core SQL compatible expression here
    public class CreatedByAnonymizationQueryExpressionBuilder<TSource> where TSource : IAnonymizeCreatedBy
    {
        // do not refactor parameter into constructor (as with standard builders),
        // this build is used by AutoMapper's projectTo and needs
        // to be in this exact format, also name "parameters" needs to be exactly the same as in the MappingProfiles
        public Expression<Func<TSource, string>> Build(MappingParameters parameters) =>
            src =>
                src.CreatedOn >= parameters.AnonymizationParameter.AnonymizeBefore &&
                (
                    parameters.AnonymizationParameter.SkipAnonymizationIfDateConditionIsMeet ||
                    parameters.AnonymizationParameter.CurrentUserId == src.CreatedById ||
                    parameters.AnonymizationParameter.CurrentUserOrganizationId == src.CreatedBy.OrganizationId ||
                    parameters.AnonymizationParameter.CurrentUserOrganizationId == src.LocationOrganizationId
                )
                ? src.CreatedBy.Name
                : User.AnonymizedName;
    }
}
