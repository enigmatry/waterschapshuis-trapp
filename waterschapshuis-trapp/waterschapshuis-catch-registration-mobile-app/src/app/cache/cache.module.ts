import { InjectionToken, NgModule } from '@angular/core';
import { NetworkModule } from '../network/network.module';
import { NetworkService } from '../network/network.service';
import { OfflineServiceContext } from '../shared/offline/base-offline-entity.service';
import { CacheService } from './cache.service';
import { CacheStorageService } from './cache.storage';
import { CachedClient } from './clients/cached.client';
import { LookupsCachedClient } from './clients/lookups-cached.client';
import { MapsCachedClient } from './clients/maps-cached.client';
import { ObservationsCachedClient } from './clients/observation-cached.client';
import { TrapsCachedClient } from './clients/traps-cached.client';
import { TypedCacheService } from './typed-cache.service';
import { GeoJsonUpdateCommandsStoreService } from '../geo-json/services/geo-json-update-commands-store.service';
import { MapStateService } from '../maps/services/map-state.service';
import { BackgroundJobService } from '../background-job/background-job.service';
import { SqliteCacheStorage } from './sqlite-cache.storage';

export const CACHED_CLIENTS = new InjectionToken<CachedClient>('CachedClient');

export function buildCacheService(storage: SqliteCacheStorage, networkService: NetworkService) {
  return new CacheService(
    new CacheStorageService(storage, 'catch-reg-cache'),
    networkService
  );
}

export function buildOfflineServiceContext(
  networkService: NetworkService,
  cacheService: CacheService,
  geoJsonUpdateCommandsStore: GeoJsonUpdateCommandsStoreService,
  mapStateService: MapStateService,
  backgroundJobService: BackgroundJobService): OfflineServiceContext {
  return {
    networkService,
    cacheService,
    geoJsonUpdateCommandsStore,
    mapStateService,
    backgroundJobService
  };
}

@NgModule({
  imports: [NetworkModule],
  providers: [
    {
      provide: CacheService,
      useFactory: buildCacheService,
      deps: [SqliteCacheStorage, NetworkService],
    },
    {
      provide: OfflineServiceContext,
      useFactory: buildOfflineServiceContext,
      deps: [
        NetworkService,
        CacheService,
        GeoJsonUpdateCommandsStoreService,
        MapStateService,
        BackgroundJobService
      ],
    },
    TypedCacheService,
    { provide: CACHED_CLIENTS, useClass: LookupsCachedClient, multi: true },
    LookupsCachedClient,
    { provide: CACHED_CLIENTS, useClass: MapsCachedClient, multi: true },
    MapsCachedClient,
    { provide: CACHED_CLIENTS, useClass: TrapsCachedClient, multi: true },
    TrapsCachedClient,
    { provide: CACHED_CLIENTS, useClass: ObservationsCachedClient, multi: true },
    ObservationsCachedClient,
  ]
})
export class CacheModule { }

