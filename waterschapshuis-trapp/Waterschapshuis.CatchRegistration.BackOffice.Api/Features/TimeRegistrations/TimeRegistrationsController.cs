using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TimeRegistrations
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TimeRegistrationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public TimeRegistrationsController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get time registrations for the current user for the requested period
        /// </summary>
        /// <param name="startDate"> The start date of the requested time registrations </param>
        /// <param name="endDate"> The end date of the requested time registrations </param>
        /// <param name="userId"> User id to get time registrations for </param>
        /// <param name="rayonId"> Rayon id where user registered time </param>
        [HttpGet]
        [Route("management")]
        [UserHasPermission(PolicyNames.BackOffice.TimeRegistrationManagementReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrationsOfWeek.Response>> GetForUser([FromQuery] DateTimeOffset startDate, DateTimeOffset endDate, Guid userId, Guid? rayonId)
        {
            var response = await _mediator.Send(GetTimeRegistrationsOfWeek.Query.Create(startDate, endDate, userId, rayonId));
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get time registrations for the current user for the requested period
        /// </summary>
        /// <param name="startDate"> The start date of the requested time registrations </param>
        /// <param name="endDate"> The end date of the requested time registrations </param>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.TimeRegistrationPersonalReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrationsOfWeek.Response>> Get([FromQuery] DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var response = await _mediator.Send(GetTimeRegistrationsOfWeek.Query.Create(startDate, endDate, null, null));
            return response.ToActionResult();
        }

        /// <summary>
        ///     Get time registration per rayon
        /// </summary>
        /// <param name="startDate"> The start date of the requested time registrations </param>
        /// <param name="endDate"> The end date of the requested time registrations </param>
        [HttpGet]
        [Route("users-per-rayon")]
        [UserHasPermission(PolicyNames.BackOffice.TimeRegistrationManagementReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrationPerRayon.Response>> GetUsersWhoHaveRegisteredTimePerRayon([FromQuery] DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var response = await _mediator.Send(GetTimeRegistrationPerRayon.Query.Create(startDate, endDate));
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates, updates or deletes TimeRegistration data
        /// </summary>
        /// <param name="command"> TimeRegistration data for update </param>
        [HttpPost]
        [UserHasPermission(PolicyNames.BackOffice.TimeRegistrationPersonalReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrationsOfWeek.Response>> Post(TimeRegistrationsEdit.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(result.StartDate, result.EndDate);
        }

        /// <summary>
        ///  Creates, updates or deletes TimeRegistration data
        /// </summary>
        /// <param name="userId"> id of user for whom to edit time registrations </param>
        /// <param name="rayonId"> id of rayon where user registered hours </param>
        /// <param name="command"> TimeRegistration data for update </param>
        [HttpPost]
        [Route("management")]
        [UserHasPermission(PolicyNames.BackOffice.TimeRegistrationManagementReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrationsOfWeek.Response>> PostForUser([FromQuery] Guid userId, [FromQuery] Guid? rayonId, TimeRegistrationsEdit.Command command)
        {
            if (userId == Guid.Empty)
            {
                return NotFound();
            }
            command.SetUserId(userId);
            command.SetRayonId(rayonId);
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await GetForUser(result.StartDate, result.EndDate, result.UserId, result.RayonId);
        }
    }
}
