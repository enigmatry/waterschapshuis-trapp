namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security
{
    public static class PolicyNames
    {
        public const string UserApproved = "UserApproved";
        public const string UserHasValidSession = "UserHasValidSession";
        public const string UserRequestCameFromSameIpAddress = "UserRequestCameFromSameIpAddress";
        public const string ControllerSecuredWithUserHasPermissionAttribute = "ControllerHasPermissionAccessAttribute";
        
        public static class Mobile
        {
            public const string UserMobileAccess = "UserMobileAccess";
        }

        public static class BackOffice
        {
            public const string AnyPermission = "AnyPermission";
            public const string MapRead = "MapRead";
            public const string MapContentRead = "MapContentRead";
            public const string MapContentWrite = "MapContentWrite";
            public const string TimeRegistrationPersonalReadWrite = "TimeRegistrationPersonalReadWrite";
            public const string TimeRegistrationManagementReadWrite = "TimeRegistrationManagementReadWrite";
            public const string UserRead = "UserRead";
            public const string UserWrite = "UserWrite";
            public const string RoleRead = "RoleRead";
            public const string RoleWrite = "RoleWrite";
            public const string ReportReadWrite = "ReportReadWrite";
            public const string OrganizationRead = "OrganizationRead";
            public const string Management = "Management";
        }

        public static class ExternalApi
        {
            public const string LimitedAccess = "ExternalApiLimitedAccess";
            public const string UnlimitedAccess = "ExternalApiUnlimitedAccess";
        }
    }
}
