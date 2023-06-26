import { Injectable } from '@angular/core';
import { OrganizationsClient, GetOrganizationsResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { Observable, of, Subject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { IListItem, ListItem } from '../models/list-item';

@Injectable({
    providedIn:
        'root'
})

export class OrganizationService {
    constructor(private organizationsClient: OrganizationsClient) { }

    private organizations: GetOrganizationsResponse | null = null;

    getOrganizations(): Observable<GetOrganizationsResponse> {
        return this.organizations
        ? of(this.organizations)
        : this.organizationsClient.search(null)
        .pipe(
            map(response =>
              this.organizations = response)
          );
    }

}
