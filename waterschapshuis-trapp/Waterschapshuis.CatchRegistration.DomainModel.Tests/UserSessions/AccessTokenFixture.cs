using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.UserSessions
{
    [Category("unit")]
    public class AccessTokenFixture
    {
        [TestCase("", @"{""Value"":""""}")]
        [TestCase("Bearer adkjfweroiaudfasd", @"{""Value"":""adkjf""}")]
        [TestCase("adkflsjesdfjasdw", @"{""Value"":""adkfl""}")]
        [TestCase("1234", @"{""Value"":""1234""}")]
        [TestCase("123456789", @"{""Value"":""12345""}")]
        public void Test(string value, string expectedJson)
        {
            var accessToken = AccessToken.Create(value);

            var serialize = JsonConvert.SerializeObject(accessToken);

            serialize.Should().Be(expectedJson);
        }
    }
}
