using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Observations
{
    public static partial class GetMultipleObservationDetails
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<GetObservationDetails.ResponseItem>>
        {
            public IEnumerable<Guid> Ids { get; private set; } = new List<Guid>();

            public static Query Create(IEnumerable<Guid> observationIds)
            {
                return new Query { Ids = observationIds };
            }
        }
    }
}
