import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {
  TimeRegistrationsClient,
  TimeRegistrationsEditCommand,
  GetTimeRegistrationsOfWeekResponse
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { TimeRegistrationsOfDate as TimeRegistrationsOnDate } from '../models/time-registrations-of-date';
import { WeekDateRange } from '../models/week-date-range';
import { CatchRegistration } from '../models/catch-registration';
import { UsersWithRegisteredTime } from '../models/users-with-registered-time';
import { TimeRegistrationGeneral } from '../models/time-registration-general';


@Injectable({
  providedIn: 'root'
})
export class TimeRegistrationService {

  constructor(
    private timeRegistrationsClient: TimeRegistrationsClient) { }

  fetchTimeRegistrations(weekDateRange: WeekDateRange): Observable<GetTimeRegistrationsOfWeekResponse> {
    return this.timeRegistrationsClient.get(weekDateRange.startDate, weekDateRange.endDate);
  }

  fetchTimeRegistrationsForUser(weekDateRange: WeekDateRange, userId: string, rayonId: string)
    : Observable<GetTimeRegistrationsOfWeekResponse> {
    return this.timeRegistrationsClient.getForUser(weekDateRange.startDate, weekDateRange.endDate, userId, rayonId);
  }

  fetchUsersWithRegisteredTimePerRayon(weekDateRange: WeekDateRange): Observable<UsersWithRegisteredTime> {
    return this.timeRegistrationsClient.getUsersWhoHaveRegisteredTimePerRayon(weekDateRange.startDate, weekDateRange.endDate)
      .pipe(map(response => UsersWithRegisteredTime.fromResponse(response)));
  }

  saveTimeRegistration(command: TimeRegistrationsEditCommand): Observable<any> {
    return this.timeRegistrationsClient.post(command)
      .pipe(
        map(response => {
          return {
            timeRegistrations: response.daysOfWeek.map(TimeRegistrationsOnDate.fromResponse),
            timeRegistrationGeneralItems : response.timeRegistrationGeneralItems.map(TimeRegistrationGeneral.fromResponse),
            catches: response.catches.map(CatchRegistration.fromResponse)
          };
        })
      );
  }

  saveTimeRegistrationForUser(userId: string, rayonId: string, command: TimeRegistrationsEditCommand): Observable<any> {
    return this.timeRegistrationsClient.postForUser(userId, rayonId, command)
      .pipe(
        map(response => {
          return {
            timeRegistrations: response.daysOfWeek.map(TimeRegistrationsOnDate.fromResponse),
            timeRegistrationGeneralItems : response.timeRegistrationGeneralItems.map(TimeRegistrationGeneral.fromResponse),
            catches: response.catches.map(CatchRegistration.fromResponse)
          };
        })
      );
  }

  getTodayUtcDateShortString(): string {
    return new Date().toISOString().substring(0, 10);
  }
}
