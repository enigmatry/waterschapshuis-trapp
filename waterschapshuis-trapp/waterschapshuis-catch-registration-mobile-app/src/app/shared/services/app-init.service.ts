import { Injectable } from '@angular/core';
import { SplashScreen } from '@capacitor/splash-screen';
import { StatusBar, Style } from '@capacitor/status-bar';
import { Platform } from '@ionic/angular';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { AppSettingsService } from './app-settings.service';
import { AppCenterService } from 'src/app/core/app-center/app-center.service';
import { AppInsightsInitService } from 'src/app/core/application-insights/application-insights';

export function initFactory(config: AppInitService) {
    return () => config.initializeApp();
}

@Injectable({
    providedIn: 'root'
})
export class AppInitService {
    constructor(
        private platform: Platform,
        private appInsightsService: AppInsightsInitService,
        private sqlLiteProviderService: SqliteProviderService,
        private appSettingsService: AppSettingsService,
        private appCenterService: AppCenterService
    ) { }

    async initializeApp(): Promise<boolean> {
        return this.platform.ready()
         .then(() => {
             StatusBar.setStyle({ style: Style.Default });
             SplashScreen.hide();
         })
        .then(() => this.sqlLiteProviderService.init())
        .then(() => this.appInsightsService.init())
        .then(() => this.appCenterService.init())
        .then(() => this.appSettingsService.init());
    }
}
