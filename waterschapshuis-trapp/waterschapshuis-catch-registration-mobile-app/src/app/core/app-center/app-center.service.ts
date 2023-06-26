import { Injectable } from '@angular/core';
import { AppCenterCrashes, AppCenterCrashReport } from '@ionic-native/app-center-crashes/ngx';
import { AppCenterAnalytics, StringMap } from '@ionic-native/app-center-analytics/ngx';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppCenterService {

  constructor(private appCenterCrashes: AppCenterCrashes, private appCenterAnalytics: AppCenterAnalytics) { }

  init() {
    this.appCenterAnalytics.setEnabled(environment.appCenter.enableAnalytics);
  }

  trackEvent(eventName: string, properties: StringMap) {
    this.appCenterAnalytics.trackEvent(eventName, properties);
  }

  generateTestCrash(): void {
    return this.appCenterCrashes.generateTestCrash();
  }

  lastSessionCrashReport(): Promise<AppCenterCrashReport> {
    return this.appCenterCrashes.lastSessionCrashReport();
  }
}
