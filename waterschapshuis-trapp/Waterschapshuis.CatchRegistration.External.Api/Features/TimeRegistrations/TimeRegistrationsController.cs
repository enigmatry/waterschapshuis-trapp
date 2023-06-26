using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("time-registrations")]
    [UserHasPermission(PolicyNames.ExternalApi.UnlimitedAccess)]
    public class TimeRegistrationsController : Controller
    {
        private readonly IMediator _mediator;

        public TimeRegistrationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get paged time registrations
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetTimeRegistration.TimeRegistrationItem>>> GetAll(
            [FromQuery] int pageSize = 100,
            [FromQuery] int pageNumber = 1)
        {
            var response = await _mediator.Send(new GetTimeRegistrations.Query());
            return await response.TimeRegistrations.ToPagedActionResult(pageSize, pageNumber);
        }

        /// <summary>
        ///     Get time registration by id
        /// </summary>
        /// <param name="id">Time registration Id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetTimeRegistration.TimeRegistrationItem>> Get(Guid id)
        {
            var response = await _mediator.Send(new GetTimeRegistration.Query(id));
            return response.TimeRegistration.ToActionResult();
        }
    }
}
