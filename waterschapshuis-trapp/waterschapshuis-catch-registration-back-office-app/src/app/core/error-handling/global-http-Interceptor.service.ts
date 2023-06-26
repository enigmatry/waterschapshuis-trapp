import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { SpinnerService } from 'src/app/shared/services/spinner.service';

@Injectable()
export class GlobalHttpInterceptorService implements HttpInterceptor {
  private spinnerIgnoreUrlSuffixes = [
    'version-regional-layout/export',
    'version-regional-layout/import',
    'version-regional-layout-import',
    'calculate-km-waterways'
  ];

  constructor(public router: Router,
              private authService: AuthService,
              private spinnerService: SpinnerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.tryShowSpinner(req);
    return next.handle(req)
      .pipe(
        catchError((error) => {
          if (error.status === 403 && error.headers.get('InvalidUserSession')) {
            this.authService.logout().subscribe();
          } else if (error.status !== 400) {
            this.router.navigateByUrl(`/error/${error.status}`);
          }
          // Bad request (400) should be handled on page where happened
          return throwError(error);
        }),
        finalize(() => this.spinnerService.hide())
      );
  }

  private tryShowSpinner = (req: HttpRequest<any>) => {
    if (this.spinnerIgnoreUrlSuffixes.some(suffix => req.url.endsWith(suffix))) { return; }
    this.spinnerService.show();
  }
}

