import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { NetworkService } from 'src/app/network/network.service';
import { BackgroundJobService } from 'src/app/background-job/background-job.service';
import { catchError } from 'rxjs/operators';
import { NoConnectionError } from './models/no-connection.error';

@Injectable()
export class OfflineHttpInterceptorService implements HttpInterceptor {

  private readonly httpMethodsPostPutDelete = ['post', 'put', 'delete'];

  constructor(private networkService: NetworkService, private backgroundJobService: BackgroundJobService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isPostPutOrDeleteRequest(req.method)) {
      return next.handle(req);
    }

    if (this.networkService.isOffline()) {
      // request is added to the queue
      // error is handled by the caller (BaseOfflineEntityService)
      return this.backgroundJobService.queue(req).pipe(() => throwError(new NoConnectionError()));
    }

    return next.handle(req)
      .pipe(catchError(err => {
        // try to determine if request failed because of connection issues
        // status code 0 indicates that request failed for connection issues or unknown reason!?

        if (err instanceof HttpErrorResponse && err.status === 0) {
          return this.backgroundJobService.queue(req).pipe(() => throwError(new NoConnectionError()));
        }
        return throwError(err);
      }));
  }

  private isPostPutOrDeleteRequest(method: string) {
    return this.httpMethodsPostPutDelete.indexOf(method.toLowerCase()) !== -1;
  }
}
