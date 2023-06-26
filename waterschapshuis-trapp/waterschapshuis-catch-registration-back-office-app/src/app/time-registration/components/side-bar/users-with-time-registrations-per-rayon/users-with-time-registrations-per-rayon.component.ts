import { Component, OnInit } from '@angular/core';
import { TimeRegistrationWeekNavigationService } from 'src/app/time-registration/services/time-registration-week-navigation.service';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { TimeRegistrationService } from 'src/app/time-registration/services/time-registration.service';
import { UsersWithRegisteredTimePerRayon } from 'src/app/time-registration/models/users-with-registered-time-per-rayon';
import { UsersWithTimeRegistrationsPerRayonService } from 'src/app/time-registration/services/users-with-time-registrations-per-rayon.service';
import { WeekDateRange } from 'src/app/time-registration/models/week-date-range';
import { TimeRegistrationUser } from 'src/app/time-registration/models/time-registrations-user';

@Component({
  selector: 'app-users-with-time-registrations-per-rayon',
  templateUrl: './users-with-time-registrations-per-rayon.component.html',
  styleUrls: ['./users-with-time-registrations-per-rayon.component.scss']
})
export class UsersWithTimeRegistrationsPerRayonComponent extends OnDestroyMixin implements OnInit {

  component: any = this;
  usersWithRegisteredTimePerRayon: UsersWithRegisteredTimePerRayon[] = [];
  usersWithTimeRegistrationGeneralItems: TimeRegistrationUser[] = [];
  usersInOrganization: TimeRegistrationUser[];
  selectedWeekDateRange: WeekDateRange;
  selectedUser: TimeRegistrationUser;

  constructor(
    private timeRegistrationWeekNavigationService: TimeRegistrationWeekNavigationService,
    private timeRegistrationService: TimeRegistrationService,
    private usersWithTimeRegistrationsPerRayonService: UsersWithTimeRegistrationsPerRayonService) {
    super();
  }

  ngOnInit(): void {
    this.timeRegistrationWeekNavigationService.selectedYearAndWeekNumber$
      .pipe(untilComponentDestroyed(this.component))
      .subscribe(data => {
        this.selectedWeekDateRange = this.timeRegistrationWeekNavigationService
          .getStartAndEndDateOfWeek(data.currentYear, data.currentWeekNumber);

        this.timeRegistrationService.fetchUsersWithRegisteredTimePerRayon(this.selectedWeekDateRange)
          .subscribe(response => {
            this.usersWithRegisteredTimePerRayon = response.usersPerRayon;
            this.usersInOrganization = response.usersInOrganization;
            this.usersWithTimeRegistrationGeneralItems = response.usersWithTimeRegistrationGeneralItems;
            this.selectedUser = null;
          });
      });
  }

  refreshTimeRegistrationTable(user: TimeRegistrationUser, rayonId?: string) {
    this.selectedUser = user;
    user.rayonId = rayonId;
    this.usersWithTimeRegistrationsPerRayonService.changeSelectedUser(user);
  }

  updateUsersWeekCompletedStatus(userId: string, rayonId: string) {
    this.usersWithRegisteredTimePerRayon.flatMap(x => x.users).filter(u => u.id === userId && u.rayonId === rayonId).forEach(
      u => u.weekCompleted = true
    );
  }
}
