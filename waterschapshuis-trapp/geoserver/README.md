# Configure & run development (localhost) Geoserver

## Index

* [Configuring Geoserver on localhost](#Configuring) (done only once)
* [Running Geoserver on localhost](#Running)

**NOTE**: Use `git-bash` console as preferred way to execute commands/scripts (enables better log tracing)!

## Configuring

1. Make sure Docker for Windows is installed, running, and that it uses Linux containers
2. Build local `docker-catch-registration-geoserver` docker image by executing the following script:

    ```
    ./docker-geoserver-catch-registration/build.sh
    ```

3. Configure SQL Server ([Connecting to local or remote SQL Server](https://vivekcek.wordpress.com/2018/06/10/connecting-to-local-or-remote-sql-server-from-docker-container)):
   * You must configure SQL Server with _Mixed Mode Authentication_ (i.e. allow login with username and password). In _MS SQL Server Management Studio_, open database properties by right clicking on it and under _Security_ page select _SQL Server and Windows Authentication mode_
   * Open _SQL Server Configuration Manager_ as Administrator (_Computer Management > Services and Applications_).
   * Expand _SQL Server Network Configuration_
   * Select _Protocols for MSSQLSERVER_, then select _TCP/IP_ and open the properties by right click.
   * In the TCP/IP properties window enable TCP/IP for SQL Server.
   * Now select _IP Addresses_ tab in properties window. Under IPAll give port number as 1433.
   * Now restart SQL Server (_SQL Server Configuration Manager > SQL Server Services > SQL Server (MSSQLSERVER)_).

4. Configure _Geoserver_ database user - In _SQL Server Management Studio_ run the following script (ask other developers fro password):

    ```
    USE [master]
    GO
        CREATE LOGIN [waterschapshuis-catch-registration-geoserver]
            WITH PASSWORD=N'<PutPasswordHere>',
            DEFAULT_DATABASE=[waterschapshuis-catch-registration],
            CHECK_EXPIRATION=OFF,
            CHECK_POLICY=OFF
    GO
        USE [waterschapshuis-catch-registration]
    GO
        CREATE USER [waterschapshuis-catch-registration-geoserver]
            FOR LOGIN [waterschapshuis-catch-registration-geoserver]
        ALTER ROLE [db_datareader] ADD MEMBER [waterschapshuis-catch-registration-geoserver]
        ALTER ROLE [db_datawriter] ADD MEMBER [waterschapshuis-catch-registration-geoserver]
    GO
    ```

    This script will create user `waterschapshuis-catch-registration-geoserver` in the `waterschapshuis-catch-registration` database with appropriate permissions.
5. Setup your firewall to accept inbound connection to port 1433:
   * Open _Windows Defender Firewall_
   * Right click on _Inbound Rules_, click on _New Rule_,
   * Select Port as type of rule, click next
   * Select TCP, specify local ports: 1433
   * Allow the connection
   * Apply to Domain and Private
   * Name rule: "Sql Server" or similar
6. Using _Extract Here_ (IMPORTANT) option, extract `./geoserver/data_dir.zip`. This will extract content of zip file to `./geoserver/data_dir/` folder, that will be data volume for our local docker container!

## Running

* Run `./docker-geoserver-catch-registration/run-dev-compose.sh` script to start the container. When executing the script for the first time, _Windows_ will prompt you with _Docker Desktop - Filesharing_ dialog asking to allow _Docker_ to access `./geoserver/data_dir/`. Allow it!
  * It may happen that port _1670_ is taken by another application (you will get descriptive error when running the script). In this case replace all occurrences of _1670_ port inside `./docker-geoserver-catch-registration/` folder with free port Id, run `build.sh` script again & then run `run-dev-compose.sh` again. Do not commit custom port number settings!
* Open http://127.0.0.1:1670/geoserver & login as _admin/myawesomegeoserver_
  * It may happen that _Geoserver_ does not acknowledge changes after last git pull, in that case go to _Server Status_ page & click `Reload` under _Configuration and catalog_.
* If you want you API to hit local geoserver, in _appsettings_ files update _GeoServer > Uri_ to http://127.0.0.1:1670/geoserver
  * Backoffice API: `./Waterschapshuis.CatchRegistration.BackOffice.Api/appsettings.Development.json`
  * Mobile API: `./Waterschapshuis.CatchRegistration.Mobile.Api/appsettings.Development.json`