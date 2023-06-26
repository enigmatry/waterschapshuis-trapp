import { environment } from '../../../environments/environment';
import { Configuration, LogLevel } from '@azure/msal-browser';
import { InjectionToken } from '@angular/core';

export const MSAL_CONFIG = new InjectionToken<string>('MSAL_CONFIG');

export function MSALConfigFactory(): Configuration {
  return {
    auth: {
      clientId: environment.azureAd.clientId,
      authority: environment.azureAd.authority,
      redirectUri: `${window.location.protocol}//${window.location.host}/login`,
      postLogoutRedirectUri: `${window.location.protocol}//${window.location.host}/login`,
      navigateToLoginRequestUrl: true,
    },
    cache: {
      cacheLocation: 'sessionStorage',
      storeAuthStateInCookie: isIEOrEdge()
    },
    system: {
      loggerOptions: {
        loggerCallback: msalLoggerCallback,
        piiLoggingEnabled: !environment.production
      }
    }
  };
}

export function msalLoggerCallback(logLevel: LogLevel, message: string, containsPii: boolean) {
  if (!environment.production) {
    console.log(message);
  }
}

function isIEOrEdge(): boolean {
  const ua = window.navigator.userAgent;
  const msie = ua.indexOf('MSIE ');
  const msie11 = ua.indexOf('Trident/');
  const msedge = ua.indexOf('Edge/');
  const isIE = msie > 0 || msie11 > 0;
  const isEdge = msedge > 0;
  return isIE || isEdge;
}
