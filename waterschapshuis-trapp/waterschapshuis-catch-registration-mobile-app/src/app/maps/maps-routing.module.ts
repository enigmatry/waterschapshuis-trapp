import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { WmtsMapComponent } from './wmts-map/wmts-map.component';
import { MapMenuLayersComponent } from './map-menu/map-menu-layers/map-menu-layers.component';
import { MapLegendComponent } from './map-menu/map-legend/map-legend.component';
import { MapFilterComponent } from './map-filter/map-filter.component';
import { MapFineTunningComponent } from './map-fine-tunning/map-fine-tunning.component';
import { MapTrackingFilterComponent } from './map-menu/map-tracking-filter/map-tracking-filter.component';

const appRoutes: Routes = [
  {
    path: '', component: WmtsMapComponent,
  },
  {
    path: 'map-menu-layers', component: MapMenuLayersComponent
  },
  {
    path: 'map-tracking-filter', component: MapTrackingFilterComponent
  },
  {
    path: 'legend', component: MapLegendComponent
  },
  {
    path: 'filter', component: MapFilterComponent
  },
  {
    path: 'fine-tunning', component: MapFineTunningComponent
  },
  {
    path: 'fine-tunning/:trapId', component: MapFineTunningComponent
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
export class MapsRoutingModule { }
