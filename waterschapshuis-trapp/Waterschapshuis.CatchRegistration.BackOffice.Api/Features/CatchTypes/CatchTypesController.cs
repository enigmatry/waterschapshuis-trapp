using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.CatchTypes
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class CatchTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CatchTypesController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets listing of all available Catch types info
        /// </summary>
        /// <returns>List of Catch types</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetCatchType.Response>>> SearchCatchType([FromQuery] GetAllCatchTypes.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates or updates a catch type
        /// </summary>
        /// <param name="command">Catch type data</param>
        [HttpPost]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCatchType.Response>> Post(CatchTypeCreateOrUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.CatchTypeId);
        }

        /// <summary>
        ///     Get trap type details for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [Route("{id}")]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCatchType.Response>> Get(Guid id)
        {
            var response = await _mediator.Send(CatchTypes.GetCatchType.Query.ById(id));
            return response.ToActionResult();
        }


    }
}
