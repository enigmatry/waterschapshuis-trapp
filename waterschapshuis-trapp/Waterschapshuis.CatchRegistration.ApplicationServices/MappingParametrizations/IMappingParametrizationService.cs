using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations
{
    public interface IMappingParametrizationService
    {
        AnonymizationProjectToParameters CreateAnonymizationParameters();
        CanBeEditedProjectToParameters CreateCanBeEditedParameters();
    }
}
