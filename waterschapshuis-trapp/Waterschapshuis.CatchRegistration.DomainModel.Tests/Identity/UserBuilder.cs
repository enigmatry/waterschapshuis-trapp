using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Utils;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity
{
    public class UserBuilder
    {
        private string _name = String.Empty;
        private string _email = String.Empty;
        private string _surname = String.Empty;
        private string _givenName = String.Empty;
        private bool _authorized;
        private bool _confidentialityConfirmed;
        private Guid[] _roleIds = new Guid[0];
        private Guid? _organizationId;
        private DateTimeOffset? _inactiveOn;

        public UserBuilder Authorized(bool value)
        {
            _authorized = value;
            return this;
        }        
        
        public UserBuilder ConfidentialityConfirmed(bool value)
        {
            _confidentialityConfirmed = value;
            return this;
        }

        public UserBuilder Surname(string value)
        {
            _surname = value;
            return this;
        }

        public UserBuilder GivenName(string value)
        {
            _givenName = value;
            return this;
        }

        public UserBuilder Name(string value)
        {
            _name = value;
            return this;
        }

        public UserBuilder Email(string value)
        {
            _email = value;
            return this;
        }

        public UserBuilder Organization(Guid value)
        {
            _organizationId = value;
            return this;
        }
        public UserBuilder InactiveOn(DateTimeOffset? value)
        {
            _inactiveOn = value;
            return this;
        }

        public UserBuilder WithRoles(params Guid[] roleIds)
        {
            _roleIds = roleIds;
            return this;
        }

        public static implicit operator User(UserBuilder builder)
        {
            return builder.Build();
        }

        private User Build()
        {
            User result =
                User.Create(_name, _email, null)
                    .WithAuthorized(_authorized)
                    .WithSurname(_surname)
                    .WithGivenName(_givenName)
                    .WithOrganization(_organizationId)
                    .WithInactiveOn(_inactiveOn);
            result
                .Update(
                    new UpdateUserRoles.Command {Roles = _roleIds},
                    Enum<PermissionId>.GetAll().ToArray()); // in test we fake that we have all permissions

            return result;
        }
    }
}
