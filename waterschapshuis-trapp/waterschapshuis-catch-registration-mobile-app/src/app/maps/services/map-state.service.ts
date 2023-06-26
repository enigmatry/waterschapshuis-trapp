import { Injectable } from '@angular/core';
import { File } from '@ionic-native/file/ngx';
import { BehaviorSubject, Subject, Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';
import { IMapStyleLookup, MapNetworkType } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { AppSettingsService } from 'src/app/shared/services/app-settings.service';
import { BackgroundLayer } from '../models/background-layer.model';
import { GeoDataOptions } from '../models/geodata-options.model';
import { GeoDataResponse } from '../models/geodata-response.model';
import { MAP_OVERLAY_LAYERS, OverlayLayer } from '../models/overlay-layer.model';
import { ProjectionModel } from '../models/projection.model';
import { GeoDataService } from './geo-data-service';
import { LayersService } from './layers.service';
import { transform } from 'ol/proj';
import { Coordinate } from 'ol/coordinate';
import { NetworkService } from 'src/app/network/network.service';
import { getContentLayers } from 'src/app/shared/models/overlay-layer-name.enum';

@Injectable({
  providedIn: 'root'
})
export class MapStateService {

  projectionCode = ProjectionModel.dutchMatrix;
  private center = [146966.70, 444713.85];
  zoom = 1;
  initialZoomSet = false;
  readonly currentLocationZoomLevelDutchProjection = 10;
  readonly osmAndPdokMapZoomDifference = 6.31084197658705;
  availableBackgroundLayersData: BackgroundLayer[];
  availableOverlayLayersData: OverlayLayer[];
  availableMapStylesData: IMapStyleLookup[];
  currentlyActiveBackgroundLayer: string;

  private backGroundLayersSubject = new BehaviorSubject<BackgroundLayer[]>([]);
  backgroundLayers = this.backGroundLayersSubject.asObservable();

  private geoDataSubject = new Subject<GeoDataOptions>();
  geoDataOptions = this.geoDataSubject.asObservable();

  private overlayLayersSubject = new Subject<OverlayLayer[]>();
  overlayLayers = this.overlayLayersSubject.asObservable();

  private mapStylesSubject = new Subject<IMapStyleLookup[]>();
  mapStyles = this.mapStylesSubject.asObservable();

  private contentLayersToRefreshSubject = new Subject<string[]>();
  contentLayersToRefresh = this.contentLayersToRefreshSubject.asObservable();
  backgroundLayerSubscription: Subscription;

  constructor(
    private geoDataService: GeoDataService,
    private layersService: LayersService,
    private file: File,
    private appSettingsService: AppSettingsService,
    private networkService: NetworkService) { }

  async init(): Promise<void> {
    this.listenToBackgroundLayersChanges();

    if (!this.availableBackgroundLayersData || !this.availableOverlayLayersData || !this.availableMapStylesData) {
      await Promise.all([
        this.loadBackgroundLayers(),
        this.loadOverlayLayers(),
        this.loadMapStyles()
      ]);
    }

    this.updateBackgroundLayers(this.availableBackgroundLayersData);
  }

  private async loadBackgroundLayers(): Promise<void> {
    this.availableBackgroundLayersData = await this.layersService.getBackgroundLayers()
      .pipe(tap(this.reselectBackgroundLayer))
      .toPromise();
  }

  private reselectBackgroundLayer = (layers: BackgroundLayer[]) => {
    this.currentlyActiveBackgroundLayer = this.appSettingsService.getBackgroundLayerId();
    layers.forEach(layer => layer.selected = layer.key === this.currentlyActiveBackgroundLayer);
  }

  private async loadOverlayLayers(): Promise<void> {
    this.availableOverlayLayersData = await this.layersService.getOverlayLayers(MAP_OVERLAY_LAYERS)
      .pipe(tap(this.reselectOverlayLayers))
      .toPromise();
  }

  private reselectOverlayLayers = (layers: OverlayLayer[]) => layers.forEach(layer =>
    layer.selected = this.appSettingsService.getOverlayLayerIds().indexOf(layer.fullName) > -1)

  private async loadMapStyles(): Promise<void> {
    this.availableMapStylesData = await this.layersService.getMapStyles().toPromise();
  }

  private listenToBackgroundLayersChanges() {
    if (this.backgroundLayerSubscription) { return; }

    this.backgroundLayerSubscription = this.backgroundLayers.subscribe(backgroundLayers => {
      const layer = backgroundLayers.length > 0 ?
        backgroundLayers.filter((x) => x.key === this.currentlyActiveBackgroundLayer)[0] : null;
      if (layer) {
        layer.selected = true;
        if (layer.networkType === MapNetworkType.Online && !this.networkService.isOffline()) {
          this.geoDataService.get(layer.name, layer.url)
            .then((response: GeoDataResponse) => {
              const options = GeoDataOptions.fromOnline(response);
              this.geoDataSubject.next(options);
            });
        } else {
          setTimeout(x => this.geoDataSubject.next(GeoDataOptions.fromOffline(layer.name, this.file)), 0);
        }
      }
    });
  }

  async updateBackgroundLayers(value: BackgroundLayer[]) {
    this.backGroundLayersSubject.next(value);
    await this.appSettingsService.setBackgroundLayerId(this.currentlyActiveBackgroundLayer);
  }

  async updateOverlayLayers(value: OverlayLayer[]) {
    this.overlayLayersSubject.next(value);
    await this.appSettingsService.setOverlayLayerIds(value
      .filter(layer => layer.selected)
      .map(layer => layer.fullName));
  }

  setCenter(center: Coordinate) {
    this.center = center;
  }

  getCenterCoordinatesInSpecifiedProjection(projection: string): Coordinate {
    if (this.projectionCode === projection) {
      return this.center;
    } else {
      return transform(this.center, this.projectionCode, projection);
    }
  }

  getZoomLevelInProjection(projection: string): number {
    // calculate only if projections are different
    if (this.projectionCode === projection) { return this.zoom; }

    // OSM and PDOK map zoom scale differs, in case of OSM add difference/substract for PDOK
    return projection === ProjectionModel.mercatorMatrix ?
      this.zoom + this.osmAndPdokMapZoomDifference : this.zoom - this.osmAndPdokMapZoomDifference;
  }

  getCurrentLocationZoomLevel(): number {
    return this.projectionCode === ProjectionModel.dutchMatrix ?
      this.currentLocationZoomLevelDutchProjection : this.currentLocationZoomLevelDutchProjection + this.osmAndPdokMapZoomDifference;
  }

  updateMapStyles(value: IMapStyleLookup[]): void {
    this.mapStylesSubject.next(value);
  }

  refreshLayers(...queries: string[]): void {
    this.contentLayersToRefreshSubject.next(queries);
  }

  refreshMapContentLayers(): void {
    const contentLayers = getContentLayers();

    this.contentLayersToRefreshSubject.next(contentLayers);
  }
}
