using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands
{
    public static class SessionAccessTokenCreate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [Required] public string AccessToken { get; set; } = String.Empty;
        }

        [PublicAPI]
        public class Result 
        {
            public Guid SessionAccessTokenId { get; set; }

            public static Result Create(Guid id) => new Result { SessionAccessTokenId = id };
        }
    }
}
