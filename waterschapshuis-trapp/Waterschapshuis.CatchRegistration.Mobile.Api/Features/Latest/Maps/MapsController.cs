using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Layers;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Maps
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class MapsController : Controller
    {
        private readonly IMediator _mediator;

        public MapsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets listing of all available overlay layers
        /// </summary>
        /// <returns>List of layers</returns>
        [HttpGet]
        [Route("overlay-layers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListResponse<GetOverlayLayers.ResponseItem>>> OverlayLayers([FromQuery] GetOverlayLayers.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Gets listing of all available background layers
        /// </summary>
        /// <returns>List of layers</returns>
        [HttpGet]
        [Route("background-layers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListResponse<GetBackgroundLayers.ResponseItem>>> BackgroundLayers([FromQuery] GetBackgroundLayers.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Gets listing of all map styles
        /// </summary>
        /// <returns>List of map styles</returns>
        [HttpGet]
        [Route("styles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetMapStyles.Response>> Styles()
        {
            var response = await _mediator.Send(new GetMapStyles.Query());
            return response.ToActionResult();
        }

    }
}
