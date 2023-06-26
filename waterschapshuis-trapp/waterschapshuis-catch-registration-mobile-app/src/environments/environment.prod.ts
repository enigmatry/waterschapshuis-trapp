import { LogLevel } from 'src/app/core/logger/log-level.enum';

export const environment = {
  production: true,
  apiUrl: '__apiUrl__',
  azureAd: {
    authority: 'https://login.microsoftonline.com/organizations/oauth2/v2.0',
    clientId: '__azureAdClientId__',
    apiScopes: ['__azureAdApiScopes__'],
  },
  appInsights: {
    instrumentationKey: '__appInsightsKey__'
  },
  azureStorage: {
    url: '__azureStorageUrl__',
    baseObservationBlobContainer: '__baseObservationBlobContainer__'
  },
  appVersion: '__appVersion__',
  appSettings: {
    numberOfHistoricalTrapLayers: '__numberOfHistoricalTrapLayers__'
  },
  searchMap: {
    url: 'https://api.pdok.nl/bzk/locatieserver/search/v3_1/'
  },
  appCenter: {
    enableAnalytics: true
  },
  logger: {
    consoleLogLevel: LogLevel.Debug,
    appInsightsLogLevel: LogLevel.Info
  }
};
