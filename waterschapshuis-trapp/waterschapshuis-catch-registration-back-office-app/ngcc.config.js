// devextreme-angular: 
// https://supportcenter.devexpress.com/ticket/details/T860229/devextreme-entry-point-contains-deep-imports-warnings-occur-during-angular-9-project

module.exports = {
  packages: {
    'devextreme-angular': {
      ignorableDeepImportMatchers: [
        /devextreme\//
      ]
    }
  }
};