using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands
{
    public static class ScheduledJobExecute
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [Required]
            public ScheduledJobName Name { get; set; }

            [Required]
            public ScheduledJobState State { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public bool Succeed { get; set; }
            public string Output { get; set; } = String.Empty;

            public static Result Create() =>
                new Result
                {
                    Succeed = true
                };

            public void Invalidate() => Succeed = false;

            public void AddInfoMessage(string message) =>
                Output += $"INFO {DateTime.Now.ToString(DateTimeFormat)}: {message}{Environment.NewLine}";

            public void AddWarnMessage(string message) =>
                Output += $"WARN {DateTime.Now.ToString(DateTimeFormat)}: {message}{Environment.NewLine}";

            public void AddErrorMessage(string message) =>
                Output += $"ERROR {DateTime.Now.ToString(DateTimeFormat)}: {message}{Environment.NewLine}";

            public void AddMessages(List<string> messages) =>
                Output += $"{String.Join(Environment.NewLine, messages)}{Environment.NewLine}";
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).IsInEnum();
                RuleFor(x => x.State).IsInEnum();
            }
        }
    }
}
