using MediatR;
using System;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public static class AutocompleteRegistrationDataAfterPeriod
    {
        public class Command : IRequest<Result>
        {
            public DateTimeOffset Date { get; set; }

            public CatchStatus CatchStatus { get; set; }
            public TimeRegistrationStatus TimeRegistrationStatus { get; set; }
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
