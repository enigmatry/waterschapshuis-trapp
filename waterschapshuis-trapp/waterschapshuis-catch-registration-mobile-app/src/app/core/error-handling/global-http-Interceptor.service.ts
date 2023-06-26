import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavController } from '@ionic/angular';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast.service';
import { AuthService } from '../auth/auth.service';
import { Logger } from '../logger/logger';

const logger = new Logger('GlobalHttpInterceptorService');

@Injectable()
export class GlobalHttpInterceptorService implements HttpInterceptor {

  constructor(private nav: NavController, private toastService: ToastService, private authService: AuthService) { }

  intercept = (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> =>
    next
      .handle(req)
      .pipe(
        tap({
          error: async err => {
            logger.error(err, 'Response errror caught');
            if (err instanceof HttpErrorResponse) {
              switch (err.status) {
                case 400:
                  // Bad request should be handled on component where it happened
                  break;
                case 403:
                  if (err.headers.get('InvalidUserSession')) {
                    logger.warn('Status 403, InvalidUserSession, about to logout');
                    await this.authService.logout();
                    await this.nav.navigateRoot('');
                  } else {
                    logger.warn('About to navigate to error page');
                    await this.nav.navigateRoot(`/error/${err.status}`);
                  }
                  break;
                default:
                  logger.warn('Default error handling');
                  await this.toastService.error('Er is een fout opgetreden.');
                  break;
              }
            }
          }
        })
      )
}
