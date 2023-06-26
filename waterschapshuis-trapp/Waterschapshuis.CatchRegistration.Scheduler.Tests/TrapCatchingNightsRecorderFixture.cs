using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;
using Waterschapshuis.CatchRegistration.Scheduler.Services;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests
{
    [Category("integration")]
    public class TrapCatchingNightsRecorderFixture : SchedulerIntegrationFixtureBase
    {
        private CatchRegistrationDbContext _dbContext = null!;
        private ILogger<TrapCatchingNightsRecorder> _logger = null!;

        [SetUp]
        public void SetUp()
        {
            _dbContext = Resolve<CatchRegistrationDbContext>();
            _logger = Resolve<ILogger<TrapCatchingNightsRecorder>>();
        }

        [Test]
        public async Task TryRecord_UpdatesTrapCatchingNightsForCatchingTraps()
        {
            var updater = new TrapCatchingNightsRecorder(_logger, _dbContext);
            const int numberOfTraps = 2;
            var catchingTrapId = Guid.Parse("9EE473E4-E10F-4947-80EB-9BA4DA906504");
            var notCatchingTrapId = Guid.Parse("FDA67348-DEB4-42E2-87C7-5574716170FF");
            var removedTrapId = Guid.Parse("126E492E-9C20-413F-8E3A-F07FA8044C14");
            AddAndSaveChanges(
                (Trap) new TrapBuilder()
                    .WithStatus(TrapStatus.Catching)
                    .WithNumberOfTraps(numberOfTraps)
                    .WithRecordedOn(DateTimeOffset.Now)
                    .WithId(catchingTrapId),
                (Trap) new TrapBuilder()
                    .WithStatus(TrapStatus.NotCatching)
                    .WithNumberOfTraps(numberOfTraps)
                    .WithRecordedOn(DateTimeOffset.Now)
                    .WithId(notCatchingTrapId),
                (Trap) new TrapBuilder()
                    .WithStatus(TrapStatus.Removed)
                    .WithNumberOfTraps(numberOfTraps)
                    .WithRecordedOn(DateTimeOffset.Now)
                    .WithId(removedTrapId));

            await updater.TryRecord();

            var traps = QueryDbSkipCache<Trap>().Where(t =>
                t.Id == catchingTrapId || t.Id == notCatchingTrapId || t.Id == removedTrapId)
                .ToList();

            var catchingTrap = traps.Single(t => t.Id == catchingTrapId);
            catchingTrap.CatchingNights.Should().Be(numberOfTraps);

            var notCatchingTrap = traps.Single(t => t.Id == notCatchingTrapId);
            notCatchingTrap.CatchingNights.Should().Be(0);

            var removedTrap = traps.Single(t => t.Id == removedTrapId);
            removedTrap.CatchingNights.Should().Be(0);
        }
    }
}
