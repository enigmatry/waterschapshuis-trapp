import { NgModule } from '@angular/core';

import { CatchService } from './services/catch.service';
import { LookupsService } from '../shared/services/lookups.service';
import { TrapComponent } from './trap/trap.component';
import { TrapService } from './services/trap.service';
import { TrapsRoutingModule } from './traps-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { CacheModule } from '../cache/cache.module';
import { SharedModule } from '../shared/shared.module';
import { UserSummaryComponent } from './user-summary/user-summary.component';
import { TrapGeneralInfoComponent } from './trap/trap-general-info/trap-general-info.component';
import { CatchDetailsComponent } from './trap/catch-details/catch-details.component';
import { ByCatchDetailsComponent } from './trap/by-catch-details/by-catch-details.component';
import { ByCatchDetailsEditComponent } from './trap/by-catch-details/by-catch-details-edit/by-catch-details-edit.component';
import { TrapHistoryComponent } from './trap/trap-history/trap-history.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    TrapsRoutingModule,
    CacheModule,
    SharedModule
  ],
  declarations: [
    TrapComponent,
    TrapGeneralInfoComponent,
    UserSummaryComponent,
    CatchDetailsComponent,
    ByCatchDetailsComponent,
    ByCatchDetailsEditComponent,
    TrapHistoryComponent,
  ],
  providers: [
    LookupsService,
    CatchService,
    TrapService
  ]
})
export class TrapsModule { }
