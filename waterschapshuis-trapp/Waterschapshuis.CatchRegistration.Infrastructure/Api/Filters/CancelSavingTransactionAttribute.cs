using Microsoft.AspNetCore.Mvc.Filters;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters
{
    public class CancelSavingTransactionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CancelSavingIfModelInvalid(context);
         }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            CancelSavingIfModelInvalid(context);
         }

        private static void CancelSavingIfModelInvalid(FilterContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var unitOfWork = context.HttpContext.Resolve<IUnitOfWork>();
                unitOfWork.CancelSaving();
            }
        }
    }
}
