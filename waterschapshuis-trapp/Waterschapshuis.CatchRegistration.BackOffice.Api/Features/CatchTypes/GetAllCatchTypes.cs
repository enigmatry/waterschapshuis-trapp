using System;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.CatchTypes
{
    public static partial class GetAllCatchTypes
    {
        [PublicAPI]
        public class Query : PagedQuery, IRequest<PagedResponse<GetCatchType.Response>>
        {
            public string Keyword { get; set; } = String.Empty;
        }
    }
}
