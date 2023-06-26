using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TrapTypes
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TrapTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public TrapTypesController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }


        /// <summary>
        ///  Gets listing of all available Trap types info
        /// </summary>
        /// <returns>List of Trap types</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetTrapType.Response>>> SearchTrapType([FromQuery] GetAllTrapTypes.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates or updates trap types
        /// </summary>
        /// <param name="command">Trap type data</param>
        [HttpPost]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTrapType.Response>> Post(TrapTypeCreateOrUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await GetTrapType(result.TrapTypeId);
        }

        /// <summary>
        ///     Get trap type details for given id
        /// </summary>
        /// <param name="id">Id of the trap type</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        public async Task<ActionResult<GetTrapType.Response>> GetTrapType(Guid id)
        {
            var response = await _mediator.Send(TrapTypes.GetTrapType.Query.ById(id));
            return response.ToActionResult();
        }

    }
}
