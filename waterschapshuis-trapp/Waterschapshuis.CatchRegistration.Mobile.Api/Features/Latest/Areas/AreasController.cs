using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.Areas;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Areas
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class AreasController
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get Area details based on the geo location
        /// </summary>
        /// <param name="lon"> Geographic longitude of the location in EPSG:28992 projection </param>
        /// <param name="lat"> Geographic latitude of the location in EPSG:28992 projection </param>
        /// <returns> AreaDetails object for the requested location </returns>
        [HttpGet]
        [Route("location")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetLocationAreaDetails.Response>> GetLocationDetails([FromQuery] double lon, [FromQuery] double lat)
        {
            var response = await _mediator.Send(GetLocationAreaDetails.Query.Create(lon, lat));
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get data for the catch area and its sub-area.
        /// </summary>
        /// <param name="catchAreaId">
        ///     Id of the catch-area.
        /// </param>
        /// <param name="subAreaId">
        ///     Id of the sub-area.
        /// </param>
        /// <returns>
        ///     Summary for catching traps, last week catches and by-catches, last week time registrations.
        /// </returns>
        [HttpGet]
        [Route("location-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<GetLocationAreaData.Response>> GetLocationData(
            [FromQuery] Guid catchAreaId, [FromQuery] Guid subAreaId)
        {
            var response = await _mediator.Send(
                new GetLocationAreaData.Query { CatchAreaId = catchAreaId, SubAreaId = subAreaId});

            return response.ToActionResult();
        }

        /// <summary>
        ///     Get CatchAreas based on filter and RayonId
        /// </summary>
        /// <param name="filter"> The string used to filter CatchAreas by Name </param>
        /// <param name="rayonId"> The RayonId of the requested CatchAreas </param>
        [HttpGet]
        [Route("catch-areas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetAreaEntities.Response>> GetCatchAreas([FromQuery] string filter, Guid? rayonId)
        {
            var response = await _mediator.Send(new GetAreaEntities.CatchAreaQuery { NameFilter = filter, RayonId = rayonId });
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
        public async Task<ActionResult<GetAreaEntities.Response>> GetSubAreas([FromQuery] string filter, Guid? catchAreaId)
        {
            var response = await _mediator.Send(new GetAreaEntities.SubAreaQuery { NameFilter = filter, CatchAreaId = catchAreaId });
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
        public async Task<ActionResult<GetAreaEntities.Response>> GetHourSquares([FromQuery] string filter, Guid? subAreaId)
        {
            var response = await _mediator.Send(new GetAreaEntities.HourSquareQuery { NameFilter = filter, SubAreaId = subAreaId });
            return response.ToActionResult();
        }
    }
}
