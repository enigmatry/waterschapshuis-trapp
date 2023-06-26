import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { TimeRegistrationComponent } from '../time-registration/time-registration/time-registration.component';
import { TimeRegistrationRoutingModule } from './time-registration-routing.module';
import { TimeRegistrationService } from './services/time-registration.service';
import { TimeRegistrationsClient } from '../api/waterschapshuis-catch-registration-mobile-api';
import { TimeRegistrationItemComponent } from './time-registration-item/time-registration-item.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    ReactiveFormsModule,
    TimeRegistrationRoutingModule,
    SharedModule
  ],
  declarations: [
    TimeRegistrationComponent,
    TimeRegistrationItemComponent
  ],
  providers: [
    TimeRegistrationsClient,
    TimeRegistrationService
  ],
  bootstrap: [
    TimeRegistrationComponent
  ]
})
export class TimeRegistrationModule { }
