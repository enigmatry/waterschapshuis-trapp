using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests
{
    public abstract class BackOfficeApiIntegrationFixtureBase : ApiIntegrationFixtureBase<BackOfficeApiTestStartup>
    {
        protected override AccessToken GetAccessToken() => TestPrincipal.BackOfficeApiAccessToken;

        protected override Dictionary<string, string> GetAdditionalSettings() => new Dictionary<string, string>
            {
                {"App:UserSessions:SessionsEnabled", "false"},
                {"App:UserSessions:SessionOrigin", "0"},
                {"App:UserSessions:SessionDurationTimespan", "0.00:01:00"}
            };
    }
}
