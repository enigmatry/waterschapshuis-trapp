using System;
using System.Security.Principal;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation
{
    public class TestPrincipal : IPrincipal
    {
        public static readonly Guid TestUserId = new Guid("bc54b640-9d2f-4f8c-b963-7d8602aa6fdb");
        public static readonly string TestUserName = "INTEGRATION_TEST";
        public static readonly string TestUserEmail = "INTEGRATION_TEST@CatchReg.com";
        public static readonly Guid TestUserOrganizationId = new Guid("c9796264-4174-4c75-86a9-eb9fe5aa5d1e");
        public static readonly string TestUserOrganizationName = "INTEGRATION_TEST_USER_ORGANIZATION";
        public static readonly AccessToken BackOfficeApiAccessToken = AccessToken.Create("BACKOFFICE_API_ACCESS_TOKEN");
        public static readonly AccessToken MobileApiAccessToken = AccessToken.Create(" MOBILE_API_ACCESS_TOKEN");
        public static readonly AccessToken ExternalApiAccessToken = AccessToken.Create("EXTERNAL_API_ACCESS_TOKEN");

        private TestPrincipal(Guid userId, string email, string name)
        {
            UserId = userId;
            Email = email;
            Name = name;
            Identity = new GenericIdentity(email);
        }

        public Guid UserId { get; }
        public string Email { get; }
        public string Name { get; }

        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            return true;
        }

        public static TestPrincipal CreateDefaultForIntegrationTesting()
        {
            return new TestPrincipal(TestUserId, TestUserEmail, TestUserName);
        }
    }
}
