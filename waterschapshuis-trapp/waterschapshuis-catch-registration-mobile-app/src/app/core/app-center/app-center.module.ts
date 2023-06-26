import { NgModule } from '@angular/core';

import { AppCenterCrashes } from '@ionic-native/app-center-crashes/ngx';
import { AppCenterAnalytics } from '@ionic-native/app-center-analytics/ngx';

@NgModule({
  declarations: [],
  imports: [],
  providers: [
    AppCenterCrashes,
    AppCenterAnalytics
  ]
})
export class AppCenterModule { }
