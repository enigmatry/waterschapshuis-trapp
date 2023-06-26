import { IGetBackgroundLayersResponseItem, MapServiceType, MapNetworkType } from '../../api/waterschapshuis-catch-registration-mobile-api';

export class BackgroundLayer {
  key: string;
  selected: boolean;
  text: string;
  url: string;
  name: string;
  serviceType: MapServiceType;
  networkType: MapNetworkType;
  defaultOverlayLayer: string;

  constructor(
    key: string, text: string, name: string,
    url: string, serviceType: MapServiceType,
    networkType: MapNetworkType, selected: boolean = false,
    defaultOverlayLayer: string) {
    this.key = key;
    this.text = text;
    this.selected = selected;
    this.url = url;
    this.name = name;
    this.serviceType = serviceType;
    this.networkType = networkType;
    this.defaultOverlayLayer = defaultOverlayLayer;
  }

  static fromResponse(response: IGetBackgroundLayersResponseItem): BackgroundLayer {
    return new BackgroundLayer
      (response.id,
        response.name,
        response.id,
        response.url,
        response.serviceType,
        response.networkType,
        false,
        response.defaultOverlayLayer);
  }
}
