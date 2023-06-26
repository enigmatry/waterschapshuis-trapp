using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Observations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Observations
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
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
        [UserHasPermission(PolicyNames.BackOffice.MapContentRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetObservationDetails.ResponseItem>>> GetMultiple([FromQuery] GetMultipleObservationDetails.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        /// Get  details for a specific observation
        /// </summary>
        /// <param name="id">GUID of a observation</param>
        /// <returns>Observation details</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.MapContentRead)]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetObservationDetails.ResponseItem>> Get(Guid id)
        {
            var response = await _mediator.Send(GetObservationDetails.Query.ById(id));
            return response.ToActionResult();
        }

        /// <summary>
        /// Updates an observation
        /// </summary>
        /// <param name="command">A command for archiving observation or updating remarks</param>
        /// <returns>Updated details of the observation</returns>
        [HttpPut]
        [UserHasPermission(PolicyNames.BackOffice.MapContentWrite)]
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
