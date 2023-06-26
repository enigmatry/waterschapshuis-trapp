import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import {
  GetTrapTypesResponseItem,
  GetCatchTypesResponseItem,
  ListResponseOfGetTrapTypesResponseItem,
  ListResponseOfGetCatchTypesResponseItem,
  ListResponseOfGetTrappingTypesResponseItem,
  ListResponseOfGetTimeRegistrationCategoriesResponseItem
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { LookupsCachedClient } from 'src/app/cache/clients/lookups-cached.client';
import { IListItem, ListItem } from '../models/list-item';

@Injectable({
  providedIn: 'root'
})
export class LookupsService {

  constructor(
    private lookupsClient: LookupsCachedClient
  ) { }

  getCatchTypeListItems(catches?: boolean): Observable<IListItem[]> {
    return this.lookupsClient.catchTypes()
      .pipe(
        map((response: ListResponseOfGetCatchTypesResponseItem) => ListItem.mapToListItems(
          (catches === undefined) ?
            response.items :
            response.items.filter(i => i.isByCatch !== catches)))
      );
  }

  getCatchTypes(catches?: boolean): Observable<GetCatchTypesResponseItem[]> {
    return this.lookupsClient.catchTypes()
      .pipe(
        map((response: ListResponseOfGetCatchTypesResponseItem) =>
          (catches === undefined) ?
            response.items :
            response.items.filter(i => i.isByCatch !== catches))
      );
  }

  getCatchType(catchTypeId: string): Observable<GetCatchTypesResponseItem> {
    return this.lookupsClient.catchTypes()
      .pipe(
        map((response: ListResponseOfGetCatchTypesResponseItem) =>
          response.items.find(c => c.id === catchTypeId))
      );
  }

  getTrapTypes(trappingTypeId?: string): Observable<GetTrapTypesResponseItem[]> {
    return this.lookupsClient.trapTypes()
      .pipe(
        map((response: ListResponseOfGetTrapTypesResponseItem) =>
            (trappingTypeId === undefined) ?
              response.items :
              response.items.filter(i => i.trappingTypeId === trappingTypeId))
      );
  }

  getTrapType(trapTypeId: string): Observable<GetTrapTypesResponseItem> {
    return this.lookupsClient.trapTypes()
      .pipe(
        map((response: ListResponseOfGetTrapTypesResponseItem) =>
          response.items.find(t => t.id === trapTypeId))
      );
  }

  getTrappingTypes(): Observable<IListItem[]> {
    return this.lookupsClient.trappingTypes()
      .pipe(
        map((response: ListResponseOfGetTrappingTypesResponseItem) =>
          ListItem.mapAreaEntitiesResponse(response))
      );
  }

  getTimeRegistrationActiveCategories(): Observable<IListItem[]> {
    return this.lookupsClient.timeRegistrationCategories()
      .pipe(map((response: ListResponseOfGetTimeRegistrationCategoriesResponseItem) => {
        const activeCategories =
          new ListResponseOfGetTimeRegistrationCategoriesResponseItem({ items: response.items.filter(c => c.active) });
        return ListItem.mapAreaEntitiesResponse(activeCategories);
      }));
  }
}
