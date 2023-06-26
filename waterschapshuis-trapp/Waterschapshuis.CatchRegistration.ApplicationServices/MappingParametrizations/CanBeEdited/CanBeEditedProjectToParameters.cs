namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited
{
    public class CanBeEditedProjectToParameters
    {
        public bool IsTraper { get; }
        public bool HasEditDataPermission { get; }

        public CanBeEditedProjectToParameters(bool isTraper, bool hasEditDataPermission)
        {
            IsTraper = isTraper;
            HasEditDataPermission = hasEditDataPermission;
        }

        public static CanBeEditedProjectToParameters CreateEmpty()
        {
            return new CanBeEditedProjectToParameters(false, true);
        }
    }
}
