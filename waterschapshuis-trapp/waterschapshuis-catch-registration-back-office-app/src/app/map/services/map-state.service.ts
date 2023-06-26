import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject, Subscription } from 'rxjs';
import { IMapStyleLookup } from '../../api/waterschapshuis-catch-registration-backoffice-api';
import { BackgroundLayer } from '../../shared/models/background-layer.model';
import { GeoDataOptions } from '../../shared/models/geodata-options.model';
import { GeoDataResponse } from '../../shared/models/geodata-response.model';
import { OverlayLayer } from '../../shared/models/overlay-layer.model';
import { MapService } from 'src/app/shared/services/map.service';
import { Coordinate } from 'ol/coordinate';
import { MapFilter } from 'src/app/shared/models/map-filter.model';

@Injectable({
  providedIn: 'root'
})
export class MapStateService {
  mapCenter: Coordinate;

  private backGroundLayersSubject = new BehaviorSubject<BackgroundLayer[]>([]);
  backgroundLayers = this.backGroundLayersSubject.asObservable();

  private geoDataSubject = new Subject<GeoDataOptions>();
  geoDataOptions = this.geoDataSubject.asObservable();

  private overlayLayersSubject = new Subject<OverlayLayer[]>();
  overlayLayers = this.overlayLayersSubject.asObservable();

  private mapStylesSubject = new Subject<IMapStyleLookup[]>();
  mapStyles = this.mapStylesSubject.asObservable();

  backgroundLayerSubscription: Subscription;

  private contentLayersToRefreshSubject = new Subject<string>();
  contentLayersToRefresh = this.contentLayersToRefreshSubject.asObservable();

  private mapFilterSubject = new Subject<MapFilter>();
  mapFilter = this.mapFilterSubject.asObservable();

  constructor(private mapService: MapService) { }

  init() {
    this.listenToBackgroundLayersChanges();
    this.mapService
      .getBackgroundLayers()
      .subscribe(response => this.updateBackgroundLayers(response));
    this.mapService
      .getStyles()
      .subscribe(response => this.mapStylesSubject.next(response));
  }

  private listenToBackgroundLayersChanges() {
    if (this.backgroundLayerSubscription) { return; }

    this.backgroundLayerSubscription = this.backgroundLayers.subscribe(backgroundLayers => {
      const layer = backgroundLayers.length > 0 ? backgroundLayers[0] : null;
      if (layer) {
        layer.selected = true;
        this.mapService
          .getGeoData(layer)
          .subscribe((response: GeoDataResponse) => {
            const options = GeoDataOptions.fromObject(response);
            this.geoDataSubject.next(options);
          });
      }
    });
  }

  setCenter(coordinate: Coordinate) {
    this.mapCenter = coordinate;
  }

  updateBackgroundLayers(value: BackgroundLayer[]) {
    this.backGroundLayersSubject.next(value);
  }

  updateOverlayLayers(value: OverlayLayer[]): void {
    this.overlayLayersSubject.next(value);
  }

  refreshLayers(query: string): void {
    this.contentLayersToRefreshSubject.next(query);
  }

  updateFilter(value: MapFilter): void {
    this.mapFilterSubject.next(value);
}

}
