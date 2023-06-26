using System;
using System.Linq;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization
{
    // we need to build EF core SQL compatible expression here
    public class UpdatedByTrapAnonymizationQueryExpressionBuilder<TSource> where TSource : Trap
    {
        // do not refactor parameter into constructor (as with standard builders),
        // this build is used by AutoMapper's projectTo and needs
        // to be in this exact format, also name "parameters" needs to be exactly the same as in the MappingProfiles
        public Expression<Func<TSource, string>> Build(MappingParameters parameters) =>
            src =>
                src.CreatedOn >= parameters.AnonymizationParameter.AnonymizeBefore &&
                (
                    parameters.AnonymizationParameter.SkipAnonymizationIfDateConditionIsMeet ||
                    parameters.AnonymizationParameter.CurrentUserId == src.UpdatedById ||
                    parameters.AnonymizationParameter.CurrentUserOrganizationId == src.UpdatedBy.OrganizationId
                )
                    ? src.TrapHistories.Count > 0 ?
                        src.TrapHistories.OrderByDescending(x => x.UpdatedOn).FirstOrDefault().UpdatedBy.Name : src.UpdatedBy.Name
                    : User.AnonymizedName;
    }
}
