using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public static partial class GetMultipleTrapDetails
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<GetTrapDetails.TrapItem>>
        {
            public IEnumerable<Guid> Ids { get; private set; } = new List<Guid>();

            public static Query Create(IEnumerable<Guid> trapIds)
            {
                return new Query() { Ids = trapIds };
            }
        }
    }
}
