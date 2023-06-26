using NUnit.Framework;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests
{
    public abstract class ExternalApiIntegrationFixtureBase : ApiIntegrationFixtureBase<ExternalApiTestStartup>
    {
        protected override AccessToken GetAccessToken() => TestPrincipal.ExternalApiAccessToken;

        protected override Dictionary<string, string> GetAdditionalSettings() => new Dictionary<string, string>
            {
                {"App:UserSessions:SessionsEnabled", "false"},
                {"App:UserSessions:SessionOrigin", "2"},
                {"App:UserSessions:SessionDurationTimespan", "0.00:01:00"}
            };
    }
}
