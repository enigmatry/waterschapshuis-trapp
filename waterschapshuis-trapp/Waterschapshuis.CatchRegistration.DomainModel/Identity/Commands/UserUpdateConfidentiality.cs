using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    public static class UserUpdateConfidentiality
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of the user
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Indicator whether user agreed with organization policies
            /// </summary>
            [DisplayName(nameof(ConfidentialityConfirmed))] public bool ConfidentialityConfirmed { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public Guid UserId { get; set; }

            public static Result CreateResult(Guid userId)
            {
                return new Result {UserId = userId};
            }
        }
    }
}
