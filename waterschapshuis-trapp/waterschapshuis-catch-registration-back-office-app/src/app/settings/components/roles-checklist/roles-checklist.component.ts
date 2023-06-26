import { Component, OnInit, Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RolesService } from '../../services/roles.service';
import { GetUsersResponseItemRole } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { Role } from '../../models/role.model';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';

@Component({
  selector: 'app-roles-checklist',
  templateUrl: './roles-checklist.component.html',
  styleUrls: ['./roles-checklist.component.scss']
})
export class RolesChecklistComponent implements OnInit {
  assignedRoles: GetUsersResponseItemRole[] = [];
  allRoles: Role[] = [];
  policyName = PolicyName;

  constructor(
    public dialogRef: MatDialogRef<RolesChecklistComponent>,
    private rolesService: RolesService,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.assignedRoles = data.roles;
  }

  close() {
    this.dialogRef.close();
  }

  saveAndClose() {
    this.dialogRef.close({ event: 'close', roles: this.allRoles });
  }

  ngOnInit() {
    this.getRoles();
  }

  getRoles() {
    this.rolesService.getAllRoles()
      .subscribe(response => {
        this.allRoles = Role.fromResponse(response, this.assignedRoles);
      });
  }

}
