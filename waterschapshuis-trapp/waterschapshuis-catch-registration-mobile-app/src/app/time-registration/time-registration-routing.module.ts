import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { TimeRegistrationComponent } from './time-registration/time-registration.component';

const appRoutes: Routes = [
  {
    path: '', component: TimeRegistrationComponent,
  },
  {
    path: 'home', component: TimeRegistrationComponent
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class TimeRegistrationRoutingModule { }
