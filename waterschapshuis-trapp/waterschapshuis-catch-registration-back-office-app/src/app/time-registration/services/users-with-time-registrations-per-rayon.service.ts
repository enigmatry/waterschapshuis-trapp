import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { TimeRegistrationUser } from '../models/time-registrations-user';

@Injectable({
    providedIn: 'root'
})
export class UsersWithTimeRegistrationsPerRayonService {
    private selectedUserSubject: Subject<TimeRegistrationUser> = new Subject<TimeRegistrationUser>();
    selectedUser$ = this.selectedUserSubject.asObservable();

    changeSelectedUser(user?: TimeRegistrationUser) {
        this.selectedUserSubject.next(user);
    }
}
