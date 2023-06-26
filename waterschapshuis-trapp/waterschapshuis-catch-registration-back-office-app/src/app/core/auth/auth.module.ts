import { NgModule, ModuleWithProviders } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
import { MSAL_CONFIG, MSALConfigFactory } from './msal-config';
import { AuthService } from './auth.service';
import { MsalService } from './msal.service';
import { AuthGuard } from './auth.guard';
import { GeoServerAuthInterceptor } from './geoserver-auth-interceptor';

@NgModule({
  declarations: [],
  imports: [],
  providers: []
})
export class AuthModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: AuthModule,
      providers: [
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
          provide: MSAL_CONFIG,
          useFactory: MSALConfigFactory
        },
        MsalService,
        AuthGuard,
        AuthService
      ]
    };
  }
}
