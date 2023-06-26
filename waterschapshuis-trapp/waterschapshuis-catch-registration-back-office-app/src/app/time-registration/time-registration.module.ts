import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPermissionsModule } from 'ngx-permissions';
import { TimeRegistrationsClient } from '../api/waterschapshuis-catch-registration-backoffice-api';
import { SharedModule } from '../shared/shared.module';
import { TimeRegistrationRoutingModule } from './time-registration-routing.module';
import { TimeRegistrationService } from './services/time-registration.service';
import { TimeRegistrationStateService } from './services/time-registration-state.service';
import { TimeRegistrationsManagementComponent } from './components/time-registrations-management/time-registrations-management.component';
import { SideBarTimeRegistrationComponent } from './components/side-bar/side-bar-time-registrations/side-bar-time-registration.component';
import { UsersWithTimeRegistrationsPerRayonComponent } from './components/side-bar/users-with-time-registrations-per-rayon/users-with-time-registrations-per-rayon.component';
import { TimeRegistrationsPersonalComponent } from './components/time-registrations-personal/time-registrations-personal.component';
import { TimeRegistrationsHeaderComponent } from './components/time-registrations-header/time-registrations-header.component';
import { TimeRegistrationsTableComponent } from './components/time-registrations-table/time-registrations-table.component';
import { CatchRegistrationsTableComponent } from './components/catch-registrations-table/catch-registrations-table.component';
import { TimeRegistrationsOfDateComponent } from './components/time-registrations-table/time-registrations-of-date/time-registrations-of-date.component';
import { GeneralTimeRegistrationsTableComponent } from './components/general-time-registrations-table/general-time-registrations-table.component';

@NgModule({
  declarations: [
    SideBarTimeRegistrationComponent,
    UsersWithTimeRegistrationsPerRayonComponent,
    TimeRegistrationsPersonalComponent,
    TimeRegistrationsOfDateComponent,
    TimeRegistrationsHeaderComponent,
    TimeRegistrationsManagementComponent,
    TimeRegistrationsTableComponent,
    CatchRegistrationsTableComponent,
    GeneralTimeRegistrationsTableComponent
  ],
  imports: [
    TimeRegistrationRoutingModule,
    SharedModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPermissionsModule.forChild()
  ],
  providers: [
    TimeRegistrationsClient,
    TimeRegistrationService,
    TimeRegistrationStateService,
    DatePipe
  ],
  entryComponents: [
    SideBarTimeRegistrationComponent
]
})
export class TimeRegistrationModule { }
