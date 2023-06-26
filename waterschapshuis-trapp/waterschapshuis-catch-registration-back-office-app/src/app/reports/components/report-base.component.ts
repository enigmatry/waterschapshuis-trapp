import { Component, OnInit, OnDestroy } from '@angular/core';

import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { SideBarMode } from 'src/app/core/side-bar/side-bar-abstract/side-bar-mode.enum';
import { SideBarActions } from 'src/app/core/side-bar/side-bar-abstract/side-bar-actions.enum';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { SideBarReportsComponent } from './side-bar-reports/side-bar-reports.component';
import { locale, loadMessages } from 'devextreme/localization';
import { ReportTemplateBase } from '../models/common/report-template-base.model';

import nlMessages from 'devextreme/localization/messages/nl.json';
import { OnDestroyMixin } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-report-base',
  template: `map-report-base.component.html`,
  styleUrls: []
})
export class ReportBaseComponent extends OnDestroyMixin implements OnInit, OnDestroy {

  reportTemplate: ReportTemplateBase;

  constructor(private sideBarService: SideBarService) {
    super();
    loadMessages(nlMessages);
    locale('nl');
  }

  ngOnInit() {
    this.sideBarService.registerSideBar({
      type: SideBarReportsComponent,
      position: SideBarPosition.start,
      header: { showBackButton: true, showExpansionButton: false, title: 'Rapportage' }
    });

    this.sideBarService.toggleSideBar({
      position: SideBarPosition.start,
      action: SideBarActions.open,
      mode: SideBarMode.side
    });
  }

  ngOnDestroy(): void {
    this.sideBarService.toggleSideBar({
      position: SideBarPosition.start,
      action: SideBarActions.close
    });
  }
}
