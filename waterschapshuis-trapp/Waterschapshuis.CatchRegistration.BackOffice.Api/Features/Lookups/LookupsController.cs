using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.Lookups;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Lookups
{
    [Produces(MediaTypeNames.Application.Json)]
    public class LookupsController : Controller
    {
        private readonly IMediator _mediator;

        public LookupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets listing of all available Trapping types
        /// </summary>
        /// <returns>List of trap types</returns>
        [HttpGet]
        [Route("trapping-types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<ListResponse<GetTrappingTypes.ResponseItem>>> TrappingTypes()
        {
            var response = await _mediator.Send(GetTrappingTypes.Query.Create());
            return response.ToActionResult();
        }

        /// <summary>
        ///     Gets listing of all available Trap types
        /// </summary>
        /// <returns>List of trap types</returns>
        [HttpGet]
        [Route("trap-types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<ListResponse<GetTrapTypes.ResponseItem>>> TrapTypes([FromQuery] GetTrapTypes.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Gets listing of all available Catch types
        /// </summary>
        /// <returns>List of catch types</returns>
        [HttpGet]
        [Route("catch-types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<ListResponse<GetCatchTypes.ResponseItem>>> CatchTypes([FromQuery] GetCatchTypes.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Gets listing of all available time registration categories
        /// </summary>
        /// <returns>List of time registration categories</returns>
        [HttpGet]
        [Route("time-registration-categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<ListResponse<GetTimeRegistrationCategories.ResponseItem>>> TimeRegistrationCategories()
        {
            var response = await _mediator.Send(GetTimeRegistrationCategories.Query.Create());
            return response.ToActionResult();
        }
    }
}
