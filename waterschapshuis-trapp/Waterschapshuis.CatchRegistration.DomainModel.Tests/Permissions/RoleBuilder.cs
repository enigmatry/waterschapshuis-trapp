using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Permissions
{
    public class RoleBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _name = "someRoleName";
        private int _displayOrderIndex = 0;
        private PermissionId[] _permissions = new PermissionId[0];

        public RoleBuilder With(Guid id, string name)
        {
            _id = id;
            _name = name;
            return this;
        }

        public RoleBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public RoleBuilder WithPermissions(params PermissionId[] permissions)
        {
            _permissions = permissions;
            return this;
        }

        private Role Build()
        {
            var result = Role.Create(_name, _name, _displayOrderIndex)
                .WithPermissions(_permissions)
                .WithId(_id);
            return result;
        }

        public static implicit operator Role(RoleBuilder builder) => builder.Build();
    }
}
