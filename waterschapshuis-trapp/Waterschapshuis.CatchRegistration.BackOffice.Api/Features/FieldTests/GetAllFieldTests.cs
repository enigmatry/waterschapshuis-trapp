using System;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.FieldTests
{
    public static partial class GetAllFieldTests
    {
        [PublicAPI]
        public class Query : PagedQuery, IRequest<PagedResponse<GetFieldTest.Response>>
        {
            public string Keyword { get; set; } = String.Empty;
        }
    }
}
