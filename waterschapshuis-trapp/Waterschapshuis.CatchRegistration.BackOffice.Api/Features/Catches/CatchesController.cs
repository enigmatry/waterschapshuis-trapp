using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Catches
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class CatchesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CatchesController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get catch for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [Route("{id}")]
        [UserHasPermission(PolicyNames.BackOffice.MapContentRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationServices.Traps.GetCatchDetails.CatchItem>> Get(Guid id)
        {
            var response = await _mediator.Send(ApplicationServices.Traps.GetCatchDetails.Query.ById(id));
            return response.ToActionResult();
        }

        /// <summary>
        ///  Updates a catch
        /// </summary>
        /// <param name="command">Catch data</param>
        [HttpPut]
        [UserHasPermission(PolicyNames.BackOffice.MapContentWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationServices.Traps.GetCatchDetails.CatchItem>> Put(CatchCreateOrUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.CatchId);
        }

        /// <summary>
        ///  Physically delete catch
        /// </summary>
        /// <param name="id">Catch id</param>
        [HttpDelete]
        [Route("{id}")]
        [UserHasPermission(PolicyNames.BackOffice.MapContentWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(CatchDelete.Command.Create(id));
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
