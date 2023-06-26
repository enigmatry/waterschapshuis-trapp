# CatchRegistration Data ImportTool

ImportTool is a console application that is used to import the historical data of previous application version from different data sources into the SQL Server database that the current application is using.

## Application Configuration

There are 3 different appsettings files for three Environments. The environment is configured using the ```WCR_ENVIRONMENT``` environment variable that can have any of the following three values:

   ```text
   Development
   Test
   Production
   ```

Based on the environment specific appsettings configuration will be used.

The most important configuration settings are connection strings for accessing V2 Central PostgreSQL database, Mura organization PostgreSQL database and desired V3 SQL Server database.

## Command Line Arguments

The command line arguments define which of the implemented __Import Tasks__ to run and pass task specific options.

#### Geographical data

```shell
--import Geo
```

This option is used to import the following data from V2 Central PostgreSQL database:
 * Organizations
 * Rayons
 * CatchAreas
 * HourSquares
 * WaterAuthorities
 * SubArea
 * SubAreaHourSquare

#### Users

```shell
--import Users
```

This option is used to import Users from V2 Central PostgreSQL database.

#### Provinces

```shell
--import Provinces --file {GEOJSON_FILE_NAME}
```

This option is used to import Provinces from specified GeoJSON file. The file must be placed in the __Resoruces__ subfolder of the application. 
If the ```--file``` argument is not specified, application will try to use default file name _Provinces.json_.

Importing Provinces from the included GeoJSON file can be done using following arguments:

```shell
--import Provinces --file Provinces.json
```

#### Traps

```shell
--import Traps:{ORG_NAME} --file {GEOJSON_FILE_NAME}
```

This option is used to import Traps from specified GeoJSON file, or from PostgreSQL database in case of Mura organization. The file must be placed in the __Files__ subfolder of the application. 
If the ```--file``` argument is not specified, application will try to use default file name _Traps.json_.

Currently rules and mappings are completed for the following organizations/groups:
 * Scheldestromen
 * West en Midden Nederland (WestEnMidden)
 * Mura
 * Brabant
 * Noordoostnederland (NONL)
 * Zuiderzeeland
 * Fryslân
 * MRB Rivierenland (Rivierenland)

and importing their data can be done using following arguments:

```shell
--import Traps:Scheldestromen --file Traps.Scheldestromen.json
--import Traps:WestEnMidden --file Traps.WestEnMidden.json
--import Traps:Limburg --file Traps.Limburg.json
--import Traps:Mura
--import Traps:Brabant
--import Traps:NONL
--import Traps:Zuiderzeeland
--import Traps:Fryslan
--import Traps:Rivierenland
```

NOTE: Please make sure to import Geographical data and Users before importing Traps.

NOTE: By importing Mura traps you will also import traps for the following organizations:
 * Brabant
 * Noordoostnederland (NONL)
 * Zuiderzeeland
 * Fryslân
 * MRB Rivierenland (Rivierenland)

#### Catches

```shell
--import Catches:{ORG_NAME} --file {GEOJSON_FILE_NAME}
```

This option is used to import Catches from specified GeoJSON file, or from PostgreSQL database in case of Mura organization. The file must be placed in the __Files__ subfolder of the application. 
If the ```--file``` argument is not specified, application will try to use default file name _Catches.json_.

Currently rules and mappings are completed for the following organizations/groups:
 * Scheldestromen
 * West en Midden Nederland (WestEnMidden)
 * Mura
 * Brabant
 * Noordoostnederland (NONL)
 * Zuiderzeeland
 * Fryslân
 * MRB Rivierenland (Rivierenland)

and importing their data can be done using following arguments:

```shell
--import Catches:Scheldestromen --file Catches.Scheldestromen.json
--import Catches:WestEnMidden --file Catches.WestEnMidden.json
--import Catches:Limburg --file Catches.Limburg.json
--import Catches:Mura
--import Catches:Brabant
--import Catches:NONL
--import Catches:Zuiderzeeland
--import Catches:Fryslan
--import Catches:Rivierenland
```

NOTE: Please make sure to import Users and that organization's Traps before importing Catches.

NOTE: By importing Mura catches you will also import catches for the following organizations:
 * Brabant
 * Noordoostnederland (NONL)
 * Zuiderzeeland
 * Fryslân
 * MRB Rivierenland (Rivierenland)

#### Time Registration

```shell
--import TimeRegistrations:{ORG_NAME} --file {GEOJSON_FILE_NAME}
```

This option is used to import time registration from specified GeoJSON file, or from PostgreSQL database in case of Mura organization. The file must be placed in the __Files__ subfolder of the application. 
If the ```--file``` argument is not specified, application will try to use default file name _Catches.json_.

Currently rules and mappings are completed for the following organizations/groups:
 * Scheldestromen
 * West en Midden Nederland (WestEnMidden)
 * Limburg
 * Mura
 * Brabant
 * Noordoostnederland (NONL)
 * Zuiderzeeland
 * Fryslân
 * MRB Rivierenland (Rivierenland)
 
and importing their data can be done using following arguments:

```shell
--import TimeRegistrations:Scheldestromen --file TimeRegistrations.Scheldestromen.json
--import TimeRegistrations:WestEnMidden --file TimeRegistrations.WestEnMidden.json
--import TimeRegistrations:Limburg --file TimeRegistrations.Limburg.json
--import TimeRegistrations:Mura
--import TimeRegistrations:Brabant
--import TimeRegistrations:NONL
--import TimeRegistrations:Zuiderzeeland
--import TimeRegistrations:Fryslan
--import TimeRegistrations:Rivierenland
```

NOTE: Please make sure to import Users before time registrations

NOTE: By importing Mura time registrations you will also import time registrations for the following organizations:
 * Brabant
 * Noordoostnederland (NONL)
 * Zuiderzeeland
 * Fryslân
 * MRB Rivierenland (Rivierenland)

#### Time Registration General

```shell
--import TimeRegistrationGeneral:{ORG_NAME} --file {GEOJSON_FILE_NAME}
```

This option is used to import time registration general items from specified GeoJSON file. The file must be placed in the __Files__ subfolder of the application. 
If the ```--file``` argument is not specified, application will try to use default file name _Catches.json_.

Currently rules and mappings are completed for the following organizations/groups:
 * West en Midden Nederland (WestEnMidden)
 
and importing their data can be done using following arguments:

```shell
--import TimeRegistrations:WestEnMidden --file TimeRegistrations.WestEnMidden.json
```

NOTE: Please make sure to import Users before time registration generals

## Running the application

It is possible to run the application with the debugger in Visual Studio, and also in Terminal (Command Prompt, Powershell) using the .NET Core CLI.

### Visual Studio

Set the __Waterschapshuis.CatchRegistration.Data.ImportTool__ as Startup Project. 
Open project Properties and go to Debug tab to configure Environment and Application arguments.
Use Debug -> Start to run the application.

![alt text](https://i.imgur.com/ZdReMb2.png "Visual Studio Debug options")

### Terminal

Open either Command Prompt or Powershell and go to ImportTool project directory (not the ```\bin\Debug\netcoreapp3.1``` sub-directory).
Use dotnet run and pass the Application arguments to start the ImportTool.

![alt text](https://i.imgur.com/5zU7ROp.png "Developer Command Prompt - dotnet run")

## Troubleshooting

ImportTool application uses text log file to save all important information during execution.
The application log file location and other logging settings can be configured in appsettings. The default log file location is: ```c:\temp\logs\```.

When importing data from V2 Central PostgreSQL database your external IP address must be white-listed in order to access it.
