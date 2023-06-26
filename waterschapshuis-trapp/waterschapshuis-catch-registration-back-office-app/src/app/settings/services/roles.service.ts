import { Injectable } from '@angular/core';
import { RolesClient, GetRolesQuery, GetRolesResponseItem, UpdateRolesPermissionsCommand } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class RolesService {

  constructor(private rolesClient: RolesClient) { }

  getAllRoles = (): Observable<GetRolesResponseItem[]> =>
    this.rolesClient
      .getAllRoles(new GetRolesQuery())
      .pipe(map(x => x.items))

  updateRolesPermissions = (allRolesAndPermissions: UpdateRolesPermissionsCommand): Observable<GetRolesResponseItem[]> =>
    this.rolesClient
      .updateRolesPermissions(allRolesAndPermissions)
      .pipe(map(x => x.items))

}
