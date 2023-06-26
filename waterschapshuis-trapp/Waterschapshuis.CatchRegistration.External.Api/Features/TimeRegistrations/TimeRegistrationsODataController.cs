using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations
{
    [Route("odata")]
    [OpenApiIgnore]
    [UserHasPermission(PolicyNames.ExternalApi.UnlimitedAccess)]
    public class TimeRegistrationsODataController : ODataController
    {
        private readonly IMediator _mediator;

        public TimeRegistrationsODataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("TimeRegistrations")]
        public async Task<ActionResult<IQueryable<GetTimeRegistration.TimeRegistrationItem>>> GetTimeRegistrations()
        {
            var response = await _mediator.Send(new GetTimeRegistrations.Query());
            return response.TimeRegistrations.ToActionResult();
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("TimeRegistrations({id})")]
        public async Task<ActionResult<GetTimeRegistration.TimeRegistrationItem>> GetTimeRegistrationById([FromODataUri] Guid id)
        {
            var response = await _mediator.Send(new GetTimeRegistration.Query(id));
            return response.TimeRegistration.ToActionResult();
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("TimeRegistrations.fromYear(year={year})")]
        [ODataRoute("TimeRegistrations.fromYear(year={year},organization={organization})")]
        public async Task<ActionResult<IQueryable<GetTimeRegistration.TimeRegistrationItem>>> GetTimeRegistrationsFromYear(
            [FromODataUri] int year,
            [FromODataUri] string organization)
        {
            var response = await _mediator.Send(new GetTimeRegistrations.Query { CreatedOnYear = year, Organization = organization });
            return response.TimeRegistrations.ToActionResult();
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("TimeRegistrations.fromOrganization(organization={organization})")]
        [ODataRoute("TimeRegistrations.fromOrganization(organization={organization},year={year})")]
        public async Task<ActionResult<IQueryable<GetTimeRegistration.TimeRegistrationItem>>> GetTimeRegistrationsFromOrganization(
            [FromODataUri] string organization,
            [FromODataUri] int? year = null)
        {
            var response = await _mediator.Send(new GetTimeRegistrations.Query { Organization = organization, CreatedOnYear = year });
            return response.TimeRegistrations.ToActionResult();
        }
    }
}
