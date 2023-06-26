import WMTSCapabilities from 'ol/format/WMTSCapabilities';
import TileLayer from 'ol/layer/Tile';
import WMTS, { Options, optionsFromCapabilities } from 'ol/source/WMTS';
import { GeoDataResponse } from './geodata-response.model';
import VectorTileLayer from 'ol/layer/VectorTile';
import VectorTileSource from 'ol/source/VectorTile';
import MVT from 'ol/format/MVT';
import { File } from '@ionic-native/file/ngx';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import { Logger } from 'src/app/core/logger/logger';

const logger = new Logger('GeoDataOptions');

export class GeoDataOptions {
  private static parser = new WMTSCapabilities();

  options: Options;
  layer: string;
  private file: File;

  private constructor(layer: string, capabilities: string, file: File) {
    this.layer = layer;
    if (capabilities) {
      this.options = optionsFromCapabilities(capabilities, {
        layer
      });
    }
    this.file = file;
  }

  public static fromOnline(response: GeoDataResponse): GeoDataOptions {
    let result = null;
    if (response) {
      result = this.parser.read(response.data);
    }
    return new GeoDataOptions(response.layer, result, null);
  }

  public static fromOffline(layerName: string, file: File): GeoDataOptions {
    return new GeoDataOptions(layerName, null, file);
  }

  public toTileLayer(layerLookupKey: string, backgroundLayerZIndex: number): TileLayer {
    const tileLayer = new TileLayer({
      className: this.layer,
      source: this.options ? new WMTS(this.options) : null
    });

    tileLayer.setZIndex(backgroundLayerZIndex);
    tileLayer.set(layerLookupKey, this.layer);

    return tileLayer;
  }

  public toVectorTileLayer(layerLookupKey: string, backgroundLayerZIndex: number) {
    const vectorTileLayer = new VectorTileLayer({
      declutter: true,
      source: new VectorTileSource({
        format: new MVT(),
        url: 'offline-map-pbfs/{z}/{x}/{y}.pbf', // Url on device
        tileLoadFunction: this.tileload,
        maxZoom: AppSettings.offlineMapSettings.maxZoomLevelForSelectedMap
      })
    });

    vectorTileLayer.setZIndex(backgroundLayerZIndex);
    vectorTileLayer.set(layerLookupKey, this.layer);

    return vectorTileLayer;
  }

  private tileload = (tile, url) => {
    tile.setLoader((extent, resolution, projection) => {
      this.file.readAsArrayBuffer(this.file.dataDirectory, url)
        .then(data => {
          const format: any = tile.getFormat();
          tile.setFeatures(format.readFeatures(data, {
            extent
          }));
        })
        .catch(err => logger.error(err, 'Error loading tile', url));
    });
  }
}
