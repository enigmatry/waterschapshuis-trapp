using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Areas
{
    public static partial class GetAreaEntities
    {
        [PublicAPI]
        public class CatchAreaQuery : QueryBase
        {
            public bool FilterByOrganization { get; set; }
            public Guid? RayonId { get; set; } = null!;
        }

        [PublicAPI]
        public class SubAreaQuery : QueryBase
        {
            public Guid? CatchAreaId { get; set; } = null!;
        }

        [PublicAPI]
        public class HourSquareQuery : QueryBase
        {
            public Guid? SubAreaId { get; set; } = null!;
        }

        public abstract class QueryBase : IRequest<Response>
        {
            public string NameFilter { get; set; } = null!;
        }

        [PublicAPI]
        public class Response
        {
            public IEnumerable<NamedEntity.Item> Items { get; set; } = Enumerable.Empty<NamedEntity.Item>();
        }
    }
}
