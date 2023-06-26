import {
  HttpErrorResponse, HttpEvent, HttpHandler,
  HttpInterceptor, HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept = (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> => {
    if (!req.url.startsWith(environment.apiUrl)) {
      return next.handle(req);
    }
    return this.authService
      .getAccessToken()
      .pipe(
        switchMap(accessToken => next.handle(this.addAccessToken(req, accessToken))),
        catchError(error => this.handle401Response(error))
      );
  }

  private addAccessToken = (req: HttpRequest<any>, accessToken: string): HttpRequest<any> =>
    req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } })

  private handle401Response = (err: any): Observable<any> =>
    (err instanceof HttpErrorResponse && err.status === 401)
      ? this.authService.loginRedirect()
      : throwError(err)
}
