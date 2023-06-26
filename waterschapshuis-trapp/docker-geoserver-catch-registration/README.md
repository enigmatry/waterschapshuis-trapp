# catch-registration-geoserver

A docker container that runs GeoServer customized for the purposes of the Catch registration project and influenced by this docker
recipe: https://github.com/kartoza/docker-geoserver/blob/master/Dockerfile

## Getting the image

Login to Enigmatry's Azure container registry using your account

```
az login (if you are not logged on already or if credentials expired)
az account show (to check your currently selected subscription)
az account set -s <subscription id> (if wrong subscription is selected)
az acr login --name <name of container registry e.g. enigmatrytest>
```

```shell
docker pull enigmatrytest.azurecr.io/waterschapshuis-catch-registration/dev/catch-registration-geoserver
```

### To build yourself with a local checkout using the build script

Edit the .env file to change the following variables:

- The variables below represent the latest stable release you need to build.

   ```text
   GS_VERSION=2.19.0
   ```

```shell
git clone https://git.enigmatry.com/Enigmatry/waterschapshuis-catch-registration.git
In Visual Studio Code open folder docker-geoserver-catch-registration
Open wsl terminal
Run command: . build.sh
```

Ensure that you look at the build script to see what other build arguments you can include whilst building your image.

### Other build options

See the original [Kartoza Geoserver Readme](README-Kartoza-geoserver.md)

## Before run

The image can be run without any additional setup but you won't get any layers. Also any changes to the GeoServer won't be persisted between the Docker restarts.

In order to have data persistence between docker restarts create directory for storing the Geoserver data:

- Create directory ```c:\geoserver\data_dir```
- Copy **contents** of ```data-2.15.x.zip\data``` to ```data_dir``` and **NOT** the ```data``` folder itself

## Run (manual docker commands)

**Note:** You probably want to use docker-compose for running as it will provide
a repeatable orchestrated deployment system.

You probably want to also have SQL Server running too. To create a running
container do:

```shell
run.sh
```

See the original [Kartoza Geoserver Readme](README-Kartoza-geoserver.md) for more options on running

## Run (automated using docker-compose)

The file ``docker-compose.yml`` is used for starting the container instance on dev machine

The file ``docker-compose-azure.yml`` is used for starting the container instance on Azure Web App

Please read the ``docker-compose``
[documentation](https://docs.docker.com/compose/) for details
on usage and syntax of ``docker-compose`` - it is not covered here.

To run the example do:

```shell
SET GEOSERVER_DATA_DIR=path_to_data_dir
docker-compose up
```

or

```shell
run.sh
```

Once all services are started, test by visiting the GeoServer landing
page in your browser: [http://localhost:1670/geoserver](http://localhost:1670/geoserver).

To run in the background rather, press ``ctrl-c`` to stop the
containers and run again in the background:

```shell
SET GEOSERVER_DATA_DIR=path_to_data_dir
docker-compose up -d
```

## Storing data on the host (local dev machine) rather than the container

Docker volumes can be used to persist your data.

Create an empty data directory to use to persist your data.

```shell
md c:\geoserver\data_dir
run.sh
```

## Pushing built Docker image to Azure container registry

Adjust the Docker image versions according to the [Azure recommendations](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-image-tag-version)

```shell
build.sh
docker-push-dev.sh
```

## Connecting Geoserver to local SQL server

Original article: [Connecting to local or remote SQL Server](https://vivekcek.wordpress.com/2018/06/10/connecting-to-local-or-remote-sql-server-from-docker-container)

- You must configure SQL Server with Mixed Mode Authentication (i.e. allow login with username and password).
- Open SQL Server Configuration Manager as Administrator.
- Expand SQL Server Network Configuration.
- Select Protocols for MSSQLSERVER and then select TCP/IP. Now open the Properties by right click.
- In the TCP/IP properties window enable TCP/IP for SQL Server.
- Now select IP Addresses tab in properties window. Under IPAll give port number as 1433.
- Now restart SQL Server (SQL Server Configuration Manager/SQL Server Services/SQL Server (MSSQLSERVER)).
- Now setup your firewall to accept inbound connection to port 1433
  
  - Open Windows Defender Firewall
  - Right click on Inbound Rules, click on New Rule,
  - Select Port as type of rule, click next
  - Select TCP, specify local ports: 1433
  - Allow the connection
  - Apply to Domain and Private
  - Name rule: "Sql Server" or similar

- Setting up SQL server user
  - In SQL server management Studio run the following script (adjust name, password and database names):

```
USE [master]
GO
	CREATE LOGIN [waterschapshuis-catch-registration-geoserver] WITH PASSWORD=N'<PutPasswordHere>', DEFAULT_DATABASE=[waterschapshuis-catch-registration], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [waterschapshuis-catch-registration]
GO
CREATE USER [waterschapshuis-catch-registration-geoserver] FOR LOGIN [waterschapshuis-catch-registration-geoserver]
ALTER ROLE [db_datareader] ADD MEMBER [waterschapshuis-catch-registration-geoserver]
ALTER ROLE [db_datawriter] ADD MEMBER [waterschapshuis-catch-registration-geoserver]
GO
```

This script will create user in the Catch registration database with appropriate permissions.

### Configure Geoserver to use Sql Server store

- Go to Geoserver
- Login as administrator
- Select ```Stores```
- Click on ```Add new Store```
- Click on ```Microsoft SQL Server```
- Populate following fields exactly:
  - host: ```host.docker.internal```
  - port: ```1433```
  - database: ```waterschapshuis-catch-registration```
  - schema: ```dbo```
  - user: ```catch-registration-geo-server```
  - password: same as used in the script
- Remaining fields can be populated freely