import { Injectable } from '@angular/core';
import {
    TrapTypesClient,
    PagedResponseOfGetTrapTypeResponse,
    GetTrapTypeResponse,
    TrapTypeCreateOrUpdateCommand
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { QueryModel } from 'src/app/shared/models/query-model';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class TrapTypesService {

    constructor(
        private trapTypesClient: TrapTypesClient
    ) { }

    getAllTrapTypes(query: QueryModel) {
        return this.trapTypesClient.searchTrapType(query.keyword, query.sortField,
            query.sortDirection, query.pageSize,
            query.currentPage)
            .pipe(map((response: PagedResponseOfGetTrapTypeResponse) => {
                return response;
            }));
    }

    saveTrapType(command: TrapTypeCreateOrUpdateCommand): Observable<GetTrapTypeResponse> {
        return this.trapTypesClient.post(command)
          .pipe(
            map((response: GetTrapTypeResponse) => response)
          );
      }
}
