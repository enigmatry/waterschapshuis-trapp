import { Feature } from 'ol';
import GeoJSON from 'ol/format/GeoJSON';
import ImageWrapper from 'ol/Image';
import { Image as ImageLayer } from 'ol/layer';
import TileLayer from 'ol/layer/Tile';
import VectorLayer from 'ol/layer/Vector';
import { all, bbox } from 'ol/loadingstrategy';
import ImageWMS from 'ol/source/ImageWMS';
import VectorSource, { LoadingStrategy } from 'ol/source/Vector';
import WMTS from 'ol/source/WMTS';
import WMTSTileGrid from 'ol/tilegrid/WMTS';
import { parseUrl, stringifyUrl } from 'query-string';
import { getStyle } from 'src/app/api/map/overlay-layer-styles.helper';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import {
  IGetOverlayLayersResponseItem, IMapStyleLookup,

  OverlayLayerCategoryCode,
  OverlayLayerLookupStrategy,
  OverlayLayerPlatformType,
  OverlayLayerType
} from '../../api/waterschapshuis-catch-registration-backoffice-api';
import { IGetGeoServerSettingsResponse } from './../../api/waterschapshuis-catch-registration-backoffice-api';
import { bboxExtended } from './bbox-loading-strategy';
import { getGeoserverAcessHeaders } from './geo-server-settings-response.extensions';
import { ProjectionModel } from './projection.model';
import { addHeaders, loadImageWithCustomHeaders } from './xml-http-request.extensions';

export class OverlayLayer {
  readonly maxLookupResolution: number = AppSettings.mapSettings.maxLookupResolution;

  displayName: string;
  fullName: string;
  url: string;
  categoryCode: OverlayLayerCategoryCode;
  categoryDisplayName: string;
  selected: boolean;
  vectorSource: VectorSource;
  platformType: OverlayLayerPlatformType;
  imageWmsSource: ImageWMS;
  type: OverlayLayerType;
  wmts: WMTS;
  geoServerSettings: IGetGeoServerSettingsResponse;

  constructor(private layer: IGetOverlayLayersResponseItem, geoServerSettings: IGetGeoServerSettingsResponse) {
    this.displayName = layer.displayName;
    this.fullName = layer.fullName;
    this.url = layer.url;
    this.categoryCode = layer.categoryCode;
    this.categoryDisplayName = layer.categoryDisplayName;
    this.selected = this.getDefaultSelectionStatus(layer.fullName);
    this.platformType = layer.platformType;
    this.type = layer.type;
    this.geoServerSettings = geoServerSettings;
  }

  static fromResponse = (response: IGetOverlayLayersResponseItem, geoServerSettings: IGetGeoServerSettingsResponse):
    OverlayLayer => new OverlayLayer(response, geoServerSettings)

  createVectorLayer(layerLookupKey: string, styles: IMapStyleLookup[]) {
    this.vectorSource = new VectorSource({
      strategy: this.useBboxStrategy() ? bbox : all,
      loader: (extent) => {
        const xhr = new XMLHttpRequest();
        const format = new GeoJSON({ dataProjection: ProjectionModel.dutchMatrix });
        const url = this.useBboxStrategy() ? this.extendUrlWithBboxExtent(extent, this.url) : this.url;
        xhr.open('GET', url);
        addHeaders(xhr, getGeoserverAcessHeaders(url, this.geoServerSettings));

        const onError = () => {
          this.vectorSource.clear();
        };
        xhr.onerror = onError;
        xhr.onload = () => {
          if (xhr.status === 200) {
            this.vectorSource.addFeatures(format.readFeatures(xhr.responseText));
          } else {
            onError();
          }
        };
        xhr.send();
      }
    });

    const vectorLayer = new VectorLayer({
      source: this.vectorSource,
      zIndex: this.layer.displayZIndex,
      minResolution: 0,
      maxResolution: this.useBboxStrategy() ? this.maxLookupResolution : Infinity,
      style: (feature: Feature, resolution: any) => getStyle(feature.getProperties(), styles, this.layer, resolution)
    });

    vectorLayer.set(layerLookupKey, this.fullName);
    return vectorLayer;
  }

  private getLoadingStrategy(): LoadingStrategy {
    switch (this.layer.lookupStrategy) {
      case OverlayLayerLookupStrategy.All:
        return all;
      case OverlayLayerLookupStrategy.BBox:
        const scaleFactor = AppSettings.mapSettings.bboxExtendFactor;
        return bboxExtended(scaleFactor);
      case OverlayLayerLookupStrategy.Tracking:
      // supported only on mobile
      default:
        return all;
    }
  }

  createSingleImageLayer(layerLookupKey: string) {
    this.imageWmsSource = new ImageWMS({
      url: this.url,
      params: { LAYERS: this.layer.fullName },
      projection: ProjectionModel.dutchMatrix,
      imageLoadFunction: (image: ImageWrapper, src: string): void => this.loadImage(image, src)
    });

    const imageLayer = new ImageLayer({
      zIndex: this.layer.displayZIndex,
      source: this.imageWmsSource
    });

    imageLayer.set(layerLookupKey, this.fullName);
    return imageLayer;
  }

  private loadImage(image: ImageWrapper, url: string) {
    const img = image.getImage() as HTMLImageElement;
    if (!url.startsWith(this.geoServerSettings.url)) {
      // image is not for our Geoserver
      img.src = url;
      return;
    }

    loadImageWithCustomHeaders(img, url, getGeoserverAcessHeaders(url, this.geoServerSettings));
  }

  createWMTSTileLayer(layerLookupKey: string, grid: WMTSTileGrid) {
    this.wmts = new WMTS({
      url: this.url,
      layer: this.layer.name,
      matrixSet: ProjectionModel.dutchMatrix,
      tileGrid: grid,
      style: 'default',
    });

    const tileLayer = new TileLayer({
      zIndex: this.layer.displayZIndex,
      source: this.wmts
    });

    tileLayer.set(layerLookupKey, this.fullName);
    return tileLayer;
  }

  // Bbox strategy shouldn't be used for map area layers (such as rayons, organizations etc.)
  private useBboxStrategy = (): boolean => {
    return this.layer.lookupStrategy === OverlayLayerLookupStrategy.BBox;
  }

  private extendUrlWithBboxExtent = (extent, url): string => {
    const params = parseUrl(url);
    params.query.CQL_FILTER =  (params.query.CQL_FILTER ? params.query.CQL_FILTER + `and` : '')
            + `(BBOX(${this.layer.geometryFieldName},${extent.toString()}))`;
    return stringifyUrl(params, { encode: true });
  }

  private getDefaultSelectionStatus = (layerName: string): boolean => {
    return  AppSettings.defaultSelectedLayers.overlayLayers.includes(layerName);
  }
}
