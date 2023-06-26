using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Users;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
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
        ///     Gets listing of all available users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.UserRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetUsers.ResponseItem>>> Search([FromQuery] GetUsers.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get user details for given id
        /// </summary>
        /// <param name="id">GUID of the user</param>
        [HttpGet]
        [Route("{id}")]
        [UserHasPermission(PolicyNames.BackOffice.UserRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUserDetails.Response>> Get(Guid id)
        {
            var response = await _mediator.Send(GetUserDetails.Query.ById(id));
            return response.ToActionResult();
        }

        /// <summary>
        ///  Updates user data
        /// </summary>
        /// <param name="command">User data for authorized and organization</param>
        [HttpPost]
        [UserHasPermission(PolicyNames.BackOffice.UserWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUserDetails.Response>> Post(UserUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.UserId);
        }

        /// <summary>
        ///  Update user roles
        /// </summary>
        /// <param name="command">List of </param>
        [HttpPut("user-roles")]
        [UserHasPermission(PolicyNames.BackOffice.UserWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUserDetails.Response>> Put(UpdateUserRoles.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.UserId);
        }

        /// <summary>
        ///  Creates or updates user confidentiality
        /// </summary>
        /// <param name="command">User data about confidentiality confirmation</param>
        [HttpPost("update-confidentiality")]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
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
