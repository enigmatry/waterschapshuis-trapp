import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import {
  GetBackgroundLayersQuery,

  MapNetworkType, MapsClient,

  MapStyleLookup,
  OverlayLayerCategoryCode
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { BackgroundLayer } from 'src/app/shared/models/background-layer.model';
import { GeoDataResponse } from 'src/app/shared/models/geodata-response.model';
import { environment } from 'src/environments/environment';
import { OverlayLayer } from '../models/overlay-layer.model';
import { SettingsService } from './settings.service';

@Injectable({
  providedIn: 'root'
})
export class MapService {

  constructor(
    private httpClient: HttpClient,
    private mapsClient: MapsClient,
    private settingsService: SettingsService) { }

  getGeoData = (layer: BackgroundLayer): Observable<GeoDataResponse> =>
    this.httpClient
      .get(layer.url, { responseType: 'text' })
      .pipe(map((data: string) => GeoDataResponse.fromObject(layer.name, data)))

  getBackgroundLayers = (): Observable<BackgroundLayer[]> =>
    this.settingsService.getGeoServerSettings()
      .pipe(
        switchMap(settings => this.mapsClient
          .backgroundLayers(new GetBackgroundLayersQuery())
          .pipe(
            map(x => x.items.filter(bl => bl.networkType === MapNetworkType.Online)
              .map(item => BackgroundLayer.fromResponse(item, settings))))))

  getOverlayLayers = (orderedLayerCategoryCodes: OverlayLayerCategoryCode[]): Observable<OverlayLayer[]> =>
    this.settingsService.getGeoServerSettings()
      .pipe(
        switchMap(settings => this.mapsClient
          .overlayLayers(orderedLayerCategoryCodes, new Date().getFullYear(), Number(environment.appSettings.numberOfHistoricalTrapLayers))
          .pipe(map(x => x.items.map(item => OverlayLayer.fromResponse(item, settings))))))

  getStyles = (): Observable<MapStyleLookup[]> =>
    this.mapsClient
      .styles()
      .pipe(map(x => x.items))
}
