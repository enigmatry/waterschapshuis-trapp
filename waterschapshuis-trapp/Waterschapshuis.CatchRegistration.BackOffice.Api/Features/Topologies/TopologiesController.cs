using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.ScheduledJobs;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Topologies
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class TopologiesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICsvService _csvService;

        public TopologiesController(IMediator mediator, ICsvService csvService, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _csvService = csvService;
        }

        /// <summary>
        ///     Returns all version regional layouts
        /// </summary>
        [HttpGet]
        [Route("version-regional-layout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<ListResponse<GetVersionRegionalLayouts.VersionRegionalLayoutResponse>>> GetAllVersionRegionalLayouts()
        {
            return await _mediator.Send(new GetVersionRegionalLayouts.Query());
        }

        /// <summary>
        ///     Returns job info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("job-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        public async Task<ActionResult<GetScheduledJobs.Response>> GetScheduledJobInfo(ScheduledJobName name)
        {
            var response = await _mediator.Send(GetScheduledJobs.Query.ByJobName(name));
            return response.ToActionResult();
        }

        /// <summary>
        ///     Returns version regional layout import info, if exists
        /// </summary>
        [HttpGet]
        [Route("version-regional-layout-import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        public async Task<ActionResult<GetVersionRegionalLayoutImport.Response>> GetVersionRegionalLayoutImport()
        {
            return await _mediator.Send(new GetVersionRegionalLayoutImport.Query());
        }

        /// <summary>
        ///     Export current SubAreas topology configurations in CSV format
        /// </summary>
        [HttpGet]
        [Route("version-regional-layout/export")]
        [SwaggerResponse(typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        public async Task<FileStreamResult> ExportTopologies()
        {
            var response = await _mediator.Send(new GetSubAreaCsvRecords.Query());
            var csvBytes = _csvService.AsBytes<SubAreaCsvRecord, SubAreaCsvRecrodMap>(response.Items);
            return new FileStreamResult(new MemoryStream(csvBytes), "text/csv");
        }

        /// <summary>
        ///     Import new SubAreas topology configurations in CSV format
        /// </summary>
        [HttpPost]
        [Route("version-regional-layout/import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        public ActionResult ImportTopologies([FromBody] ImportVersionRegionalLayout command)
        {
            var csvRecords = _csvService
                .AsCsvRecords<SubAreaCsvRecord, SubAreaCsvRecrodMap>(command.File);
            try
            {
                return Ok("Request submitted successfully");
            }
            finally
            {
                Response.OnCompleted(async () =>
                {
                    await _mediator.Send(VersionRegionalLayoutCreate.Command.Create(command.Name, csvRecords));
                });
            }
        }

        [HttpPost]
        [Route("calculate-km-waterways")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserHasPermission(PolicyNames.BackOffice.Management)]
        public async Task<ActionResult<GetScheduledJobs.Response>> CalculateKmWaterways()
        {
            await _mediator.Send(new ScheduledJobCreate.Command
            {
                Name = ScheduledJobName.CalculatingKmWaterways
            });
            await _unitOfWork.SaveChangesAsync();
            return await GetScheduledJobInfo(ScheduledJobName.CalculatingKmWaterways);
        }
    }
}
