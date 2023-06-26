using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.Observations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Observations
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class ObservationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ObservationsController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Get observations for given observation ids
        /// </summary>
        /// <param name="query"> Observation query containing observations Ids </param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetObservationDetails.ResponseItem>>> GetMultiple([FromQuery] GetMultipleObservationDetails.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }


        /// <summary>
        ///     Get observations within bounding box
        /// </summary>
        /// <param name="query"> Observation query by bounding box </param>
        [HttpGet("get-observations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResponse<GetObservationDetails.ResponseItem>>> GetObservations([FromQuery] GetObservations.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }
        
        /// <summary>
        /// Get observation details by id
        /// </summary>
        /// <param name="id">GUID of observation</param>
        /// <returns>Observation details.</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetObservationDetails.ResponseItem>> Get(Guid id)
        {
            var response = await _mediator.Send(GetObservationDetails.Query.ById(id));
            return response.ToActionResult();
        }

        /// <summary>
        /// Creates an observation
        /// </summary>
        /// <param name="command">Observation data to be created</param>
        /// <returns>Observation details</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetObservationDetails.ResponseItem>> Update(ObservationUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.ObservationId);
        }
    }
}
