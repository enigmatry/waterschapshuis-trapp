import { Injectable } from '@angular/core';
import { forkJoin, from, Observable, of, throwError } from 'rxjs';
import { catchError, flatMap, map, tap } from 'rxjs/operators';
import { BackgroundJobService } from 'src/app/background-job/background-job.service';
import { CacheService } from 'src/app/cache/cache.service';
import { GeoJsonUpdateCommand } from 'src/app/geo-json/models/geo-json-update-command.model';
import { GeoJsonUpdateCommandsStoreService } from 'src/app/geo-json/services/geo-json-update-commands-store.service';
import { MapStateService } from 'src/app/maps/services/map-state.service';
import { NetworkService } from 'src/app/network/network.service';

import { HaveId } from './models/have-id.model';
import { NoConnectionError } from './models/no-connection.error';

@Injectable({
    providedIn: 'root'
})
export class OfflineServiceContext {
    networkService: NetworkService;
    cacheService: CacheService;
    geoJsonUpdateCommandsStore: GeoJsonUpdateCommandsStoreService;
    mapStateService: MapStateService;
    backgroundJobService: BackgroundJobService;
}

export abstract class BaseOfflineEntityService<TCommand extends HaveId, TResponse> {

    private cacheService: CacheService;
    private geoJsonUpdateCommandsStore: GeoJsonUpdateCommandsStoreService;
    private mapStateService: MapStateService;
    private backgroundJobService: BackgroundJobService;

    constructor(
        offlineServiceContext: OfflineServiceContext
    ) {
        this.cacheService = offlineServiceContext.cacheService;
        this.geoJsonUpdateCommandsStore = offlineServiceContext.geoJsonUpdateCommandsStore;
        this.mapStateService = offlineServiceContext.mapStateService;
        this.backgroundJobService = offlineServiceContext.backgroundJobService;
    }

    executeSave(
        command: TCommand,
        getResponseFunc: (command: TCommand) => Observable<TResponse>,
        cacheEntityId?: string
    ): Observable<TResponse> {
        return getResponseFunc(command).pipe(
            flatMap((response: TResponse) => {
                const key = this.getDetailsKey(cacheEntityId ? cacheEntityId : command.id);
                return this.saveCacheItem(key, response);
            }),
            tap(() => this.deQueueBackgroungJobs()),
            catchError((err) => {
                if (!(err instanceof NoConnectionError)) {
                    return throwError(err);
                }
                return this
                    .updateGeoJsonLayer(command)
                    .pipe(
                        flatMap(c => this.updateCacheById(command, cacheEntityId)),
                        tap(() => this.backgroundJobService.syncItemAdded())
                    );
            })
        );
    }

    executeDelete(
        command: TCommand,
        getResponseFunc: (command: TCommand) => Observable<boolean>
    ): Observable<boolean> {
        return getResponseFunc(command).pipe(
            catchError((err) => {
                if (!(err instanceof NoConnectionError)) {
                    return throwError(err);
                }
                return this.updateGeoJsonLayer(command).pipe(
                    flatMap(c => this.cacheService.removeItem(command?.toString()))
                );
            })
        );
    }

    protected deQueueBackgroungJobs(): void {
        this.backgroundJobService.deQueue();
    }

    protected updateGeoJsonLayer(command: TCommand): Observable<any> {
        const updateGeoJsonCommands = this.mapToGeoJsonUpdateCommands(command);

        if (!updateGeoJsonCommands) { return of(true); }

        return forkJoin([
            this.geoJsonUpdateCommandsStore.add(updateGeoJsonCommands),
            of(this.mapStateService.refreshLayers(updateGeoJsonCommands[0].layerName))
        ]);
    }

    protected updateCacheById(command: TCommand, cacheEntityId?: string): Observable<TResponse> {
        const key = this.getDetailsKey(cacheEntityId ? cacheEntityId : command.id);
        return from(this.cacheService.getItem<TResponse>(key))
            .pipe(
                flatMap(valueFromCache => this.updateCacheValue(command, valueFromCache)),
                catchError(() => this.updateCacheValue(command))
            )
            .pipe(
                flatMap(data => this.saveCacheItem(key, data))
            );
    }

    private saveCacheItem(key: string, item: TResponse): Observable<TResponse> {
        return from(this.cacheService.saveItem(key, item)).pipe(map(savedItem => item));
    }

    abstract mapToGeoJsonUpdateCommands(command: TCommand): Array<GeoJsonUpdateCommand>;
    abstract updateCacheValue(command: TCommand, target?: TResponse): Observable<TResponse>;
    abstract get entityCacheKey(): string;

    private getDetailsKey(key: string): string {
        return `${this.entityCacheKey}_${key}`;
    }
}
