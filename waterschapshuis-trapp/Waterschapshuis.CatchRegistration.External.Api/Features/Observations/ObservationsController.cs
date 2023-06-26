using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Observations
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("observations")]
    public class ObservationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ObservationsController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get paged Observations
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetObservation.ObservationItem>>> GetAll(
            [FromQuery] int pageSize = 100,
            [FromQuery] int pageNumber = 1)
        {
            var response = await _mediator.Send(new GetObservations.Query());
            return await response.Observations.ToPagedActionResult(pageSize, pageNumber);
        }

        /// <summary>
        ///     Get Observation by id
        /// </summary>
        /// <param name="id">Observation Id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetObservation.ObservationItem>> Get(Guid id)
        {
            var response = await _mediator.Send(new GetObservation.Query(id));
            return response.Observation.ToActionResult();
        }

        /// <summary>
        ///     Update the observation
        /// </summary>
        /// <param name="command">Json containing id of an observation, archived (false,true) and remarks as string</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetObservation.ObservationItem>> Update(UpdateStatusAndRemarks.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.ObservationId);
        }
    }
}
