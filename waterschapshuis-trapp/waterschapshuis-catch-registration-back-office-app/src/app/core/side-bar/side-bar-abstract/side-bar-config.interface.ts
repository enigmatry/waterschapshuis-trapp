import { Type } from '@angular/core';
import { ISideBar } from './side-bar.interface';
import { SideBarMode } from './side-bar-mode.enum';
import { ISideBarHeaderConfig } from './side-bar-header-config.interface';

export interface ISideBarConfig {
  position: string;
  type?: Type<ISideBar>;
  data?: any;
  action?: string;
  mode?: SideBarMode;
  expanded?: boolean;
  header?: ISideBarHeaderConfig;
}
