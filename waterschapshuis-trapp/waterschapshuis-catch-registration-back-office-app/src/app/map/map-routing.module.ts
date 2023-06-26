import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MapOverviewComponent } from './map-overview/map-overview.component';
import { AuthGuard } from '../core/auth/auth.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: MapOverviewComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MapRoutingModule { }
