using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable.FakeItEasy;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.UserSessions;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Tests.UserSessions
{
    [Category("unit")]
    public class ValidateAccessTokenRequestHandlerFixture
    {
        private ValidateAccessToken.RequestHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            var (sessionAccessTokens, userSessions) = new UserSessionsTestDataBuilder().Build();

            var repository = A.Fake<IRepository<SessionAccessToken>>();
            A.CallTo(() => repository.QueryAll()).Returns(sessionAccessTokens.AsQueryable().BuildMock());
            _handler = new ValidateAccessToken.RequestHandler(repository,
                new NullLogger<ValidateAccessToken.RequestHandler>());
        }

        [TestCase("", UserSessionOrigin.BackOfficeApi, false)]
        [TestCase("valid_access_token_from_valid_session", UserSessionOrigin.BackOfficeApi, true)]
        [TestCase("valid_access_token_from_valid_session", UserSessionOrigin.ExternalApi, false)]
        [TestCase("valid_access_token_from_valid_session", UserSessionOrigin.MobileApi, false)]
        [TestCase("valid_access_token_from_invalid_session", UserSessionOrigin.BackOfficeApi, false)]
        [TestCase("non_existing_access_token", UserSessionOrigin.BackOfficeApi, false)]
        [TestCase("expired_access_token_from_valid_session", UserSessionOrigin.BackOfficeApi, false)]
        public async Task Test(string accessToken, UserSessionOrigin origin, bool expectedIsValid)
        {
            var request = ValidateAccessToken.Query.Create(AccessToken.Create(accessToken), origin);
            ValidateAccessToken.Response result = await _handler.Handle(request, CancellationToken.None);

            result.IsValid.Should().Be(expectedIsValid);
        }
    }
}
