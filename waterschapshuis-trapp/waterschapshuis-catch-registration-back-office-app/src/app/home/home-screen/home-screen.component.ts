import { Component } from '@angular/core';
import { PermissionId } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { environment } from 'src/environments/environment';
import { MatDialog } from '@angular/material/dialog';
import { HelpPageComponent } from 'src/app/common/help-page/help-page.component';

@Component({
  selector: 'app-home-screen',
  templateUrl: './home-screen.component.html',
  styleUrls: ['./home-screen.component.scss']
})
export class HomeScreenComponent {
  PermissionId = PermissionId;
  version = environment.appVersion;

  constructor(
    public dialog: MatDialog
  ) {}

  onInfoIconClick(title: string) {
    const dialogRef = this.dialog.open(HelpPageComponent, {
      width: '50%',
      height: '80%',
      data: { pageName: title }
    });
  }
}
