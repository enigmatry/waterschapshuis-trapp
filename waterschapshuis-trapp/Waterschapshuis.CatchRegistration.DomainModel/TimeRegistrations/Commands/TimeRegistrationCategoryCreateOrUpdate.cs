using System;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public static class TimeRegistrationCategoryCreateOrUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of the trap type
            /// </summary>
            public Guid? Id { get; set; }

            /// <summary>
            /// Name of the trap type
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// Indicator whether trap type is active
            /// </summary>
            public bool Active { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public Guid TimeRegistrationCategoryId { get; set; }

            public static Result CreateResult(Guid timeRegistrationCategoryId)
            {
                return new Result { TimeRegistrationCategoryId = timeRegistrationCategoryId };
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }
    }
}
