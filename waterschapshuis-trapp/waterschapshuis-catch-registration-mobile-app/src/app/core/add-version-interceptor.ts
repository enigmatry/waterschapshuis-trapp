import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable()
export class AddVersionInterceptor implements HttpInterceptor {

  constructor() { }

  intercept = (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> => {
    if (!req.url.startsWith(environment.apiUrl)) {
      return next.handle(req);
    }

    req = this.addVersionHeader(req);
    return next.handle(req);
  }

  private addVersionHeader = (req: HttpRequest<any>): HttpRequest<any> =>
    req.clone({ setHeaders: { MobileVersion: environment.appVersion } })
}
