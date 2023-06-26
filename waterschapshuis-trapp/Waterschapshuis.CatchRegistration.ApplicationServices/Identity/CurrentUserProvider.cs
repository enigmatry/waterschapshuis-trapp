using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using static Waterschapshuis.CatchRegistration.DomainModel.Identity.UserAuthorizationStatus;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly ICurrentUserIdProvider _currentUserIdProvider;
        private readonly IRepository<User> _userRepository;
        private User? _user;

        public CurrentUserProvider(
            ICurrentUserIdProvider currentUserIdProvider,
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _currentUserIdProvider = currentUserIdProvider;
        }

        public bool IsAuthenticated => _currentUserIdProvider.IsAuthenticated;
        public Guid? UserId => _currentUserIdProvider.FindUserId(_userRepository.QueryAll());
        public string Email => _currentUserIdProvider.Email;
        public string Name => User == null ? String.Empty : User.Name;

        public PermissionId[] PermissionIds => User != null ? User.GetPermissionIds() : new PermissionId[0];
        public Guid[] RoleIds => User != null ? User.GetRoleIds() : new Guid[0];
        public Organization? Organization { get { return User?.Organization; } }

        public bool UserHasAnyPermission(IEnumerable<PermissionId> permissionIds) => User != null && User.HasAnyPermission(permissionIds);

        public void TryReloadUser()
        {
            if (UserId == null)
            {
                return;
            }
            ReloadUser(UserId.Value);
        }

        public Guid GetUserId()
        {
            return UserId ?? throw new InvalidOperationException("Cannot find user id.");
        }

        public UserAuthorizationStatus UserAuthorizationStatus
        {
            get
            {
                if (!IsAuthenticated)
                {
                    return NotAuthenticated;
                }
                if (User == null)
                {
                    return NotFound;
                }
                return User.Authorized ?
                    Success :
                    NotAuthorized;
            }
        }

        private User? User
        {
            get
            {
                if (_user != null)
                {
                    return _user;
                }

                if (UserId == null)
                {
                    return null;
                }

                ReloadUser(UserId.Value);

                if (_user == null)
                {
                    throw new InvalidOperationException("The UserId is populated by user could not be found.");
                }
                return _user;
            }
        }

        private void ReloadUser(Guid userId) =>
            _user = _userRepository
                .QueryAll()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .Include(u => u.Organization)
                .QueryById(userId)
                .SingleOrDefault();
    }
}
