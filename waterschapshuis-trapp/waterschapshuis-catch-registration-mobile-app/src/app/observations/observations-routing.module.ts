import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ObservationDetailsComponent } from './observation-details/observation-details.component';


const routes: Routes = [
  {
    path: '', component: ObservationDetailsComponent
  },
  {
    path: ':observationId', component: ObservationDetailsComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ObservationsRoutingModule { }
