using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Traps
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("traps")]
    public class TrapsController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Summary of trap
        /// </summary>
        /// <param name="mediator"></param>
        public TrapsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get paged Traps
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetTrap.TrapItem>>> GetAll(
            [FromQuery] int pageSize = 100,
            [FromQuery] int pageNumber = 1)
        {
            var response = await _mediator.Send(new GetTraps.Query());
            return await response.Traps.ToPagedActionResult(pageSize, pageNumber);
        }

        /// <summary>
        ///     Get Trap by id
        /// </summary>
        /// <param name="id">Trap Id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetTrap.TrapItem>> Get(Guid id)
        {
            var response = await _mediator.Send(new GetTrap.Query(id));
            return response.Trap.ToActionResult();
        }
    }
}
