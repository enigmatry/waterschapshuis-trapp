import { Injectable } from '@angular/core';
import {
    FieldTestsClient,
    PagedResponseOfGetFieldTestResponse,
    FieldTestCreateOrUpdateCommand,
    GetFieldTestResponse
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { QueryModel } from 'src/app/shared/models/query-model';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class FieldTestsService {

    constructor(
        private fieldTestClient: FieldTestsClient
    ) { }

    getAllFieldTests(query: QueryModel) {
        return this.fieldTestClient.searchFieldTests(query.keyword, query.sortField,
            query.sortDirection, query.pageSize,
            query.currentPage)
            .pipe(map((response: PagedResponseOfGetFieldTestResponse) => response));
    }

    saveFieldTest(command: FieldTestCreateOrUpdateCommand): Observable<GetFieldTestResponse> {
        return this.fieldTestClient.post(command)
          .pipe(
            map((response: GetFieldTestResponse) => response)
          );
      }

}
