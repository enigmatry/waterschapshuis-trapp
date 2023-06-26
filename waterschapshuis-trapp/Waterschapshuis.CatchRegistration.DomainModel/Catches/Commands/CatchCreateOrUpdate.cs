using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands
{
    [UsedImplicitly]
    public class CatchCreateOrUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of the catch
            /// </summary>
            [Required] public Guid Id { get; set; }

            /// <summary>
            /// Recorded on
            /// </summary>
            [Required] public DateTimeOffset RecordedOn { get; set; }

            /// <summary>
            /// Number of catches
            /// </summary>
            [Required] public int Number { get; set; }

            /// <summary>
            /// Catch status
            /// </summary>
            [Required] public CatchStatus Status { get; set; } = CatchStatus.Written;

            /// <summary>
            /// GUID of the trap the catch is registered on
            /// </summary>
            [Required] public Guid TrapId { get; set; }

            /// <summary>
            /// GUID of the catch type
            /// </summary>
            [Required] public Guid CatchTypeId { get; set; }

            /// <summary>
            /// When set to true, the catch will be removed from database 
            /// </summary>
            [Required] public bool MarkedForRemoval { get; set; }


            public static Command CreateFrom(Catch value) =>
                new Command
                {
                    Id = value.Id,
                    TrapId = value.TrapId,
                    CatchTypeId = value.CatchTypeId,
                    Number = value.Number,
                    Status = value.Status,
                    RecordedOn = value.RecordedOn
                };
        }

        [PublicAPI]
        public class Result
        {
            public Guid CatchId { get; set; }

            public static Result CreateResult(Guid catchId) => new Result { CatchId = catchId };
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).Must(x => x.NotEmpty())
                    .WithMessage("Vangst Id niet voorzien.");
                RuleFor(x => x.TrapId).Must(x => x.NotEmpty())
                    .WithMessage("Vangmiddel Id niet voorzien.");
                RuleFor(x => x.CatchTypeId).Must(x => x.NotEmpty())
                    .WithMessage("VangstType Id niet voorzien.");
                RuleFor(x => x.Status).IsInEnum();
                RuleFor(x => x.Number).GreaterThanOrEqualTo(0);
            }
        }
    }
}
