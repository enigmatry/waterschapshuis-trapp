import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export class AlertAction {
  text: string;
  action: any;
  isDefault = false;
  emptyFillButton ? = false;
}

export interface AlertData {
  title: string;
  header: string;
  message: string;
  actions: AlertAction[];
}

@Component({
  selector: 'app-alert-content',
  templateUrl: './alert-content.component.html',
})
export class AlertContentComponent {
  constructor(
    public dialogRef: MatDialogRef<AlertContentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AlertData) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
