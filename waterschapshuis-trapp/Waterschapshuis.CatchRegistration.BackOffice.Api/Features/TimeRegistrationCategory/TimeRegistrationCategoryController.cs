using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TimeRegistrationCategory
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TimeRegistrationCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public TimeRegistrationCategoryController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }


        /// <summary>
        ///  Gets listing of all available time registration categories info
        /// </summary>
        /// <returns>List of time registration categories</returns>
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetTimeRegistrationCategory.Response>>> SearchTimeRegistrationCategories([FromQuery] GetAllTimeRegistrationCategories.Query query)
        {
            var response = await _mediator.Send(query);
            return response.ToActionResult();
        }

        /// <summary>
        ///  Creates or updates time registration category
        /// </summary>
        /// <param name="command">time registration category data</param>
        [HttpPost]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTimeRegistrationCategory.Response>> Post(TimeRegistrationCategoryCreateOrUpdate.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return await GetTimeRegistrationCategory(result.TimeRegistrationCategoryId);
        }

        /// <summary>
        ///     Get time registration category details for given id
        /// </summary>
        /// <param name="id">Id of the time registration category</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        public async Task<ActionResult<GetTimeRegistrationCategory.Response>> GetTimeRegistrationCategory(Guid id)
        {
            var response = await _mediator.Send(TimeRegistrationCategory.GetTimeRegistrationCategory.Query.ById(id));
            return response.ToActionResult();
        }

    }
}
