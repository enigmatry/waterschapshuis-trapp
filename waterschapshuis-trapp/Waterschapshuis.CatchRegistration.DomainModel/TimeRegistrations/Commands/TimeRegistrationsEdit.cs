using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public static partial class TimeRegistrationsEdit
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            private Guid? _userId = null!;
            private Guid? _rayonId;
            private bool _oneDayValidRange = false;

            /// <summary>
            /// GUID of the Sub Area Hour Square where time is registered
            /// </summary>
            public Guid? SubAreaHourSquareId { get; set; } = null!;

            /// <summary>
            /// First day of week this time registration applies to
            /// </summary>
            public DateTimeOffset StartDate { get; set; }

            /// <summary>
            /// Last day of week this time registration applies to
            /// </summary>
            public DateTimeOffset EndDate { get; set; }

            /// <summary>
            /// List of time registrations per day
            /// </summary>
            public IEnumerable<TimeRegistrationsOfDate> DaysOfWeek { get; set; } = Enumerable.Empty<TimeRegistrationsOfDate>();

            /// <summary>
            /// List of catches in that week
            /// </summary>
            public IEnumerable<CatchItem> Catches { get; set; } = Enumerable.Empty<CatchItem>();

            /// <summary>
            /// List of general time registrations per day
            /// </summary>
            public IEnumerable<TimeRegistrationGeneral> TimeRegistrationGeneralItems { get; set; } = Enumerable.Empty<TimeRegistrationGeneral>();

            public bool IsManagerEnteringDataForUser() => GetUserId() != null;

            public void SetUserId(Guid userId)
            {
                _userId = userId;
            }

            public Guid? GetUserId()
            {
                return _userId;
            }

            public void SetRayonId(Guid? rayonId)
            {
                _rayonId = rayonId;
            }

            public Guid? GetRayonId()
            {
                return _rayonId;
            }

            public void SetOneDayValidRange(bool oneDayValidRange)
            {
                _oneDayValidRange = oneDayValidRange;
            }

            public bool GetOneDayValidRange()
            {
                return _oneDayValidRange;
            }

            [PublicAPI]
            public class TimeRegistrationsOfDate
            {
                /// <summary>
                /// Day this time registration applies to
                /// </summary>
                public DateTimeOffset Date { get; set; }

                /// <summary>
                /// List of time registratios for current day
                /// </summary>
                public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();

                public static TimeRegistrationsOfDate Create(DateTimeOffset date, IEnumerable<Item> items)
                {
                    return new TimeRegistrationsOfDate
                    {
                        Date = date,
                        Items = items
                    };
                }
            }

            [PublicAPI]
            public class Item
            {
                /// <summary>
                /// GUID of this time registration
                /// </summary>
                public Guid? Id { get; set; } = null!;

                /// <summary>
                /// Day this record applies to
                /// </summary>
                public DateTimeOffset Date { get; set; }

                /// <summary>
                /// Sub Area guid
                /// </summary>
                public Guid SubAreaId { get; set; } = Guid.Empty;

                /// <summary>
                /// Hour Square guid
                /// </summary>
                public Guid HourSquareId { get; set; } = Guid.Empty;

                /// <summary>
                /// GUID of the trapping type the time applies to
                /// </summary>
                public Guid TrappingTypeId { get; set; } = Guid.Empty;

                /// <summary>
                /// Amount of hours
                /// </summary>
                public int Hours { get; set; }

                /// <summary>
                /// Amount of minutes
                /// </summary>
                public int Minutes { get; set; }

                /// <summary>
                /// Status of this time registration record
                /// </summary>
                public TimeRegistrationStatus Status { get; set; }

                public double GetHours()
                {
                    return Hours + (double)Minutes / 60;
                }

                public static Item Create(Guid? id,
                    DateTimeOffset date,
                    Guid subAreaId,
                    Guid hourSquareId,
                    Guid trappingTypeId,
                    int hours,
                    int minutes,
                    TimeRegistrationStatus status)
                {
                    return new Item
                    {
                        Id = id,
                        Date = date,
                        SubAreaId = subAreaId,
                        HourSquareId = hourSquareId,
                        TrappingTypeId = trappingTypeId,
                        Hours = hours,
                        Minutes = minutes,
                        Status = status
                    };
                }
            }
        }

        [PublicAPI]
        public class CatchItem
        {
            /// <summary>
            /// GUID of the catch
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Created on
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Catch type guid for this catch
            /// </summary>
            public Guid CatchTypeId { get; set; }

            /// <summary>
            /// Number of catches
            /// </summary>
            public int Number { get; set; }

            /// <summary>
            /// Status of this catch
            /// </summary>
            public CatchStatus Status { get; set; }

            /// <summary>
            /// Indicator if this is catch or by-catch
            /// </summary>
            public bool IsByCatch { get; set; }
        }
        [PublicAPI]
        public class TimeRegistrationGeneral
        {
            public Guid? Id { get; set; } = null!;
            public DateTimeOffset Date { get; set; }
            public Guid CategoryId { get; set; } = Guid.Empty;
            public TimeRegistrationStatus Status { get; set; }
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public double GetHours()
            {
                return Hours + (double)Minutes / 60;
            }

        }

        [PublicAPI]
        public class Result
        {
            public Guid UserId { get; set; }
            public Guid? RayonId { get; set; }
            public DateTimeOffset StartDate { get; set; }
            public DateTimeOffset EndDate { get; set; }
        }

        [UsedImplicitly]
        public class ItemValidator : AbstractValidator<Command.Item>
        {
            public ItemValidator()
            {
                RuleFor(x => x.Hours).LessThanOrEqualTo(24);
                RuleFor(x => x.Minutes).LessThan(60);
                RuleFor(x => (int)x.Status).GreaterThanOrEqualTo(1).LessThanOrEqualTo(3);
                RuleFor(x => new { x.Hours, x.Minutes })
                    .Must(x => HoursOrMinutesHaveValue(x.Hours, x.Minutes))
                    .WithMessage("De ingevulde tijd mag niet 00:00 zijn.");
            }

            private bool HoursOrMinutesHaveValue(int hours, int minutes)
            {
                if (hours > 0 || minutes > 0)
                    return true;
                return false;
            }
        }

        [UsedImplicitly]
        public class TimeRegistrationGeneralValidator : AbstractValidator<Command>
        {
            public TimeRegistrationGeneralValidator()
            {
                RuleFor(x => x.TimeRegistrationGeneralItems)
                    .Must(x =>
                        x.GroupBy(e => new { e.Date, e.CategoryId })
                            .All(e => e.Count() == 1)
                    )
                    .When(x => x.TimeRegistrationGeneralItems.Any())
                    .WithMessage(x => "De gekozen categorie bestaat al voor de huidige dag.");

                RuleFor(x => x.EndDate).NotNull()
                    .GreaterThan(x => x.StartDate)
                    .WithMessage(x => "EndDate must be bigger than StartDate");

                RuleFor(x => new { x.StartDate, x.EndDate, x.DaysOfWeek, x.TimeRegistrationGeneralItems })
                    .Must(x => IsValidDateRange(x.StartDate, x.EndDate, x.DaysOfWeek, x.TimeRegistrationGeneralItems))
                    .WithMessage(x => "Time registration date range does not fit to the DaysOfWeek dates");

                When(x => x.GetOneDayValidRange(), () =>
                {
                    RuleFor(x => x.EndDate)
                        .Must((x, y) => x.EndDate.Subtract(x.StartDate).Days == 1)
                        .WithMessage(x => "Time registration date range is invalid.");
                });
            }

            private bool IsValidDateRange(DateTimeOffset startDate,
                DateTimeOffset endDate,
                IEnumerable<Command.TimeRegistrationsOfDate> daysOfWeek,
                IEnumerable<TimeRegistrationGeneral> generalItems)
            {
                var isTimeRegistrationDateValid = true;
                var isTimeRegistrationGeneralDateValid = true;
                if (generalItems.Any())
                {
                    isTimeRegistrationGeneralDateValid = startDate <= generalItems.FirstOrDefault().Date
                                        && endDate >= generalItems.LastOrDefault().Date;
                }
                if (daysOfWeek.Any())
                {
                    var orderedDaysOfWeek = daysOfWeek.OrderBy(y => y.Date);
                    isTimeRegistrationDateValid = startDate <= orderedDaysOfWeek.FirstOrDefault().Date
                                        && endDate >= orderedDaysOfWeek.LastOrDefault().Date;
                }

                return isTimeRegistrationGeneralDateValid && isTimeRegistrationDateValid;
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command.TimeRegistrationsOfDate>
        {
            public Validator(ITimeProvider timeProvider)
            {
                RuleFor(x => x.Date.Date).LessThanOrEqualTo(timeProvider.Now.GetLastDayOfCurrentWeek().Date);
                RuleFor(x => x.Items)
                    .Must(x =>
                        x.GroupBy(e => new { e.SubAreaId, e.HourSquareId, e.TrappingTypeId })
                        .All(e => e.Count() == 1)
                        )
                    .When(x => x.Items.Any())
                    .WithMessage(x => "De gekozen combinatie Vanggebied / Deelgebied / Uurhok bestaat al voor de huidige dag.");

                RuleForEach(x => x.Items).SetValidator(new ItemValidator()).When(x => x.Items.Any());
            }
        }
    }
}
