import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { BackgroundGeolocation } from '@ionic-native/background-geolocation/ngx';
import { Camera } from '@ionic-native/camera/ngx';
import { File } from '@ionic-native/file/ngx';
import { HTTP } from '@ionic-native/http/ngx';
import { InAppBrowser } from '@ionic-native/in-app-browser/ngx';
import { Keyboard } from '@ionic-native/keyboard/ngx';
import { Network } from '@ionic-native/network/ngx';
import { SQLite } from '@ionic-native/sqlite/ngx';
import { Zip } from '@ionic-native/zip/ngx';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';

import { API_BASE_URL } from './api/waterschapshuis-catch-registration-mobile-api';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AzureStorageModule } from './azure-blob-storage/azure-storage.module';
import { ErrorPageComponent } from './common/error-page/error-page.component';
import { AppInsightsFactory } from './core/application-insights/application-insights';
import { AUTH_CONFIG, AuthConfigFactory } from './core/auth/auth-configuration';
import { AuthGuard } from './core/auth/auth-guard';
import { AuthInterceptor } from './core/auth/auth-interceptor';
import { AuthService } from './core/auth/auth.service';
import { GlobalErrorHandlerService } from './core/error-handling/global-error-handler.service';
import { GlobalHttpInterceptorService } from './core/error-handling/global-http-Interceptor.service';
import { HomeComponent } from './home/home.component';
import { AppInitService, initFactory } from './shared/services/app-init.service';
import { SharedModule } from './shared/shared.module';
import { EmailComposer } from '@ionic-native/email-composer/ngx';
import { Geolocation } from '@ionic-native/geolocation/ngx';
import { OfflineHttpInterceptorService } from './shared/offline/offline-http-interceptor.service';
import { LaunchNavigator } from '@ionic-native/launch-navigator/ngx';
import { OfflineMapDownloadModalComponent } from './offline/offline-map-download-modal/offline-map-download-modal.component';
import { OfflineDataComponent } from './offline/offline-data-component/offline-data.component';
import { FileTransfer } from '@ionic-native/file-transfer/ngx';
import { AppCenterModule } from './core/app-center/app-center.module';
import { LoggerModule } from './core/logger/logger.module';
import { Diagnostic } from '@ionic-native/diagnostic/ngx';
import { AuthorityClient } from './core/auth/authority.client';
import { Insomnia } from '@ionic-native/insomnia/ngx';
import { AddVersionInterceptor } from './core/add-version-interceptor';
import { GeoServerAuthInterceptor } from './core/auth/geoserver-auth-interceptor';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    OfflineDataComponent,
    ErrorPageComponent,
    OfflineMapDownloadModalComponent
  ],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    AzureStorageModule,
    AppCenterModule,
    LoggerModule.forRoot()
  ],
  providers: [
    {
      provide: RouteReuseStrategy,
      useClass: IonicRouteStrategy
    },
    {
      provide: API_BASE_URL,
      useValue: environment.apiUrl
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddVersionInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: GeoServerAuthInterceptor,
      multi: true
    },
    {
      provide: AUTH_CONFIG,
      useFactory: AuthConfigFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: OfflineHttpInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: GlobalHttpInterceptorService,
      multi: true
    },
    {
      provide: ApplicationInsights,
      useFactory: AppInsightsFactory
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandlerService
    },
    Keyboard,
    Camera,
    BackgroundGeolocation,
    Geolocation,
    File,
    FileTransfer,
    Zip,
    Insomnia,
    // replace SQLite with SQLiteMock to be able to test features that use SQLite in browser
    SQLite,
    HTTP,
    AuthGuard,
    AuthService,
    AuthorityClient,
    InAppBrowser,
    AppInitService,
    {
      provide: APP_INITIALIZER,
      useFactory: initFactory,
      deps: [AppInitService],
      multi: true
    },
    Network,
    EmailComposer,
    LaunchNavigator,
    Diagnostic
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export class FileReaderA extends window.FileReader {
	constructor() {
		super();
		const zoneOriginalInstance = (this as any)['__zone_symbol__originalInstance'];
		return zoneOriginalInstance || this;
	}
}

window.FileReader = FileReaderA;