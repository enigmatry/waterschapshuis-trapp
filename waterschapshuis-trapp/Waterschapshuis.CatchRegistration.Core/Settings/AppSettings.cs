using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class AppSettings
    {
        [UsedImplicitly]
        public SmtpSettings Smtp { get; set; } = new SmtpSettings();

        [UsedImplicitly]
        public GeoServerSettings GeoServer { get; set; } = new GeoServerSettings();

        [UsedImplicitly]
        public AzureAdSettings AzureAd { get; set; } = new AzureAdSettings();

        [UsedImplicitly]
        public AzureBlobSettings AzureBlob { get; set; } = new AzureBlobSettings();

        [UsedImplicitly]
        public ApiConfigurationSettings ApiConfiguration { get; set; } = new ApiConfigurationSettings();

        [UsedImplicitly]
        public EasyAuthSettings EasyAuth { get; set; } = new EasyAuthSettings();

        [UsedImplicitly]
        public ApiVersioningSettings ApiVersioning { get; set; } = new ApiVersioningSettings();

        [UsedImplicitly]
        public UserSessionSettings UserSessions { get; set; } = new UserSessionSettings();
    }
}
