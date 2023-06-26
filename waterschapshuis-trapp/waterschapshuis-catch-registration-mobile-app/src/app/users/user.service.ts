import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import {
  GetUserDetailsResponse,
  UsersClient,
  UserUpdateConfidentialityCommand,
} from '../api/waterschapshuis-catch-registration-mobile-api';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private usersClient: UsersClient) { }

  updateConfidentiality(command: UserUpdateConfidentialityCommand): Observable<GetUserDetailsResponse> {
    return this.usersClient.updateConfidentiality(command)
      .pipe(
        map((response: GetUserDetailsResponse) => response)
      );
  }

}
