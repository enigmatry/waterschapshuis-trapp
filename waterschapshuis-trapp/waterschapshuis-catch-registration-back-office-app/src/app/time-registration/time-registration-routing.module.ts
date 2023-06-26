import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TimeRegistrationsManagementComponent } from './components/time-registrations-management/time-registrations-management.component';
import { TimeRegistrationsPersonalComponent } from './components/time-registrations-personal/time-registrations-personal.component';
import { TimeRegistrationsManagementPermissionGuard } from './services/time-registrations-management-permission.guard';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: `/time-registration/personal`
    },
    {
        path: 'personal',
        component: TimeRegistrationsPersonalComponent
    },
    {
        path: 'management',
        component: TimeRegistrationsManagementComponent,
        canActivate: [TimeRegistrationsManagementPermissionGuard]
    },
    {
        path: 'personal/:year/:week',
        component: TimeRegistrationsPersonalComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TimeRegistrationRoutingModule { }
