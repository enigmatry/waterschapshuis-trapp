using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.TimeRegistrations
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
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
        ///     Get time registrations for the current user for the requested date in optional SubAreaHourSquare
        /// </summary>
        /// <param name="date"> The date of the requested time registrations </param>
        /// <param name="subAreaHourSquareId"> The Id of the SubAreaHourSquare for which to get time registration </param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrations.Response>> Get([FromQuery] DateTimeOffset date, [FromQuery] Guid? subAreaHourSquareId = null)
        {
            var response = await _mediator.Send(GetTimeRegistrations.Query.Create(date, subAreaHourSquareId));
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates, updates or deletes TimeRegistration data
        /// </summary>
        /// <param name="command"> TimeRegistration data for update </param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrations.Response>> Post([FromBody] TimeRegistrationsEdit.Command command)
        {
            command.SetOneDayValidRange(true);
            await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await Get(command.DaysOfWeek.FirstOrDefault().Date, command.SubAreaHourSquareId);
        }
    }
}
