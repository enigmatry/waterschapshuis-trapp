import { Component, OnInit } from '@angular/core';
import { TrapService } from '../services/trap.service';
import { UserSummary } from '../model/user-summary';
import { UserTrapInfo } from '../model/user-trap-info';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-user-summary',
  templateUrl: './user-summary.component.html',
  styleUrls: ['./user-summary.component.scss'],
})
export class UserSummaryComponent implements OnInit {
  summary: UserSummary;
  trapTypes: Array<string> = [];
  loader: any;

  constructor(
    private trapService: TrapService,
    private loaderService: LoaderService,
    private toastService: ToastService) { }

  ngOnInit() {
    this.initView().then(
      () => {
        this.loader.dismiss();
      },
      () => {
        this.loader.dismiss();
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      }
    );
  }

  async initView(): Promise<void> {
    this.loader = await this.loaderService.createLoader();
    this.loader.present();

    this.trapService.getCurrentUserSummary(true)
      .subscribe(result => {
        this.summary = result;
        this.trapTypes = [...new Set(result.trapDetails.map(item => item.typeLabel))];
      });
  }

  getTrapsByType(trapType: string): Array<UserTrapInfo> {
    return this.summary.trapDetails.filter(x => x.typeLabel === trapType);
  }

  getTrapCountByType(trapType: string): number {
    return this.summary.trapDetails.filter(x => x.typeLabel === trapType).length;
  }
}
