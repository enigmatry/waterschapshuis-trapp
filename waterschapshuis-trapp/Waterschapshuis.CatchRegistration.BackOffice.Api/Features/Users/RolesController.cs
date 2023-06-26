using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class RolesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public RolesController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Gets listing of all existing Roles
        /// </summary>
        /// <returns>List of Roles</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.RoleRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRoles.Response>> GetAllRoles([FromQuery] GetRoles.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///     Updates Roles Permissions
        /// </summary>
        /// <returns>List of Roles With Permissions</returns>
        [HttpPut]
        [UserHasPermission(PolicyNames.BackOffice.RoleWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetRoles.Response>> UpdateRolesPermissions(UpdateRolesPermissions.Command command)
        {
            await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await GetAllRoles(new GetRoles.Query());
        }
    }
}
