using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Reports.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests
{
    [Category("integration")]
    public class WeeklyOverviewReportsGenerateFixture : SchedulerIntegrationFixtureBase
    {
        private IMediator _mediator = null!;
        private Guid[] _roleIdsToSendWeekReports = null!;
        private Guid[] _roleIdsNotToSendWeekReports = null!;

        [SetUp]
        public void SetUp()
        {
            _mediator = Resolve<IMediator>();
            _roleIdsToSendWeekReports = new Guid[] {
                Role.TrapperRoleId, 
                Role.SeniorUserId 
            };
            _roleIdsNotToSendWeekReports = new Guid[] { 
                Role.ExternalPrivateInterfaceId, 
                Role.ExternalPublicInterfaceId,
                Role.MaintainerRoleId,
                Role.NationalApplicationManagerId,
                Role.NationalReporterId,
                Role.RegionsApplicationManagerId
            };
        }

        [Test]
        public async Task ForAuthorizedUsers_WeeklyOverviewReport_ShouldSendAnEmail()
        {
            var users = CreateUsersForRoles(true, _roleIdsToSendWeekReports);

            users.Count.Should().Be(_roleIdsToSendWeekReports.Count());

            var command = new WeeklyOverviewReportsGenerate.Command();
            
            WeeklyOverviewReportsGenerate.Result result = await Send(command);

            result.EmailsSent.Should().Be(users.Count);
        }

        [Test]
        public async Task ForAuthorizedUsers_WeeklyOverviewReport_ShouldNotSendAnyEmail()
        {
            var users = CreateUsersForRoles(true, _roleIdsNotToSendWeekReports);

            users.Count.Should().Be(_roleIdsNotToSendWeekReports.Count());

            var command = new WeeklyOverviewReportsGenerate.Command();

            WeeklyOverviewReportsGenerate.Result result = await Send(command);

            result.EmailsSent.Should().Be(0);
        }

        [Test]
        public async Task ForNotAuthorizedUsers_WeeklyOverviewReport_ShouldNotSendAnyEmail()
        {
            var users = CreateUsersForRoles(false, _roleIdsNotToSendWeekReports.Concat(_roleIdsToSendWeekReports).ToArray());

            users.Count.Should().Be(_roleIdsNotToSendWeekReports.Concat(_roleIdsToSendWeekReports).Count());

            var command = new WeeklyOverviewReportsGenerate.Command();

            WeeklyOverviewReportsGenerate.Result result = await Send(command);

            result.EmailsSent.Should().Be(0);
        }


        private List<User> CreateUsersForRoles(bool authorized, params Guid[] roleIds)
        {
            var users = new List<User>();

            roleIds.ToList()
                .ForEach(roleId => 
                    users.Add(new UserBuilder().Email($"{roleId}@email.com").WithRoles(roleId).Authorized(authorized)));
              
            AddAndSaveChanges(users.ToArray());

            return users;
        }

        private async Task<WeeklyOverviewReportsGenerate.Result> Send(WeeklyOverviewReportsGenerate.Command command) => await _mediator.Send(command);
    }
}
