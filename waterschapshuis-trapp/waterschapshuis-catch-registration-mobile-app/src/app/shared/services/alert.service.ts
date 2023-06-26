import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { AlertOptions } from '@ionic/core';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  confirmed = false;

  constructor(
    public alertController: AlertController
  ) { }

  async getConfirmDialog(
    header: string,
    message: string,
    cancelText: string,
    okText: string,
    delegate: (result: boolean, args: any) => any,
    args: any,
    hideCancelButton?: boolean,
    backdropDismiss?: boolean): Promise<HTMLIonAlertElement> {

    return this.alertController.create({
      header: `${header}`,
      message: `${message}`,
      buttons: this.getButtons(okText, cancelText, hideCancelButton, args, delegate),
      backdropDismiss
    });
  }

  private getButtons(
    okText: string,
    cancelText: string,
    hideCancelButton: boolean,
    args: any,
    delegate: (result: boolean, args: any) => any
  ): Array<any> {
    const buttons: Array<any> = [
      {
        text: okText,
        handler: () => {
          delegate(true, args);
        }
      }
    ];

    if (!hideCancelButton) {
      buttons.push({
        text: cancelText,
        role: 'cancel',
        cssClass: 'secondary',
        handler: () => {
          delegate(false, args);
        }
      });
    }

    return buttons;
  }
}
