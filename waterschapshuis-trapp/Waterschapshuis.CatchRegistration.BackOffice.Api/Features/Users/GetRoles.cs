using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users
{
    public static partial class GetRoles
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public IEnumerable<Item> Items { get; set; } = new List<Item>();

            [PublicAPI]
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
                /// Indication whether current user can assign this role to him
                /// </summary>
                public bool CanCurrentUserChangeToThisRole { get; set; }

                /// <summary>
                /// List of permission the user has with his roles
                /// </summary>
                public List<Permission> Permissions { get; set; } = new List<Permission>();
            }

            [PublicAPI]
            public class Permission
            {
                public PermissionId Id { get; set; }
                public string Name { get; set; } = String.Empty;
                public bool AssignedToRole { get; set; } = false;
                public bool CanBeChanged { get; set; } = true;
            }

            [UsedImplicitly]
            public class MappingProfile : Profile
            {
                public MappingProfile()
                {
                    CreateMap<Role, Item>()
                        .ForMember(dest => dest.Permissions, opt => opt.Ignore())
                        .ForMember(dest => dest.CanCurrentUserChangeToThisRole, opt => opt.Ignore())
                        .AfterMap<RoleToItemAfterMapAction>();
                }
            }

            public class RoleToItemAfterMapAction : IMappingAction<Role, Item>
            {
                private readonly ICurrentUserProvider _currentUserProvider;
                private readonly IRepository<DomainModel.Roles.Permission> _permissionRepository;

                public RoleToItemAfterMapAction(ICurrentUserProvider currentUserProvider,
                    IRepository<DomainModel.Roles.Permission> permissionRepository)
                {
                    _currentUserProvider = currentUserProvider;
                    _permissionRepository = permissionRepository;
                }

                public void Process(Role source, Item destination, ResolutionContext context)
                {
                    IEnumerable<DomainModel.Roles.Permission>? permissions = GetAllPermissions();
                    destination.Permissions =
                        permissions
                            .Select(permission => new Permission
                            {
                                Id = permission.Id,
                                Name = permission.Name,
                                AssignedToRole =
                                    source.RolePermissions.SingleOrDefault(x => x.PermissionId == permission.Id) !=
                                    null,
                                CanBeChanged = source.Id != Role.MaintainerRoleId
                                               || (source.Id == Role.MaintainerRoleId &&
                                                   permission.Id != PermissionId.Management)
                            }).ToList();
                    destination.CanCurrentUserChangeToThisRole =
                        source.CanUserChangeToThisRole(_currentUserProvider.PermissionIds);
                }

                private IEnumerable<DomainModel.Roles.Permission> GetAllPermissions()
                {
                    return _permissionRepository.QueryAll().OrderBy(x => x.Order).ToList();
                }
            }
        }

        private static IQueryable<Role> BuildInclude(this IQueryable<Role> query)
        {
            return query.Include(x => x.RolePermissions);
        }
    }
}
