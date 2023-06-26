using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    public static class UserUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of the user
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Indicator whether user is authorized for access
            /// </summary>
            [DisplayName(nameof(Authorized))] public bool Authorized { get; set; }

            /// <summary>
            /// Organization id of the organization user will be assigned to
            /// </summary>
            public Guid? OrganizationId { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            /// <summary>
            /// GUID of the user
            /// </summary>
            public Guid UserId { get; set; }

            public static Result CreateResult(Guid userId)
            {
                return new Result {UserId = userId};
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
        }
    }
}
