import { AfterViewInit, Component, Input, OnDestroy } from '@angular/core';
import { ModalController, NavController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { Feature, Map, View } from 'ol';
import { applyStyle } from 'ol-mapbox-style';
import { defaults } from 'ol/control';
import { Coordinate } from 'ol/coordinate';
import Point from 'ol/geom/Point';
import BaseLayer from 'ol/layer/Base';
import VectorLayer from 'ol/layer/Vector';
import { get as getProjection, transform, transformExtent } from 'ol/proj';
import VectorSource from 'ol/source/Vector';
import { Circle as CircleStyle, Fill, Stroke, Style } from 'ol/style';
import proj4 from 'proj4';
import { Subscription } from 'rxjs';
import { map, throttleTime } from 'rxjs/operators';
import { IMapStyleLookup, OverlayLayerType } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import { Logger } from 'src/app/core/logger/logger';
import { GeoJsonUpdateCommandsStoreService } from 'src/app/geo-json/services/geo-json-update-commands-store.service';
import { LocalGeoJsonService } from 'src/app/geo-json/services/local-geo-json.service';
import { NetworkService } from 'src/app/network/network.service';
import { ToastService } from 'src/app/services/toast.service';
import { AppSettingsService } from 'src/app/shared/services/app-settings.service';
import { GeolocationService } from 'src/app/shared/services/geolocation.service';

import { MapLocationModalComponent } from '../map-location-modal/map-location-modal.component';
import { MapMultiSelectModalComponent } from '../map-multi-select-modal/map-multi-select-modal.component';
import { GeoDataOptions } from '../models/geodata-options.model';
import { MapItemSelectionModel, MapItemType } from '../models/map-item-selection.model';
import { OverlayLayer } from '../models/overlay-layer.model';
import { ProjectionModel } from '../models/projection.model';
import { LayersService } from '../services/layers.service';
import { MapStateService } from '../services/map-state.service';
import { OfflineMapService } from '../services/offline-map.service';
import { TrapModalComponent } from '../trap-modal/trap-modal.component';
import WMTSTileGrid from 'ol/tilegrid/WMTS';
import TileLayer from 'ol/layer/Tile';
import { Extent } from 'ol/extent';

const logger = new Logger('MapComponent');

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent extends OnDestroyMixin implements AfterViewInit, OnDestroy {
  @Input() mapId: string;

  map: Map;
  view: View;
  currentPositionLayer: VectorLayer;

  readonly baseLayerZIndex = 0;
  readonly extentLimitInDutchProj: Extent = [-260000.00698604077, 50000.27594158864, 589999.788636393, 899999.9343662034];
  private mapStyles: IMapStyleLookup[] = [];

  loader: any;
  layerLookupKey = 'name';

  positionFeatureKey = 'position_feature';

  component: any = this;

  watchCurrentPosition: Subscription;
  locateEnabled = false;

  mapLongpress: boolean;
  longpressTimeout: NodeJS.Timeout;

  constructor(
    private toastService: ToastService,
    private modal: ModalController,
    private layersService: LayersService,
    private nav: NavController,
    private offlineMapService: OfflineMapService,
    private networkService: NetworkService,
    private localGeoJsonService: LocalGeoJsonService,
    private geolocationService: GeolocationService,
    private geoJsonUpdateCommandsStore: GeoJsonUpdateCommandsStoreService,
    public mapStateService: MapStateService,
    private appSettingsService: AppSettingsService
  ) {
    super();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();

    if (this.loader) {
      this.loader.dismiss();
    }
  }

  ngAfterViewInit() {
    proj4.defs('urn:ogc:def:crs:EPSG::28992', proj4.defs('EPSG::28992'));

    this.mapStateService.init().then(
      () => {
        this.initializeMap();

        this.mapStateService.geoDataOptions
          .pipe(untilComponentDestroyed(this.component))
          .subscribe(options => {
            this.handleBackgroundLayer(options);

            this.layersService.getMapStyles()
              .pipe(untilComponentDestroyed(this.component))
              .subscribe(styles => {
                this.mapStyles = styles;
                this.handleOverlayLayers(this.mapStateService.availableOverlayLayersData);
              });
          });

        this.mapStateService.contentLayersToRefresh
          .pipe(
            map((queries: string[]) => this.refreshLayers(...queries)),
            untilComponentDestroyed(this.component))
          .subscribe();

        this.mapStateService.overlayLayers
          .pipe(untilComponentDestroyed(this.component))
          .subscribe(layers => this.handleOverlayLayers(layers));
      }
    );
  }

  locate() {
    this.geolocationService.getCurrentPositionAs(this.mapStateService.projectionCode)
      .pipe(untilComponentDestroyed(this.component))
      .subscribe(coords => {
        this.locateEnabled = true;
        this.zoomToCurrentLocation(coords);
        this.showCurrentLocation();
      });
  }

  zoomToCurrentLocation(currentPosition: Coordinate) {
    const center = transform(
      currentPosition,
      this.mapStateService.projectionCode,
      this.map.getView().getProjection().getCode()
    );

    this.view.animate({
      center,
      zoom: this.mapStateService.getCurrentLocationZoomLevel(),
      duration: 1800
    });
  }

  openMapItemDetails(selectedItems: MapItemSelectionModel[]): void {
    if (selectedItems?.length === 1) {
      if (selectedItems[0].type === MapItemType.Trap) {
        this.openTrapDetailsModal(selectedItems[0].id);
      } else {
        this.nav.navigateForward(`/melding/${selectedItems[0].id}`);
      }
    } else {
      this.openMultiSelectModal(selectedItems);
    }
  }

  async openTrapDetailsModal(trapId: string) {
    const detailsModal = await this.modal.create({
      component: TrapModalComponent,
      id: 'TrapDetailsModal',
      componentProps: {
        trapId
      },
      cssClass: 'trap-modal'
    });

    detailsModal.present();
  }

  disposeLayer(layer: BaseLayer): void {
    if (!layer) { return; }
    layer.dispose();
    this.map.removeLayer(layer);
  }

  findLayer(id: string): BaseLayer {
    return this.map.getLayers()
      .getArray()
      .find(layer => layer.get(this.layerLookupKey) === id);
  }

  async openLocationModal(coords) {
    await this.modal.create({
      component: MapLocationModalComponent,
      componentProps: { coordinate: coords },
      cssClass: 'location-modal'
    }).then(modal => modal.present());
  }

  private showCurrentLocation() {
    // Prevent multiple location points
    if (this.currentPositionLayer) {
      this.map.removeLayer(this.currentPositionLayer);
    }

    // Current location marker (circle)
    const positionFeature = new Feature({ key: this.positionFeatureKey });
    positionFeature.setStyle(new Style({
      image: new CircleStyle({
        radius: 10,
        fill: new Fill({
          color: '#3399CC'
        }),
        stroke: new Stroke({
          color: '#fff',
          width: 2
        })
      })
    }));

    this.currentPositionLayer = new VectorLayer({
      source: new VectorSource({
        features: [positionFeature]
      }),
      zIndex: AppSettings.currentLocationSettings.ZIndex
    });

    this.map.addLayer(this.currentPositionLayer);

    this.updateCurrentPostionMarker(positionFeature);
  }

  private updateCurrentPostionMarker(positionFeature) {
    // prevent multiple subscriptions
    if (this.watchCurrentPosition) {
      this.watchCurrentPosition.unsubscribe();
    }

    this.watchCurrentPosition = this.geolocationService.watchAs(this.mapStateService.projectionCode)
      .pipe(
        throttleTime(AppSettings.currentLocationSettings.refreshPeriod),
        untilComponentDestroyed(this.component))
      .subscribe((coords: Coordinate) => {
        positionFeature.setGeometry(new Point(coords));
        if (!this.mapStateService.initialZoomSet) {
          this.mapStateService.initialZoomSet = true;
          this.zoomToCurrentLocation(coords);
        }
        if (this.locateEnabled) {
          this.zoomToCurrentLocation(coords);
        }
      });
  }

  private initializeMap(): void {
    this.map = new Map({
      target: `${this.mapId}`,
      controls:
        defaults({
          attribution: false,
          zoom: false,
        })
    });

    this.map.on('pointerdrag', evt => {
      if (this.locateEnabled) {
        this.locateEnabled = false;
      }
    });

    this.map.on('moveend', evt => {
      this.mapStateService.setCenter(evt.frameState.viewState.center);
      this.mapStateService.zoom = evt.frameState.viewState.zoom;
      clearTimeout(this.longpressTimeout);
    });

    this.map.on('singleclick', async evt => {
      const pointFeatures = this.map.getFeaturesAtPixel(
        evt.pixel,
        { hitTolerance: AppSettings.mapSettings.hitTolerance }
      )
        .filter(x => x.getGeometry().getType() === 'Point'
          && !x.getProperties().TrackingId);

      if (pointFeatures.length === 0) {
        return;
      }

      // Current location marker
      if (pointFeatures.length === 1 && pointFeatures[0].get('key') === this.positionFeatureKey) {
        return;
      }

      if (pointFeatures.length > 20) {
        this.toastService.error('U heeft meer dan 20 vangmiddelen geselecteerd. Zoom verder in en selecteer minder vangmiddelen.');
        return;
      }

      const selectedItems: Array<MapItemSelectionModel> = [];

      pointFeatures.forEach(tl => {
        if (tl.getProperties().TrapId) {
          selectedItems.push(MapItemSelectionModel.create(tl.getProperties().TrapId, MapItemType.Trap));
        } else if (tl.getProperties().Id) {
          selectedItems.push(MapItemSelectionModel.create(tl.getProperties().Id, MapItemType.Observation));
        }
      });

      this.openMapItemDetails(selectedItems);
    });

    this.map.on('pointerdown', async evt => {
      if (this.longpressTimeout) {
        this.clearHoldClick();
        return;
      }

      this.detectHoldClick(evt);
    });

    this.map.on('pointerup', async evt => {
      this.clearHoldClick();
    });
  }

  private async openMultiSelectModal(mapItemsToDisplay: MapItemSelectionModel[]) {
    // added to prevent multiple modals overlapping and not dismissing on emulator
    // ( details modal called from selection modal wouldn't close)
    const dismissSelectionModal = () => {
      this.modal.dismiss(null, null, 'MapMultiSelectModal');
    };

    const selectionModal = await this.modal.create({
      component: MapMultiSelectModalComponent,
      id: 'MapMultiSelectModal',
      componentProps: {
        mapItemsToDisplay,
        dismissSelectionModal
      },
      cssClass: 'trap-modal'
    });

    selectionModal.present();
  }

  private handleBackgroundLayer(response: GeoDataOptions): void {
    this.removeAllLayers();

    let layer = this.findLayer(response.layer);

    let projection, zoom, extentLimit;
    if (response.layer === 'osm') {
      layer = response.toVectorTileLayer(this.layerLookupKey, this.baseLayerZIndex);
      projection = getProjection(ProjectionModel.mercatorMatrix);
      this.networkService.forceOfflineMode(false);
      zoom = this.mapStateService.getZoomLevelInProjection(ProjectionModel.mercatorMatrix);
      extentLimit = transformExtent(this.extentLimitInDutchProj, ProjectionModel.dutchMatrix, ProjectionModel.mercatorMatrix);
    } else {
      layer = response.toTileLayer(this.layerLookupKey, this.baseLayerZIndex);
      projection = ProjectionModel.initDutchProjection();
      this.networkService.forceOnlineMode(false);
      zoom = this.mapStateService.getZoomLevelInProjection(ProjectionModel.dutchMatrix);
      extentLimit = this.extentLimitInDutchProj;
    }

    const mapCenter = this.mapStateService.getCenterCoordinatesInSpecifiedProjection(projection.getCode());
    this.view = new View({
      enableRotation: false,
      projection,
      center: mapCenter,
      zoom,
      extent: extentLimit
    });
    this.map.setView(this.view);

    this.mapStateService.projectionCode = projection.getCode();

    if (response.layer === 'osm') {
      this.offlineMapService.getOfflineMapStyle().subscribe(styleData => {
        applyStyle(layer, styleData, 'openmaptiles').then((_: any) => {
          this.map.addLayer(layer);
        });
      });
    } else {
      this.map.addLayer(layer);
    }

    this.showCurrentLocation();
  }

  private removeAllLayers() {
    const layers = [...this.map.getLayers().getArray()];
    layers.forEach((layer) => {
      this.map.removeLayer(layer);
      layer.dispose();
    });
  }

  private handleOverlayLayers(layers: OverlayLayer[]): void {

    const layerFilter = this.appSettingsService.getMapLayerFilter();

    layers.forEach((layer: OverlayLayer) => {
      const mapLayer = this.findLayer(layer.fullName);

      // dispose if not selected
      if (!layer.selected) {
        this.disposeLayer(mapLayer);
        return;
      }

      // don't add if already there
      if (mapLayer) {
        return;
      }

      if (layerFilter?.layerFullName === layer.fullName) {
        layer.url = layerFilter.apply(layer.url);
      }

      const mapCurrentProjection = this.map.getView().getProjection().getCode();
      let newLayer: BaseLayer;
      if (layer.type === OverlayLayerType.Wfs) {
        newLayer = layer.createVectorLayer(
          this.mapStyles,
          mapCurrentProjection,
          this.localGeoJsonService,
          this.geoJsonUpdateCommandsStore,
          this.networkService,
          this.geolocationService);
      } else {
        const tileLayer = this.map.getLayers().getArray().find(l => l instanceof TileLayer) as TileLayer;
        const grid = tileLayer.getSource().getTileGrid() as WMTSTileGrid;
        newLayer = layer.createWMTSTileLayer(layer.layerLookupKey, grid);
      }

      this.map.addLayer(newLayer);
    });
  }

  private refreshLayers = (...queries: string[]) =>
    queries.forEach(query => this.map.getLayers().getArray()
      .filter(mapLayer => mapLayer.get(this.layerLookupKey)?.includes(query))
      .forEach(mapLayer => (mapLayer as VectorLayer).getSource().refresh()))

  private detectHoldClick(evt) {
    this.mapLongpress = true;
    this.longpressTimeout = setTimeout(() => {
      if (this.mapLongpress) {
        this.openLocationModal(evt.coordinate);
      }
    }, AppSettings.mapSettings.longpressTimeoutSec * 1000);
  }

  private clearHoldClick() {
    clearTimeout(this.longpressTimeout);
    this.longpressTimeout = null;
    this.mapLongpress = false;
  }
}
