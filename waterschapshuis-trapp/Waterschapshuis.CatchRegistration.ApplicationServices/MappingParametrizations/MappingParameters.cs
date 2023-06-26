using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations
{
    public class MappingParameters
    {
        public AnonymizationProjectToParameters AnonymizationParameter { get; }
        public CanBeEditedProjectToParameters CanBeEditedParameter { get; }

        public MappingParameters(
            AnonymizationProjectToParameters anonymizationParameter,
            CanBeEditedProjectToParameters canBeEditedParameter)
        {
            AnonymizationParameter = anonymizationParameter;
            CanBeEditedParameter = canBeEditedParameter;
        }

        public static MappingParameters CreateEmpty() =>
            new MappingParameters(
                AnonymizationProjectToParameters.CreateEmpty(),
                CanBeEditedProjectToParameters.CreateEmpty());
    }
}
