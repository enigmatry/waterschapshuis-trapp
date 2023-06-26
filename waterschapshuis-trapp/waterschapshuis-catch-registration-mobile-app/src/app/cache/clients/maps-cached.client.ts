import {
    IMapsClient,
    MapsClient,
    OverlayLayerCategoryCode,
    ListResponseOfGetOverlayLayersResponseItem,
    ListResponseOfGetBackgroundLayersResponseItem,
    GetBackgroundLayersQuery,
    GetMapStylesResponse
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Observable, forkJoin } from 'rxjs';
import { CacheService } from '../cache.service';
import { Injectable } from '@angular/core';
import { CachedClient } from './cached.client';
import { environment } from 'src/environments/environment';
import { MAP_OVERLAY_LAYERS } from 'src/app/maps/models/overlay-layer.model';

@Injectable({
    providedIn: 'root'
})
export class MapsCachedClient implements IMapsClient, CachedClient {

    constructor(private mapsClient: MapsClient, private cache: CacheService) { }

    overlayLayers(orderedLayerCategoryCodes: OverlayLayerCategoryCode[], year: number, numberOfYears: number)
        : Observable<ListResponseOfGetOverlayLayersResponseItem> {

        const result$ = this.mapsClient.overlayLayers(orderedLayerCategoryCodes, year, numberOfYears);
        const cacheKey = `${orderedLayerCategoryCodes.join()}-${year}-${numberOfYears}`;

        return this.cache.loadFromObservable(`mapOverlayLayers_${cacheKey}`, result$);
    }

    backgroundLayers(): Observable<ListResponseOfGetBackgroundLayersResponseItem> {
        const result$ = this.mapsClient.backgroundLayers(new GetBackgroundLayersQuery());
        return this.cache.loadFromObservable(`mapBackgroundLayers`, result$);
    }

    styles(): Observable<GetMapStylesResponse> {
        const result$ = this.mapsClient.styles();
        return this.cache.loadFromObservable(`mapStyles`, result$);
    }

    cacheAllWithin(): Observable<any> {
        const overlayLayers$ = this.overlayLayers(
            MAP_OVERLAY_LAYERS,
            new Date().getFullYear(),
            Number(environment.appSettings.numberOfHistoricalTrapLayers)
        );
        const backgroundLayers$ = this.backgroundLayers();
        const styles$ = this.styles();

        return forkJoin([overlayLayers$, backgroundLayers$, styles$]);
    }
}
