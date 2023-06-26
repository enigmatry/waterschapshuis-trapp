using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security
{
    public static class AuthorizationStartupExtensions
    {
        public static ControllerActionEndpointConventionBuilder AppRequireAuthorization(
            this ControllerActionEndpointConventionBuilder builder,
            bool enable)
        {
            if (!enable)
            {
                return builder;
            }

            return builder.RequireAuthorization();
        }

        public static void AppAddAuthorization(this IServiceCollection services, bool enable, IHostEnvironment hostEnvironment)
        {
            if (!enable)
            {
                return;
            }

            services.AddAuthorization(options =>
            {
                // policy based authorization https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-3.1
                options.AddPolicy(
                    PolicyNames.ControllerSecuredWithUserHasPermissionAttribute,
                    policy => policy.AddRequirements(new ControllerSecuredWithUserHasPermissionAttribute.Requirement()));

                options.AddPolicy(
                    PolicyNames.UserHasValidSession,
                    policy => policy.AddRequirements(new UserHasValidSession.Requirement()));

                options.AddPolicy(
                    PolicyNames.UserApproved,
                    policy => policy.AddRequirements(new UserApproved.Requirement()));

                // BackOffice policies
                options.AppAddUserHasPermissionPolicies(BackOfficePoliciesToPermissionsMap.Map());

                options.AddPolicy(
                    PolicyNames.UserRequestCameFromSameIpAddress,
                    policy => policy.AddRequirements(new UserRequestCameFromSameIpAddress.Requirement(hostEnvironment)));

                // Mobile policy
                options.AddPolicy(
                    PolicyNames.Mobile.UserMobileAccess,
                    policy => policy.AddRequirements(new UserCanAccessMobile.Requirement()));

                // External api policy
                options.AppAddUserHasPermissionPolicies(ExternalApiPoliciesToPermissionsMap.Map());
            });

            services.AddScoped<IAuthorizationHandler, ControllerSecuredWithUserHasPermissionAttribute.RequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UserHasValidSession.RequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UserRequestCameFromSameIpAddress.RequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UserApproved.RequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UserHasPermission.RequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UserCanAccessMobile.RequirementHandler>();
        }

        public static void AppAddAuthorizationFilters(this MvcOptions options, bool enable)
        {
            if (!enable)
                return;

            // this adds filters that are applied to all controllers
            options.Filters.Add(new DefaultAuthorizeFilter(PolicyNames.UserApproved));
            options.Filters.Add(new DefaultAuthorizeFilter(PolicyNames.UserHasValidSession));
        }

        public static void AppAddBackOfficeAuthorizationFilters(this MvcOptions options, bool enable)
        {
            if (!enable)
                return;

            // this adds filter that is applied to all controllers
            options.Filters.Add(new DefaultAuthorizeFilter(PolicyNames.ControllerSecuredWithUserHasPermissionAttribute));
            options.Filters.Add(new DefaultAuthorizeFilter(PolicyNames.UserRequestCameFromSameIpAddress));
        }

        public static void AppAddMobileAuthorizationFilters(this MvcOptions options, bool enable)
        {
            if (!enable)
                return;

            // this adds filter that is applied to all controllers
            options.Filters.Add(new UserCanAccessMobileFilter());
        }

        public static void AppAddExternalApiAuthorizationFilters(this MvcOptions options, bool enable)
        {
            if (!enable)
                return;

            // this adds filter that is applied to all controllers
            options.Filters.Add(new UserCanAccessExternalApiFilter(PolicyNames.ExternalApi.LimitedAccess));
        }
    }
}
