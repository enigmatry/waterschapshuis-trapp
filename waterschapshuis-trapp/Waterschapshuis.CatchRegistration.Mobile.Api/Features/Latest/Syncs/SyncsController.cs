using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Syncs
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class SyncsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public SyncsController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///  Sync tracking locations
        /// </summary>
        /// <param name="command">Tracking locations data</param>
        [HttpPost]
        [Route("tracking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrackingSync.Result>> Tracking(TrackingSync.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        /// <summary>
        ///  Sync observation
        /// </summary>
        /// <param name="command">Observation data</param>
        [HttpPost]
        [Route("observation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ObservationSync.Result>> Observation(ObservationSync.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
