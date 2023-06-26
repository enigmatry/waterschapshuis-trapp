import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import {
  IGetCurrentUserProfileResponse,
  UserUpdateConfidentialityCommand,
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { CacheService } from 'src/app/cache/cache.service';
import { AccountCachedClient } from 'src/app/cache/clients/account-cached.client';
import { UserService } from 'src/app/users/user.service';

import { AlertService } from './alert.service';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserProviderService {

  private currentUserSubject = new BehaviorSubject<IGetCurrentUserProfileResponse>(null);

  public currentUser$ = this.currentUserSubject.asObservable();

  public get currentUser(): IGetCurrentUserProfileResponse {
    return this.currentUserSubject.getValue();
  }

  constructor(
    private accountCachedClient: AccountCachedClient,
    private userService: UserService,
    private alertService: AlertService,
    private cacheService: CacheService
  ) { }

  async loadCurrentUserProfile(): Promise<boolean> {
    const currentUser = await this.accountCachedClient.getUserProfile().toPromise();
    this.currentUserSubject.next(currentUser);
    return !!currentUser;
  }

  async checkConfidentialityConfirm(): Promise<boolean> {

    if (!this.currentUser.confidentialityConfirmed) {
      await this.showConfirmDialog();
    }
    return true;
  }

  private async showConfirmDialog(): Promise<void> {

    this.alertService.getConfirmDialog(
      'LetOp!',
      `Let op, je werkt met vertrouwelijke gegevens, ga hier zorgvuldig mee om!
       Houd je hierbij aan de afspraken rondom informatiebeveiliging en privacy binnen jouw organisatie.`,
      '',
      'Gelezen',
      async () => {

        const cmd = this.updateConfidentialityConfirmedFlagCommand(this.currentUser?.id);

        await this.userService.updateConfidentiality(cmd).toPromise();
        await this.updateLocalUserDataAfterConfirmation();
      },
      null,
      true,
      false
    ).then(x => x.present());
  }

  private updateConfidentialityConfirmedFlagCommand(id: string): UserUpdateConfidentialityCommand {
    return new UserUpdateConfidentialityCommand({
      id,
      confidentialityConfirmed: true
    });
  }

  private async updateLocalUserDataAfterConfirmation(): Promise<void> {

    this.currentUser.confidentialityConfirmed = true;
    await this.cacheService.saveItem('userProfile', this.currentUser);
  }
}
