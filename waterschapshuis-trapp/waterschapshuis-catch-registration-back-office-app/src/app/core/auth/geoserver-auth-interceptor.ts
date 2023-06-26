import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { IGetGeoServerSettingsResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { SettingsService } from 'src/app/shared/services/settings.service';
import { environment } from 'src/environments/environment';

@Injectable()
export class GeoServerAuthInterceptor implements HttpInterceptor {

    constructor(private settingsService: SettingsService) { }

    intercept = (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> => {
        if (req.url.startsWith(environment.apiUrl)) {
            return next.handle(req);
        }
        return this.settingsService.getGeoServerSettings().pipe(
            switchMap(settings => {
                if (req.url.startsWith(settings.url)) {
                    req = this.addAccessToken(req, settings);
                }
                return next.handle(req);
            }));
    }

    private addAccessToken = (req: HttpRequest<any>, settings: IGetGeoServerSettingsResponse): HttpRequest<any> => {
        return req.clone({ headers: req.headers.append(settings.accessKey, settings.backOfficeUser) });
    }
}
