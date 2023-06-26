import { Inject, Injectable } from '@angular/core';
import {
  IPublicClientApplication,
  AccountInfo,
  EndSessionRequest,
  AuthorizationUrlRequest,
  AuthenticationResult,
  RedirectRequest,
  SilentRequest,
  Configuration,
  PublicClientApplication
} from '@azure/msal-browser';
import { Observable, from } from 'rxjs';
import { MSAL_CONFIG } from './msal-config';


@Injectable()
export class MsalService {

  private msalInstance: IPublicClientApplication;

  constructor(@Inject(MSAL_CONFIG) msalConfig: Configuration) {
    this.msalInstance = new PublicClientApplication(msalConfig);
  }

  acquireTokenPopup(request: AuthorizationUrlRequest): Observable<AuthenticationResult> {
    return from(this.msalInstance.acquireTokenPopup(request));
  }

  acquireTokenRedirect(request: RedirectRequest): Observable<void> {
    return from(this.msalInstance.acquireTokenRedirect(request));
  }

  acquireTokenSilent(silentRequest: SilentRequest): Observable<AuthenticationResult> {
    return from(this.msalInstance.acquireTokenSilent(silentRequest));
  }

  getAccountByUsername(userName: string): AccountInfo {
    return this.msalInstance.getAccountByUsername(userName);
  }

  getAllAccounts(): AccountInfo[] {
    return this.msalInstance.getAllAccounts();
  }

  handleRedirectPromise(): Promise<AuthenticationResult> {
    return this.msalInstance.handleRedirectPromise();
  }

  loginPopup(request?: AuthorizationUrlRequest): Observable<AuthenticationResult> {
    return from(this.msalInstance.loginPopup(request));
  }

  loginRedirect(request?: RedirectRequest): Observable<void> {
    return from(this.msalInstance.loginRedirect(request));
  }

  logout(logoutRequest?: EndSessionRequest): Observable<void> {
    return from(this.msalInstance.logout(logoutRequest));
  }

  ssoSilent(request: AuthorizationUrlRequest): Observable<AuthenticationResult> {
    return from(this.msalInstance.ssoSilent(request));
  }
}
