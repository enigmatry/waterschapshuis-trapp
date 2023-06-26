using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;
using Waterschapshuis.CatchRegistration.Scheduler.Jobs;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests.Jobs
{
    [Category("integration")]
    public class RemoveSessionsJobFixture : SchedulerIntegrationFixtureBase
    {
        private RemoveSessionsJob _removeSessionsJob = null!;
        private IJobExecutionContext _jobContext = null!;

        private readonly Guid _invalidBackOfficeSessionId = new Guid("3ba435f5-7979-4998-ac1e-42188c68d0ec");
        private readonly Guid _invalidMobileSessionId = new Guid("e7d160bd-2418-416f-8ee6-733bb0e6399d");
        private readonly Guid _invalidExternalApiSessionId = new Guid("4fce334b-4ab4-41bd-b229-5abc26a5ad9e");

        [SetUp]
        public void SetUp()
        {
            _removeSessionsJob = Resolve<RemoveSessionsJob>();
            _jobContext = A.Fake<IJobExecutionContext>();

            var currentUser = QueryDbSkipCache<User>()
                .Include(u => u.UserSessions)
                .ThenInclude(us => us.AccessTokens)
                .QueryByEmail(TestPrincipal.TestUserEmail)
                .Single();

            UserSession invalidBackOfficeSession = new UserSessionBuilder()
                .WithId(_invalidBackOfficeSessionId)
                .WithCreatedBy(currentUser)
                .WithOrigin(UserSessionOrigin.BackOfficeApi)
                .WithExpiresOn(DateTimeOffset.Now.AddDays(-5))
                .WithAccessTokens(SessionAccessToken.Create(AccessToken.Create("TOKEN"), _invalidBackOfficeSessionId));
            UserSession invalidMobileSession = new UserSessionBuilder()
                .WithId(_invalidMobileSessionId)
                .WithCreatedBy(currentUser)
                .WithOrigin(UserSessionOrigin.MobileApi)
                .WithExpiresOn(DateTimeOffset.Now.AddMinutes(15))
                .WithAccessTokens(SessionAccessToken.Create(AccessToken.Create("TOKEN"), _invalidMobileSessionId).WithExpired());
            UserSession invalidExternalApiSession = new UserSessionBuilder()
                .WithId(_invalidExternalApiSessionId)
                .WithCreatedBy(currentUser)
                .WithOrigin(UserSessionOrigin.ExternalApi)
                .WithExpiresOn(DateTimeOffset.Now.AddMinutes(15));

            AddAndSaveChanges(
                invalidBackOfficeSession,
                invalidMobileSession,
                invalidExternalApiSession
            );
        }

        [Test]
        public async Task Execute_Skip3Remove3()
        {
            SetCreatedOnForInvalidSessions(DateTimeOffset.Now.AddYears(-10));

            QueryDbSkipCache<UserSession>().Count().Should()
                .Be(6, "3 valid sessions assigned to test user by default + 3 invalid sessions");

            await _removeSessionsJob.Execute(_jobContext);

            var sessions = QueryDbSkipCache<UserSession>().Include(x => x.AccessTokens).ToList();
            sessions.Count.Should()
                .Be(3, "3 valid sessions assigned to test user by default - 3 invalid ones (removed)");
            sessions.All(x => x.IsValid()).Should().BeTrue();
            sessions.All(x =>
                !new[] {
                    _invalidBackOfficeSessionId,
                    _invalidMobileSessionId,
                    _invalidExternalApiSessionId
                }.Contains(x.Id)
            )
            .Should().BeTrue();
        }

        private void SetCreatedOnForInvalidSessions(DateTimeOffset createdOn)
        {
            QueryDb<UserSession>()
                .Where(x =>
                    new[] {
                        _invalidBackOfficeSessionId,
                        _invalidMobileSessionId,
                        _invalidExternalApiSessionId
                    }.Contains(x.Id)
                )
                .ToList()
                .ForEach(x => x.SetCreated(createdOn, x.CreatedById));
            SaveChanges();
        }
    }
}
