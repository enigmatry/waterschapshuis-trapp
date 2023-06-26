import WMTSCapabilities from 'ol/format/WMTSCapabilities';
import TileLayer from 'ol/layer/Tile';
import WMTS, { Options, optionsFromCapabilities } from 'ol/source/WMTS';
import { GeoDataResponse } from './geodata-response.model';

export class GeoDataOptions {
  private static parser = new WMTSCapabilities();

  options: Options;
  layer: string;

  constructor(layer: string, capabilities: string) {
    this.layer = layer;
    this.options = optionsFromCapabilities(capabilities, {
      layer
    });
  }

  public static fromObject(response: GeoDataResponse): GeoDataOptions {
    const result = this.parser.read(response.data);
    return new GeoDataOptions(response.layer, result);
  }

  public toTileLayer(layerLookupKey: string, backgroundLayerZIndex: number): TileLayer {
    const tileLayer = new TileLayer({
      source: new WMTS(this.options)
    });

    tileLayer.setZIndex(backgroundLayerZIndex);
    tileLayer.set(layerLookupKey, this.layer);

    return tileLayer;
  }
}
