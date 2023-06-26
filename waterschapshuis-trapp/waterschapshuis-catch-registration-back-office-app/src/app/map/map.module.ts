import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TrapsClient, CatchesClient } from '../api/waterschapshuis-catch-registration-backoffice-api';
import { SharedModule } from '../shared/shared.module';
import { MapOverviewComponent } from './map-overview/map-overview.component';
import { MapRoutingModule } from './map-routing.module';
import { MapStateService } from './services/map-state.service';
import { TrapsService } from './services/traps.service';
import { SideBarRightComponent } from './side-bar-right/side-bar-right.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { TrapInfoComponent } from './trap-info/trap-info.component';
import { TrapDetailsComponent } from './trap-details/trap-details.component';
import { EditCatchesComponent } from './components/edit-catches/edit-catches.component';
import { CatchService } from './services/catch.service';
import { ObservationInfoComponent } from './observation-info/observation-info.component';
import { ObservationDetailsComponent } from './observation-details/observation-details.component';
import { ObservationService } from './services/observation.service';
import { MapFilterComponent } from './side-bar/map-filter/map-filter.component';

@NgModule({
  declarations: [
    MapOverviewComponent,
    SideBarComponent,
    SideBarRightComponent,
    TrapInfoComponent,
    ObservationInfoComponent,
    TrapDetailsComponent,
    ObservationDetailsComponent,
    EditCatchesComponent,
    MapFilterComponent
  ],
  imports: [
    MapRoutingModule,
    SharedModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    MapOverviewComponent
  ],
  providers: [
    MapStateService,
    TrapsService,
    TrapsClient,
    CatchService,
    CatchesClient,
    ObservationService
  ],
  entryComponents: [
    SideBarComponent,
    SideBarRightComponent
  ]
})
export class MapModule { }
