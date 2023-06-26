using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Collections;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.DomainEvents;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity
{
    public class User : EntityHasCreatedUpdated<Guid>
    {
        public const int TextualFieldMaxLength = 255;
        public static readonly Guid SystemUserId = new Guid("8207DB25-94D1-4F3D-BF18-90DA283221F7");
        public static readonly string AnonymizedName = "Geanonimiseerd";
        public static readonly string AnonymizedGivenName = "anno";
        public static readonly string AnonymizedSurname = "niem";
        public static readonly string AnonymizedEmailSuffix = "@anoniem.hwh";

        private readonly List<User> _createdUsers = new List<User>();
        private readonly List<User> _updatedUsers = new List<User>();
        private readonly List<Trap> _createdTraps = new List<Trap>();
        private readonly List<Trap> _updatedTraps = new List<Trap>();
        private readonly List<Catch> _createdCatches = new List<Catch>();
        private readonly List<Catch> _updatedCatches = new List<Catch>();
        private readonly List<Tracking> _createdTrackings = new List<Tracking>();
        private readonly List<Tracking> _updatedTrackings = new List<Tracking>();
        private readonly List<Observation> _createdObservations = new List<Observation>();
        private readonly List<Observation> _updatedObservations = new List<Observation>();
        private readonly List<UserRole> _userRoles = new List<UserRole>();
        private readonly List<TimeRegistration> _timeRegistrations = new List<TimeRegistration>();
        private readonly List<TimeRegistration> _createdTimeRegistrations = new List<TimeRegistration>();
        private readonly List<TimeRegistration> _updatedTimeRegistrations = new List<TimeRegistration>();
        private readonly List<TimeRegistrationGeneral> _timeRegistrationsGeneral = new List<TimeRegistrationGeneral>();
        private readonly List<TimeRegistrationGeneral> _createdTimeRegistrationsGeneral = new List<TimeRegistrationGeneral>();
        private readonly List<TimeRegistrationGeneral> _updatedTimeRegistrationsGeneral = new List<TimeRegistrationGeneral>();
        private readonly List<TrapHistory> _createdTrapHistories = new List<TrapHistory>();
        private readonly List<TrapHistory> _updatedTrapHistories = new List<TrapHistory>();
        private readonly List<UserSession> _userSessions = new List<UserSession>();

        private User() { }

        public string Name { get; private set; } = String.Empty;
        public string Email { get; private set; } = String.Empty;
        public long? ExternalId { get; private set; }
        public string? Surname { get; private set; }
        public string? GivenName { get; private set; }
        public bool Authorized { get; private set; }
        public User CreatedBy { get; } = null!;
        public Guid? OrganizationId { get; private set; }
        public Organization? Organization { get; private set; } = null;

        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types
        public User UpdatedBy { get; } = null!;
        public bool ConfidentialityConfirmed { get; private set; }
        public DateTimeOffset? InactiveOn { get; private set; }

        public IReadOnlyCollection<User> CreatedUsers => _createdUsers.AsReadOnly();
        public IReadOnlyCollection<User> UpdatedUsers => _updatedUsers.AsReadOnly();
        public IReadOnlyCollection<Trap> CreatedTraps => _createdTraps.AsReadOnly();
        public IReadOnlyCollection<Trap> UpdatedTraps => _updatedTraps.AsReadOnly();
        public IReadOnlyCollection<Catch> CreatedCatches => _createdCatches.AsReadOnly();
        public IReadOnlyCollection<Catch> UpdatedCatches => _updatedCatches.AsReadOnly();
        public IReadOnlyCollection<Tracking> CreatedTrackings => _createdTrackings.AsReadOnly();
        public IReadOnlyCollection<Tracking> UpdatedTrackings => _updatedTrackings.AsReadOnly();
        public IReadOnlyCollection<Observation> CreatedObservations => _createdObservations.AsReadOnly();
        public IReadOnlyCollection<Observation> UpdatedObservations => _updatedObservations.AsReadOnly();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();
        public IReadOnlyCollection<TimeRegistration> TimeRegistrations => _timeRegistrations.AsReadOnly();
        public IReadOnlyCollection<TimeRegistration> CreatedTimeRegistrations => _createdTimeRegistrations.AsReadOnly();
        public IReadOnlyCollection<TimeRegistration> UpdatedTimeRegistrations => _updatedTimeRegistrations.AsReadOnly();
        public IReadOnlyCollection<TimeRegistrationGeneral> TimeRegistrationsGeneral => _timeRegistrationsGeneral.AsReadOnly();
        public IReadOnlyCollection<TimeRegistrationGeneral> CreatedTimeRegistrationsGeneral => _createdTimeRegistrationsGeneral.AsReadOnly();
        public IReadOnlyCollection<TimeRegistrationGeneral> UpdatedTimeRegistrationsGeneral => _updatedTimeRegistrationsGeneral.AsReadOnly();
        public IReadOnlyCollection<TrapHistory> CreatedTrapHistories => _createdTrapHistories.AsReadOnly();
        public IReadOnlyCollection<TrapHistory> UpdatedTrapHistories => _updatedTrapHistories.AsReadOnly();
        public IReadOnlyCollection<UserSession> UserSessions => _userSessions.AsReadOnly();

        public bool HasAnyRole(IEnumerable<Guid> roleIds)
        {
            return GetRoles().Any(role => roleIds.Any(roleId => roleId == role.Id));
        }

        public IEnumerable<Role> GetRoles()
        {
            return UserRoles.Select(x => x.Role);
        }

        public Guid[] GetRoleIds()
        {
            return GetRoles().Select(x => x.Id).ToArray();
        }

        public PermissionId[] GetPermissionIds()
        {
            return GetPermissions().Select(p => p.Id).ToArray();
        }

        [UsedImplicitly]
        public IEnumerable<Permission> GetPermissions()
        {
            return UserRoles.Select(ur => ur.Role)
                .SelectMany(r => r.GetPermissions())
                .Distinct();
        }

        public static User Create(string name, string email, long? externalId)
        {
            var result = new User
            {
                Id = GenerateId(),
                Name = name,
                Email = email,
                ExternalId = externalId
            };
            RaiseCreatedEvent(result, false);
            return result;
        }

        public static User Create(AutoCreateUserAfterLogin.Command command)
        {
            var result = new User
            {
                Id = GenerateId(),
                Name = command.Name,
                Email = command.Email,
                Surname = command.Surname,
                GivenName = command.GivenName,
                Authorized = false
            };
            RaiseCreatedEvent(result, true);
            return result;
        }

        public User Update(UserUpdate.Command request, DateTimeOffset? currentDate)
        {
            var oldAuthorized = Authorized;
            var oldConfidentialityConfirmed = ConfidentialityConfirmed;
            Authorized = request.Authorized;
            OrganizationId = request.OrganizationId;
            InactiveOn = request.Authorized ? null : currentDate;
            RaiseUpdatedEvent(oldAuthorized, oldConfidentialityConfirmed);
            return this;
        }

        public User Update(UpdateUserRoles.Command request, PermissionId[] currentUserPermissionIds)
        {
            AssignRoles(request.Roles, currentUserPermissionIds);
            RaiseUpdatedEvent(Authorized, ConfidentialityConfirmed);
            return this;
        }

        public void UpdateConfidentialityConfirmed(UserUpdateConfidentiality.Command request)
        {
            var oldConfidentialityConfirmed = ConfidentialityConfirmed;
            ConfidentialityConfirmed = request.ConfidentialityConfirmed;
            RaiseUpdatedEvent(Authorized, oldConfidentialityConfirmed);
        }

        public void AssignRoles(Guid[] roleIds, PermissionId[] currentUserPermissionIds)
        {
            var originalRoles = _userRoles.Select(ur => ur.RoleId).ToArray();
            if (!currentUserPermissionIds.Any(p => Role.PermissionsThatCanChangeRole.Any(c => c == p)))
            {
                // only theses roles can alter roles
                return;
            }

            var hasPermissionToEditMaintainerRole =
                currentUserPermissionIds.Any(p => p == PermissionId.AssignMaintainerRole);

            //filter out maintainer role if user tried to add it
            roleIds = roleIds.Where(r =>
                    r != Role.MaintainerRoleId || (r == Role.MaintainerRoleId && hasPermissionToEditMaintainerRole))
                .ToArray();

            var hadMaintainerRole = _userRoles.Any(r => r.RoleId == Role.MaintainerRoleId);

            _userRoles.UpdateWith(roleIds,
                (roleId, userRole) => roleId == userRole.RoleId,
                (roleId) => UserRole.Create(roleId, Id),
                (roleId, userRole) => { });

            if (!hasPermissionToEditMaintainerRole && hadMaintainerRole)
            {
                //add back maintainer role since user had it 
                _userRoles.Add(UserRole.Create(Role.MaintainerRoleId, Id));
            }

            var newRoleIds = _userRoles.Select(ur => ur.RoleId).ToArray();

            if (!originalRoles.SequenceEqual(newRoleIds))
            {
                AddDomainEvent(new UserRolesUpdatedDomainEvent(Id, Email, originalRoles, newRoleIds));
            }
        }

        public bool HasAnyPermission(IEnumerable<PermissionId> permissionIds)
        {
            return permissionIds.Any(HasPermissionId);
        }

        internal User WithOrganization(Organization? value)
        {
            Organization = value;
            OrganizationId = value?.Id;
            return this;
        }

        internal User WithName(string name)
        {
            Name = name;
            return this;
        }

        internal User WithEmail(string email)
        {
            Email = email;
            return this;
        }

        internal User WithAuthorized(bool authorized)
        {
            Authorized = authorized;
            return this;
        }

        internal User WithInactiveOn(DateTimeOffset? inactiveOn)
        {
            InactiveOn = inactiveOn;
            return this;
        }

        internal User WithConfidentialityConfirmed(bool confidentialityConfirmed)
        {
            ConfidentialityConfirmed = confidentialityConfirmed;
            return this;
        }

        internal User WithSurname(string surname)
        {
            Surname = surname;
            return this;
        }

        internal User WithGivenName(string givenName)
        {
            GivenName = givenName;
            return this;
        }

        internal User WithOrganization(Guid? organizationId)
        {
            OrganizationId = organizationId;
            return this;
        }

        private bool HasPermissionId(PermissionId permissionId)
        {
            return UserRoles.Any(ur => ur.Role.RolePermissions.Any(rp => rp.PermissionId == permissionId));
        }

        private static void RaiseCreatedEvent(User result, bool isAutoCreate)
        {
            result.AddDomainEvent(new UserCreatedDomainEvent(result.Id, result.Email, result.Authorized,
                result.OrganizationId, isAutoCreate));
        }

        private void RaiseUpdatedEvent(bool oldAuthorized, bool oldConfidentialityConfirmed)
        {
            AddDomainEvent(new UserUpdatedDomainEvent(Id, Email, oldAuthorized, Authorized, oldConfidentialityConfirmed, ConfidentialityConfirmed, OrganizationId));
        }

        public void Anonymize()
        {
            Name = AnonymizedName;
            Email = $"{Id}{AnonymizedEmailSuffix}";
            GivenName = $"{AnonymizedGivenName}";
            Surname = $"{AnonymizedSurname}";
        }

        public void AddUserSession(UserSession session)
        {
            _userSessions
                .Where(x => x.Origin == session.Origin && x.IsValid()).ToList()
                .ForEach(x => x.Terminate());
            _userSessions.Add(session);
        }

        public User WithUserSessions(List<UserSession> value)
        {
            _userSessions.Clear();
            value.ForEach(x => _userSessions.Add(x));
            return this;
        }
    }
}
