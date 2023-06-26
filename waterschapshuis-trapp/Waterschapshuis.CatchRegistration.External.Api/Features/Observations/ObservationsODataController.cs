using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Observations
{
    [Route("odata")]
    [OpenApiIgnore]
    public class ObservationsODataController : ODataController
    {
        private readonly IMediator _mediator;

        public ObservationsODataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery(PageSize = 10000)]
        [ODataRoute("observations")]
        public async Task<ActionResult<IQueryable<GetObservation.ObservationItem>>> GetObservations()
        {
            var response = await _mediator.Send(new GetObservations.Query());
            return response.Observations.ToActionResult();
        }

        [HttpGet]
        [EnableQuery(PageSize = 10000)]
        [ODataRoute("observations({id})")]
        public async Task<ActionResult<GetObservation.ObservationItem>> GetObservationById([FromODataUri] Guid id)
        {
            var response = await _mediator.Send(new GetObservation.Query(id));
            return response.Observation.ToActionResult();
        }

        [HttpGet]
        [EnableQuery(PageSize = 10000)]
        [ODataRoute("observations.fromYear(year={year})")]
        [ODataRoute("observations.fromYear(year={year},organization={organization})")]
        public async Task<ActionResult<IQueryable<GetObservation.ObservationItem>>> GetObservationsFromYear(
            [FromODataUri] int year,
            [FromODataUri] string organization)
        {
            var response = await _mediator.Send(new GetObservations.Query {CreatedOnYear = year, Organization = organization});
            return response.Observations.ToActionResult();
        }

        [HttpGet]
        [EnableQuery(PageSize = 10000)]
        [ODataRoute("observations.fromOrganization(organization={organization})")]
        [ODataRoute("observations.fromOrganization(organization={organization},year={year})")]
        public async Task<ActionResult<IQueryable<GetObservation.ObservationItem>>> GetObservationsFromOrganization(
            [FromODataUri] string organization,
            [FromODataUri] int? year = null)
        {
            var response = await _mediator.Send(new GetObservations.Query { Organization = organization, CreatedOnYear = year });
            return response.Observations.ToActionResult();
        }
    }
}
