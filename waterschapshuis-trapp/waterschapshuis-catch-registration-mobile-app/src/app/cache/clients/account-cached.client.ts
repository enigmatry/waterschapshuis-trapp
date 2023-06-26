import { Injectable } from '@angular/core';
import {
  GetCurrentUserProfileResponse,
  AccountClient } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { CachedClient, BoundingBox } from './cached.client';
import { Observable } from 'rxjs';
import { TypedCacheService } from '../typed-cache.service';

@Injectable({
  providedIn: 'root'
})
export class AccountCachedClient implements CachedClient {
  constructor(
    private accountClient: AccountClient,
    private cache: TypedCacheService
  ) { }

  cacheAllWithin = (boundingBox: BoundingBox): Observable<any> => this.accountClient.getUserProfile();

  getUserProfile = (): Observable<GetCurrentUserProfileResponse> =>
    this.cache.loadFromObservable('userProfile', this.accountClient.getUserProfile())

  removeUserProfile = (): Observable<any> => this.cache.removeItem('userProfile');
}
