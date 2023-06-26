import { Injectable } from '@angular/core';
import {
    CatchTypesClient,
    PagedResponseOfGetCatchTypeResponse,
    CatchTypeCreateOrUpdateCommand,
    GetCatchTypeResponse
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { QueryModel } from 'src/app/shared/models/query-model';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CatchTypesService {

    constructor(
        private catchTypesClient: CatchTypesClient
    ) { }

    getAllCatchTypes(query: QueryModel) {
        return this.catchTypesClient.searchCatchType(query.keyword, query.sortField,
            query.sortDirection, query.pageSize,
            query.currentPage)
            .pipe(map((response: PagedResponseOfGetCatchTypeResponse) => response));
    }

    saveCatchType(command: CatchTypeCreateOrUpdateCommand): Observable<GetCatchTypeResponse> {
        return this.catchTypesClient.post(command)
          .pipe(
            map((response: GetCatchTypeResponse) => response)
          );
      }

}
