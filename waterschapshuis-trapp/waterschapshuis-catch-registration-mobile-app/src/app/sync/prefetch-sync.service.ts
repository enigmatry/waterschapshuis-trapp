import { Inject, Injectable } from '@angular/core';
import { Coordinate } from 'ol/coordinate';
import { Observable, of, from, concat, forkJoin } from 'rxjs';
import { map, switchMap, tap, finalize, concatMap } from 'rxjs/operators';
import { CACHED_CLIENTS } from '../cache/cache.module';
import { BoundingBox, CachedClient } from '../cache/clients/cached.client';
import { ProjectionModel } from '../maps/models/projection.model';
import { NetworkService } from '../network/network.service';
import { GeolocationService } from '../shared/services/geolocation.service';
import { AppSettingsService } from '../shared/services/app-settings.service';
import { AppSettings } from '../app-configuration/app-settings';
import { BackgroundJobService } from '../background-job/background-job.service';
import { Logger } from '../core/logger/logger';

const logger = new Logger('PreFetchSyncService');

@Injectable({
  providedIn: 'root'
})
export class PreFetchSyncService {

  constructor(
    @Inject(CACHED_CLIENTS) private cachedClients: CachedClient[],
    private networkService: NetworkService,
    private geoLocationService: GeolocationService,
    private userAppSettingsService: AppSettingsService,
    private backgroundJobService: BackgroundJobService) { }

  prefetchAllForCurrentLocation(): Observable<any> {
    if (this.networkService.isOffline()) {
      return of([]);
    }

    return from(this.backgroundJobService.deQueue())
      .pipe(
        concatMap(() => this.getCurrentBoundingBox()),
        concatMap(boundingBox => this.preFetchAllWithin(boundingBox))
      );
  }

  private getCurrentBoundingBox(): Observable<BoundingBox> {
    return this.geoLocationService.getCurrentPositionAs(ProjectionModel.dutchMatrix)
      .pipe(map(coordinate => this.mapToBoundingBox(coordinate, AppSettings.prefetchDataSettings.widthKilometers)));
  }

  private preFetchAllWithin(boundingBox: BoundingBox): Observable<any> {
    const storeBoundingBoxSettings = from(this.userAppSettingsService.setOfflineBoundingBox(boundingBox));
    const cacheAllObservables = this.cachedClients.map(client => client.cacheAllWithin(boundingBox));
    const allObservables = cacheAllObservables.concat(storeBoundingBoxSettings);

    return concat(...allObservables);
  }

  private mapToBoundingBox(coordinate: Coordinate, widthKilometers: number): BoundingBox {
    return { location: { longitude: coordinate[0], latitude: coordinate[1] }, widthKilometers };
  }
}
