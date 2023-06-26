import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NgxPermissionsService } from 'ngx-permissions';
import { BehaviorSubject } from 'rxjs';
import {
  AccountClient,
  IGetCurrentUserProfileResponse,
  UserUpdateConfidentialityCommand
} from '../api/waterschapshuis-catch-registration-backoffice-api';
import { UsersService } from '../settings/services/users.service';
import { AlertContentComponent } from './alert/alert/alert-content.component';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {

  private currentUserSubject = new BehaviorSubject<IGetCurrentUserProfileResponse>(null);

  public currentUser$ = this.currentUserSubject.asObservable();

  public get currentUser(): IGetCurrentUserProfileResponse {
    return this.currentUserSubject.getValue();
  }

  constructor(
    private accountClient: AccountClient,
    private permissionsService: NgxPermissionsService,
    private usersService: UsersService,
    public dialog: MatDialog
  ) { }

  async createUserFromIdentity(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.accountClient.createUserFromIdentity().toPromise()
        .then(() => this.loadUserProfile())
        .then(() => this.checkConfidentialityConfirm())
        .then(() => resolve(true))
        .catch((e: any) => resolve(true)); // error resolved as true because of initial application load,
      // in case of error in creating initial user, http interceptor will redirect to error page
    });
  }

  private async loadUserProfile(): Promise<boolean> {
    const currentUser = await this.accountClient.getUserProfile().toPromise();
    this.currentUserSubject.next(currentUser);
    this.permissionsService.loadPermissions(this.currentUser.policies);
    return true;
  }

  private async checkConfidentialityConfirm(): Promise<boolean> {
    if (!this.currentUser.confidentialityConfirmed) {
      await this.showConfirmDialog();
    }
    return true;
  }

  private updateConfidentialityConfirmedFlagCommand(id: string): UserUpdateConfidentialityCommand {
    return new UserUpdateConfidentialityCommand({
      id,
      confidentialityConfirmed: true
    });
  }

  private async showConfirmDialog(): Promise<void> {
    const dialogRef = this.dialog.open(AlertContentComponent, {
      data: {
        title: 'Let op!',
        header: '',
        message: 'Let op, je werkt met vertrouwelijke gegevens, ga hier zorgvuldig mee om! Houd je hierbij aan de afspraken rondom informatiebeveiliging en privacy binnen jouw organisatie.',
        actions: [{ text: 'Gelezen', action: true, isDefault: true }]
      },
      disableClose: true
    });

    await dialogRef.afterClosed().toPromise();

    const cmd = this.updateConfidentialityConfirmedFlagCommand(this.currentUser?.id);

    await this.usersService.updateConfidentiality(cmd).toPromise();

    this.currentUser.confidentialityConfirmed = true;
  }
}
