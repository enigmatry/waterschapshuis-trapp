import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ObservationDetailsComponent } from './observation-details/observation-details.component';
import { IonicModule } from '@ionic/angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { ObservationsRoutingModule } from './observations-routing.module';

@NgModule({
  declarations: [ ObservationDetailsComponent ],
  imports: [
    CommonModule,
    ObservationsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    SharedModule
  ]
})
export class ObservationsModule { }
