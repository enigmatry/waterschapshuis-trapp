using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users
{
    public static class GetCurrentUserProfile
    {
        [PublicAPI]
        public class Query : IRequest<Response> { }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// GUID of the user
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            ///  Full name from the user
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// Email that serves as UPN (User Prinicapal Name) for Azure AD account
            /// </summary>
            public string Email { get; set; } = String.Empty;

            /// <summary>
            /// Indication whether user is granted access (true or false)
            /// </summary>
            public bool Authorized { get; set; }

            /// <summary>
            /// Indication if the user complied with the regulations policies for using trAPP
            /// </summary>
            public bool ConfidentialityConfirmed { get; set; }

            /// <summary>
            /// Set of rights a user has with the assigned roles to do in trAPP
            /// </summary>
            public string[] Policies { get; set; } = new string[0];

            /// <summary>
            /// Organization to which user belongs
            /// </summary>
            public Guid? OrganizationId { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() =>
                CreateMap<User, Response>()
                    .ForMember(
                        dest => dest.Policies, 
                        opt => opt.MapFrom(src => BackOfficePoliciesToPermissionsMap.GetPolicies(src.GetPermissionIds())));
        }

        private static IQueryable<User> BuildInclude(this IQueryable<User> query) => 
            query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission);

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly ICurrentUserProvider _currentUserProvider;
            private readonly IRepository<User> _userRepository;
            private readonly IMapper _mapper;

            public RequestHandler(
                ICurrentUserProvider currentUserIdProvider,
                IMapper mapper,
                IRepository<User> userRepository)
            {
                _currentUserProvider = currentUserIdProvider;
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!_currentUserProvider.UserId.HasValue)
                {
                    return new Response();
                }

                var response = await _userRepository
                    .QueryAllAsNoTracking()
                    .BuildInclude()
                    .QueryById(_currentUserProvider.UserId.Value)
                    .SingleOrDefaultMappedAsync<User, Response>(_mapper, cancellationToken);

                return response;
            }
        }
    }
}
