using System;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    public static class AnonymizeInactiveUsers
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            public DateTimeOffset InactiveBefore{ get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public int UsersAnonymized { get; }
            private Result(in int usersAnonymized)
            {
                UsersAnonymized = usersAnonymized;
            }

            public static Result CreateResult(int usersAnonymized)
            {
                return new Result(usersAnonymized);
            }
        }
    }
}
