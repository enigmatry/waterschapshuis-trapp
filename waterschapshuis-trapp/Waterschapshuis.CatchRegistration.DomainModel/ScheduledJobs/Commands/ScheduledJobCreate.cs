using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands
{
    public static class ScheduledJobCreate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [Required] 
            public ScheduledJobName Name { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public Guid Id { get; set; }
            public static Result Create(Guid id) => new Result { Id = id};
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IRepository<ScheduledJob> jobRepository)
            {
                RuleFor(x => x.Name).IsInEnum();
                RuleFor(x => x)
                    .Must(x => jobRepository.QueryAll().AllowCreatingJob(x.Name))
                    .WithMessage("Job execution is already in progress.");
            }
        }
    }
}
