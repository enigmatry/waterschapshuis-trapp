// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { LogLevel } from 'src/app/core/logger/log-level.enum';

export const environment = {
  production: false,
  apiUrl: 'http://10.0.2.2:5450/',
  // apiUrl: 'http://192.168.100.4:45455/',
  azureAd: {
    authority: 'https://login.microsoftonline.com/organizations/oauth2/v2.0',
    clientId: '20e7fb85-9ecc-4cb8-9bb3-ed0d0ac4664e',
    apiScopes: ['api://20e7fb85-9ecc-4cb8-9bb3-ed0d0ac4664e/mobile-api openid profile offline_access']
  },
  appInsights: {
    instrumentationKey: ''
  },
  azureStorage: {
    url: 'https://wscatchregistrationdev.blob.core.windows.net',
    baseObservationBlobContainer: 'observations'
  },
  appVersion: 'development',
  appSettings: {
    numberOfHistoricalTrapLayers: 5
  },
  searchMap: {
    url: 'https://api.pdok.nl/bzk/locatieserver/search/v3_1/'
  },
  appCenter: {
    enableAnalytics: true
  },
  logger: {
    consoleLogLevel: LogLevel.Debug,
    appInsightsLogLevel: LogLevel.Off
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
