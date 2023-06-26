using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Catches
{
    [Route("odata")]
    [OpenApiIgnore]
    public class CatchesODataController : ODataController
    {
        private readonly IMediator _mediator;

        public CatchesODataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ODataRoute("catches")]
        [EnableQuery(PageSize = 10000)]
        public async Task<ActionResult<IQueryable<GetCatch.CatchItem>>> GetCatches()
        {
            var response = await _mediator.Send(new GetCatches.Query());
            return response.Catches.ToActionResult();
        }

        [HttpGet]
        [ODataRoute("catches({id})")]
        [EnableQuery(PageSize = 10000)]
        public async Task<ActionResult<GetCatch.CatchItem>> GetCatchById([FromODataUri] Guid id)
        {
            var response = await _mediator.Send(new GetCatch.Query(id));
            return response.Catch.ToActionResult();
        }

        [HttpGet]
        [ODataRoute("catches.fromYear(year={year})")]
        [ODataRoute("catches.fromYear(year={year},organization={organization})")]
        [EnableQuery(PageSize = 10000)]
        public async Task<ActionResult<IQueryable<GetCatch.CatchItem>>> GetCatchesFromYear(
            [FromODataUri] int year,
            [FromODataUri] string organization)
        {
            var request = new GetCatches.Query {CreatedOnYear = year, Organization = organization};
            var response = await _mediator.Send(request);

            return response.Catches.ToActionResult();
        }

        [HttpGet]
        [ODataRoute("catches.fromOrganization(organization={organization})")]
        [ODataRoute("catches.fromOrganization(organization={organization},year={year})")]
        [EnableQuery(PageSize = 10000)]
        public async Task<ActionResult<IQueryable<GetCatch.CatchItem>>> GetCatchesFromOrganization(
            [FromODataUri] string organization,
            [FromODataUri] int? year = null
        )
        {
            var request = new GetCatches.Query { Organization = organization, CreatedOnYear = year };
            var response = await _mediator.Send(request);

            return response.Catches.ToActionResult();
        }
    }
}
