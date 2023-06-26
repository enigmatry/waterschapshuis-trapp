import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersRolesComponent } from './components/users-roles/users-roles.component';
import { UsersOverviewComponent } from './components/users-overview/users-overview.component';
import { RolesAndPermissionsComponent } from './components/roles-and-permissions/roles-and-permissions.component';
import { CatchTypesComponent } from './components/catch-types/catch-types.component';
import { TrapTypesComponent } from './components/trap-types/trap-types.component';
import { FieldTestsComponent } from './components/field-tests/field-tests.component';
import { AuthGuard } from '../core/auth/auth.guard';
import { TopologyMaintenanceComponent } from './components/topology-maintenance/topology-maintenance.component';
import { TimeRegistrationCategoriesComponent } from './components/time-registration-categories/time-registration-categories.component';



const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo:  `/settings/users-overview`
  },
  {
    path: 'users-roles',
    canActivate: [AuthGuard],
    component: UsersRolesComponent
  },
  {
    path: 'users-overview',
    canActivate: [AuthGuard],
    component: UsersOverviewComponent
  },
  {
    path: 'users-roles-and-permissions',
    canActivate: [AuthGuard],
    component: RolesAndPermissionsComponent
  },
  {
    path: 'catch-types-list',
    canActivate: [AuthGuard],
    component: CatchTypesComponent
  },
  {
    path: 'trap-types-list',
    canActivate: [AuthGuard],
    component: TrapTypesComponent
  },
  {
    path: 'field-tests-list',
    canActivate: [AuthGuard],
    component: FieldTestsComponent
  },
  {
    path: 'time-registration-categories-list',
    canActivate: [AuthGuard],
    component: TimeRegistrationCategoriesComponent
  },
  {
    path: 'topology-maintenance',
    canActivate: [AuthGuard],
    component: TopologyMaintenanceComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
