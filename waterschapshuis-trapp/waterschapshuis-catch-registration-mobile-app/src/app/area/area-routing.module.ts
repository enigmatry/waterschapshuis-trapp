import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AreaDataComponent } from './area-data/area-data.component';


const routes: Routes = [
  { path: '', component: AreaDataComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AreaRoutingModule { }
