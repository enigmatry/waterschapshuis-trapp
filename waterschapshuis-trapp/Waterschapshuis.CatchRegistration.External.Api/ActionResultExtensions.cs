using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;

namespace Waterschapshuis.CatchRegistration.External.Api
{
    public static class ActionResultExtensions
    {
        private const int MaxPageSize = 10000;

        public static async Task<ActionResult<PagedResponse<T>>> ToPagedActionResult<T>(this IQueryable<T>? queryable, int pageSize, int pageNumber) 
            where T : class
        {
            if (pageNumber <= 0)
                return new BadRequestObjectResult("Page number should be larger then 0.");

            if (pageSize > MaxPageSize)
                return new BadRequestObjectResult($"Page size should be less then {MaxPageSize}.");

            if (queryable == null)
            {
                return new NotFoundResult();
            }

            var pagedResponse = await queryable.ToPagedResponse(pageNumber, pageSize);
            return new OkObjectResult(pagedResponse);
        }
    }
}
