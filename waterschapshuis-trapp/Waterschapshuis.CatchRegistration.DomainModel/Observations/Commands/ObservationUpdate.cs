using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands
{
    [UsedImplicitly]
    public class ObservationUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of the observation
            /// </summary>
            [Required] public Guid Id { get; set; }

            /// <summary>
            /// Observation type (schade, overig)
            /// </summary>
            public ObservationType Type { get; set; }

            /// <summary>
            /// Indicator whether observation is archived
            /// </summary>
            public bool Archived { get; set; }

            /// <summary>
            /// Remarks for this observation
            /// </summary>
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
                    .Must(EntityExists)
                    .WithMessage(x => $"Observation with id ${x.Id} is not found.");

                RuleFor(x => (int)x.Type).GreaterThanOrEqualTo(1).LessThanOrEqualTo(2);

                RuleFor(x => x.Remarks).MaximumLength(Observation.RemarkFieldMaxLength);
            }

            private bool EntityExists(Guid id)
            {
                return _repository.EntityExists(id);
            }
        }
    }
}
