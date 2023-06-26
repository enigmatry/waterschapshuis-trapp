import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AreaRoutingModule } from './area-routing.module';
import { AreaDataComponent } from './area-data/area-data.component';
import { SharedModule } from '../shared/shared.module';
import { IonicModule } from '@ionic/angular';

@NgModule({
  declarations: [AreaDataComponent],
  imports: [
    IonicModule,
    CommonModule,
    AreaRoutingModule
  ]
})
export class AreaModule { }
