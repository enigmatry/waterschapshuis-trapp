using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Traps
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TrapsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public TrapsController( IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Get trap details for given id
        /// </summary>
        /// <param name="id">GUID of the trap</param>
        [HttpGet]
        [Route("{id}")]
        [UserHasPermission(PolicyNames.BackOffice.MapContentRead)]
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
        [UserHasPermission(PolicyNames.BackOffice.MapContentRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetTrapDetails.TrapItem>>> GetMultiple([FromQuery] GetMultipleTrapDetails.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///  Updates trap
        /// </summary>
        /// <param name="command">Trap data</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [UserHasPermission(PolicyNames.BackOffice.MapContentWrite)]
        public async Task<ActionResult<GetTrapDetails.TrapItem>> Put(TrapUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.TrapId);
        }

        /// <summary>
        ///  Physically delete trap
        /// </summary>
        /// <param name="id">Trap id to be deleted</param>
        [HttpPost]
        [Route("{id}")]
        [UserHasPermission(PolicyNames.BackOffice.MapContentWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _mediator.Send(TrapDelete.Command.Create(id));
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
