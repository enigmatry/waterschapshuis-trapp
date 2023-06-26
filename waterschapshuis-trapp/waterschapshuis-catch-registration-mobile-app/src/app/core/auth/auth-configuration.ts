import { environment } from 'src/environments/environment';
import { InjectionToken } from '@angular/core';

export const AUTH_CONFIG = new InjectionToken<string>('AUTH_CONFIG');

export class AuthConfiguration {
  authority: string;
  scope: string;
  clientId: string;
  redirectUrl: string;
}


export function AuthConfigFactory(): AuthConfiguration {
  return {
    authority: environment.azureAd.authority,
    scope: environment.azureAd.apiScopes.join(' '),
    clientId: environment.azureAd.clientId,
    redirectUrl: 'https://login.microsoftonline.com/common/oauth2/nativeclient'
  };
}
