import { Component, Input } from '@angular/core';
import { OnDestroyMixin } from '@w11k/ngx-componentdestroyed';
import { MatDialog } from '@angular/material/dialog';
import { HelpPageComponent } from 'src/app/common/help-page/help-page.component';
import { HasBackConfirmDialog } from 'src/app/shared/alert/alert/has-back-confirm-dialog';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { TimeRegistrationStateService } from 'src/app/time-registration/services/time-registration-state.service';
import { AlertContentComponent, AlertAction } from 'src/app/shared/alert/alert/alert-content.component';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-side-bar-time-registration',
  templateUrl: './side-bar-time-registration.component.html',
  styleUrls: ['./side-bar-time-registration.component.scss']
})
export class SideBarTimeRegistrationComponent extends OnDestroyMixin implements HasBackConfirmDialog {
  @Input() title: string;

  protected alertActions: AlertAction[] = [{ text: 'Nee', action: false, isDefault: true }, { text: 'Ja', action: true, isDefault: false }];

  constructor(
    private router: Router,
    private timeRegistrationStateService: TimeRegistrationStateService,
    public dialog: MatDialog) {
    super();
  }

  navigateToHome(): void {
    this.showDialog().subscribe(result => {
      if (result) {
        this.router.navigateByUrl('/home');
        this.timeRegistrationStateService.hasChanges = false;
      }
    });
  }

  onInfoIconClick() {
    const dialogRef = this.dialog.open(HelpPageComponent, {
      width: '50%',
      height: '80%',
      data: { pageName: this.title }
    });
  }

  showDialog(): Observable<boolean> {
    if (!this.timeRegistrationStateService.hasChanges) { return of(true); }

    const dialogRef = this.dialog.open(AlertContentComponent, {
      data: {
        title: 'Bevestig teruggaan',
        header: '',
        message: 'Weet u zeker dat u wilt annuleren? Uw wijzingen zullen niet bewaard worden.',
        actions: this.alertActions
      }
    });
    return dialogRef.afterClosed().pipe(map(result => {
      return result;
    }));
  }
}
