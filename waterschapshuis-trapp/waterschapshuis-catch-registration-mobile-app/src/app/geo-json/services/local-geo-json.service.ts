import { Injectable } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { catchError, concatMap, flatMap, switchMap } from 'rxjs/operators';
import {
  OverlayLayerCacheStrategy as CacheStrategy,
  OverlayLayerCategoryCode,
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Logger } from 'src/app/core/logger/logger';
import { OverlayLayer } from 'src/app/maps/models/overlay-layer.model';
import { LayersService } from 'src/app/maps/services/layers.service';
import { NetworkService } from 'src/app/network/network.service';
import { GeolocationService } from 'src/app/shared/services/geolocation.service';

import { GeoJsonCacheManager } from '../cache/geo-json-cache-manager';
import { GeoJsonClient } from '../clients/geo-json.client';
import { GeoJsonUpdateCommandsStoreService } from './geo-json-update-commands-store.service';

const logger = new Logger('LocalGeoJsonService');

@Injectable({
  providedIn: 'root'
})
export class LocalGeoJsonService {

  constructor(
    private client: GeoJsonClient,
    private cacheManager: GeoJsonCacheManager,
    private networkService: NetworkService,
    private geoLocationService: GeolocationService,
    private layersService: LayersService,
    private commandsStoreService: GeoJsonUpdateCommandsStoreService) { }

  prefetchAll(): Observable<void> {
    const getExtent$ = this.geoLocationService.getCurrentCacheableExtent();
    const getLayers$ = this.layersService.getOverlayLayers([OverlayLayerCategoryCode.MapAreas, OverlayLayerCategoryCode.MapLocations]);
    const clearCmds$ = this.commandsStoreService.clear();

    return getExtent$.pipe(
      switchMap(extent =>
        getLayers$.pipe(
          concatMap(layers => layers),
          concatMap(layer => this.prefetchGeoJson(layer, extent))
        )),
      flatMap(() => clearCmds$));
  }

  getGeoJson(layer: OverlayLayer, bbox: number[]): Observable<string> {
    return from(this.getGeoJsonInternal(layer, bbox));
  }

  private prefetchGeoJson(layer: OverlayLayer, bbox: number[]): Observable<unknown> {
    return this.getGeoJsonFromNetwork(layer, bbox)
      .pipe(
        flatMap(response => this.cacheManager.updateCachedGeoJson(layer, bbox, response)),
        catchError(err => {
          logger.error(err);
          return of();
        })
      );
  }

  private async getGeoJsonInternal(layer: OverlayLayer, bbox: number[]): Promise<string> {
    if (this.networkService.isOffline()) {
      return this.cacheManager.getCachedGeoJson(layer, bbox);
    }
    const cacheStrategy = this.getCacheStrategy(layer);

    switch (cacheStrategy) {
      case CacheStrategy.NoCache:
        return this.getGeoJsonNoCacheStrategy(layer, bbox);
      case CacheStrategy.CacheFirst:
        return this.getGeoJsonCacheFirstStrategy(layer, bbox);
      case CacheStrategy.NetworkFirst:
        return this.getGeoJsonNetworkFirstStrategy(layer, bbox);
    }
  }

  private getGeoJsonNoCacheStrategy(layer: OverlayLayer, bbox: number[]): Promise<string> {
    // NoCache: cache is not used
    return this.getGeoJsonFromNetwork(layer, bbox).toPromise();
  }

  private async getGeoJsonCacheFirstStrategy(layer: OverlayLayer, bbox: number[]): Promise<string> {
    // CacheFirst: check cache first before going to network
    const foundInCache = await this.cacheManager.getCachedGeoJson(layer, bbox);
    if (foundInCache !== null) {
      return foundInCache;
    }
    return this.getGeoJsonAndUpdateCache(layer, bbox);
  }

  private getGeoJsonNetworkFirstStrategy(layer: OverlayLayer, bbox: number[]): Promise<string> {
    // NetworkFirst: go to network first, update cache periodically
    return this.getGeoJsonAndUpdateCache(layer, bbox);
  }

  private async getGeoJsonAndUpdateCache(layer: OverlayLayer, bbox: number[]): Promise<string> {
    let cacheResults = true;

    if (layer.isBBoxLookupStrategy) {
      // update cache periodically when using bbox lookup strategy
      cacheResults = false;
    }
    const response = await this.getGeoJsonFromNetwork(layer, bbox).toPromise();

    if (response && cacheResults) {
      this.cacheManager.updateCachedGeoJson(layer, bbox, response);
    }
    return response;
  }

  private getGeoJsonFromNetwork(layer: OverlayLayer, bbox: number[]): Observable<string> {
    return (layer.isBBoxLookupStrategy)
      ? this.client.getGeoJsonInBbox(layer.url, bbox, layer.geometryFieldName)
      : this.client.getGeoJson(layer.url);
  }

  private getCacheStrategy(layer: OverlayLayer): CacheStrategy {
    return layer.cacheSettings ? layer.cacheSettings.cacheStrategy : CacheStrategy.NoCache;
  }

}
