import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { NgxPermissionsModule } from 'ngx-permissions';
import { UserIdleModule } from 'angular-user-idle';
import { environment } from 'src/environments/environment';

import { AccountClient, API_BASE_URL } from './api/waterschapshuis-catch-registration-backoffice-api';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { AppComponent } from './app.component';
import { ErrorPageComponent } from './common/error-page/error-page.component';
import { AppInsightsFactory } from './core/application-insights/application-insights';
import { AuthModule } from './core/auth/auth.module';
import { CoreModule } from './core/core.module';
import { GlobalErrorHandlerService } from './core/error-handling/global-error-handler.service';
import { GlobalHttpInterceptorService } from './core/error-handling/global-http-Interceptor.service';
import { HomeScreenDashboardComponent } from './home/home-screen-dashboard/home-screen-dashboard.component';
import { HomeScreenComponent } from './home/home-screen/home-screen.component';
import { MapMainComponent } from './map/map-main/map-main.component';
import { MapModule } from './map/map.module';
import { CurrentUserService } from './shared/current-user.service';
import { LoginRedirectComponent } from './shared/login-redirect/login-redirect.component';
import { CustomMatPaginator } from './shared/models/paginator.model';
import { AppInitService, initFactory } from './shared/services/app-init.service';
import { SharedModule } from './shared/shared.module';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS } from './shared/models/format-datepicker';
import { AppSettings } from './app-configuration/app-settings';

@NgModule({
  declarations: [
    AppComponent,
    MapMainComponent,
    LoginRedirectComponent,
    HomeScreenComponent,
    HomeScreenDashboardComponent,
    ErrorPageComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    MapModule,
    AuthModule.forRoot(),
    CoreModule,
    NgxPermissionsModule.forRoot(),
    UserIdleModule.forRoot(AppSettings.userIdleSettings)
  ],
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiUrl
    },
    {
      provide: ApplicationInsights,
      useFactory: AppInsightsFactory
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initFactory,
      deps: [AppInitService],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: GlobalHttpInterceptorService,
      multi: true
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandlerService
    },
    {
      provide: MatPaginatorIntl,
      useClass: CustomMatPaginator
    },
    {
      provide: DateAdapter,
      useClass: AppDateAdapter
    },
    {
      provide: MAT_DATE_FORMATS,
      useValue: APP_DATE_FORMATS
    },
    AccountClient,
    CurrentUserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() { }
}

