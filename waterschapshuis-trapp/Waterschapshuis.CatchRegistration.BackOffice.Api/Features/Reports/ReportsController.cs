using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data.ResponseModel;
using NSwag.Annotations;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates.Commands;
using System;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class ReportsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ReportsController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Gets listing of all Catches per Organization
        /// </summary>
        /// <returns>List of catches per organizations</returns>
        [OpenApiIgnore]
        [HttpGet]
        [UserHasPermission(PolicyNames.BackOffice.ReportReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LoadResult>> GetDevExtremeReport([FromQuery] GetDevExtremeReport.Query query)
        {
            var response = await _mediator.Send(query);
            return response;
        }


        /// <summary>
        /// Gets catches and hours prediction for four seasons
        /// </summary>
        /// <param name="query">List per hoursquare amount of catched and hours for all seasons</param>
        /// <returns></returns>
        [HttpGet("prediction")]
        [UserHasPermission(PolicyNames.BackOffice.ReportReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetPrediction.Response>> GetPrediction([FromQuery] GetPrediction.Query query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        ///     Gets all active and not exported report templates
        /// </summary>
        /// <returns>List of all active and not exported report templates</returns>
        [HttpGet("templates")]
        [UserHasPermission(PolicyNames.BackOffice.ReportReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetReportTemplates.Response>> GetReportTemplates()
        {
            var response = await _mediator.Send(new GetReportTemplates.Query());
            return response.ToActionResult();
        }

        /// <summary>
        ///     Gets report template by Id
        /// </summary>
        /// <returns>Active report template</returns>
        [HttpGet("templates/{id}")]
        [UserHasPermission(PolicyNames.BackOffice.ReportReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetReportTemplate.Response>> GetReportTemplate(Guid id)
        {
            var response = await _mediator.Send(new GetReportTemplate.Query { TemplateId = id });
            return response.ToActionResult();
        }

        /// <summary>
        ///     Creates report template for export
        /// </summary>
        /// <param name="command">Origin report template Uri, export report title and fields selection</param>
        /// <returns>Report template for export</returns>
        [HttpPost("templates")]
        [UserHasPermission(PolicyNames.BackOffice.ReportReadWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateReportTemplateExport.Result>> CreateReportTemplate(CreateReportTemplateExport.Command command)
        {
            var result = await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
