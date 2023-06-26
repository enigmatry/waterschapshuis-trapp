using System;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TrapTypes
{
    public static partial class GetAllTrapTypes
    {
        [PublicAPI]
        public class Query : PagedQuery, IRequest<PagedResponse<GetTrapType.Response>>
        {
            public string Keyword { get; set; } = String.Empty;

        }
    }
}
