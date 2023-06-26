using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Catches
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("catches")]
    public class CatchesController : Controller
    {
        private readonly IMediator _mediator;

        public CatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get paged Catches
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetCatch.CatchItem>>> GetAll(
            [FromQuery] int pageSize = 100,
            [FromQuery] int pageNumber = 1)
        {
            var response = await _mediator.Send(new GetCatches.Query());
            return await response.Catches.ToPagedActionResult(pageSize, pageNumber);
        }

        /// <summary>
        ///     Get Catch by id
        /// </summary>
        /// <param name="id">Catch Id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCatch.CatchItem>> Get(Guid id)
        {
            var response = await _mediator.Send(new GetCatch.Query(id));
            return response.Catch.ToActionResult();
        }
    }
}
