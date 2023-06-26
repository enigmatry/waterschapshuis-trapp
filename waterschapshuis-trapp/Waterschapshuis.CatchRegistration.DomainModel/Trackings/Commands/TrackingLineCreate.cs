using MediatR;
using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands
{
    public static class TrackingLineCreate
    {
        public class Command : IRequest<Result>
        {
            public DateTimeOffset Date { get; set; }
        }

        public class Result
        {
            public static Result CreateResult()
            {
                return new Result();
            }
        }
    }
}
