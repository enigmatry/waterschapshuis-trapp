import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPermissionsModule } from 'ngx-permissions';
import { NgSelectModule } from '@ng-select/ng-select';

import { MapsClient, ReportsClient, AreasClient, LookupsClient, SettingsClient } from '../api/waterschapshuis-catch-registration-backoffice-api';
import { MaterialModule } from './material/material.module';
import { MapService } from './services/map.service';
import { AlertContentComponent } from './alert/alert/alert-content.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { SpinnerService } from './services/spinner.service';

@NgModule({
  imports: [
    MaterialModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule
  ],
  exports: [
    MaterialModule,
    CommonModule,
    NgxPermissionsModule,
    FormsModule,
    ReactiveFormsModule,
    AlertContentComponent,
    SpinnerComponent,
    NgSelectModule
  ],
  providers: [
    MapService,
    SettingsClient,
    MapsClient,
    ReportsClient,
    AreasClient,
    LookupsClient,
    SpinnerService
  ],
  declarations: [
    AlertContentComponent,
    SpinnerComponent
  ]
})
export class SharedModule {}
