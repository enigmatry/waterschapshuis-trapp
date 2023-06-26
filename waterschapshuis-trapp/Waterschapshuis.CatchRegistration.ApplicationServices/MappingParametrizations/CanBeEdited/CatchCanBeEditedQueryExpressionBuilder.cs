using System;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited
{
    public class CatchCanBeEditedQueryExpressionBuilder<TSource> where TSource : Catch
    {
        // do not refactor parameter into constructor (as with standard builders),
        // this build is used by AutoMapper's projectTo and needs
        // to be in this exact format, also name "parameters" needs to be exactly the same as in the MappingProfiles
        public Expression<Func<TSource, bool>> Build(MappingParameters parameters) =>
            src =>
                parameters.CanBeEditedParameter.HasEditDataPermission &&
                (
                    parameters.CanBeEditedParameter.IsTraper && src.Status == CatchStatus.Written || 
                    !parameters.CanBeEditedParameter.IsTraper && src.Status == CatchStatus.Closed
                );
    }
}
