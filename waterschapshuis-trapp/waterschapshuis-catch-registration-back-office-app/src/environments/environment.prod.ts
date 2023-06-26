export const environment = {
  production: true,
  apiUrl: '__apiUrl__',
  azureAd: {
    authority: 'https://login.microsoftonline.com/organizations',
    clientId: '__azureAdClientId__',
    apiScopes: ['__azureAdApiScopes__']
  },
  applicationInsights: {
    instrumentationKey: '__applicationInsightsInstrumentationKey__'
  },
  appVersion: '__appVersion__',
  appSettings: {
    numberOfHistoricalTrapLayers: '__numberOfHistoricalTrapLayers__'
  },
  userIdlePeriodInSec: '__idleSecondsPeriod__'
};
