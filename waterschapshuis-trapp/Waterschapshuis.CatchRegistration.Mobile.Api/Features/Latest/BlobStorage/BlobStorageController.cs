using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.BlobStorage
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
    public class BlobStorageController : Controller
    {
        private readonly IMediator _mediator;

        public BlobStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets SAS key from the BLOB storage
        /// </summary>
        /// <returns>SAS key</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetBlobStorageSasKey.Response>> GetSasKey()
        {
            var response = await _mediator.Send(new GetBlobStorageSasKey.Query());
            return response.ToActionResult();
        }
    }
}
