using System;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles
{
    public class UserRole
    {
        public Guid RoleId { get; private set; }
        public Guid UserId { get; private set; }

        public Role Role { get; private set; } = null!;
        public User User { get; private set; } = null!;

        public static UserRole Create(Guid roleId, Guid userId) => new UserRole { RoleId = roleId, UserId = userId };
    }
}
