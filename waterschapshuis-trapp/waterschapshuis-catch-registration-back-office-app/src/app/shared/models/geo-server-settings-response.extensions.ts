import { IGetGeoServerSettingsResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export function getGeoserverAcessHeaders(url: string, geoServerSettings: IGetGeoServerSettingsResponse): { name: string, value: string }[] {
    return url.startsWith(geoServerSettings.url) && geoServerSettings.accessKey ?
        [{ name: geoServerSettings.accessKey, value: geoServerSettings.backOfficeUser }] :
        [];
}
