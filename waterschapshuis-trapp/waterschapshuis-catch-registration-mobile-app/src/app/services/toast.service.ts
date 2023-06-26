import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';
import { ValidationProblemDetails } from '../api/waterschapshuis-catch-registration-mobile-api';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(public toastController: ToastController) { }

  async success(message: string, duration = 2000) {
    const toast = await this.toastController.create({
      message,
      duration,
      color: 'primary',
      position: 'bottom'
    });
    toast.present();
  }

  async error(message: string, duration = 4000) {
    const toast = await this.toastController.create({
      message,
      duration,
      color: 'danger',
      position: 'bottom'
    });
    toast.present();
  }

  async warning(message: string, duration?: number, button?: any[]) {
    const toast = await this.toastController.create({
      message,
      duration,
      color: 'warning',
      position: 'top',
      buttons: button
    });
    toast.present();
  }

  async validationError(error: ValidationProblemDetails, showFieldName = true) {
    const dutchTitle = 'Eén of meer validatiefouten opgetreden.';
    let message = `${dutchTitle} <br>`;
    if (error.errors) {
      for (const err of Object.keys(error.errors)) {
        message += showFieldName ? `Eigenschap <b>${err}</b>: <br>` : '';
        for (const detail of error.errors[err]) {
          message += ` - ${detail}<br>`;
        }
      }
    }
    await this.error(message, 5000);
  }

}
