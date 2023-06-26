using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters
{
    public class TransactionFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext resultContext = await next(); 

            var unitOfWork = context.HttpContext.Resolve<IUnitOfWork>();

            if (resultContext.Exception == null &&
                context.HttpContext.Response.StatusCode >= 200 &&
                context.HttpContext.Response.StatusCode < 300 && 
                context.ModelState.IsValid)
                await unitOfWork.SaveChangesAsync();
        }
    }
}
