using Microsoft.AspNetCore.Mvc;

namespace Waterschapshuis.CatchRegistration.Mobile.Api
{
    public static class ActionResultExtensions
    {
        public static ActionResult<TDestination> ToActionResult<TDestination>(this TDestination? model) where TDestination : class
        {
            return model == null ? 
                (ActionResult<TDestination>)new NotFoundResult() 
                : new OkObjectResult(model);
        }
    }
}
