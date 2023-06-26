using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable.FakeItEasy;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.UserSessions;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Tests.UserSessions
{
    [Category("unit")]
    public class CreateOrUpdateUserSessionRequestHandlerFixture
    {
        private CreateOrUpdateUserSession.RequestHandler _handler = null!;
        private readonly Guid _userId = new Guid("A33D46DF-006E-45E2-A0C7-83AA66B3B4AC");

        [SetUp]
        public void SetUp()
        {
            var (sessionAccessTokens, userSessions) = new UserSessionsTestDataBuilder().Build();

            var sessionAccessTokenRepository = A.Fake<IRepository<SessionAccessToken>>();
            var userSessionRepository = A.Fake<IRepository<UserSession>>();
            var unitOfWork = A.Fake<IUnitOfWork>();
            var timeProvider = A.Fake<ITimeProvider>();

            A.CallTo(() => timeProvider.Now).Returns(DateTimeOffset.Now);
            A.CallTo(() => sessionAccessTokenRepository.QueryAll())
                .Returns(sessionAccessTokens.AsQueryable().BuildMock());
            A.CallTo(() => userSessionRepository.QueryAll()).Returns(userSessions.AsQueryable().BuildMock());
            _handler = new CreateOrUpdateUserSession.RequestHandler(userSessionRepository, sessionAccessTokenRepository,
                unitOfWork, new UserSessionSettings {SessionOrigin = UserSessionOrigin.BackOfficeApi}, timeProvider,
                new NullLogger<CreateOrUpdateUserSession.RequestHandler>());
        }

        [TestCase("", UserSessionOrigin.BackOfficeApi, true, "")]
        [TestCase("valid_access_token_from_valid_session", UserSessionOrigin.BackOfficeApi, true, "")]
        [TestCase("valid_access_token_from_valid_session", UserSessionOrigin.ExternalApi, false, "Same Access token was found in different user session origin")]
        [TestCase("valid_access_token_from_valid_session", UserSessionOrigin.MobileApi, false, "Same Access token was found in different user session origin")]
        [TestCase("valid_access_token_from_invalid_session", UserSessionOrigin.BackOfficeApi, false, "User session is expired")]
        [TestCase("non_existing_access_token", UserSessionOrigin.BackOfficeApi, true, "")]
        [TestCase("expired_access_token_from_valid_session", UserSessionOrigin.BackOfficeApi, false, "Access token is expired")]
        public async Task Test(string accessToken, UserSessionOrigin origin, bool expectedIsValid, string expectedFailReason)
        {
            var request = CreateOrUpdateUserSession.Command.Create(AccessToken.Create(accessToken), _userId, origin);
            CreateOrUpdateUserSession.Response result = await _handler.Handle(request, CancellationToken.None);

            result.IsSuccess.Should().Be(expectedIsValid);
            result.FailReason.Should().Be(expectedFailReason);
        }
    }
}
