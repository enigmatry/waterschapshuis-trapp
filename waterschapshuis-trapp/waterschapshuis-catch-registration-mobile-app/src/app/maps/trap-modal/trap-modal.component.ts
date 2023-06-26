import { Component, OnInit } from '@angular/core';
import { ModalController, NavController, NavParams } from '@ionic/angular';
import { noop } from 'rxjs';
import { TrapDetails } from 'src/app/maps/models/trap-details.model';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';
import { TrapService } from 'src/app/traps/services/trap.service';


@Component({
  selector: 'app-trap-modal',
  templateUrl: './trap-modal.component.html',
  styleUrls: ['./trap-modal.component.scss'],
})
export class TrapModalComponent implements OnInit {
  trap: TrapDetails;
  loader: any;

  constructor(
    private navParams: NavParams,
    public nav: NavController,
    private toastService: ToastService,
    public modal: ModalController,
    private trapService: TrapService,
    private loaderService: LoaderService
  ) { }

  ngOnInit() {
    this.initView().then(
      noop,
      () => {
        this.loader.dismiss();
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      }
    );
  }
  async initView(): Promise<void> {
    this.loader = await this.loaderService.createLoader();
    this.loader.present();

    const trapId = this.navParams.get('trapId');
    this.trap = await this.trapService.get(trapId).toPromise();

    this.loader.dismiss();
  }

  openTrapDetails(): void {
    this.nav.navigateForward(`traps/vangmiddel/${this.trap.id}`);
    this.modal.dismiss(null, null, 'TrapDetailsModal');
  }
}
