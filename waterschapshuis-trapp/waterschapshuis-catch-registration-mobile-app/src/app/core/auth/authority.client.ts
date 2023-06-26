import { Inject, Injectable } from '@angular/core';
import { HTTP, HTTPResponse } from '@ionic-native/http/ngx';
import { InAppBrowser, InAppBrowserEvent } from '@ionic-native/in-app-browser/ngx';
import { Logger } from '../logger/logger';
import { AuthConfiguration, AUTH_CONFIG } from './auth-configuration';
import { AuthTokenData } from './auth-token-data';

const logger = new Logger('AuthorityClient');
@Injectable({
  providedIn: 'root'
})
export class AuthorityClient {

  constructor(
    @Inject(AUTH_CONFIG) private authConfig: AuthConfiguration,
    private inAppBrowser: InAppBrowser,
    private http: HTTP) {
  }

  login = (): Promise<AuthTokenData | null> =>
    new Promise((resolve, reject) => {
      const browser = this.inAppBrowser.create(
        this.createAuthorizeUrl(),
        '_blank',
        { location: 'no', clearcache: 'yes', cleardata: 'yes', hardwareback: 'no', toolbar: 'no', clearsessioncache: 'yes' }
      );

      browser
        .on('loadstart')
        .subscribe(
          async (event: InAppBrowserEvent) => {
            const urlCode = this.getCodeFromUrl(event.url);
            if (urlCode) {
              browser.close();
              const accessToken = await this.acquireToken(urlCode);
              resolve(accessToken);
            }
          },
          (error) => {
            browser.close();
            reject(null);
          }
        );
    })

  refreshToken = async (refreshToken: string): Promise<AuthTokenData> =>
    await this.http
      .post(
        `${this.authConfig.authority}/token`,
        Object.assign(
          {
            grant_type: 'refresh_token',
            refresh_token: refreshToken
          },
          { client_id: this.authConfig.clientId, scope: this.authConfig.scope }
        ),
        { 'Content-Type': 'application/x-www-form-urlencoded' }
      )
      .then(this.asAuthTokenData)

  private acquireToken = async (urlCode: string): Promise<AuthTokenData> =>
    await this.http
      .post(
        `${this.authConfig.authority}/token`,
        Object.assign(
          {
            grant_type: 'authorization_code',
            code: urlCode,
            redirect_uri: this.authConfig.redirectUrl
          },
          { client_id: this.authConfig.clientId, scope: this.authConfig.scope }
        ),
        { 'Content-Type': 'application/x-www-form-urlencoded' }
      )
      .then(this.asAuthTokenData)


  private createAuthorizeUrl = (): string =>
    `${this.authConfig.authority}/authorize
    ?client_id=${this.authConfig.clientId}
    &redirect_uri=${encodeURIComponent(this.authConfig.redirectUrl)}
    &response_type=code
    &response_mode=query
    &scope=${encodeURIComponent(this.authConfig.scope)}`

  private asAuthTokenData = (response: HTTPResponse): AuthTokenData => {
    const responseData = JSON.parse(response.data);
    const expiresInDate = new Date();
    expiresInDate.setSeconds(expiresInDate.getSeconds() + responseData.expires_in);
    return {
      accessToken: responseData.access_token,
      idToken: responseData.id_token,
      refreshToken: responseData.refresh_token,
      expiresIn: expiresInDate
    } as AuthTokenData;
  }

  private getCodeFromUrl = (url: string): string =>
    url.startsWith(this.authConfig.redirectUrl)
      ? this.getQueryVariable(url, 'code')
      : undefined

  private getQueryVariable = (url: string, variable: string): string => {
    const items = url.replace(`${this.authConfig.redirectUrl}?`, '').split('&');
    for (const item of items) {
      const pair = item.split('=');
      if (pair[0] === variable) { return pair[1]; }
    }
    return undefined;
  }
}
