import { Injectable, Type } from '@angular/core';
import { Subject } from 'rxjs';
import { ISideBarConfig } from './side-bar-abstract/side-bar-config.interface';

@Injectable({
  providedIn: 'root'
})
export class SideBarService {
  private toggleSideBarSwitchSubject = new Subject<ISideBarConfig>();
  toggleSideBarSwitch = this.toggleSideBarSwitchSubject.asObservable();

  private sideBarInnerTypeSubject = new Subject<ISideBarConfig>();
  sideBarInnerType = this.sideBarInnerTypeSubject.asObservable();

  toggleSideBar(config: ISideBarConfig) {
    this.toggleSideBarSwitchSubject.next(config);
  }

  registerSideBar(config: ISideBarConfig) {
    this.sideBarInnerTypeSubject.next(config);
  }
}
