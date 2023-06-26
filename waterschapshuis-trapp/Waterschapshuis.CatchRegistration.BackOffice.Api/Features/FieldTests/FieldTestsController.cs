using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.FieldTests
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class FieldTestsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public FieldTestsController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets listing of all available field tests info
        /// </summary>
        /// <returns>List of Field tests</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetFieldTest.Response>>> SearchFieldTests([FromQuery] GetAllFieldTests.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates or updates field tests
        /// </summary>
        /// <param name="command">Field test data</param>
        [HttpPost]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetFieldTest.Response>> Post( FieldTestCreateOrUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.FieldTestId);
        }

        /// <summary>
        ///     Get field test details for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [Route("{id}")]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetFieldTest.Response>> Get(Guid id)
        {
            var response = await _mediator.Send(GetFieldTest.Query.ById(id));
            return response.ToActionResult();
        }


    }
}
