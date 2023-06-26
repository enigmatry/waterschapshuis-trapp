import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { QueryModel } from 'src/app/shared/models/query-model';
import {
  GetUserDetailsResponse,
  IPagedResponseOfGetUsersResponseItem,
  UpdateUserRolesCommand,
  UsersClient,
  UserUpdateConfidentialityCommand
} from '../../api/waterschapshuis-catch-registration-backoffice-api';
import { UserUpdateCommand } from './../../api/waterschapshuis-catch-registration-backoffice-api';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private usersClient: UsersClient) { }

  getUsers(query: QueryModel) {
    return this.usersClient.search(
      query.keyword, query.sortField,
      query.sortDirection, query.pageSize,
      query.currentPage
    ).pipe(
      map((res: IPagedResponseOfGetUsersResponseItem) => res)
    );
  }

  updateUser(command: UserUpdateCommand): Observable<GetUserDetailsResponse> {
    return this.usersClient.post(command)
      .pipe(
        map((response: GetUserDetailsResponse) => response)
      );
  }

  updateConfidentiality(command: UserUpdateConfidentialityCommand): Observable<GetUserDetailsResponse> {
    return this.usersClient.updateConfidentiality(command)
      .pipe(
        map((response: GetUserDetailsResponse) => response)
      );
  }

  updateUserRoles = (command: UpdateUserRolesCommand): Observable<GetUserDetailsResponse> =>
    this.usersClient
      .put(command)
      .pipe(map((response: GetUserDetailsResponse) => response))
}

