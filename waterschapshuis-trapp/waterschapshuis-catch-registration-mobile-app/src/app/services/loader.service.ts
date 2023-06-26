import { Injectable } from '@angular/core';
import { LoadingController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  constructor(public loadingController: LoadingController) { }

  async createLoader(): Promise<HTMLIonLoadingElement> {
    return this.loadingController.create({
      message: 'Even geduld alstublieft...'
    });
  }

}
