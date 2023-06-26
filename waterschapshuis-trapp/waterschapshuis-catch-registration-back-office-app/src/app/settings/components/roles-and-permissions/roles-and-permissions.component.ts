import { Component, OnInit, OnDestroy } from '@angular/core';
import { SettingsBaseComponent } from '../settings-base.component';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { RolesService } from '../../services/roles.service';
import {
  GetRolesResponseItem,
  UpdateRolesPermissionsCommandItem,
  UpdateRolesPermissionsCommand
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { Router } from '@angular/router';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CurrentUserService } from 'src/app/shared/current-user.service';
import { NgxPermissionsService } from 'ngx-permissions';

@Component({
  selector: 'app-roles-and-permissions',
  templateUrl: './roles-and-permissions.component.html',
  styleUrls: ['./roles-and-permissions.component.scss']
})
export class RolesAndPermissionsComponent extends SettingsBaseComponent implements OnInit, OnDestroy {

  allRoles: GetRolesResponseItem[] = [];
  policyName = PolicyName;
  rolesHeader: string[] = [];
  permissionsHeader: string[] = [];

  constructor(
    sideBarService: SideBarService,
    private rolesService: RolesService,
    private route: Router,
    private successBar: MatSnackBar,
    private currentUserService: CurrentUserService,
    private permissionsService: NgxPermissionsService) {
    super(sideBarService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.getRoles();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  getRoles() {
    this.rolesService.getAllRoles()
      .subscribe(response => {
        this.allRoles = response;
        this.rolesHeader = response.map(r => r.name);
        this.permissionsHeader = response[0].permissions.map(p => p.name);
      });
  }

  saveRolesAndPermissions(): void {
    this.rolesService.updateRolesPermissions(this.createUpdateRolesPermissionsCommand())
      .subscribe(response => {
        this.tryLoadDataAfterRolesPermissionsUpdate();
        this.openSnackBar('Succesvolle toewijzing van rechten.', 'OK');
      });
  }

  private tryLoadDataAfterRolesPermissionsUpdate = () => {
    this.currentUserService
      .createUserFromIdentity()
      .then(() => {
        this.permissionsService
          .hasPermission(PolicyName.RoleRead)
          .then(hasPermission => {
            if (hasPermission) { this.getRoles(); } else { this.route.navigate(['home']); }
          });
      });
  }

  createUpdateRolesPermissionsCommand(): UpdateRolesPermissionsCommand {
    return UpdateRolesPermissionsCommand.fromJS({roles: this.allRoles
      .map((role: GetRolesResponseItem) => {
        return UpdateRolesPermissionsCommandItem.fromJS({
          id: role.id,
          name: role.name,
          permissionIds: (role.permissions.filter(p => p.assignedToRole)).map(p => p.id)
        });
      })});
  }

  openSnackBar(message: string, action: string) {
    this.successBar.open(message, action, {
      duration: 2000
    });
  }

}
