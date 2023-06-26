import { MapBrowserEvent, View } from 'ol';
import BaseLayer from 'ol/layer/Base';
import TileLayer from 'ol/layer/Tile';
import Map from 'ol/Map';
import { fromLonLat } from 'ol/proj';
import Projection from 'ol/proj/Projection';
import { IMapStyleLookup, OverlayLayerType } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { GeoDataOptions } from 'src/app/shared/models/geodata-options.model';
import { OverlayLayer } from 'src/app/shared/models/overlay-layer.model';
import { ProjectionModel } from 'src/app/shared/models/projection.model';
import { Subject } from 'rxjs';
import RenderEvent from 'ol/render/Event';
import WMTSTileGrid from 'ol/tilegrid/WMTS';

export class AppMap extends Map {
  private layerLookupKey = 'name';
  private backgroundLayerZIndex = 0;
  private mapStyles: IMapStyleLookup[] = [];

  private mapRenderedSubject = new Subject<boolean>();
  mapRendered = this.mapRenderedSubject.asObservable();

  constructor(
    public containerElementId: string,
    public singleClickCallback: (event: MapBrowserEvent) => any = null,
    public moveEndCallback: (event: MapBrowserEvent) => any = null,
    public centerLongitude: number = 5.38,
    public centerLatitude: number = 52.15,
    public zoomLevel: number = 2,
    public projection: Projection = null,
    public mapLayers: Array<BaseLayer> = []
  ) {
    super({
      target: containerElementId,
      layers: mapLayers,
      view: new View({
        projection: projection === null ? ProjectionModel.initDutchProjection() : projection,
        center: fromLonLat(
          [centerLongitude, centerLatitude],
          projection === null ? ProjectionModel.initDutchProjection() : projection),
        zoom: zoomLevel
      })
    });

    if (singleClickCallback !== null) {
      this.on('singleclick', singleClickCallback);
    }
    if (moveEndCallback !== null) {
      this.on('moveend', moveEndCallback);
    }
    this.on('rendercomplete', this.renderCompleteEvent);
  }

  private renderCompleteEvent(event: RenderEvent) {
    this.mapRenderedSubject.next(true);
  }

  disposeLayerById = (id: string) => this.disposeLayer(this.findLayer(id));

  disposeLayer = (layer: BaseLayer) => {
    if (!layer) { return; }
    layer.dispose();
    this.removeLayer(layer);
  }

  findLayer = (id: string): BaseLayer =>
    this.getLayers().getArray().find(x => x.get(this.layerLookupKey) === id)

  addStyles = (mapStyles: IMapStyleLookup[]) => this.mapStyles = mapStyles;

  addBackgroundLayer = (response: GeoDataOptions) => {
    this.removeCurrentBackgroundLayers();

    const newLayer = response.toTileLayer(this.layerLookupKey, this.backgroundLayerZIndex);
    this.addLayer(newLayer);
  }

  removeCurrentBackgroundLayers(): void {
    this.getLayers().getArray().forEach(currentLayer => {
      if (currentLayer instanceof TileLayer) {
        this.disposeLayer(currentLayer);
      }
    });
  }

  tryAddOverlayLayer = (overlayLayer: OverlayLayer) => {
    if (this.findLayer(overlayLayer.fullName)) { return; }
    if (overlayLayer.type === OverlayLayerType.Wms) {
      this.addLayer(overlayLayer.createSingleImageLayer(this.layerLookupKey));
    } else if (overlayLayer.type === OverlayLayerType.Wfs) {
      this.addLayer(overlayLayer.createVectorLayer(this.layerLookupKey, this.mapStyles));
    } else {
      const tileLayer = this.getLayers().getArray().find(l => l instanceof TileLayer) as TileLayer;
      const grid = tileLayer.getSource().getTileGrid() as WMTSTileGrid;
      this.addLayer(overlayLayer.createWMTSTileLayer(this.layerLookupKey, grid));
    }
  }

  addOrReplaceExistingOverlayLayer = (overlayLayer: OverlayLayer) => {
    if (this.findLayer(overlayLayer.fullName)) {
      this.removeLayer(this.findLayer(overlayLayer.fullName));
    }
    this.addLayer(overlayLayer.createVectorLayer(this.layerLookupKey, this.mapStyles));
  }
}
