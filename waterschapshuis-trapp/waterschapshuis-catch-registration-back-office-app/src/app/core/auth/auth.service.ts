import { Injectable } from '@angular/core';
import { AccountInfo } from '@azure/msal-browser';
import { UserIdleService } from 'angular-user-idle';
import { Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { AccountClient } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { environment } from 'src/environments/environment';
import { MsalService } from './msal.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private msalService: MsalService,
    private accountClient: AccountClient,
    private userIdle: UserIdleService) { }

  loginRedirect = (): Observable<any> =>
    this.msalService.loginRedirect({
      scopes: environment.azureAd.apiScopes,
      redirectStartPage: window.location.href
    })

  getAccount = (): AccountInfo => {
    const accounts = this.msalService.getAllAccounts();
    if (!accounts || accounts.length === 0) {
      return null;
    }
    if (accounts.length > 1) {
      console.log('Multiple accounts found. Returning first.');
    }
    return accounts[0];
  }

  getAccessToken = (): Observable<string> => {
    return this.msalService
      .acquireTokenSilent({ scopes: environment.azureAd.apiScopes, account: this.getAccount() })
      .pipe(
        map(response => response.accessToken),
        catchError(err => this.loginRedirectOnError(err, 'Could not retrieve access token'))
      );
  }

  logout = (): Observable<any> =>
    this.accountClient
      .logOut()
      .pipe(
        switchMap(next => of(this.logoutOnClient(`User ${next.userEmail} logged out!`))),
        catchError(err => this.loginRedirectOnError(err, 'Could not log out user'))
      )

  handleAuthRedirect = async (): Promise<void> =>
    this.msalService
      .handleRedirectPromise()
      .then(authResponse => {
        if (authResponse) {
          console.log('Auth redirect handled successfully.');
          this.startWatchingUserActivity();
        }
      })
      .catch(err => console.error('Error handling auth redirect', err))

  private logoutOnClient = (consoleMessage: string = null) => {
    if (consoleMessage !== null) {
      console.log(consoleMessage);
    }
    this.msalService
      .logout({ postLogoutRedirectUri: window.location.href })
      .subscribe(
        () => null,
        err => this.loginRedirectOnError(err, 'Could not log out user on client')
      );
  }

  private loginRedirectOnError = (err: any, consoleMessage: string) => {
    console.log(`${consoleMessage}. Error: `, err.message);
    this.loginRedirect();
    throw err;
  }

  private startWatchingUserActivity = () => {
    this.userIdle.startWatching();
    this.userIdle.onTimerStart().subscribe();
    this.userIdle.onTimeout().subscribe(() => {
      console.log('userIdle timeout');
      this.logout().subscribe();
    });
  }
}


