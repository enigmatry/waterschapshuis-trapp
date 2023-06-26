import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IMapStyleLookup, OverlayLayerCategoryCode } from '../../api/waterschapshuis-catch-registration-mobile-api';
import { BackgroundLayer } from '../models/background-layer.model';
import { OverlayLayer } from '../models/overlay-layer.model';
import { environment } from 'src/environments/environment';
import { MapsCachedClient } from 'src/app/cache/clients/maps-cached.client';

@Injectable({
  providedIn: 'root'
})
export class LayersService {

  constructor(private mapsClient: MapsCachedClient) { }

  getBackgroundLayers(): Observable<BackgroundLayer[]> {
    return this.mapsClient.backgroundLayers()
      .pipe(map(response => response.items.map(BackgroundLayer.fromResponse)));
  }

  getOverlayLayers(orderedLayerCategoryCodes: OverlayLayerCategoryCode[]): Observable<OverlayLayer[]> {
    return this.mapsClient.overlayLayers(
      orderedLayerCategoryCodes,
      new Date().getFullYear(),
      Number(environment.appSettings.numberOfHistoricalTrapLayers)
    )
      .pipe(map(response => response.items.map(OverlayLayer.fromResponse)));
  }

  getMapStyles(): Observable<IMapStyleLookup[]> {
    return this.mapsClient.styles()
      .pipe(map(response => response.items));
  }
}
