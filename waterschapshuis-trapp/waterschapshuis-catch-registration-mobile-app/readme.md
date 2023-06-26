# Mobile app

The mobile app is being developed using the Ionic framework.

## Prerequisites

You need to have [Node](https://nodejs.org/en/) and npm installed on your local machine.

We recommended to use [Visual Studio Code](https://code.visualstudio.com/) for project editing.

## Setup

To get the project up and running you need to:

* Install [Ionic CLI](https://ionicframework.com/docs/cli) globally by running the `npm i -g @ionic/cli` command,
* Install [native-run](https://www.npmjs.com/package/native-run) globally by running the `npm i -g native-run` command,
* Pull latest changes from this Git repository and run `npm install`,
* Run `ionic serve` command in the terminal.

The browser window will start with the current mobile application.

## Emulation (Android)

Although browser is very nice to get the source visually up quickly some APIs will not work.

To interact with the mobile application in closer to reality conditions we use an Android emulator.

To setup an emulator, you will have to:

* Download and install [Android Studio](https://developer.android.com/studio). Select the 'Custom' installation type and follow the wizard to add Android Virtual Device (AVD) during the installation.
* Once Android Studio is installed, open SDK Manager (Android Studio &#8594; Configure &#8594; Android SDK):
  * Add SDK version 28 (Android Pie)
  * On _SDK Tools_ tab unselect _Hide Obsolete Packages_ and make sure that 'Android SDK Platform-Tools' & 'Android SDK Tools (Obsolete)' are installed
* If you have skipped adding AVD previously, then add one using the Android Studio.
* Download and install [JDK](https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html)
* Download and unzip [Gradle](https://gradle.org/next-steps/?version=6.1.1&format=bin) binary, e.g. `C:\Users\[Name]\AppData\Local\gradle-6.1.1`
* Edit `Path` environment variable by adding:
  * `C:\Users\[Name]\AppData\Local\gradle-6.1.1\bin`
  * `C:\Users\[Name]\AppData\Local\Android\Sdk\platform-tools`
  * `C:\Users\[Name]\AppData\Local\Android\Sdk\tools`

> After the tools installation and environment variable setup it is recommended to restart your computer.

### Running app on the emulator

* Start your emulator (Android Studio &#8594; Configure &#8594; AVD Manager )

> The following commands will not start emulator automatically, so make sure the emulator is running.

* In Visual Studio Code terminal run:
  * ~~`ionic cordova build android` command,~~
  * `ionic cordova emulate android` command.

* After some time you should get `INSTALL SUCCESS` and `LAUNCH SUCCESS` messages in the terminal window. The app would render in the emulator.

### Running app on your android device
```Prerequisites:```

- You need to have [Conveyor by Keyoti](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1448185.ConveyorbyKeyoti) Visual Studio extension installed on your machine (the first time you run a project using HTTPS you will be prompted to install our testing certificate, this is normal and you should allow it otherwise you'll be warned by the browser not to visit your web app).

- In order to use geo-data in one of the following steps you need to modify configuration file to use locally installed geoserver port - to install geoserver locally you can use [this](https://git.enigmatry.com/Enigmatry/waterschapshuis-catch-registration/src/master/geoserver/README.md) instruction.

```Steps:```

1. Set Mobile.Api project as startup project inside Visual Studio
1. Run it with IISExpress - check Conveyor by Keyoti extension window inside Visual Studio and get http Remote Url address given.
    - **Example**: - Remote Url - http://192.168.0.42:45457/
1. Go to Mobile.Api project and modify appsettings.Development.json set "GeoServer"->"Url" key to remote URL (obtained from Conveyor by Keyoti in step above)
	- **Example**: - "Url": "http://192.168.0.42:1670/geoserver"
	- **Note**: Keep your local geoserver port (1670 in this example)
1. Restart Mobile.Api so the new configuration is refreshed and picked up.
1. Plugin android device via USB to your PC (go to Settings->Developer options and make sure USB debugging is enabled on your android device)
	- **Note**: You can enable developer options based on your android version by following instructions [here](https://developer.android.com/studio/debug/dev-options).
1. Open waterschapshuis-catch-registration-mobile-app inside Visual Studio Code 
1. Go to environment.ts file and change apiUrl key to Remote Url (obtained from Conveyor by Keyoti in step above)
	- **Example**: - apiUrl: 'http://192.168.0.42:45457/'
	- **Note**: Keep Remote Url port (45457 in this example)
1. Modify config.xml widget tab -> id attribute value to the custom app id
    - **Example**: waterschapshuis.catch.registration.mobile.app
	- **Note**: You can add custom app id but it shouldn't contain underscore (_)
1. Open Terminal inside of VS Code (Ctrl+`) and run command ```"ionic cordova run android -l"```
1. Go to "Run" (Ctrl-Shift-D) - this section can be found inside left side-bar menu of VS Code
1. On top of that section choose ```"Run Android on device"``` from drop-down menu and press Start

## Debugging

### Chrome

The Chrome browser offers its DevTools for the basic debugging (console logging) of the Ionic app.

* You should emulate the app as described in the previous section,
* Open `chrome://inspect/#devices` and wait some time until emulated device is shown in the list,
* Follow the `inspect` link.
* A new DevTools window should open, with the standard tabs, such as `Elements`, `Console`, `Sources`, etc.

### Visual Studio Code

Visual Studio Code gives us the ability to debug emulated applications by using the Cordova Tools extension built by Microsoft.

To setup debugging you need to:

* Install the [Cordova Tools](https://marketplace.visualstudio.com/items?itemName=Msjsdiag.cordova-tools)
* Take a look at the 3min [YouTube guide](https://www.youtube.com/watch?v=9o-U0vH-5DI)

> The video also covers creating a cordova app and adding platforms to it which should not be required at this stage, meaning that your app has already been set (see previous steps)

In Visual Studio Code click on the Debug icon on the left-hand side and then click the *create a launch.json file* button in the debug panel and select *Cordova* in the dropdown at the top.

![VS Code screenshot](https://i.imgur.com/Jpf8yjU.png)

This will create a launch.json file inside the .vscode folder in the project directory. You will now have several debug options at the top of the debug panel.

If you run *Serve to the browser (ionic serve)*, a new browser window will launch and you'll already be able to debug step by step within VS Code your app running in the browser.
