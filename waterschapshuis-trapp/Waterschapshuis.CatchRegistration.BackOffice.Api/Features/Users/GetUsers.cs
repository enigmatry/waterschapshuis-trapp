using System;
using System.Linq;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users
{
    public static partial class GetUsers
    {
        [PublicAPI]
        public class Query : PagedQuery, IRequest<PagedResponse<ResponseItem>>
        {
            public string Keyword { get; set; } = String.Empty;
        }

        [PublicAPI]
        public class ResponseItem
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
            /// Indication whether user complied with the organization policies to use application
            /// </summary>
            public bool ConfidentialityConfirmed { get; set; }

            /// <summary>
            /// Datetime this user has been created
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Date when some data has been changed for this user
            /// </summary>
            public DateTimeOffset UpdatedOn { get; set; }

            /// <summary>
            /// Date when the user has been revoked access. This is used for annonymization purposes.
            /// </summary>
            public DateTimeOffset? InactiveOn { get; set; }

            /// <summary>
            /// List of roles that are assigned to the user
            /// </summary>
            public Role[] Roles { get; set; } = new Role[0];

            /// <summary>
            /// Organization name user belongs to
            /// </summary>
            public string OrganizationName { get; set; } = String.Empty;

            /// <summary>
            /// Organization guid user belongs to
            /// </summary>
            public Guid? OrganizationId { get; set; }
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
            }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<User, ResponseItem>()
                    .ForMember(dest => dest.OrganizationName,
                        opt => opt.MapFrom(
                            src => src.Organization != null ? src.Organization.Name : String.Empty))
                    .ForMember(dest => dest.Roles,
                        opt => opt.MapFrom(
                            src => src.UserRoles.Select(u => u.Role)));
                CreateMap<Role, ResponseItem.Role>();
            }
        }
    }
}
