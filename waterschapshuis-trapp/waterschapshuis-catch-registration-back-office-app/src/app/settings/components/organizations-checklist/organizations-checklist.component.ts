import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrganizationService } from '../../../shared/services/organizations.service';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';

@Component({
  selector: 'app-organizations-checklist',
  templateUrl: './organizations-checklist.component.html',
  styleUrls: ['./organizations-checklist.component.scss']
})
export class OrganizationsChecklistComponent implements OnInit {
  allOrganizations: IListItem[] = [];
  selectedOrganization: string = null;

  constructor(public dialogRef: MatDialogRef<OrganizationsChecklistComponent>,
              private organizationService: OrganizationService,
              @Inject(MAT_DIALOG_DATA) data) {
                this.selectedOrganization = data.selectedOrganization;
              }

  ngOnInit() {
    this.organizationService.getOrganizations()
      .subscribe(response => {
        this.allOrganizations = ListItem.mapToListItems(response.items);
      });
  }

  close() {
    this.dialogRef.close();
  }

  saveAndClose() {
    this.dialogRef.close({ event: 'close', organizationId: this.selectedOrganization});
  }

}
