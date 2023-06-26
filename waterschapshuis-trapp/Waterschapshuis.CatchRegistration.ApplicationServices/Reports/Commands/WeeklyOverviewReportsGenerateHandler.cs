using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Email;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports.Commands
{
    [UsedImplicitly]
    public class WeeklyOverviewReportsGenerateHandler : IRequestHandler<WeeklyOverviewReportsGenerate.Command,
        WeeklyOverviewReportsGenerate.Result>
    {
        private readonly ILogger<WeeklyOverviewReportsGenerateHandler> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
        private readonly IRepository<TimeRegistrationGeneral> _timeRegistrationGeneralRepository;
        private readonly IRepository<Catch> _catchRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly IMapper _mapper;
        private readonly ITemplatingEngine _templateEngine;
        private readonly IEmailClient _emailClient;
        private readonly IMappingParametrizationService _mappingParametrizationService;

        public WeeklyOverviewReportsGenerateHandler(
            ILogger<WeeklyOverviewReportsGenerateHandler> logger,
            IRepository<User> userRepository,
            ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
            IRepository<TimeRegistrationGeneral> timeRegistrationGeneralRepository,
            IRepository<Catch> catchRepository,
            ITimeProvider timeProvider,
            IMapper mapper,
            ITemplatingEngine templateEngine,
            IEmailClient emailClient,
            IMappingParametrizationService mappingParametrizationService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            _catchRepository = catchRepository;
            _timeProvider = timeProvider;
            _mapper = mapper;
            _templateEngine = templateEngine;
            _emailClient = emailClient;
            _mappingParametrizationService = mappingParametrizationService;
            _timeRegistrationGeneralRepository = timeRegistrationGeneralRepository;
        }

        public async Task<WeeklyOverviewReportsGenerate.Result> Handle(
            WeeklyOverviewReportsGenerate.Command request,
            CancellationToken cancellationToken)
        {
            var stopWatch = new Stopwatch();
            _logger.LogDebug($"Generating weekly overview reports started at {_timeProvider.Now}");

            var reportsToSend = await GenerateWeeklyOverviewReports(request.BackOfficeAppUrl);

            _logger.LogDebug(
                $"Generating weekly overview reports finished in {stopWatch.ElapsedMilliseconds}ms at {_timeProvider.Now}. " +
                $"Generate {reportsToSend.Count}");

            await _emailClient.SendBulkAsync(reportsToSend);

            return WeeklyOverviewReportsGenerate.Result.CreateResult(reportsToSend.Count);
        }

        private async Task<List<EmailMessage>> GenerateWeeklyOverviewReports(string backOfficeUrl)
        {
            var generatedReports = new List<EmailMessage>();

            var users = await _userRepository
                .QueryAll()
                .QueryByAuthorizedOnly()
                .QueryByRoleIds(Role.TrapperRoleId, Role.SeniorUserId).ToListAsync();

            _logger.LogInformation($"Found {users.Count} trappers/senior users");

            var previousWeekStartDate = _timeProvider.Now.MondayDateInWeekOfDate().Date.AddDays(-7);
            var previousWeekEndDate = _timeProvider.Now.MondayDateInWeekOfDate().Date;

            _logger.LogInformation($"Generating reports for period {previousWeekStartDate} - {previousWeekEndDate} ...");

            foreach (User user in users)
            {
                _logger.LogDebug($"Processing user: {user.Email}");

                var timeRegistrations = await GetTimeRegistrationsAll(user.Id, previousWeekStartDate, previousWeekEndDate);
                var groupedCatches = await GetCatchesGroupedByDayAndArea(user.Id, previousWeekStartDate, previousWeekEndDate);
                var (year, week) = previousWeekStartDate.GetIso8601WeekOfYear();
                var reportModel = WeeklyOverviewReportDataModel.Create(
                    week,
                    year,
                    $"{backOfficeUrl}/map",
                    $"{backOfficeUrl}/time-registration/personal/{year}/{week}",
                    timeRegistrations.OrderBy(x => x.Date),
                    groupedCatches
                    );

                var generatedReportBody =
                    await _templateEngine.RenderFromFileAsync("~/Reports/WeeklyOverviewReport.cshtml", reportModel);

                generatedReports.Add(new EmailMessage(
                    $"Samenvatting {reportModel.Year} week {reportModel.WeekNumber} - Vangstregistratie V3",
                    generatedReportBody,
                    new List<string> { user.Email },
                    new List<string>() // From is left empty because it will be set later from Smtp Settings
                ));
            }

            return generatedReports;
        }

        private async Task<IEnumerable<CatchesPerDay>> GetCatchesGroupedByDayAndArea(Guid userId,
            DateTime previousWeekStartDate,
            DateTime previousWeekEndDate)
        {
            var catches = await GetCatches(userId, previousWeekStartDate, previousWeekEndDate);

            return catches.GroupBy(c => c.RecordedOn.Date.Date,
                (day, items) => new CatchesPerDay(day,items));
        }

        private Task<List<GetCatchDetails.CatchItem>> GetCatches(
            Guid userId,
            DateTime previousWeekStartDate,
            DateTime previousWeekEndDate) =>
            _catchRepository
                .QueryAll()
                .QueryByUserCreatedId(userId)
                .QueryByDateRecordedRangeExclusiveEnd(previousWeekStartDate, previousWeekEndDate)
                .OrderBy(t => t.RecordedOn)
                .ProjectToWithMappingParameters<Catch, GetCatchDetails.CatchItem>(_mapper, _mappingParametrizationService)
                .ToListAsync();

        private Task<List<GetTimeRegistrations.Response.Item>> GetTimeRegistrations(
            Guid userId,
            DateTime previousWeekStartDate,
            DateTime previousWeekEndDate) =>
            _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .QueryByUser(userId)
                .QueryByDateRangeExclusiveEnd(previousWeekStartDate, previousWeekEndDate)
                .ProjectTo<GetTimeRegistrations.Response.Item>(_mapper.ConfigurationProvider)
                .ToListAsync();

        private Task<List<GetTimeRegistrations.Response.Item>> GetTimeRegistrationsGeneral(
           Guid userId,
           DateTime previousWeekStartDate,
           DateTime previousWeekEndDate) =>
           _timeRegistrationGeneralRepository
              .QueryAll()
              .QueryByUser(userId)
              .QueryByDateRangeExclusiveEnd(previousWeekStartDate, previousWeekEndDate)
              .ProjectTo<GetTimeRegistrations.Response.Item>(_mapper.ConfigurationProvider)
              .ToListAsync();

        private async Task<List<GetTimeRegistrations.Response.Item>> GetTimeRegistrationsAll(
          Guid userId,
          DateTime previousWeekStartDate,
          DateTime previousWeekEndDate)
        {
            var timeRegistrations = await GetTimeRegistrations(userId, previousWeekStartDate, previousWeekEndDate);
            timeRegistrations.AddRange(await GetTimeRegistrationsGeneral(userId, previousWeekStartDate, previousWeekEndDate));

            return timeRegistrations.OrderBy(t => t.Date).ToList();
        }
    }
}
