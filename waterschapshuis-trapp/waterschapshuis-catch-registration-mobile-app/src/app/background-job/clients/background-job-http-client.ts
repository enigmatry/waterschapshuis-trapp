// tslint:disable: object-literal-key-quotes
import { QueuedRequest } from '../models/queued-request.model';
import { Observable, of, throwError } from 'rxjs';
import { HttpResponse, HttpHeaders, HttpClient } from '@angular/common/http';
import { switchMap } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BackgroundJobHttpClient {

  constructor(private httpClient: HttpClient) { }

  sendRequest(request: QueuedRequest): Observable<HttpResponse<any>> {
    const content = JSON.stringify(request.payload);

    const options = {
      body: content,
      observe: 'response' as 'response',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };
    return this
      .httpClient.request(request.method, request.url, options)
      .pipe(switchMap(response => this.processResponseStatus(response)));
  }

  private processResponseStatus(response: HttpResponse<any>) {
    return !this.isSuccessfulStatusCode(response.status)
      ? throwError(`Response status code does not indicate success: ${response.status}. ${response.statusText}`)
      : of(response);
  }

  private isSuccessfulStatusCode(status: number): boolean {
    return status >= 200 && status <= 208;
  }
}
