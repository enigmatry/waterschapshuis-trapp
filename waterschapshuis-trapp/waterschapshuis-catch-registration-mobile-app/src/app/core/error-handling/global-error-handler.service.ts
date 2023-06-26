import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { Logger } from '../logger/logger';

const logger = new Logger('GlobalErrorHandlerService');

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {

  constructor() { }

  handleError(error: Error | HttpErrorResponse) {
    logger.error(error);
  }
}
