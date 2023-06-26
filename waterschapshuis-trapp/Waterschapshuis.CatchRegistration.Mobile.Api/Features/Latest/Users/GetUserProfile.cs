using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Users
{
    public static class GetCurrentUserProfile
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
        }

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
            /// Organization to which user belongs
            /// </summary>
            public Guid? OrganizationId { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<User, Response>();
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly ICurrentUserIdProvider _currentUserIdProvider;
            private readonly IRepository<User> _userRepository;
            private readonly IMapper _mapper;

            public RequestHandler(ICurrentUserIdProvider currentUserIdProvider, IMapper mapper, IRepository<User> userRepository)
            {
                _currentUserIdProvider = currentUserIdProvider;
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = _currentUserIdProvider.FindUserId(_userRepository.QueryAll());
                if (!userId.HasValue)
                {
                    return new Response();
                }

                return await _userRepository
                    .QueryAllAsNoTracking()
                    .QueryById(userId.Value)
                    .SingleOrDefaultMappedAsync<User, Response>(_mapper, cancellationToken);

            }
        }
    }
}
