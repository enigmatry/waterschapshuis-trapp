import { filter, take } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountClient } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { AccountCachedClient } from 'src/app/cache/clients/account-cached.client';
import { NetworkService } from 'src/app/network/network.service';
import { LoaderService } from 'src/app/services/loader.service';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';
import { CurrentUserProviderService } from 'src/app/shared/services/current-user-provider.service';
import { Logger } from '../logger/logger';
import { AuthTokenData } from './auth-token-data';
import { AuthorityClient } from './authority.client';

const logger = new Logger('AuthService');
@Injectable()
export class AuthService {
  private loader: any;
  private accessTokenReadySubject = new BehaviorSubject<string | null>(null);
  private isRefreshingAccessTokenInProgress = false;

  private waitAccessTokenReady$: Observable<string> = this.accessTokenReadySubject.pipe(
      filter((token) => token !== null),
      take(1));

  constructor(
    private userService: CurrentUserProviderService,
    private sqliteProviderService: SqliteProviderService,
    private networkService: NetworkService,
    private accountClient: AccountClient,
    private accountCachedClient: AccountCachedClient,
    private loaderService: LoaderService,
    private authorityClient: AuthorityClient
  ) { }

  authenticateAndLoadUser = async (): Promise<boolean> => {
    if (this.networkService.isOffline()) {
      return this.hasAccessToken();
    }
    try {
      const isAuthenticated = await this.getAccessTokenOrLogin();
      if (isAuthenticated) {
        await this.userService.loadCurrentUserProfile();
        await this.userService.checkConfidentialityConfirm();
      }
      return isAuthenticated;
    } catch (e) {
      logger.error(e, 'Unable to authenticate and load user');
      if (this.isInvalidGrantError(e)) {
        logger.warn('Invalid grant error. About to logout.');
        await this.logout();
      } else {
        logger.warn('Removing token data');
        await this.removeTokenDataFromCache();
      }
    }

    return false;
  }

  private getAccessTokenOrLogin = async (): Promise<boolean> => {
    const token = await this.getOrRefreshAccessToken();
    if (token) {
      return true;
    }
    return this.loginAndStoreTokenToCache();
  }

  public hasAccessToken = async (): Promise<boolean> => {
    return !!(await this.tryLoadTokenDataFromCache());
  }

  private getOrRefreshAccessToken = async (): Promise<string | null> => {
    const cachedTokenData = await this.tryLoadTokenDataFromCache();
    if (cachedTokenData === null) {
      return null;
    }

    if (this.networkService.isOffline()) {
      return cachedTokenData.accessToken;
    }

    if (this.isTokenValid(cachedTokenData)) {
      return cachedTokenData.accessToken;
    }

    logger.warn('Token is not valid anymore, about to refresh');

    const refreshTokenData = await this.refreshAccessToken(cachedTokenData);
    return refreshTokenData ? refreshTokenData.accessToken : null;
  }

  private get accessTokenReady(): string | null {
      return this.accessTokenReadySubject.getValue();
  }

  private set accessTokenReady(newToken) {
      this.accessTokenReadySubject.next(newToken);
  }

  getValidAccessTokenOrWaitRefreshing = async (): Promise<string | null> => {
    if (this.isRefreshingAccessTokenInProgress) {
      return this.waitAccessTokenReady$.toPromise();
    }
    this.isRefreshingAccessTokenInProgress = true;
    this.accessTokenReady = null;
    const refreshedToken = await this.getOrRefreshAccessToken();
    if (refreshedToken) {
        this.accessTokenReady = refreshedToken;
        this.isRefreshingAccessTokenInProgress = false;
        return refreshedToken;
    }
    return null;
  }

  private isTokenValid = (authToken: AuthTokenData): boolean => new Date(authToken.expiresIn) > new Date();

  logout = async (): Promise<void> => {
    if (this.networkService.isOffline()) {
      return;
    }

    this.loader = await this.loaderService.createLoader();
    this.loader.present();

    try {
      await this.accountClient.logOut().toPromise();
    } catch (e) {
      logger.error(e, 'Unable to logout');
    } finally {
      logger.warn('removing token data');
      await this.removeTokenDataFromCache();
      await this.accountCachedClient.removeUserProfile().toPromise();
      this.loader.dismiss();
    }
  }

  private loginAndStoreTokenToCache = async (): Promise<boolean> => {
    const authTokenData = await this.authorityClient.login();
    await this.storeTokenDataInCache(authTokenData);
    return !!authTokenData;
  }

  private refreshAccessToken = async (tokenData: AuthTokenData): Promise<AuthTokenData | null> => {
    try {
      const refreshTokenData = await this.authorityClient.refreshToken(tokenData.refreshToken);
      await this.storeTokenDataInCache(tokenData);
      await this.storeTokenDataInCache(refreshTokenData);
      return refreshTokenData;
    } catch (e) {
      logger.error(e, 'Unable to refresh token. Deleting cached token.');
      await this.removeTokenDataFromCache();
      return null;
    }
  }

  private removeTokenDataFromCache = async (): Promise<void> =>
    this.sqliteProviderService.deleteAll(SqliteTableConfig.wvrToken)

  private tryLoadTokenDataFromCache = async (): Promise<AuthTokenData | null> => {
    const results = await this.sqliteProviderService
      .getAll(SqliteTableConfig.wvrToken);
    return results && results.length > 0 ?
      results[0] :
      null;
  }

  private storeTokenDataInCache = async (value: AuthTokenData) => {
    await this.removeTokenDataFromCache();
    await this.sqliteProviderService.insertData(
      SqliteTableConfig.wvrToken,
      [value.accessToken, value.idToken, value.refreshToken, value.expiresIn]);
  }

  private isInvalidGrantError(error: any): boolean {
    try {
      const e = JSON.parse(error.error);
      return e.error === 'invalid_grant';
    } catch {
      return false;
    }
  }
}
