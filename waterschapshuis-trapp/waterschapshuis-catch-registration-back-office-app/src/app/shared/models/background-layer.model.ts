import { IGetBackgroundLayersResponseItem, IGetGeoServerSettingsResponse, MapNetworkType } from '../../api/waterschapshuis-catch-registration-backoffice-api';

export class BackgroundLayer {
  key: string;
  selected: boolean;
  text: string;
  url: string;
  name: string;
  networkType: MapNetworkType;
  defaultOverlayLayer: string;
  geoServerSettings: IGetGeoServerSettingsResponse;

  constructor(
      key: string,
      text: string,
      name: string,
      url: string,
      selected: boolean = false,
      networkType: MapNetworkType,
      defaultOverlayLayer: string,
      geoServerSettings: IGetGeoServerSettingsResponse) {
    this.key = key;
    this.text = text;
    this.selected = selected;
    this.url = url;
    this.name = name;
    this.networkType = networkType;
    this.defaultOverlayLayer = defaultOverlayLayer;
    this.geoServerSettings = geoServerSettings;
  }

  static fromResponse(response: IGetBackgroundLayersResponseItem, geoServerSettings: IGetGeoServerSettingsResponse): BackgroundLayer {
    return new BackgroundLayer
    (response.id, response.name, response.id, response.url, false, response.networkType, response.defaultOverlayLayer, geoServerSettings);
  }
}
