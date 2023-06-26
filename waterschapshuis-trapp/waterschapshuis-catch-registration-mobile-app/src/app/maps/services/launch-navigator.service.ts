import { Injectable } from '@angular/core';
import { LaunchNavigator, LaunchNavigatorOptions } from '@ionic-native/launch-navigator/ngx';
import { PlatformService } from 'src/app/services/platform.service';
import { ToastService } from 'src/app/services/toast.service';

@Injectable({
  providedIn: 'root'
})
export class LaunchNavigatorService {

  constructor(
    private launchNavigatorPlugin: LaunchNavigator,
    private platformService: PlatformService,
    private toastService: ToastService
  ) { }

  async openMapAppAndNavigateToSelectedLocation(res: string | Array<number>) {
    let application: string;

    if (!this.platformService.isIos()) {
      application = this.launchNavigatorPlugin.APP.GOOGLE_MAPS;
      this.launchNavigator(application, res);
    } else {
      this.launchNavigatorPlugin.availableApps().then(apps => {
        const appKeys = Object.keys(apps);

        if (appKeys.indexOf(this.launchNavigatorPlugin.APP.APPLE_MAPS) > -1) {
          application = this.launchNavigatorPlugin.APP.APPLE_MAPS;
          this.launchNavigator(application, res);
        } else if (appKeys.indexOf(this.launchNavigatorPlugin.APP.GOOGLE_MAPS) > -1) {
          application = this.launchNavigatorPlugin.APP.GOOGLE_MAPS;
          this.launchNavigator(application, res);
        } else {
          return this.toastService.error(`Ontbrekende Apple Maps of Google Maps applicatie. Installeer een van hen.`);
        }
      });
    }
  }

  launchNavigator(application: string, res: string | Array<number>) {
    this.launchNavigatorPlugin.isAppAvailable(application).then(isAvailable => {
      if (isAvailable) {
        const options: LaunchNavigatorOptions = {
          app: application,
          enableGeolocation: true
        };

        this.launchNavigatorPlugin.navigate(res, options);
      }
    });
  }
}
