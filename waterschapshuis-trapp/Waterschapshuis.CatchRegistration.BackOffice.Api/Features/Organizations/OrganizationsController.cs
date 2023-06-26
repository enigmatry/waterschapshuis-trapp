using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Organizations
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class OrganizationsController : Controller
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets listing of all available Organizations
        /// </summary>
        /// <returns>List of organizations</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.OrganizationRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetOrganizations.Response>> Search([FromQuery] GetOrganizations.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }
    }
}
