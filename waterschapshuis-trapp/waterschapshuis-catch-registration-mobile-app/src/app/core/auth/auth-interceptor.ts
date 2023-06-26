import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { catchError, switchMap, take, filter } from 'rxjs/operators';
import { Logger } from '../logger/logger';
import { environment } from './../../../environments/environment';
import { AuthService } from './auth.service';

const logger = new Logger('AuthInterceptor');

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept = (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> => {
    if (!req.url.startsWith(environment.apiUrl)) {
      return next.handle(req);
    }

    return from(this.authService.getValidAccessTokenOrWaitRefreshing())
      .pipe(
        switchMap(accessToken => {
          req = this.addAccessToken(req, accessToken);
          return next.handle(req);
        }),
        catchError(error => {
          return this.handle401Response(error);
        })
      );
  }

  private addAccessToken = (req: HttpRequest<any>, accessToken: string | null): HttpRequest<any> =>
    accessToken ?
      req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } }) :
      req

  private handle401Response = async (err: any): Promise<any> => {
    if (err instanceof HttpErrorResponse && err.status === 401) {
      if (err.url.includes('Account/log-out')) {
        // do not logout if we called logout url
        return Promise.reject(err);
      }
      logger.warn(`Intercepted response: ${err.status} - ${err.url}. About to log out`);
      await this.authService.logout();
    } else {
      return Promise.reject(err);
    }
  }
}
