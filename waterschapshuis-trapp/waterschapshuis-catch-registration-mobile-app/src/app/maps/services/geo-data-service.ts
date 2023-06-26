import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { GeoDataResponse } from '../models/geodata-response.model';

@Injectable({
  providedIn: 'root'
})
export class GeoDataService {

  constructor(private httpClient: HttpClient) { }

  get(name: string, url: string): Promise<GeoDataResponse> {
    return this.httpClient.get(url, { responseType: 'text' })
      .pipe(map((data: string) => GeoDataResponse.fromObject(name, data))).toPromise();
  }
}
