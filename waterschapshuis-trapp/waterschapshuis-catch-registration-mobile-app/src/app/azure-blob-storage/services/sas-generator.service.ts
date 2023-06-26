import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BlobStorageRequest } from '../types/azure-storage';

@Injectable({
  providedIn: 'root'
})
export class SasGeneratorService {
  constructor() { }

  private sasData: BlobStorageRequest;

  getSasToken(): Observable<BlobStorageRequest> {
    return of(this.sasData);
  }

  getSasTokenData(): BlobStorageRequest {
    return this.sasData;
  }

  setSasToken(sasToken: string): void {
    this.sasData = {
      storageAccessToken: sasToken,
      storageUri: environment.azureStorage.url
    };
  }
}
