using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.FieldTest.Commands
{
    [UsedImplicitly]
    public class FieldTestCreateOrUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [Required]
            public Guid? Id { get; set; }
            public string Name { get; set; } = String.Empty;
            public string StartPeriod { get; set; } = String.Empty;
            public string EndPeriod { get; set; } = String.Empty;
            public Guid[] HourSquareIds { get; set; } = new Guid[0];
        }

        [PublicAPI]
        public class Result
        {
            public Guid FieldTestId { get; set; }

            public static Result CreateResult(Guid id)
            {
                return new Result { FieldTestId = id };
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
                RuleFor(x => x.StartPeriod)
                    .NotEmpty()
                    .Length(7)
                    .Matches(YearPeriod.PeriodRegEx)
                        .WithMessage("Start period must match yyyy-pp format");
                RuleFor(x => x.EndPeriod)
                    .NotEmpty()
                    .Length(7)
                    .Matches(YearPeriod.PeriodRegEx)
                        .WithMessage("End period must match yyyy-pp format");
            }

        }
    }
}
