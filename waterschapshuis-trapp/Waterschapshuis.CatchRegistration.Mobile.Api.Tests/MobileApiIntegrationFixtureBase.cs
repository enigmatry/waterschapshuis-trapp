using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests
{
    public abstract class MobileApiIntegrationFixtureBase : ApiIntegrationFixtureBase<MobileApiTestStartup>
    {
        protected MobileApiIntegrationFixtureBase()
        {
            var apiVersion = "v1.0";
            EnableApiVersioning(apiVersion);
        }

        protected override AccessToken GetAccessToken() => TestPrincipal.MobileApiAccessToken;

        protected override Dictionary<string, string> GetAdditionalSettings() => new Dictionary<string, string>
            {
                {"App:UserSessions:SessionsEnabled", "false"},
                {"App:UserSessions:SessionOrigin", "1"},
                {"App:UserSessions:SessionDurationTimespan", "0.00:01:00"}
            };
    }
}
