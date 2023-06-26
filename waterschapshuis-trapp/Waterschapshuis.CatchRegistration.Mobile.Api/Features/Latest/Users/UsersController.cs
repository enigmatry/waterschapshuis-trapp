using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.Users;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Users
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UsersController(
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get user for given id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUserDetails.Response>> Get(Guid id)
        {
            var response = await _mediator.Send(GetUserDetails.Query.ById(id));
            return response.ToActionResult();
        }

        /// <summary>
        ///  Updates confidentiality property
        /// </summary>
        /// <param name="command">User data about confidentiality confirmation</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUserDetails.Response>> UpdateConfidentiality(UserUpdateConfidentiality.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.UserId);
        }

    }
}
