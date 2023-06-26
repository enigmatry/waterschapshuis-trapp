import { Injectable } from '@angular/core';
import {
  AreasClient,
  GetAreaEntitiesResponse,
  GetLocationAreaDetailsResponse
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IListItem, ListItem } from '../models/list-item';
import { AreaDetails } from '../models/area-details';

@Injectable({
  providedIn: 'root'
})
export class AreasService {

  constructor(
    private areasClient: AreasClient
  ) { }

  getLocationDetails(longitude: number, latitude: number): Observable<AreaDetails> {
    return this.areasClient.getLocationDetails(longitude, latitude)
      .pipe(
        map((response: GetLocationAreaDetailsResponse) => AreaDetails.fromResponse(response))
      );
  }

  getLocationData(catchAreaId: string, subAreaId: string) {
    return this.areasClient.getLocationData(catchAreaId, subAreaId);
  }

  getCatchAreas(): Observable<Array<IListItem>> {
    return this.areasClient.getCatchAreas(undefined, undefined)
      .pipe(
        map((response: GetAreaEntitiesResponse) => ListItem.mapAreaEntitiesResponse(response))
      );
  }

  getSubAreas(catchAreaId: string): Observable<Array<IListItem>> {
    return this.areasClient.getSubAreas(undefined, catchAreaId)
      .pipe(
        map((response: GetAreaEntitiesResponse) => ListItem.mapAreaEntitiesResponse(response))
      );
  }

  getHourSquares(subAreaId: string): Observable<Array<IListItem>> {
    return this.areasClient.getHourSquares(undefined, subAreaId)
      .pipe(
        map((response: GetAreaEntitiesResponse) => ListItem.mapAreaEntitiesResponse(response))
      );
  }
}
