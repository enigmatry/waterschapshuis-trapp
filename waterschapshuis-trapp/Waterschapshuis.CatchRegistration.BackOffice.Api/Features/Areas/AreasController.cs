using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Areas;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Areas
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class AreasController : Controller
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get CatchAreas based on filter and RayonId
        /// </summary>
        /// <param name="filter"> The string used to filter CatchAreas by Name </param>
        /// <param name="rayonId"> The RayonId of the requested CatchAreas </param>
        /// <param name="filterByOrganization"> Flag that represents whether CatchAreas should be filtered by organization of current user </param>
        [HttpGet]
        [Route("catch-areas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<GetAreaEntities.Response>> GetCatchAreas([FromQuery] string filter, Guid? rayonId, bool filterByOrganization = false)
        {
            var response = await _mediator.Send(new GetAreaEntities.CatchAreaQuery { NameFilter = filter, RayonId = rayonId, FilterByOrganization = filterByOrganization });
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get HourSquares based on filter and SubAreaId
        /// </summary>
        /// <param name="filter"> The string used to filter HourSquares by Name </param>
        /// <param name="subAreaId"> The SubAreaId based on which to filter HourSquares </param>
        [HttpGet]
        [Route("hour-squares")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<GetAreaEntities.Response>> GetHourSquares([FromQuery] string filter, Guid? subAreaId)
        {
            var response = await _mediator.Send(new GetAreaEntities.HourSquareQuery { NameFilter = filter, SubAreaId = subAreaId });
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get SubAreas based on filter and CatchAreaId
        /// </summary>
        /// <param name="filter"> The string used to filter SubAreas by Name </param>
        /// <param name="catchAreaId"> The CatchAreaId of the requested SubAreas </param>
        [HttpGet]
        [Route("sub-areas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<GetAreaEntities.Response>> GetSubAreas([FromQuery] string filter, Guid? catchAreaId)
        {
            var response = await _mediator.Send(new GetAreaEntities.SubAreaQuery { NameFilter = filter, CatchAreaId = catchAreaId });
            return response.ToActionResult();
        }
    }
}
