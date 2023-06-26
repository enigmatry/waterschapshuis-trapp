import { Injectable } from '@angular/core';

import { MatSnackBar } from '@angular/material/snack-bar';
import { ValidationProblemDetails } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(
    private snackBar: MatSnackBar
  ) { }

  showSuccessMessage(message: string) {
    this.show(message, 'success');
  }

  showErrorMessage(message: string) {
    this.show(message, 'error');
  }

  showWarningMessage(message: string) {
    this.show(message, 'warning');
  }

  showInfoMessage(message: string) {
    this.show(message, 'info');
  }

  validationError(error: ValidationProblemDetails, showFieldName = true) {
    const dutchTitle = 'Eén of meer validatiefouten opgetreden.';
    let message = `${dutchTitle}`;
    if (error.errors) {
      for (const err of Object.keys(error.errors)) {
        message += showFieldName ? `Eigenschap ${err}:` : '';
        for (const detail of error.errors[err]) {
          message += ` - ${detail}`;
        }
      }
    }
    this.showErrorMessage(message);
  }


  private show(message: string, type: string) {
    this.snackBar.open(message, null, {
      duration: 3000,
    });
  }
}
