import { Injectable } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { concatMap, scan } from 'rxjs/operators';
import {
    TrapsClient,
    GetMySummaryResponse,
    TrapCreateOrUpdateCommand,
    GetTrapDetailsTrapItem,
    PagedResponseOfGetTrapHistoriesHistoryItem
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { TypedCacheService } from '../typed-cache.service';
import { BoundingBox, CachedClient } from './cached.client';
import { AppSettings } from 'src/app/app-configuration/app-settings';

@Injectable({
    providedIn: 'root'
})
export class TrapsCachedClient implements CachedClient {
    private cachePrefix = 'traps';
    private groupCacheKey = 'traps';

    constructor(private trapsClient: TrapsClient, private cache: TypedCacheService) { }

    // this method is exempt from caching
    getCurrentUserSummary(includeDetails: boolean): Observable<GetMySummaryResponse> {
        return this.trapsClient.getCurrentUserSummary(includeDetails);
    }

    getMultiple(ids: string[]): Observable<GetTrapDetailsTrapItem[]> {
        return ids && ids.length > 0 ? from(ids).pipe(concatMap(id => this.get(id)),
            scan((acc, curr) => {
                acc.push(curr);
                return acc;
            }, [])
        ) : of([]);
    }

    get(id: string): Observable<GetTrapDetailsTrapItem> {
        return this.cache.loadFromObservable(this.createCacheKey(id), this.trapsClient.get(id), this.groupCacheKey);
    }

    post(command: TrapCreateOrUpdateCommand): Observable<GetTrapDetailsTrapItem> {
        return this.cache.removeAfterObservable(this.createCacheKey(command.id), this.trapsClient.post(command));
    }

    delete(command: TrapCreateOrUpdateCommand): Observable<boolean> {
        return this.cache.removeAfterObservable(this.createCacheKey(command.id), this.trapsClient.delete(command.id));
    }

    getTraps(locationLatitude: number, locationLongitude: number, widthKilometers: number): Observable<GetTrapDetailsTrapItem[]> {
        const getPage = (currentPage: number) => this.trapsClient.getTraps(
            locationLatitude, locationLongitude, widthKilometers, AppSettings.prefetchDataSettings.pageSize, currentPage);

        return this.cache.getAllPagesAndCache(getPage, item => this.createCacheKey(item.id), this.groupCacheKey);
    }

    cacheAllWithin = (boundingBox: BoundingBox): Observable<any> =>
        this.getTraps(boundingBox.location.latitude, boundingBox.location.longitude, boundingBox.widthKilometers)

    getHistories = (trapId: string, sortField: string, sortDirection: string, pageSize: number, currentPage: number)
        : Observable<PagedResponseOfGetTrapHistoriesHistoryItem> =>
        this.trapsClient
            .getHistories(trapId, sortField, sortDirection, pageSize, currentPage)

    private createCacheKey = (id: string) => `${this.cachePrefix}_${id}`;
}
