import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGetGeoServerSettingsResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { SettingsClient } from './../../api/waterschapshuis-catch-registration-backoffice-api';
import { ObservablesCache } from './observables.cache';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  private cache = new ObservablesCache();

  constructor(private settingsClient: SettingsClient) { }

  getGeoServerSettings(): Observable<IGetGeoServerSettingsResponse> {
    return this.cache.getFromCache('geoServerSettings', () => this.settingsClient.getGeoServerSettings());
  }
}
