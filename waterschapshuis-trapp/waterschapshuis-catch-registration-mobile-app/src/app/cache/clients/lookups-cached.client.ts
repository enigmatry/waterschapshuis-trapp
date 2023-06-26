import {
    ILookupsClient,
    LookupsClient,
    ListResponseOfGetTrappingTypesResponseItem,
    ListResponseOfGetCatchTypesResponseItem,
    ListResponseOfGetTrapTypesResponseItem,
    ListResponseOfGetTimeRegistrationCategoriesResponseItem
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Observable, forkJoin } from 'rxjs';
import { CacheService } from '../cache.service';
import { Injectable } from '@angular/core';
import { CachedClient, BoundingBox } from './cached.client';

@Injectable({
    providedIn: 'root'
})
export class LookupsCachedClient implements ILookupsClient, CachedClient {
    constructor(private lookupsClient: LookupsClient, private cache: CacheService) { }
    timeRegistrationCategories(): Observable<ListResponseOfGetTimeRegistrationCategoriesResponseItem> {
        const result = this.lookupsClient.timeRegistrationCategories();
        return this.cache.loadFromObservable(`timeRegistrationCategories`, result);
    }

    trapTypes(): Observable<ListResponseOfGetTrapTypesResponseItem> {
        const result = this.lookupsClient.trapTypes(null);
        return this.cache.loadFromObservable(`trapTypes`, result);
    }

    catchTypes(): Observable<ListResponseOfGetCatchTypesResponseItem> {
        const result = this.lookupsClient.catchTypes(null);
        return this.cache.loadFromObservable(`catchTypes`, result);
    }

    trappingTypes(): Observable<ListResponseOfGetTrappingTypesResponseItem> {
        const result = this.lookupsClient.trappingTypes();
        return this.cache.loadFromObservable(`trappingTypes`, result);
    }

    cacheAllWithin(): Observable<any> {
        return forkJoin([
            this.catchTypes(),
            this.trappingTypes(),
            this.trapTypes()]);
    }
}
