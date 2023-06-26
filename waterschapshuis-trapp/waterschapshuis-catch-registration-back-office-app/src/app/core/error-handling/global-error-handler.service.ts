
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';

@Injectable()
export class GlobalErrorHandlerService extends ErrorHandler {

  constructor(private appInsights: ApplicationInsights) {
    super();
  }

  handleError(error: Error | HttpErrorResponse) {
    if (this.appInsights ) {this.appInsights.trackException({ exception: error }); }
    console.log('GlobalErrorHandlerService ERROR');
    console.error(error);
  }
}
