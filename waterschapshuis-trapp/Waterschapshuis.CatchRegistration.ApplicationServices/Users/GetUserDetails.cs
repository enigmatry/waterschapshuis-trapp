using System;
using System.Linq;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Users
{
    public static partial class GetUserDetails
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public Guid Id { get; set; }

            public static Query ById(Guid id)
            {
                return new Query {Id = id};
            }
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// GUID of user
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Full name of the user
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// Email, which is also account id
            /// </summary>
            public string Email { get; set; } = String.Empty;

            /// <summary>
            /// Surname
            /// </summary>
            public string Surname { get; set; } = String.Empty;

            /// <summary>
            /// Givenname, sometimes consists only of initials
            /// </summary>
            public string GivenName { get; set; } = String.Empty;

            /// <summary>
            /// Indicator whether user is given access to the application
            /// </summary>
            public bool Authorized { get; set; }

            /// <summary>
            /// Datetime this user has been created
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Date when some data has been changed for this user
            /// </summary>
            public DateTimeOffset UpdatedOn { get; set; }

            /// <summary>
            /// List of roles that are assigned to the user
            /// </summary>
            public Role[] Roles { get; set; } = new Role[0];

            /// <summary>
            /// Organization name user belongs to
            /// </summary>
            public string OrganizationName { get; set; } = String.Empty;

            /// <summary>
            /// Indication whether user complied with the organization policies to use application
            /// </summary>
            public bool ConfidentialityConfirmed { get; set; }

            /// <summary>
            /// Date when the user has been revoked access. This is used for annonymization purposes.
            /// </summary>
            public DateTimeOffset InactiveOn { get; set; }

            public class Role
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
                /// Set of permission user has acquired through assigned roles
                /// </summary>
                public Permission[] Permissions { get; set; } = new Permission[0];
            }

            public class Permission
            {
                /// <summary>
                /// GUID of permission
                /// </summary>
                public PermissionId Id { get; set; }

                /// <summary>
                /// Name of the permission
                /// </summary>
                public string Name { get; set; } = String.Empty;
            }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<User, Response>()
                    .ForMember(dest => dest.OrganizationName,
                    opt => opt.MapFrom(
                        src => src.Organization != null ? src.Organization.Name : String.Empty));
                CreateMap<Role, Response.Role>();
                CreateMap<Permission, Response.Permission>();
            }
        }

        private static IQueryable<User> BuildInclude(this IQueryable<User> query)
        {
            return
                query.Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .Include(u=> u.Organization);
        }
    }
}
