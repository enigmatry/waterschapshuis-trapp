import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';

import { CacheModule } from '../cache/cache.module';
import { WmtsMapComponent } from '../maps/wmts-map/wmts-map.component';
import { SharedModule } from '../shared/shared.module';
import { TrapService } from '../traps/services/trap.service';
import { BackgroundLocationComponent } from './background-location/background-location.component';
import { MapFilterComponent } from './map-filter/map-filter.component';
import { MapFineTunningComponent } from './map-fine-tunning/map-fine-tunning.component';
import { MapLocationModalComponent } from './map-location-modal/map-location-modal.component';
import { MapLegendComponent } from './map-menu/map-legend/map-legend.component';
import { MapMenuLayersComponent } from './map-menu/map-menu-layers/map-menu-layers.component';
import { MapMultiSelectModalComponent } from './map-multi-select-modal/map-multi-select-modal.component';
import {
  ObservationBasicComponent,
} from './map-multi-select-modal/observation-basic-details/observation-basic-details.component';
import { TrapBasicDetailsComponent } from './map-multi-select-modal/trap-basic-details/trap-basic-details.component';
import { MapTrackingModalComponent } from './map-tracking-modal/map-tracking-modal.component';
import { MapComponent } from './map/map.component';
import { MapsRoutingModule } from './maps-routing.module';
import { FindService } from './services/find.service';
import { LaunchNavigatorService } from './services/launch-navigator.service';
import { LayersService } from './services/layers.service';
import { TrapModalComponent } from './trap-modal/trap-modal.component';
import { MapTrackingFilterComponent } from './map-menu/map-tracking-filter/map-tracking-filter.component';


@NgModule({
  declarations: [
    MapComponent,
    WmtsMapComponent,
    MapFineTunningComponent,
    MapMenuLayersComponent,
    MapLegendComponent,
    MapFilterComponent,
    BackgroundLocationComponent,
    MapMultiSelectModalComponent,
    TrapBasicDetailsComponent,
    ObservationBasicComponent,
    TrapModalComponent,
    MapTrackingModalComponent,
    MapLocationModalComponent,
    MapTrackingFilterComponent
  ],
  providers: [
    TrapService,
    LayersService,
    FindService,
    LaunchNavigatorService
  ],
  imports: [
    CommonModule,
    IonicModule,
    MapsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CacheModule,
    SharedModule
  ],
  entryComponents: [
    MapMultiSelectModalComponent,
    TrapModalComponent
  ]
})
export class MapsModule { }
