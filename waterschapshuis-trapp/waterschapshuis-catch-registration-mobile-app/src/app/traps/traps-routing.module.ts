import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TrapComponent } from './trap/trap.component';
import { ByCatchDetailsEditComponent } from './trap/by-catch-details/by-catch-details-edit/by-catch-details-edit.component';

const routes: Routes = [
  {
    path: 'vangmiddel', component: TrapComponent
  },
  { path: 'vangmiddel/:trapId', component: TrapComponent },
  { path: 'vangmiddel/:trapId/by-catch/:animalType', component: ByCatchDetailsEditComponent },
  { path: 'vangmiddel/by-catch/:animalType', component: ByCatchDetailsEditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TrapsRoutingModule { }
