using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Waterschapshuis.CatchRegistration.Infrastructure.Validation;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters
{
    public class HandleExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly bool _useDeveloperExceptionPage;

        public HandleExceptionsFilterAttribute(bool useDeveloperExceptionPage)
        {
            _useDeveloperExceptionPage = useDeveloperExceptionPage;
        }

        public override void OnException(ExceptionContext context)
        {
            ILogger<HandleExceptionsFilterAttribute> logger = context.HttpContext.Resolve<ILogger<HandleExceptionsFilterAttribute>>();
            if (context.Exception is ValidationException validationException)
            {
                logger.LogDebug(context.Exception, "Validation exception");
                context.Result = context.HttpContext.CreateValidationProblemDetailsResponse(validationException);
                return;
            }

            logger.LogError(context.Exception, "Unexpected error");

            IList<MediaTypeHeaderValue> accept = context.HttpContext.Request.GetTypedHeaders().Accept;
            if (accept != null && accept.All(header => header.MediaType != "application/json"))
            {
                // server does not accept Json, leaving to default MVC error page handler.
                return;
            }

            var jsonResult = new JsonResult(GetProblemDetails(context)) {StatusCode = (int)HttpStatusCode.InternalServerError, ContentType = "application/problem+json"};
            context.Result = jsonResult;
        }

        private ProblemDetails GetProblemDetails(ExceptionContext context)
        {
            string errorDetail = context.HttpContext.Request.IsTrusted(_useDeveloperExceptionPage)
                ? context.Exception.Demystify().ToString()
                : "The instance value should be used to identify the problem when calling customer support";

            var problemDetails = new ProblemDetails {Title = "An unexpected error occurred!", Instance = context.HttpContext.Request.Path, Status = StatusCodes.Status500InternalServerError, Detail = errorDetail};

            return problemDetails;
        }
    }
}
