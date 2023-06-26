using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.Settings;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Settings
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
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
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<GetGeoServerSettings.Response>>
            GetGeoServerSettings()
        {
            GetGeoServerSettings.Response response =
                await _mediator.Send(new GetGeoServerSettings.Query());
            return response.ToActionResult();
        }
    }
}
