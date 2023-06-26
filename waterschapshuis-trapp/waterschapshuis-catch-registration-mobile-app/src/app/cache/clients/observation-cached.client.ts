import { Injectable } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { concatMap, scan, tap } from 'rxjs/operators';
import {
    GetObservationDetailsResponseItem,
    ObservationsClient,
    ObservationUpdateCommand
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

import { TypedCacheService } from '../typed-cache.service';
import { BoundingBox, CachedClient } from './cached.client';
import { AppSettings } from 'src/app/app-configuration/app-settings';

@Injectable({
    providedIn: 'root'
})
export class ObservationsCachedClient implements CachedClient {
    private cachePrefix = 'observations';
    private groupCacheKey = 'observations';

    constructor(private observationClient: ObservationsClient, private cache: TypedCacheService) { }

    getMultiple(ids: string[]): Observable<GetObservationDetailsResponseItem[]> {
        return ids && ids.length > 0 ? from(ids).pipe(concatMap(id => this.get(id)),
            scan((acc, curr) => {
                acc.push(curr);
                return acc;
            }, [])
        ) : of([]);
    }

    get(id: string): Observable<GetObservationDetailsResponseItem> {
        return this.cache.loadFromObservable(this.createCacheKey(id), this.observationClient.get(id), this.groupCacheKey);
    }

    update(command: ObservationUpdateCommand): Observable<GetObservationDetailsResponseItem> {
        return this.observationClient.update(command);
    }

    deleteCacheItem(id: string) {
        this.cache.removeItem(this.createCacheKey(id));
    }

    getObservations(
        locationLatitude: number,
        locationLongitude: number,
        widthKilometers: number):
    Observable<GetObservationDetailsResponseItem[]> {
        const getPage = (currentPage: number) => this.observationClient.getObservations(
            locationLatitude, locationLongitude, widthKilometers, AppSettings.prefetchDataSettings.pageSize, currentPage);

        return this.cache.getAllPagesAndCache(getPage, item => this.createCacheKey(item.id), this.groupCacheKey);
    }

    cacheAllWithin(boundingBox: BoundingBox): Observable<any> {
        return this.getObservations(boundingBox.location.latitude, boundingBox.location.longitude, boundingBox.widthKilometers);
    }

    private createCacheKey = (id: string) => `${this.cachePrefix}_${id}`;
}
