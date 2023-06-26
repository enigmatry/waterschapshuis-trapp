import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IListItem, ListItem } from '../models/list-item';
import { AreasClient, GetAreaEntitiesResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

@Injectable({
  providedIn: 'root'
})
export class AreasService {

  constructor(
    private areasClient: AreasClient
  ) { }

  getCatchAreas(rayonId: string, filterByOrganization: boolean): Observable<Array<IListItem>> {
    return this.areasClient.getCatchAreas(undefined, rayonId, filterByOrganization)
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
