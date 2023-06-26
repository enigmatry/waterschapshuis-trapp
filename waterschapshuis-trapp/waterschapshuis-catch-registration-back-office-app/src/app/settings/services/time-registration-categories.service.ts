import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import {
    GetTimeRegistrationCategoryResponse,
    PagedResponseOfGetTimeRegistrationCategoryResponse,
    TimeRegistrationCategoryClient,
    TimeRegistrationCategoryCreateOrUpdateCommand
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { QueryModel } from 'src/app/shared/models/query-model';

@Injectable({
    providedIn: 'root'
})
export class TimeRegistrationCategoriesService {

    constructor(
        private timeRegistrationCategoriesClient: TimeRegistrationCategoryClient
    ) { }

    getAllTimeRegistrationCategories(query: QueryModel) {
        return this.timeRegistrationCategoriesClient.searchTimeRegistrationCategories(query.keyword, query.sortField,
            query.sortDirection, query.pageSize,
            query.currentPage)
            .pipe(map((response: PagedResponseOfGetTimeRegistrationCategoryResponse) => {
                return response;
            }));
    }

    saveTimeRegistrationCategory(command: TimeRegistrationCategoryCreateOrUpdateCommand): Observable<GetTimeRegistrationCategoryResponse> {
        return this.timeRegistrationCategoriesClient.post(command)
            .pipe(
                map((response: GetTimeRegistrationCategoryResponse) => response)
            );
    }
}
