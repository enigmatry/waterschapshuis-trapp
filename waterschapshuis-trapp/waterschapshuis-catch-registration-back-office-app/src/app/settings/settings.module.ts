import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPermissionsModule } from 'ngx-permissions';
import {
  UsersClient,
  RolesClient,
  OrganizationsClient,
  TrapTypesClient,
  CatchTypesClient,
  FieldTestsClient
} from '../api/waterschapshuis-catch-registration-backoffice-api';
import { SharedModule } from '../shared/shared.module';
import { UsersRolesComponent } from './components/users-roles/users-roles.component';
import { SettingsBaseComponent } from './components/settings-base.component';
import { SideBarSettingsComponent } from './components/side-bar/side-bar-settings.component';
import { UsersOverviewComponent } from './components/users-overview/users-overview.component';
import { UsersService } from './services/users.service';
import { SettingsRoutingModule } from './settings-routing.module';
import { OrganizationsChecklistComponent } from './components/organizations-checklist/organizations-checklist.component';
import { RolesAndPermissionsComponent } from './components/roles-and-permissions/roles-and-permissions.component';
import { CatchTypesComponent } from './components/catch-types/catch-types.component';
import { TrapTypesComponent } from './components/trap-types/trap-types.component';
import { EditCatchTypeComponent } from './components/edit-catch-type/edit-catch-type.component';
import { EditTrapTypeComponent } from './components/edit-trap-type/edit-trap-type.component';
import { FieldTestsComponent } from './components/field-tests/field-tests.component';
import { EditFieldTestComponent } from './components/edit-field-test/edit-field-test.component';
import { RolesChecklistComponent } from './components/roles-checklist/roles-checklist.component';
import { DeactivateUserDialogComponent } from './components/users-overview/deactivate-user-dialog/deactivate-user-dialog.component';
import { TopologyMaintenanceComponent } from './components/topology-maintenance/topology-maintenance.component';
import { TimeRegistrationCategoriesComponent } from './components/time-registration-categories/time-registration-categories.component';
import { EditTimeRegistrationCategoryComponent } from './components/edit-time-registration-category/edit-time-registration-category.component';
import { ImportTopologyDialogComponent } from './components/topology-maintenance/import-topology-dialog/import-topology-dialog.component';

@NgModule({
  declarations: [
    SettingsBaseComponent,
    SideBarSettingsComponent,
    UsersRolesComponent,
    UsersOverviewComponent,
    RolesChecklistComponent,
    OrganizationsChecklistComponent,
    RolesAndPermissionsComponent,
    CatchTypesComponent,
    TrapTypesComponent,
    EditCatchTypeComponent,
    EditTrapTypeComponent,
    FieldTestsComponent,
    EditFieldTestComponent,
    DeactivateUserDialogComponent,
    TopologyMaintenanceComponent,
    TimeRegistrationCategoriesComponent,
    EditTimeRegistrationCategoryComponent,
    ImportTopologyDialogComponent
  ],
  imports: [
    SettingsRoutingModule,
    SharedModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPermissionsModule.forChild()
  ],
  providers: [
    UsersClient,
    UsersService,
    RolesClient,
    OrganizationsClient,
    CatchTypesClient,
    TrapTypesClient,
    FieldTestsClient
  ],
  entryComponents: [SideBarSettingsComponent, RolesChecklistComponent, OrganizationsChecklistComponent]
})
export class SettingsModule { }
