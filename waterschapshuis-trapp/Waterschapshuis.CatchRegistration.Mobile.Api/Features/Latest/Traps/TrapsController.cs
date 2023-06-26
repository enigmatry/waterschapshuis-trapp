using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.TrapHistories;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Traps
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class TrapsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public TrapsController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get trap details for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetTrapDetails.TrapItem>> Get(Guid id)
        {
            var response = await _mediator.Send(GetTrapDetails.Query.ById(id));
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get trap details for multiple given trap ids
        /// </summary>
        /// <param name="query"> Trap query containing trap ids </param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetTrapDetails.TrapItem>>> GetMultiple([FromQuery] GetMultipleTrapDetails.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get trap details within bounding box
        /// </summary>
        /// <param name="query"> Trap query by bounding box </param>
        [HttpGet("get-traps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResponse<GetTrapDetails.TrapItem>>> GetTraps([FromQuery] GetTraps.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get current user Traps summary
        /// </summary>
        /// <param name="query"> Query indicates whether to include Trap details </param>
        [HttpGet("my-summary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetMySummary.Response>> GetCurrentUserSummary([FromQuery] GetMySummary.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get paged trap history entries
        /// </summary>
        /// <param name="query"> Query indicates for which trap to get histories </param>
        [HttpGet("histories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetTrapHistories.HistoryItem>>> GetHistories([FromQuery] GetTrapHistories.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates or updates a trap
        /// </summary>
        /// <param name="command">Trap data</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTrapDetails.TrapItem>> Post(TrapCreateOrUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.TrapId);
        }
        /// <summary>
        ///  Physically delete trap
        /// </summary>
        /// <param name="id">Trap id</param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _mediator.Send(TrapDelete.Command.Create(id));
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
