using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Data;
using static Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands.UpdateRolesPermissions.Command;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands
{
    public static class UpdateRolesPermissions
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [DisplayName(nameof(Roles))]
            public List<Item> Roles { get; set; } = new List<Item>();

            public static Command Create(List<Role> roles) =>
                new Command
                {
                    Roles = roles.Select(x => new Item { Id = x.Id, Name = x.Name }).ToList()
                };

            public List<Guid> GetRoleIds() => Roles.Select(x => x.Id).ToList();

            public class Item
            {
                /// <summary>
                /// GUID of the role
                /// </summary>
                public Guid Id { get; set; }

                /// <summary>
                /// Name of the role
                /// </summary>
                public string Name { get; set; } = String.Empty;

                /// <summary>
                /// New permission list to be assigned to this role
                /// </summary>
                public PermissionId[] PermissionIds { get; set; } = new PermissionId[0];
            }
        }

        [PublicAPI]
        public class Result { }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            private readonly IRepository<Role> _roleRepository;

            public Validator(IRepository<Role> roleRepository)
            {
                _roleRepository = roleRepository;

                RuleFor(x => x.Roles).NotEmpty();
                RuleFor(x => x.Roles)
                    .Must(AllRolesSelected).WithMessage("Not all of the possible roles are selected");
                RuleFor(x => x.Roles.Single(role => role.Id == Role.MaintainerRoleId))
                    .Must(MaintainerRoleMustHaveManagementPermission).WithMessage("Maintainer role must have Management permission");
            }

            private bool AllRolesSelected(List<Item> roles)
            {
                var roleIds = _roleRepository.QueryAll().Select(x => x.Id).ToList();
                return roleIds.Intersect(roles.Select(x => x.Id)).Count() == roleIds.Count;
            }

            private bool MaintainerRoleMustHaveManagementPermission(Item role) =>
                role.PermissionIds.Any(x => x == PermissionId.Management);
        }
    }
}
