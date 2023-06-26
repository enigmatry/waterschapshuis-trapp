

export class GeoDataResponse {
  data: string;
  layer: string;

  constructor(layer: string, data: string) {
    this.layer = layer;
    this.data = data;
  }

  public static fromObject(layer: string, data: string): GeoDataResponse {
    return new GeoDataResponse(layer, data);
  }
}
