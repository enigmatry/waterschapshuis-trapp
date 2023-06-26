# test

## Index

* [Project Overview](#project-overview)
* [Development Environment](#development-environment)
* [Test Environment](#test-environment)
* [Additional Deployment Information](#additional-deployment-information)
* [Team](#team)

## Project Overview

### Project Functionality

_Describe some of the important use cases and who the users are. Also describe if the software plays in important role in business processes._

### Components

* [Backoffice Application](waterschapshuis-catch-registration-back-office-app/README.md)
* [Mobile Application](waterschapshuis-catch-registration-mobile-app/readme.md)
* [Import Tool](Waterschapshuis.CatchRegistration.Data.ImportTool/README.md)
* [Geoserver](docker-geoserver-catch-registration/README.md)
* [Configure & run development (localhost) Geoserver](geoserver/README.md)

### Project Architecture

_Give an architectural overview of the project. Describe the different building blocks of the application, layers, API's and databases._

### External Dependencies

_Does the application depend on any external API's or 3rd party libraries? Describe them here._

### Solution Overview

_List the Modules / Projects / Projects that make up the complete solution, and give a short description of the module. For example with a table:_

Project/Module | Description
---|---
_Name_ | _Description_

### Frameworks Used

_List the Frameworks and SDK's, including the version numbers, used by the project._

## Development Environment (Getting Started)

This section describes how to set up the application locally.

### Prerequisites

_List here the required tooling to start developing locally, including version (i.e. Visual Studio 2017+, etc.)._

### Source Code

Clone the repository from: `https://git.enigmatry.com/Enigmatry/waterschapshuis-catch-registration.git`

* Console command: `git clone https://git.enigmatry.com/Enigmatry/waterschapshuis-catch-registration.git`
* Visual Studio: Go to menu Team -> Manage Connections, and then in section Local Git Repositories select action 'Clone' and specify the remote git url and checkout directory.

### Databases and Storage

By convention, development SQL server should be on localhost, default instance (i.e. we can connect to it with only localhost or .). 
Developers should have following databases created:

1. Main database - *waterschapshuis-catch-registration*
   1. After you create database through SQL management studio open *migration commands.txt* and run the *Update-Database* command. This will migrate the database to the latest version

2. Integration testing database - *waterschapshuis-catch-registration-integration-testing* - just create the empty database, this database will be used for the integration tests.

3. Geoserver database - *waterschapshuis-catch-registration-geoserver* - add more info TBD

### Configuring the Solution

In order to configure the application to run locally do the following:

1. Back Office Api:
   1. Create databases as described in step *Databases and Storage*
   2. Migrate databases to latest version as described in step  *Databases and Storage*
   3. Make sure that *Tools -> Options -> Azure Service Authentication* is set to use the Enigmatry domain developer account (in order to access the Key Vault)
   4. Open solution
   5. Set *Waterschapshuis.CatchRegistration.BackOffice.Api* as a startup project
2. Back Office Angular App:
   1. Open *waterschapshuis-catch-registration-back-office.code-workspace* in Visual Studio Code
   2. Open terminal window
   3. Run command *npm install*
3. Mobile Ionic App:
   1. [Detailed Instructions](waterschapshuis-catch-registration-mobile-app/readme.md)

### Running the Solution

_Specify the steps to take to start the application (after meeting the prerequisites and configuration)._

## Test Environment

_Use this section to specify the details of the Test environment. (url's, servers, databases, etc.)._

## Additional Deployment information

### **1. Mobile App release procedure**

   Problem at hand:

   ```After new version of Mobile App is released (TrApp/Mobile API), previous version of the app is not work with the new Mobile API because of the breaking changes```. One of the following should be treated as breaking change:

   - Mobile API end-point routes where renamed
   - GeoServer overlay layer end-point parameters were updated
   - Breaking business logic changes

   Notes

   ```List of items that should be continuously checked while developing/bug fixing.```

   Developers must think about changes that they do on Mobile/GeoServer API & how they influence new/previous mobile clients.  
   This is not only related to API end-point routing changes but also to business rules which are updated in the meantime. Backward compatibility must be supported. 

   - ```Developers checklist```:
      * Recognize when request, response, end-points where updated on Mobile API
      * Recognize when request, response, end-points where updated on GeoServer API
      * Try not to delete or rename existing fields in request/response
      * Compare Mobile API swagger document (```waterschapshuis-catch-registration-mobile-api.ts```) between previous and new release
      * When ApplicationService is updated and used by both BO & Mobile API, the mobile clients could broke
      * While developing test locally old version of mobile client (emulator) against new API (you should have 2 versions of repository locally). Do this when you suspect that you made breaking change, before sending the feature to QA team.

   - ```QA team checklist```:
      * Regression testing of old version of mobile client (previous release) against the new mobile API, before the release. Both OS should be tested, Android/iOS. No emulators!
      * The testes from The Netherlands should test map features of mobile client (Android/iOS). Alternative is to implement Serbian map on mobile client or somehow fake GPS location.
      * QA team must confirm that the app is ready for the new release, and the team must be given appropriate time to verify that the app is ready.


   _Describe any additional non-standard or manual procedures to deploy changes to other environments._


## Team

_List the team members and their contact information._