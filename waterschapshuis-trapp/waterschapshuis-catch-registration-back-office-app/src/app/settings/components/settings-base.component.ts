import { Component, OnInit, OnDestroy } from '@angular/core';

import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { SideBarMode } from 'src/app/core/side-bar/side-bar-abstract/side-bar-mode.enum';
import { SideBarActions } from 'src/app/core/side-bar/side-bar-abstract/side-bar-actions.enum';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { ISideBarHeaderConfig } from 'src/app/core/side-bar/side-bar-abstract/side-bar-header-config.interface';
import { SideBarSettingsComponent } from './side-bar/side-bar-settings.component';
import { OnDestroyMixin } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-settings-base',
  template: ``,
  styleUrls: []
})
export class SettingsBaseComponent extends OnDestroyMixin implements OnInit, OnDestroy {
  sideBarLeftHeaderConfig: ISideBarHeaderConfig = { showBackButton: true, showExpansionButton: false, title: 'Instellingen' };


  constructor(private sideBarService: SideBarService) {
    super();
  }

  ngOnInit() {
    this.sideBarService.registerSideBar({
      type: SideBarSettingsComponent, position: SideBarPosition.start, header: this.sideBarLeftHeaderConfig
    });
    this.sideBarService.toggleSideBar({ position: SideBarPosition.start, action: SideBarActions.open, mode: SideBarMode.side });
  }

  ngOnDestroy(): void {
    this.sideBarService.toggleSideBar({ position: SideBarPosition.start, action: SideBarActions.close });
  }

  toggleSideBar(): void {
    this.sideBarService.toggleSideBar({
      type: SideBarSettingsComponent, position: SideBarPosition.start, mode: SideBarMode.side, action: SideBarActions.toggle
    });
  }
}
