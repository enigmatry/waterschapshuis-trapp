using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.Settings;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Settings
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class SettingsController : Controller
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Getting authorization settings for the GeoServer
        /// </summary>
        /// <returns>Authorization keys for backoffice and mobile user</returns>
        [HttpGet]
        [Route("geoserver")]
        public async Task<ActionResult<GetGeoServerSettings.Response>> GetGeoServerSettings()
        {
            GetGeoServerSettings.Response response =
                await _mediator.Send(new GetGeoServerSettings.Query());
            return response.ToActionResult();
        }
    }
}
