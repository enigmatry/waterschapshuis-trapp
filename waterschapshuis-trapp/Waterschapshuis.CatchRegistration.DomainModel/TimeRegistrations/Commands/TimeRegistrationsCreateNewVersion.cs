using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public partial class TimeRegistrationsCreateNewVersion
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            public List<SubAreaHourSquare> NextVersionSubAreaHourSquares { get; set; } = new List<SubAreaHourSquare>();

            public static Command Create(List<SubAreaHourSquare> subAreaHourSquares) =>
                new Command { NextVersionSubAreaHourSquares = subAreaHourSquares };
        }

        [PublicAPI]
        public class Result
        {
            public bool Succeed { get; private set; }
            public List<TimeRegistration> TimeRegistrations { get; private set; } = new List<TimeRegistration>();
            public List<string> ValidationMessages { get; private set; } = new List<string>();

            public static Result Create() =>
                new Result
                {
                    Succeed = true,
                    ValidationMessages = new List<string>(),
                    TimeRegistrations = new List<TimeRegistration>()
                };

            public void Invalidate()
            {
                Succeed = false;
                TimeRegistrations.Clear();
            }

            public void AddInfoMessage(string message) => ValidationMessages
                .Add($"{VersionRegionalLayoutImport.InfoPrefix} {DateTime.Now.ToString(VersionRegionalLayoutImport.DateTimeFormat)}: {message}");
            public void AddWarnMessage(string message) => ValidationMessages
                .Add($"{VersionRegionalLayoutImport.WarningPrefix} {DateTime.Now.ToString(VersionRegionalLayoutImport.DateTimeFormat)}: {message}");
            public void AddErrorMessage(string message) => ValidationMessages
                .Add($"{VersionRegionalLayoutImport.ErrorPrefix} {DateTime.Now.ToString(VersionRegionalLayoutImport.DateTimeFormat)}: {message}");

            public void AddOrUpdate(TimeRegistration value)
            {
                var existing = TryFindBy(value);
                if (existing == null)
                    TimeRegistrations.Add(value);
                else
                    existing.IncrementHours(value.Hours);
            }

            private TimeRegistration? TryFindBy(TimeRegistration timeRegistration) =>
                TimeRegistrations.AsQueryable()
                    .ExistingTimeRegistrationEntryEntity(
                        timeRegistration.UserId,
                        timeRegistration.SubAreaHourSquareId,
                        timeRegistration.TrappingTypeId,
                        timeRegistration.Date);
        }
    }
}
