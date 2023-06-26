using System;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TrapTypes;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TimeRegistrationCategory
{
    public static partial class GetAllTimeRegistrationCategories
    {
        [PublicAPI]
        public class Query : PagedQuery, IRequest<PagedResponse<GetTimeRegistrationCategory.Response>>
        {
            public string Keyword { get; set; } = String.Empty;

        }
    }
}
