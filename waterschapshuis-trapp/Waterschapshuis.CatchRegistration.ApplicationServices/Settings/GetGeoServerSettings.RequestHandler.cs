using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Settings
{
    public partial class GetGeoServerSettings
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly GeoServerSettings _geoServerSettings;
            private readonly ICurrentUserProvider _currentUserProvider;

            public RequestHandler(GeoServerSettings geoServerSettings, ICurrentUserProvider currentUserProvider)
            {
                _geoServerSettings = geoServerSettings;
                _currentUserProvider = currentUserProvider;
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                Response response = new Response {Url = _geoServerSettings.Url};
                
                if (UserCanReadMap())
                {
                    //do not expose access key to the user that has not adequate permissions
                    response.AccessKey = _geoServerSettings.AccessKey;
                    response.BackOfficeUser = _geoServerSettings.BackOfficeUser;
                    response.MobileUser = _geoServerSettings.MobileUser;
                }

                return Task.FromResult(response);
            }

            private bool UserCanReadMap() => _currentUserProvider.UserHasAnyPermission(Permission.GetMapReadPermissionIds());
        }
    }
}
