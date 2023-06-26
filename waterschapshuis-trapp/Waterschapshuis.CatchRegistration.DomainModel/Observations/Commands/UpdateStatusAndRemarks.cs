using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands
{
    [UsedImplicitly]
    public static class UpdateStatusAndRemarks
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [Required] public Guid Id { get; set; }
            public bool Archived { get; set; }
            public string? Remarks { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public Guid ObservationId { get; set; }

            public static Result CreateResult(Guid observationId)
            {
                return new Result {ObservationId = observationId};
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            private readonly IRepository<Observation> _repository;

            public Validator(IRepository<Observation> repository)
            {
                _repository = repository;
                RuleFor(x => x.Id)
                    .Must(x => !x.IsEmpty()).WithMessage("Observation id is not provided.")
                    .Must(EntityExists).WithMessage(x => $"Observation with id ${x.Id} is not found.");

                RuleFor(x => x.Remarks).MaximumLength(Observation.RemarkFieldMaxLength);
            }

            private bool EntityExists(Guid id)
            {
                return _repository.EntityExists(id);
            }
        }
    }
}
