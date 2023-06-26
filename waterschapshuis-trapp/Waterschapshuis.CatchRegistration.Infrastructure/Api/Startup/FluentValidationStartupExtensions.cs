using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Waterschapshuis.CatchRegistration.Infrastructure.Validation;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class FluentValidationStartupExtensions
    {
        public static IMvcBuilder AppAddFluentValidation(this IMvcBuilder mvcBuilder, Assembly entryAssembly)
        {
            return mvcBuilder.AddFluentValidation(fv =>
            {
                // disables standard data annotations validation
                // https://fluentvalidation.net/aspnet.html#asp-net-core
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.ImplicitlyValidateChildProperties = true;
                fv.RegisterValidatorsFromAssemblies(new[]
                {
                    AssemblyFinder.DomainAssembly, entryAssembly
                });
            });
        }

        public static void AppConfigureFluentValidation(this IApplicationBuilder app)
        {
            ValidatorOptions.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
        }

        public static void AppAddFluentValidationApiBehaviorOptions(this ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
                context.HttpContext.CreateValidationProblemDetailsResponse(context.ModelState);
        }
    }
}
