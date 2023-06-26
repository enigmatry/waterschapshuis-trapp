import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { LookupsService } from 'src/app/shared/services/lookups.service';

import {
  AnimalType,
  IGetCatchTypesResponseItem,
  INamedEntityItem,
} from '../../api/waterschapshuis-catch-registration-mobile-api';

@Injectable({
  providedIn: 'root'
})
export class CatchService {

  constructor(
    private lookupsService: LookupsService
  ) {
  }

  getCatchTypeListItems(): Observable<Array<INamedEntityItem>> {
    return this.lookupsService.getCatchTypeListItems(true);
  }

  getByCatchTypeListItems(): Observable<Array<INamedEntityItem>> {
    return this.lookupsService.getCatchTypeListItems(false);
  }

  getCatchTypes(): Observable<Array<IGetCatchTypesResponseItem>> {
    return this.lookupsService.getCatchTypes(true);
  }

  getByCatchTypesByAnimalType(animalType: AnimalType): Observable<Array<IGetCatchTypesResponseItem>> {
    return this.lookupsService.getCatchTypes(false)
      .pipe(
        map((items: Array<IGetCatchTypesResponseItem>) => items.filter(i => i.animalType === animalType))
      );
  }
}
