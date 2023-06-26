import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { IListItem, ListItem } from '../models/list-item';
import {
  LookupsClient,
  ListResponseOfGetTrappingTypesResponseItem,
  ListResponseOfGetCatchTypesResponseItem,
  ListResponseOfGetTrapTypesResponseItem,
  GetCatchTypesQuery,
  GetTrapTypesResponseItem,
  ListResponseOfGetTimeRegistrationCategoriesResponseItem,
  TopologiesClient,
  GetVersionRegionalLayoutsVersionRegionalLayoutResponse
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

@Injectable({
  providedIn: 'root'
})
export class LookupsService {

  private versionRegionalLayouts: GetVersionRegionalLayoutsVersionRegionalLayoutResponse[] | null = null;
  private trappingTypes: IListItem[] | null = null;
  private catchTypes: IListItem[] | null = null;
  private trapTypes: GetTrapTypesResponseItem[] | null = null;

  constructor(
    private lookupsClient: LookupsClient,
    private topologyClient: TopologiesClient
  ) { }

  getTrappingTypes(): Observable<IListItem[]> {
    return this.trappingTypes
      ? of(this.trappingTypes)
      : this.lookupsClient.trappingTypes()
        .pipe(
          map((response: ListResponseOfGetTrappingTypesResponseItem) =>
            this.trappingTypes = ListItem.mapAreaEntitiesResponse(response))
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

  getTrapTypes(trappingTypeId?: string): Observable<GetTrapTypesResponseItem[]> {
    return this.trapTypes
      ? of((trappingTypeId === undefined) ?
        this.trapTypes :
        this.trapTypes.filter(i => i.trappingTypeId === trappingTypeId))
      : this.lookupsClient.trapTypes(null)
        .pipe(
          map((response: ListResponseOfGetTrapTypesResponseItem) =>
            (trappingTypeId === undefined) ?
              response.items :
              response.items.filter(i => i.trappingTypeId === trappingTypeId)));
  }

  getCatchTypes(isByCatch: boolean): Observable<IListItem[]> {
    return this.catchTypes
      ? of(this.catchTypes)
      : this.lookupsClient.catchTypes(new GetCatchTypesQuery())
        .pipe(
          map((response: ListResponseOfGetCatchTypesResponseItem) =>
            ListItem.mapToListItems(response.items.filter(i => i.isByCatch === isByCatch)))
        );
  }

  getVersionRegionalLayouts = (): Observable<GetVersionRegionalLayoutsVersionRegionalLayoutResponse[]> =>
    this.versionRegionalLayouts
      ? of(this.versionRegionalLayouts)
      : this.topologyClient.getAllVersionRegionalLayouts()
        .pipe(map(x => this.versionRegionalLayouts = x.items.sort((a, b) => {
          return b.startDate.getTime() - a.startDate.getTime();
        })))
}
