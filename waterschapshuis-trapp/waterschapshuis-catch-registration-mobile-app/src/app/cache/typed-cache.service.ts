import { Injectable } from '@angular/core';
import { from, Observable, EMPTY } from 'rxjs';
import { tap, expand, reduce, map } from 'rxjs/operators';

import { CacheService } from './cache.service';
import { IPagedResponse } from '../shared/models/paged-response';
import { Logger } from '../core/logger/logger';

const logger = new Logger('TypedCacheService');

@Injectable({
    providedIn: 'root'
})
export class TypedCacheService {
    constructor(private cache: CacheService) { }

    getAllAndCache<T>(load: Observable<T[]>, getKey: (i: T) => string, groupKey: string): Observable<T[]> {
        return load.pipe(tap(results => this.saveAllItemsToCache(item => getKey(item), results, groupKey)));
    }

    getAllPagesAndCache<T>(
        getPage: (page: number) => Observable<IPagedResponse<T>>,
        getKey: (i: T) => string,
        groupKey: string): Observable<T[]> {
        return this.getAllPages(getPage)
            .pipe(tap(items => this.saveAllItemsToCache(item => getKey(item), items, groupKey)));
    }

    removeAfterObservable<T>(key: string, observable: Observable<T>): Observable<T> {
        return observable.pipe(
            tap(() => this.cache.removeItem(key)));
    }

    loadFromObservable<T = any>(key: string, observable: any, groupKey?: string, ttl?: number): Observable<T> {
        return this.cache.loadFromObservable(key, observable, groupKey, ttl);
    }

    removeItem(key: string): Observable<any> {
        return from(this.cache.removeItem(key));
    }

    clearAll(): Observable<any> {
        return from(this.cache.clearAll());
    }

    private saveAllItemsToCache<T>(getKey: (i: T) => string, items: T[], groupKey: string): Observable<any> {
        return from(this.cache.saveAllItems(getKey, items, groupKey));
    }

    private saveItemToCache<T>(getKey: (i: T) => string, item: any, groupKey: string): Observable<any> {
        return from(this.cache.saveItem(getKey(item), item, groupKey));
    }

    private getAllPages<T>(loadPage: (page: number) => Observable<IPagedResponse<T>>): Observable<T[]> {
        return loadPage(1).pipe(
            expand(pagedResponse => pagedResponse.hasNextPage ? loadPage(pagedResponse.currentPage + 1) : EMPTY),
            map(pagedResponse => pagedResponse.items || [])
        );
    }
}
