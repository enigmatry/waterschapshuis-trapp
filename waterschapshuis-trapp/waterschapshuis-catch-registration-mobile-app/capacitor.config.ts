import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'nl.waterschapshuis.trapp',
  appName: '__AppName__',
  webDir: 'www',
  bundledWebRuntime: false,
  cordova: {
    preferences: {
      ScrollEnabled: 'false',
      'android-minSdkVersion': '26',
      'phonegap-version': 'cli-9.0.0',
      BackupWebStorage: 'none',
      GOOGLE_API_KEY_FOR_ANDROID: '',
      OKHTTP_VERSION: '3.10.0',
      APPCENTER_CRASHES_ALWAYS_SEND: 'true',
      APPCENTER_ANALYTICS_ENABLE_IN_JS: 'true',
      'cordova.plugins.diagnostic.modules': 'LOCATION',
      APP_SECRET: '__AppCenterAppSecret__'
    }
  },
  server: {
    iosScheme: "ionic"
  }
};

export default config;
