using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Traps
{
    [Route("odata")]
    [OpenApiIgnore]
    public class TrapsODataController : ODataController
    {
        private readonly IMediator _mediator;

        public TrapsODataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("traps")]
        public async Task<ActionResult<IQueryable<GetTrap.TrapItem>>> GetTraps()
        {
            var response = await _mediator.Send(new GetTraps.Query());
            return response.Traps.ToActionResult();
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("traps({id})")]
        public async Task<ActionResult<GetTrap.TrapItem>> GetTrapById([FromODataUri] Guid id)
        {
            var response = await _mediator.Send(new GetTrap.Query(id));
            return response.Trap.ToActionResult();
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("traps.fromYear(year={year})")]
        [ODataRoute("traps.fromYear(year={year},organization={organization})")]
        public async Task<ActionResult<IQueryable<GetTrap.TrapItem>>> GetTrapsFromYear(
            [FromODataUri] int year,
            [FromODataUri] string organization)
        {
            var response = await _mediator.Send(new GetTraps.Query { CreatedOnYear = year, Organization = organization });
            return response.Traps.ToActionResult();
        }

        [EnableQuery(PageSize = 10000)]
        [ODataRoute("traps.fromOrganization(organization={organization})")]
        [ODataRoute("traps.fromOrganization(organization={organization},year={year})")]
        public async Task<ActionResult<IQueryable<GetTrap.TrapItem>>> GetTrapsFromOrganization(
            [FromODataUri] string organization,
            [FromODataUri] int? year = null)
        {
            var response = await _mediator.Send(new GetTraps.Query { Organization = organization, CreatedOnYear = year });
            return response.Traps.ToActionResult();
        }
    }
}
