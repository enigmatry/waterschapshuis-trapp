import { Injectable } from '@angular/core';

import { Platform } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class PlatformService {
  constructor(private platform: Platform) { }

  isNative(): boolean {
    return this.platform.is('hybrid');
  }

  isIos(): boolean {
    return this.platform.is('ios');
  }
}
